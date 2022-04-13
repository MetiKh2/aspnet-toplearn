using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.Core.DTOs.WalletViewModels
{
   public class WalletViewModel
    {
        public string Description { get; set; }
        public long Amount { get; set; }
        public int Type { get; set; }
        public DateTime DateTime { get; set; }
    }
}
