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
        public bool VerifyUser(string account, string password)
        {
            var result = false;
            using (var conn = new SqlConnection(_dbConnStr))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select * from BankAccount where LoginAccount=@account";
                cmd.Parameters.AddWithValue("@account", account);
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    var dbHashPwd = dr["HashPassword"].ToString();
                    HashService hashService = new HashService();
                    var hashPwd = hashService.HashPwd(password, dr["CustomerId"].ToString().ToUpper());
                    if (dbHashPwd == hashPwd)
                    {
                        result = true;
                    }
                }
                //else

            }
            return result;
        }
        public void UpdatePersonalInfo(BankPersonalInfo personalInfo)
        {
            if (personalInfo != null)
            {
                using (var conn = new SqlConnection(_dbConnStr))
                {
                    string sqlStr = @"update BankPersonalInfo set UserName=@UserName, Phone=@Phone, Mobile=@Mobile,
                        Email=@Email, Address=@Address, Birthday=@Birthday, ModifyDate=GETDATE() where CustomerId=@CustomerId";
                    var cmd = new SqlCommand(sqlStr, conn);
                    cmd.Parameters.AddWithValue("@UserName", personalInfo.UserName);
                    cmd.Parameters.AddWithValue("@Phone", personalInfo.Phone);
                    cmd.Parameters.AddWithValue("@Mobile", personalInfo.Mobile);
                    cmd.Parameters.AddWithValue("@Birthday", personalInfo.Birthday);
                    cmd.Parameters.AddWithValue("@Email", personalInfo.Email);
                    cmd.Parameters.AddWithValue("@Address", personalInfo.Address);
                    cmd.Parameters.AddWithValue("@CustomerId", personalInfo.CustomerId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
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

        public BankAccount GetBankAccount(string AccountNum)
        {
            using (var conn = new SqlConnection(_dbConnStr))
            {
                var account = new BankAccount();
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"select * from BankAccount where AccountNum=@AccountNum";

                cmd.Parameters.AddWithValue("@AccountNum", AccountNum);
                cmd.Connection.Open();
                var dr = cmd.ExecuteReader();

                //if (dr["AccountNum"]==null)
                //{
                //    return null;
                //}
                while (dr.Read())
                {
                    account.CustomerId = Guid.Parse(dr["CustomerId"].ToString());
                    account.AccountNum = dr["AccountNum"].ToString();
                    account.BankId = dr["BankId"].ToString();
                    account.BankName = dr["BankName"].ToString();
                    account.LoginAccount = dr["LoginAccount"].ToString();
                    account.HashPassword = dr["HashPassword"].ToString();
                    account.AccountBalance = decimal.Parse(dr["AccountBalance"].ToString());
                    account.AllertAccount = bool.Parse(dr["AllertAccount"].ToString());
                    account.InitDate = DateTime.Parse(dr["InitDate"].ToString());
                    if (!string.IsNullOrEmpty(dr["ModifyDate"].ToString()))
                    {
                        account.InitDate = DateTime.Parse(dr["ModifyDate"].ToString());
                    }
                   

                }
                return account;
            }

        }
        //public ObservableCollection<TransactionRecordsDetails> GetAllTransaction(string AccountNum)
        //{
        //    var data = new ObservableCollection<TransactionRecordsDetails>();

        //    using (var conn = new SqlConnection(_dbConnStr))
        //    {
        //        var cmd = new SqlCommand();
        //        cmd.Connection = conn;
        //        cmd.CommandText = @"select * from TransactionRecordsDetails 
        //                                                    where ((FromAccountNum=@AccountNum and TransactionType='Transfer') or (ToAccountNum=@AccountNum and TransactionType='Receive')) 
        //                                                    order by TransactionTime desc";
        //        cmd.Parameters.AddWithValue("@AccountNum", AccountNum);
        //        cmd.Connection.Open();
        //        var dr = cmd.ExecuteReader();

        //        while (dr.Read())
        //        {
        //            var transaction = new TransactionRecordsDetails();
        //            transaction.TransactionNum = Guid.Parse(dr["TransactionNum"].ToString());
        //            transaction.TransactionTime = DateTime.Parse(dr["TransactionTime"].ToString());
        //            transaction.FromAccountNum = dr["FromAccountNum"].ToString();
        //            transaction.ToAccountNum = dr["ToAccountNum"].ToString();
        //            transaction.FromBankId = dr["FromBankId"].ToString();
        //            transaction.FromBankName = dr["FromBankName"].ToString();
        //            transaction.TransactionType = dr["TransactionType"].ToString();
        //            transaction.TransactionMoney = decimal.Parse(dr["TransactionMoney"].ToString());
        //            transaction.ToBankId = dr["ToBankId"].ToString();
        //            transaction.ToBankName = dr["ToBankName"].ToString();
        //            transaction.HandlingFees = int.Parse(dr["HandlingFees"].ToString());
        //            transaction.AccountBalance = decimal.Parse(dr["AccountBalance"].ToString());
        //            transaction.Remark = dr["Remark"].ToString();

        //            data.Add(transaction);
        //        }
        //    }
        //    return data;
        //}
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

        public TransactionRecordsDetails TransferMoneyService(BankAccount bankInfo, TransactionRecordsDetails transaction)
        {
            var currentTime = DateTime.Now;
                using (var conn = new SqlConnection(_dbConnStr))
                {
                    string insertSql = @"insert into TransactionRecordsDetails (TransactionNum, TransactionTime, FromAccountNum, ToAccountNum, FromBankId, FromBankName, 
                                                            TransactionType, TransactionMoney, ToBankId, ToBankName, HandlingFees, AccountBalance, Remark) 
                                                            values(@TransactionNum, @TransactionTime, @FromAccountNum, @ToAccountNum, @FromBankId, @FromBankName, @TransactionType, @TransactionMoney, 
                                                            @ToBankId, @ToBankName, @HandlingFees, @AccountBalance, @Remark)";

                
                //增加付款人紀錄
                    var cmd = new SqlCommand(insertSql, conn);
                    cmd.Parameters.AddWithValue("@TransactionNum", Guid.NewGuid());
                    cmd.Parameters.AddWithValue("@TransactionTime", currentTime);
                    cmd.Parameters.AddWithValue("@FromAccountNum", bankInfo.AccountNum);
                    cmd.Parameters.AddWithValue("@ToAccountNum", transaction.ToAccountNum);
                    cmd.Parameters.AddWithValue("@FromBankId", bankInfo.BankId);
                    cmd.Parameters.AddWithValue("@FromBankName", bankInfo.BankName);
                    cmd.Parameters.AddWithValue("@TransactionType", "Transfer");
                    cmd.Parameters.AddWithValue("@TransactionMoney", transaction.TransactionMoney);
                    cmd.Parameters.AddWithValue("@ToBankName", transaction.ToBankName);
                        var TobankIdList = new SettingsService(_dbConnStr).GetBankId(transaction.ToBankName);
                                foreach (var item in TobankIdList)
                                {
                                    transaction.ToBankId = item.BankId;
                                }

                    cmd.Parameters.AddWithValue("@ToBankId", transaction.ToBankId);
                        if (bankInfo.AllertAccount==true)
                        {
                            transaction.HandlingFees = 15M;
                        }
                        else
                        {
                            transaction.HandlingFees = 0;
                        }
                           
                    
                    cmd.Parameters.AddWithValue("@HandlingFees", transaction.HandlingFees);
                    cmd.Parameters.AddWithValue("@Remark", transaction.Remark);
                    cmd.Parameters.AddWithValue("@AccountBalance", bankInfo.AccountBalance-transaction.TransactionMoney);

                //更新付款人餘額
                UpdateTransferBalance(bankInfo, -transaction.TransactionMoney);

                //更新收款人餘額
                    var otherAccount = GetBankAccount(transaction.ToAccountNum);
                    UpdateTransferBalance(otherAccount, transaction.TransactionMoney);
  
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();

                cmd.Connection.Close();

                //增加收款人紀錄
                var cmd2 = new SqlCommand(insertSql, conn);
                cmd2.Parameters.AddWithValue("@TransactionNum", Guid.NewGuid());
                cmd2.Parameters.AddWithValue("@TransactionTime", currentTime);
                cmd2.Parameters.AddWithValue("@FromAccountNum", bankInfo.AccountNum);
                cmd2.Parameters.AddWithValue("@ToAccountNum", transaction.ToAccountNum);
                cmd2.Parameters.AddWithValue("@FromBankId", bankInfo.BankId);
                cmd2.Parameters.AddWithValue("@FromBankName", bankInfo.BankName);
                cmd2.Parameters.AddWithValue("@TransactionType", "Receive");
                cmd2.Parameters.AddWithValue("@TransactionMoney", transaction.TransactionMoney);
                cmd2.Parameters.AddWithValue("@ToBankName", transaction.ToBankName);
                cmd2.Parameters.AddWithValue("@ToBankId", transaction.ToBankId);
                cmd2.Parameters.AddWithValue("@HandlingFees", transaction.HandlingFees);
                cmd2.Parameters.AddWithValue("@Remark", transaction.Remark);
                cmd2.Parameters.AddWithValue("@AccountBalance", otherAccount.AccountBalance + transaction.TransactionMoney);

                cmd2.Connection.Open();
                cmd2.ExecuteNonQuery();
            }
            transaction.FromBankName = bankInfo.BankName;
            transaction.FromAccountNum = bankInfo.AccountNum;
            transaction.AccountBalance = bankInfo.AccountBalance;
            return transaction;
        }

        public bool UpdateTransferBalance(BankAccount bankinfo, decimal money)
        {
           
                using (var conn = new SqlConnection(_dbConnStr))
                {
                    string sqlStr = @"update BankAccount set AccountBalance=@AccountBalance, ModifyDate=@ModifyDate where AccountNum=@AccountNum";
                    var cmd = new SqlCommand(sqlStr, conn);
                cmd.Parameters.AddWithValue("@AccountBalance", bankinfo.AccountBalance+money);
                cmd.Parameters.AddWithValue("@ModifyDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@AccountNum", bankinfo.AccountNum);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            return true;
        }

        
    }
}
