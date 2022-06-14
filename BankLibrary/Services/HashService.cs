using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary.Services
{
    public class HashService
    {
        public string HashPwd(string password, string CustomerId)
        {
            var result=string.Empty;
            using(var sha256=new SHA256CryptoServiceProvider())
            {
                var bytes=Encoding.UTF8.GetBytes(password+ CustomerId);
                var hash= sha256.ComputeHash(bytes);
                result=Convert.ToBase64String(hash);
            }
            return result;
        }
    }
}
