using BankLibrary.BankEntity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary.Services
{
    public class CustomerServices
    {
        private string _dbConnStr;

        public CustomerServices(string dbConnStr)
        {
            _dbConnStr = dbConnStr;
        }
        public ObservableCollection<BankPersonalInfo> GetPersonalInfo()
        {
            var data=new ObservableCollection<BankPersonalInfo>();

            using (var conn=new SqlConnection(_dbConnStr))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"select info.CustomerId, info.UserName, info.Phone, info.Mobile, info.Address, info.Email, info.InitDate, info.ModifyDate, bankinfo.AccountNum,
                                                        bankinfo.BankId, bankinfo.BankName, bankinfo.LoginAccount, bankinfo.HashPassword, bankinfo.AccountBalance, bankinfo.AllertAccount
                                                        from BankPersonalInfo as info
                                                        join BankAccount as bankinfo on info.CustomerId = bankinfo.CustomerId";
                cmd.Connection.Open();
                var dr = cmd.ExecuteReader();

                
                while (dr.Read())
                {
            
                    var customer = data.FirstOrDefault(c => c.CustomerId == Guid.Parse(dr["CustomerId"].ToString()));
                    if (customer == null)
                    {
                        customer = new BankPersonalInfo();
                        customer.CustomerId = Guid.Parse(dr["CustomerId"].ToString());
                        customer.UserName = dr["UserName"].ToString();
                        customer.Phone = dr["Phone"].ToString();
                        customer.Mobile = dr["Mobile"].ToString();
                        customer.Address = dr["Address"].ToString();
                        customer.Email = dr["Email"].ToString();
                        customer.InitDate = DateTime.Parse(dr["InitDate"].ToString());
                        if (!string.IsNullOrEmpty(dr["ModifyDate"].ToString()))
                        {
                            customer.ModifyDate = DateTime.Parse(dr["ModifyDate"].ToString());
                        }
                        data.Add(customer);
                    }
                    if (customer.bankInfoList==null)
                    {
                        customer.bankInfoList = new ObservableCollection<BankAccount>();
                    }
                    customer.bankInfoList.Add(new BankAccount()
                    {
                        AccountNum = dr["AccountNum"].ToString(),
                        AllertAccount  = bool.Parse(dr["AllertAccount"].ToString()),
                        BankId = dr["BankId"].ToString(),
                        AccountBalance = decimal.Parse(dr["AccountBalance"].ToString()),
                        BankName = dr["BankName"].ToString(),
                        CustomerId = Guid.Parse(dr["CustomerId"].ToString()),
                        HashPassword = dr["HashPassword"].ToString(),
                        LoginAccount = dr["LoginAccount"].ToString(),
                    }); 

                }
            }
                return data;
        }

        public ObservableCollection<BankPersonalInfo> GetFilterCustomerInfo(string IdentityNum, string UserName)
        {
            var data = new ObservableCollection<BankPersonalInfo>();
            return data;
        }

        public ObservableCollection<TransactionRecordsDetails> GetTransaction(string AccountNum, DateTime start, DateTime end)
        {
            var data = new ObservableCollection<TransactionRecordsDetails>();

            using (var conn=new SqlConnection(_dbConnStr))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "  select * from TransactionRecordsDetails where ((FromAccountNum=@AccountNum and TransactionType='Transfer') or (ToAccountNum=@AccountNum and TransactionType='Receive')) and TransactionTime>=@start and TransactionTime<=@end";
                cmd.Parameters.AddWithValue("@AccountNum", AccountNum);
                cmd.Parameters.AddWithValue("@start", start);
                cmd.Parameters.AddWithValue("@end", end);
                cmd.Connection.Open();
                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    var transaction=new TransactionRecordsDetails();
                    transaction.TransactionNum = Guid.Parse(dr["TransactionNum"].ToString());
                    transaction.TransactionTime = DateTime.Parse(dr["TransactionTime"].ToString());
                    transaction.FromAccountNum = dr["FromAccountNum"].ToString();
                    transaction.ToAccountNum = dr["ToAccountNum"].ToString();
                    transaction.FromBankId = dr["FromBankId"].ToString();
                    transaction.FromBankName = dr["FromBankName"].ToString();
                    transaction.TransactionType = dr["TransactionType"].ToString();
                    transaction.TransactionMoney = decimal.Parse(dr["TransactionMoney"].ToString());
                    transaction.ToBankId = dr["ToBankId"].ToString();
                    transaction.ToBankName = dr["ToBankName"].ToString();
                    transaction.HandlingFees = int.Parse(dr["HandlingFees"].ToString());
                    transaction.AccountBalance = decimal.Parse(dr["AccountBalance"].ToString());
                    transaction.Remark = dr["Remark"].ToString();

                    data.Add(transaction);
                }
            }
                return data;
        }

        public ObservableCollection<BankPersonalInfo> VerifyIdentity(string LoginAccount, string Email)
        {
            var data=new ObservableCollection<BankPersonalInfo>();

            using (var conn=new SqlConnection(_dbConnStr))
            {
                var cmd = new SqlCommand();
                cmd.Connection=conn;
                cmd.CommandText = @"select LoginAccount, Email, bank.CustomerId from BankAccount bank
                                                                inner join BankPersonalInfo personal 
                                                                on bank.CustomerId = personal.CustomerId and Email =@Email and LoginAccount =@LoginAccount";
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@LoginAccount", LoginAccount);
                cmd.Connection.Open();
                var dr=cmd.ExecuteReader();

                while (dr.Read())
                {
                    var customer = new BankPersonalInfo();
                    customer.CustomerId = Guid.Parse(dr["CustomerId"].ToString());
                    customer.Email= dr["email"].ToString();
                    customer.bankInfoList = new ObservableCollection<BankAccount>();
                    customer.bankInfoList.Add(new BankAccount { LoginAccount=dr["LoginAccount"].ToString()});
                    data.Add(customer);
                    
                }
            }
            return data;
        }

        public void ModifyPassword(string LoginAccount, string Password)
        {
            using (var conn=new SqlConnection(_dbConnStr))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "update BankAccount set HashPassword=@HashPassword where LoginAccount=@LoginAccount";
                cmd.Parameters.AddWithValue("@LoginAccount", LoginAccount);
                cmd.Parameters.AddWithValue("@HashPassword", Password);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
