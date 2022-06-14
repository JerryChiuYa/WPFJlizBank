using BankLibrary.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary.BankEntity
{
    public class BankPersonalInfo:BaseClass
    {
        public Guid CustomerId { get; set; }
        public string IdentityNum { get; set; }
        public string UserName { get; set; }
        public DateTime Birthday { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public ObservableCollection<BankAccount> bankInfoList { get; set; }
    }
    public class BankAccount : BaseClass
    {
        public string AccountNum { get; set; }
        public Guid CustomerId { get; set; }
        public string BankId { get; set; }
        public string BankName { get; set; }
        public string LoginAccount { get; set; }
        public string HashPassword { get; set; }
        public decimal AccountBalance { get; set; }
        public bool AllertAccount { get; set; }
        public ObservableCollection<TransactionRecordsDetails> GetTransactionList(DateTime start, DateTime end)
        {

            var data=new ObservableCollection<TransactionRecordsDetails>();
            var services = new CustomerServices(ConfigurationManager.ConnectionStrings["JlizBank"].ConnectionString);
            data=services.GetTransaction(this.AccountNum, start, end);

            return data;
        }
    }

    public class TransactionRecordsDetails
    {
        public Guid TransactionNum { get; set; }
        public DateTime TransactionTime { get; set; }
        public string FromAccountNum { get; set; }
        public string ToAccountNum { get; set; }
        public string FromBankId { get; set; }
        public string FromBankName { get; set; }
        public string TransactionType { get; set; }
        public decimal TransactionMoney { get; set; }
        public string ToBankId { get; set; }
        public string ToBankName { get; set; }
        public decimal? HandlingFees { get; set; }
        public decimal AccountBalance { get; set; }
        public string Remark { get; set; }

    }

    public class BranchBank:BaseClass
    {
        public string BankId { get; set; }
        public string BankName { get; set; }
    }
}
