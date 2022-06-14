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
    public class SettingsService
    {
        private string _dbConnStr;
        public SettingsService(string dbConnStr)
        {
            _dbConnStr=dbConnStr;
        }

        public ObservableCollection<BankAccount> GetBankAllData()
        {
            var data = new ObservableCollection<BankAccount>();

            //ADO.NET
            using (var conn = new SqlConnection(_dbConnStr))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select * from BankAccount";
                cmd.Connection.Open();
                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    var entity = new BankAccount();
                    entity.BankId = dr["BankId"].ToString();
                    entity.BankName = dr["BankName"].ToString();
                    entity.AccountNum = dr["AccountNum"].ToString();
                    entity.AccountBalance = decimal.Parse(dr["AccountBalance"].ToString());
                    entity.AllertAccount = bool.Parse(dr["AllertAccount"].ToString());
                    entity.CustomerId = Guid.Parse(dr["CustomerId"].ToString());
                    entity.LoginAccount = dr["LoginAccount"].ToString();
                    entity.HashPassword = dr["HashPassword"].ToString();
                    entity.InitDate = DateTime.Parse(dr["InitDate"].ToString());
                    if (!string.IsNullOrEmpty(dr["ModifyDate"].ToString()))
                    {
                        entity.ModifyDate = DateTime.Parse(dr["ModifyDate"].ToString());
                    }
                    data.Add(entity);
                }

            }
            return data;
        }

        public ObservableCollection<BankAccount> GetBankAllData(string LoginAccount)
        {
            var data = new ObservableCollection<BankAccount>();

            //ADO.NET
            using (var conn = new SqlConnection(_dbConnStr))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                string sqlStr= "select * from BankAccount ";
                string whereStr = string.Empty;

                 if (!string.IsNullOrEmpty(LoginAccount))
                {
                    cmd.Parameters.AddWithValue("@LoginAccount", LoginAccount);
                    whereStr = "where LoginAccount=@LoginAccount";
                }

                if (!string.IsNullOrEmpty(whereStr))
                {
                    sqlStr += whereStr;
                }
                cmd.CommandText = sqlStr;
                cmd.Connection.Open();
                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    var entity = new BankAccount();
                    entity.BankId = dr["BankId"].ToString();
                    entity.BankName = dr["BankName"].ToString();
                    entity.AccountNum = dr["AccountNum"].ToString();
                    entity.AccountBalance = decimal.Parse(dr["AccountBalance"].ToString());
                    entity.AllertAccount = bool.Parse(dr["AllertAccount"].ToString());
                    entity.CustomerId = Guid.Parse(dr["CustomerId"].ToString());
                    entity.LoginAccount = dr["LoginAccount"].ToString();
                    entity.HashPassword = dr["HashPassword"].ToString();
                    entity.InitDate = DateTime.Parse(dr["InitDate"].ToString());
                    if (!string.IsNullOrEmpty(dr["ModifyDate"].ToString()))
                    {
                        entity.ModifyDate = DateTime.Parse(dr["ModifyDate"].ToString());
                    }
                    data.Add(entity);
                }

            }
            return data;
        }

        public ObservableCollection<BankPersonalInfo> GetData()
        {
            var data=new ObservableCollection<BankPersonalInfo>();

            //ADO.NET
            using (var conn=new SqlConnection(_dbConnStr))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select * from BankPersonalInfo";
                cmd.Connection.Open();
                var dr = cmd.ExecuteReader();


                while (dr.Read())
                {
                    var entity = new BankPersonalInfo();
                    entity.CustomerId = Guid.Parse(dr["CustomerId"].ToString());
                    entity.Email = dr["Email"].ToString();
                    entity.IdentityNum = dr["IdentityNum"].ToString();
                    entity.Address = dr["Address"].ToString();
                    entity.UserName = dr["UserName"].ToString();
                    entity.Phone = dr["Phone"].ToString();
                    entity.Mobile = dr["Mobile"].ToString();
                    entity.Birthday = DateTime.Parse(dr["Birthday"].ToString());
                    entity.InitDate = DateTime.Parse( dr["InitDate"].ToString());
                    if (!string.IsNullOrEmpty(dr["ModifyDate"].ToString()))
                    {
                        entity.ModifyDate = DateTime.Parse(dr["ModifyDate"].ToString());
                    }
                    data.Add(entity);
                }

            }
                return data;
        }

        public ObservableCollection<BankPersonalInfo> GetData(string UserName, string Email)
        {
            var data = new ObservableCollection<BankPersonalInfo>();
            using (var conn=new SqlConnection(_dbConnStr))
            {
                var cmd = new SqlCommand();
                cmd.Connection=conn;

                var sqlStr = "select * from BankPersonalInfo ";
                var whereStr = string.Empty;

                if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Email))
                {
                    cmd.Parameters.AddWithValue("@UserName", UserName);
                    cmd.Parameters.AddWithValue("@Email", Email);
                    whereStr = "where UserName=@UserName and Email=@Email";
                }
                else if (!string.IsNullOrEmpty(UserName))
                {
                    cmd.Parameters.AddWithValue("@UserName", UserName);
                    whereStr = "where UserName=@UserName";
                }
                else if(!string.IsNullOrEmpty(Email))
                {
                    cmd.Parameters.AddWithValue("@Email", Email);
                    whereStr = "where Email=@Email";
                }
                if (!string.IsNullOrEmpty(whereStr))
                {
                    sqlStr += whereStr;
                }
                cmd.CommandText=sqlStr;
                cmd.Connection.Open();
                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    var entity = new BankPersonalInfo();
                    entity.Email = dr["Email"].ToString();
                    entity.IdentityNum = dr["IdentityNum"].ToString();
                    entity.Address = dr["Address"].ToString();
                    entity.UserName = dr["UserName"].ToString();
                    entity.Phone = dr["Phone"].ToString();
                    entity.Mobile = dr["Mobile"].ToString();
                    entity.Birthday = DateTime.Parse(dr["Birthday"].ToString());
                    entity.CustomerId = Guid.Parse(dr["CustomerId"].ToString());
                    entity.InitDate = DateTime.Parse(dr["InitDate"].ToString());
                    if (!string.IsNullOrEmpty(dr["ModifyDate"].ToString()))
                    {
                        entity.ModifyDate = DateTime.Parse(dr["ModifyDate"].ToString());
                    }
                    data.Add(entity);
                }
            }
                return data;
        }

        public void UpdateData(BankPersonalInfo personalInfo)
        {
            if (personalInfo != null)
            {
                using (var conn=new SqlConnection(_dbConnStr))
                {
                    string sqlStr = "update BankPersonalInfo set UserName=@UserName, Phone=@Phone, Mobile=@Mobile, " +
                        "Email=@Email, Address=@Address, Birthday=@Birthday, ModifyDate=GETDATE() where CustomerId=@CustomerId";
                    var cmd = new SqlCommand(sqlStr, conn);
                    cmd.Parameters.AddWithValue("@UserName", personalInfo.UserName);
                    cmd.Parameters.AddWithValue("@Phone", personalInfo.Phone);
                    cmd.Parameters.AddWithValue("@Mobile", personalInfo.Mobile);
                    cmd.Parameters.AddWithValue("@Birthday", personalInfo.Birthday);
                    cmd.Parameters.AddWithValue("@Email", personalInfo.Email);
                    cmd.Parameters.AddWithValue("@Address", personalInfo.Address);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public BankPersonalInfo AddPersonalData(BankPersonalInfo personalInfo)
        {
            if (personalInfo != null)
            {
                using (var conn=new SqlConnection(_dbConnStr))
                {
                    string insertSql = "insert into BankPersonalInfo (IdentityNum, UserName, Phone, Mobile, CustomerId, Address, Email, InitDate)" +
                        "values(@IdentityNum, @UserName, @Phone, @Mobile, @CustomerId, @Address, @Email, GETDATE())";
                    var cmd = new SqlCommand(insertSql, conn);
                    cmd.Parameters.AddWithValue("@IdentityNum", personalInfo.IdentityNum);
                    cmd.Parameters.AddWithValue("@UserName", personalInfo.UserName);
                    cmd.Parameters.AddWithValue("@CustomerId", personalInfo.CustomerId);
                    cmd.Parameters.AddWithValue("@Phone", personalInfo.Phone);
                    cmd.Parameters.AddWithValue("@Birthday", personalInfo.Birthday);
                    cmd.Parameters.AddWithValue("@Mobile", personalInfo.Mobile);
                    cmd.Parameters.AddWithValue("@Address", personalInfo.Address);
                    cmd.Parameters.AddWithValue("@Email", personalInfo.Email);
           

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            return personalInfo;
        }

        public void AddBankData(BankAccount bankInfo, BankPersonalInfo personalInfo)
        {
            if (bankInfo!=null)
            {
                using (var conn=new SqlConnection(_dbConnStr))
                {
                    string insertSql = "insert into BankAccount (AccountNum, BankId, BankName, LoginAccount, HashPassword, AllertAccount, InitDate, CustomerId) " +
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

        public void DeleteData(BankPersonalInfo personalInfo)
        {
            if (personalInfo !=null)
            {
                using (var conn=new SqlConnection(_dbConnStr))
                {
                    string deleteStr = "delete BankPersonalInfo where CustomerId=@CustomerId";
                    var cmd1 = new SqlCommand(deleteStr, conn);
                    cmd1.Parameters.AddWithValue("@AccountNum", personalInfo.CustomerId);
                    string deleteStr2 = "delete BankAccount where AccountNum=@AccountNum";
                    var cmd2 = new SqlCommand(deleteStr2, conn);
                    cmd2.Parameters.AddWithValue("@AccountNum", personalInfo.CustomerId);

                    conn.Open();
                    cmd1.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();
                    
                }
            }
        }

        public ObservableCollection<BranchBank> GetBankId(string BankName)
        {
            var data=new ObservableCollection<BranchBank>();

            using (var conn=new SqlConnection(_dbConnStr))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select * from BranchBank where BankName=@BankName";
                cmd.Parameters.AddWithValue("@BankName", BankName);
                cmd.Connection.Open();
                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    var entity = new BranchBank();
                    entity.BankId = dr["BankId"].ToString();
                    entity.BankName = dr["BankName"].ToString();
                    entity.InitDate = DateTime.Parse(dr["InitDate"].ToString());
                    if (!string.IsNullOrEmpty(dr["ModifyDate"].ToString()))
                    {
                        entity.ModifyDate = DateTime.Parse(dr["ModifyDate"].ToString());
                    }
                    data.Add(entity);
                }
            }
            return data;
        }

       

    }
}
