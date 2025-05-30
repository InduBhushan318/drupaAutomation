using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drupAuto.Models
{
    public class page
    {
        public int PageNumber { get; set; }
        public List<AccountsModel> Accounts { get; set; } = new List<AccountsModel>();
    }
    
    public class AccountsModel
    {
        public string AccountName { get; set; }
        public bool isProcessed { get; set; } = false;
    }
}
