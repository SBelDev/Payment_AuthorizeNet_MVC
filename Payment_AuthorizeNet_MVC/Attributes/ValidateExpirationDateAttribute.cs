using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace Payment_AuthorizeNet_MVC.Attributes
{
    public class ValidateExpirationDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object expirationDate)
        {
            if (expirationDate == null)
            {
                return false;
            }
            Regex monthCheck = new Regex(@"^(0[1-9]|1[0-2])$");
            Regex yearCheck = new Regex(@"^[0-9]{2}$");

            List<string> dateParts = Enumerable.Range(0, expirationDate.ToString().Length / 2).Select(i => expirationDate.ToString().Substring(i * 2, 2)).ToList();

            if (!monthCheck.IsMatch(dateParts[0]) || !yearCheck.IsMatch(dateParts[1]))
            {
                return false;
            }

            string yearStr = "20" + dateParts[1];
            int month = int.Parse(dateParts[0]);
            int year = int.Parse(yearStr);

            var lastDateOfExpiryMonth = DateTime.DaysInMonth(year, month);
            var cardExpiry = new DateTime(year, month, lastDateOfExpiryMonth, 23, 59, 59);

            return (cardExpiry > DateTime.Now && cardExpiry < DateTime.Now.AddYears(6));
        }
    }
}