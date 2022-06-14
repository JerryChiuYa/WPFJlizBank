using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary.Services
{
    public class LoginService
    {
        private string _dbConnStr;
        public LoginService(string dbConnStr)
        {
            _dbConnStr=dbConnStr;
        }
        public bool VerifyUser(string account, string password)
        {
            var result = false;
            using (var conn=new SqlConnection(_dbConnStr))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select * from BankAccount where LoginAccount=@account";
                cmd.Parameters.AddWithValue("@account", account);
                cmd.Connection.Open();
                SqlDataReader dr=cmd.ExecuteReader();
                if (dr.Read())
                {
                    var dbHashPwd = dr["HashPassword"].ToString();
                    HashService hashService=new HashService();
                    var hashPwd = hashService.HashPwd(password, dr["CustomerId"].ToString().ToUpper());
                    if (dbHashPwd==hashPwd)
                    {
                        result = true;
                    }
                }
                //else

            }
                return result;
        }
    }
}
