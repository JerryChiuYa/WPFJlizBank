using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using ValidationResult = System.Windows.Controls.ValidationResult;

namespace BankLibrary.Services
{
    public class ValidationCheck : ValidationRule
    {
        public string CheckType { get; set; }
        public string ErrorMessage { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var settings = new SettingsService(ConfigurationManager.ConnectionStrings["JlizBank"].ConnectionString);
            switch (CheckType)
            {
                case "NameType":
                    if (Regex.IsMatch(value.ToString(), @"^.{2,}$") == false)
                    {
                        ErrorMessage = "至少輸入2個字元以上!!";
                        return new ValidationResult(false, ErrorMessage);
                    }
                    break;

                case "IdentityType":
                    if (Regex.IsMatch(value.ToString(), @"^[A-Z]{1}[12]{1}\d{8}$")==false)
                    {
                        ErrorMessage = "身分證格式錯誤!!";
                        return new ValidationResult(false, ErrorMessage);
                    }
                    break;

            

                case "MobileType":
                    if (Regex.IsMatch(value.ToString(), @"^09\d{8}$")==false)
                    {
                        ErrorMessage = "手機格式錯誤,請以09開頭";
                        return new ValidationResult(false, ErrorMessage);
                    }
                    break;

                case "EmailType":
                    if (new EmailAddressAttribute().IsValid(value.ToString())==false)
                    {
                        ErrorMessage = "不是有效的Email格式!!";
                        return new ValidationResult(false, ErrorMessage);
                    }
                    break;

                case "LoginAccountType":
                    var data=settings.GetBankAllData(value.ToString());
                    if (data.Count==1)
                    {
                        ErrorMessage = "此帳號已存在,請重新輸入!!";
                        return new ValidationResult(false, ErrorMessage);
                    }
                    break;
            }
            return new ValidationResult(true, null);
        }
    }
}
