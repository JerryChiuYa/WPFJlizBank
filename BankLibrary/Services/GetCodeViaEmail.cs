using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary.Services
{
    public class GetCodeViaEmail
    {
        public static string GetVerifyCode(string EmailTo)
        {
            //產生驗證碼
            string codes = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Random random = new Random();
            string verifyCode = string.Empty;
            for (int i = 0; i < 8; i++)
            {
                int index = random.Next(codes.Length);
                verifyCode += codes[index];
            }
            //發送Email驗證碼
            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress("jerry384052@gmail.com");
                mailMessage.To.Add(EmailTo);
                mailMessage.Subject = "傑力士商業銀行---重新設定密碼--驗證碼";
                mailMessage.Body = $"您的驗證碼為 {verifyCode}";

                //使用 SMTP 寄送 Email
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Credentials = new NetworkCredential("jerry384052@gmail.com", "xiwjmiggmzfbodvr");
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 25;
                    smtp.EnableSsl = true;
                    smtp.Send(mailMessage);

                }
            }
            return codes;
        }
    }
}
