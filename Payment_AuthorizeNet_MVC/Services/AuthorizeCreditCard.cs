using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Controllers.Bases;
using System;
using Payment_AuthorizeNet_MVC.Models;

namespace Payment_AuthorizeNet_MVC.Services
{
    public class AuthorizeCreditCard
    {
        public static PaymentResponseVM Payment(AuthorizeNetAccount authInfo, PaymentVM payment)
        {
            if (authInfo.IsTestMode)
            {
                ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;
            }
            else
            {
                ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.PRODUCTION;
            }

            PaymentResponseVM paymentResponse = new PaymentResponseVM();

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = authInfo.AuthNetLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = authInfo.AuthNetTransKey,
            };

            var creditCard = new creditCardType
            {
                cardNumber = payment.CardNumber.ToString(),
                expirationDate = payment.ExpirationDate.ToString(),
                cardCode = payment.SecurityCode.ToString()
            };


            var billingAddress = new customerAddressType
            {
                firstName = payment.FirstName,
                lastName = payment.LastName,
                company = payment.Company,
                address = payment.Address,
                city = payment.City,
                state = payment.State,
                zip = payment.Zip.ToString(),
                email = payment.Email
            };

            var paymentType = new paymentType { Item = creditCard };

            var transactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.authOnlyTransaction.ToString(),
                amount = payment.Amount,
                payment = paymentType,
                billTo = billingAddress
            };

            var request = new createTransactionRequest { transactionRequest = transactionRequest };

            createTransactionResponse response = new createTransactionResponse();

            try
            {
                createTransactionController controller = new createTransactionController(request);
                controller.Execute();
                response = controller.GetApiResponse();
            }
            catch (Exception ex)
            {
                paymentResponse.ErrorMessage = ex.Message;
                return paymentResponse;
            }

            if (response != null)
            {
                if (response.messages.resultCode == messageTypeEnum.Ok)
                {
                    if (response.transactionResponse.messages != null)
                    {
                        paymentResponse.TransactionID = response.transactionResponse.transId;
                        paymentResponse.ResponseCode = response.transactionResponse.responseCode;
                        paymentResponse.MessagesCode = response.transactionResponse.messages[0].code;
                        paymentResponse.MessageDescription = response.transactionResponse.messages[0].description;
                        paymentResponse.AuthorizationCode = response.transactionResponse.authCode;
                    }
                    else
                    {
                        if (response.transactionResponse.errors != null)
                        {
                            paymentResponse.ErrorCode = response.transactionResponse.errors[0].errorCode;
                            paymentResponse.ErrorMessage = response.transactionResponse.errors[0].errorText;
                        }
                    }
                }
                else
                {
                    if (response.transactionResponse != null && response.transactionResponse.errors != null)
                    {
                        paymentResponse.ErrorCode = response.transactionResponse.errors[0].errorCode;
                        paymentResponse.ErrorMessage = response.transactionResponse.errors[0].errorText;
                    }
                    else
                    {
                        paymentResponse.ErrorCode = response.transactionResponse.errors[0].errorCode;
                        paymentResponse.ErrorMessage = response.transactionResponse.errors[0].errorText;
                    }
                }
            }

            return paymentResponse;
        }
    }
}


