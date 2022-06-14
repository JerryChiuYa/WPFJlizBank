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
        public ObservableCollection<BankPersonalInfo> GetAllAccountsInfo(string LoginAccount)
        {
            var data=new ObservableCollection<BankPersonalInfo>();

            using (var conn=new SqlConnection(_dbConnStr))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"select bank.AccountNum, bank.CustomerId, bank.BankId, bank.BankName, bank.LoginAccount, bank.HashPassword, 
                                                                bank.AccountBalance, bank.AllertAccount, bank.InitDate as bank_init, bank.ModifyDate as bank_modify,
                                                                info.IdentityNum, info.UserName, info.Birthday, info.Phone, info.Mobile, info.Address, info.Email, 
                                                                info.InitDate as info_init, info.ModifyDate as info_modify
                                                                from BankAccount  as bank
                                                                join BankPersonalInfo info on bank.CustomerId=info.CustomerId
                                                                where bank.CustomerId=
                                                                (
                                                                    select bank.CustomerId
                                                                    from BankAccount as bank
                                                                    where LoginAccount=@LoginAccount
                                                                )";
                cmd.Parameters.AddWithValue("@LoginAccount",LoginAccount);
                cmd.Connection.Open();
                var dr = cmd.ExecuteReader();

                
                while (dr.Read())
                {
            
                    var personalInfo = data.FirstOrDefault(c => c.CustomerId == Guid.Parse(dr["CustomerId"].ToString()));
                    if (personalInfo == null)
                    {
                        personalInfo = new BankPersonalInfo();
                        personalInfo.CustomerId = Guid.Parse(dr["CustomerId"].ToString());
                        personalInfo.UserName = dr["UserName"].ToString();
                        personalInfo.Phone = dr["Phone"].ToString();
                        personalInfo.IdentityNum = dr["IdentityNum"].ToString();
                        personalInfo.Birthday = DateTime.Parse(dr["Birthday"].ToString());
                        personalInfo.Mobile = dr["Mobile"].ToString();
                        personalInfo.Address = dr["Address"].ToString();
                        personalInfo.Email = dr["Email"].ToString();
                        personalInfo.InitDate = DateTime.Parse(dr["info_init"].ToString());
                        if (!string.IsNullOrEmpty(dr["info_modify"].ToString()))
                        {
                            personalInfo.ModifyDate = DateTime.Parse(dr["bank_modify"].ToString());
                        }

                    }
                    if (personalInfo.bankInfoList==null)
                    {
                        personalInfo.bankInfoList = new ObservableCollection<BankAccount>();
                    }
                    personalInfo.bankInfoList.Add(new BankAccount()
                    {
                        AccountNum = dr["AccountNum"].ToString(),
                        AllertAccount  = bool.Parse(dr["AllertAccount"].ToString()),
                        BankId = dr["BankId"].ToString(),
                        AccountBalance = decimal.Parse(dr["AccountBalance"].ToString()),
                        BankName = dr["BankName"].ToString(),
                        CustomerId = Guid.Parse(dr["CustomerId"].ToString()),
                        HashPassword = dr["HashPassword"].ToString(),
                        LoginAccount = dr["LoginAccount"].ToString(),
                        InitDate = DateTime.Parse(dr["bank_init"].ToString()),
                        ModifyDate = !string.IsNullOrEmpty(dr["bank_modify"].ToString()) ? DateTime.Parse(dr["bank_modify"].ToString()) : (DateTime?)null          
                });
                    data.Add(personalInfo);
                }
            }
                return data;
        }

        public BankPersonalInfo GetCurrentAccountInfo(string LoginAccount)
        {
            using (var conn=new SqlConnection(_dbConnStr))
            {
                var personalInfo = new BankPersonalInfo();
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"select bank.AccountNum, bank.CustomerId, bank.BankId, bank.BankName, bank.LoginAccount, bank.HashPassword, 
                                                                bank.AccountBalance, bank.AllertAccount, bank.InitDate as bank_init, bank.ModifyDate as bank_modify,
                                                                info.IdentityNum, info.UserName, info.Birthday, info.Phone, info.Mobile, info.Address, info.Email, 
                                                                info.InitDate as info_init, info.ModifyDate as info_modify
                                                                from BankAccount  as bank
                                                                join BankPersonalInfo info on bank.CustomerId = info.CustomerId 
                                                                where LoginAccount=@LoginAccount";

                cmd.Parameters.AddWithValue("@LoginAccount", LoginAccount);
                cmd.Connection.Open();
                var dr=cmd.ExecuteReader();

                while (dr.Read())
                {
                    personalInfo.CustomerId = Guid.Parse(dr["CustomerId"].ToString());
                    personalInfo.UserName = dr["UserName"].ToString();
                    personalInfo.Phone = dr["Phone"].ToString();
                    personalInfo.IdentityNum = dr["IdentityNum"].ToString();
                    personalInfo.Birthday = DateTime.Parse(dr["Birthday"].ToString());
                    personalInfo.Mobile = dr["Mobile"].ToString();
                    personalInfo.Address = dr["Address"].ToString();
                    personalInfo.Email = dr["Email"].ToString();
                    personalInfo.InitDate = DateTime.Parse(dr["info_init"].ToString());
                    if (!string.IsNullOrEmpty(dr["info_modify"].ToString()))
                    {
                        personalInfo.ModifyDate = DateTime.Parse(dr["bank_modify"].ToString());
                    }
                    if (personalInfo.bankInfoList == null)
                    {
                        personalInfo.bankInfoList = new ObservableCollection<BankAccount>();
                    }
                    personalInfo.bankInfoList.Add(new BankAccount()
                    {
                        AccountNum = dr["AccountNum"].ToString(),
                        AllertAccount = bool.Parse(dr["AllertAccount"].ToString()),
                        BankId = dr["BankId"].ToString(),
                        AccountBalance = decimal.Parse(dr["AccountBalance"].ToString()),
                        BankName = dr["BankName"].ToString(),
                        CustomerId = Guid.Parse(dr["CustomerId"].ToString()),
                        HashPassword = dr["HashPassword"].ToString(),
                        LoginAccount = dr["LoginAccount"].ToString(),
                        InitDate = DateTime.Parse(dr["bank_init"].ToString()),
                        ModifyDate = !string.IsNullOrEmpty(dr["bank_modify"].ToString()) ? DateTime.Parse(dr["bank_modify"].ToString()) : (DateTime?)null
                    });
                    
                }
                return personalInfo;
            }
            
        }

        public ObservableCollection<TransactionRecordsDetails> GetTransaction(string AccountNum, DateTime start, DateTime end)
        {
            var data = new ObservableCollection<TransactionRecordsDetails>();

            using (var conn=new SqlConnection(_dbConnStr))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"select * from TransactionRecordsDetails 
                                                            where ((FromAccountNum=@AccountNum and TransactionType='Transfer') or (ToAccountNum=@AccountNum and TransactionType='Receive')) 
                                                            and TransactionTime>=@start and TransactionTime<=@end
                                                            order by TransactionTime desc";
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

        public void AddTransactionDetails(BankAccount bankInfo, decimal TransactionMoney)
        {
            if (bankInfo.GetTransactionList().Count != 0)
            {
                using (var conn = new SqlConnection(_dbConnStr))
                {
                    string insertSql = "insert into B (AccountNum, BankId, BankName, LoginAccount, HashPassword, AllertAccount, InitDate, CustomerId) " +
                        "values(@AccountNum, @BankId, @BankName, @LoginAccount, @HashPassword, 'False', GETDATE(), @CustomerId)";
                    var cmd = new SqlCommand(insertSql, conn);
                    cmd.Parameters.AddWithValue("@AccountNum", bankInfo.AccountNum);
                    cmd.Parameters.AddWithValue("@BankId", bankInfo.BankId);
                    cmd.Parameters.AddWithValue("@BankName", bankInfo.BankName);
                    cmd.Parameters.AddWithValue("@LoginAccount", bankInfo.LoginAccount);
                    cmd.Parameters.AddWithValue("@HashPassword", bankInfo.HashPassword);
                    cmd.Parameters.AddWithValue("@CustomerId", personalInfo.CustomerId.ToString().ToUpper());
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }

        }
    }
}
