using BankLibrary.BankEntity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary.Services
{
    public class TransferServices
    {
        private string _dbConnStr;

        public TransferServices(string dbConnStr)
        {
            _dbConnStr = dbConnStr;
        }

        public TransactionRecordsDetails TransferMoney()
        {
            var result=new TransactionRecordsDetails();
            return result;
        }
    }
}
