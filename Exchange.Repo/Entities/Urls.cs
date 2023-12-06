using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Repo.Entities
{
    public class Urls
    {
        public int Id { get; set; }
        public string Bank_Name { get; set; }
        public int Bank_Id { get; set; }
        public string Bank_Url { get; set; }
        public string Currency { get; set; }
        public string Buy_Xpath { get; set; }
        public string Sell_Xpath { get; set; }
        public ICollection<Rates> Rates { get; set; }
    }
}
