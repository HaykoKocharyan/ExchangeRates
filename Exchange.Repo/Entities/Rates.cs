using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Repo.Entities
{
    public class Rates
    {
        public int Id { get; set; }
        public int UrlsId { get; set; }
        public decimal Buy { get; set; }
        public decimal Sell { get; set; }
        public DateTime DateTime { get; set; }
        public Urls Urls { get; set; }
    }
}
