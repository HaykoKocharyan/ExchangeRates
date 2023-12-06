using Exchange.Repo.Entities;
using Microsoft.EntityFrameworkCore;

namespace Exchange.Repo.Repositories
{
    public class ExchangeRepository
    {
        private readonly ExchangeContext dbContext = new ExchangeContext();

        public ExchangeRepository()
        {
        }

        public ExchangeRepository(ExchangeContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public Task AddRate(Rates rate)
        {
            dbContext.Rates.Add(rate);
            return dbContext.SaveChangesAsync();
        }
        public List<Urls> GetBankXpath()
        {
            using (var context = new ExchangeContext())
            {
                var urls = context.Urls
                    .Select(url => new Urls
                    {
                        Id = url.Id,
                        Bank_Name = url.Bank_Name,
                        Bank_Url = url.Bank_Url,
                        Currency = url.Currency,
                        Buy_Xpath = url.Buy_Xpath,
                        Sell_Xpath = url.Sell_Xpath
                    }).ToList();

                return urls;
            }
        }
        public async Task<dynamic> PostRatesQuery(string currencyName)
        {
            var rate = await dbContext.Rates
                .Include(r => r.Urls)
                .GroupBy(r => r.UrlsId, r => r)
                .ToDictionaryAsync(r => r.Key, r => r.Max(g => g.DateTime));
            var rates = await dbContext.Urls
                 .Include(u => u.Rates)
                 .Where(rates => rates.Currency == currencyName)
                 .SelectMany(u =>  u.Rates)
                 .Select(rate => new
                 {
                     UrlsId = rate.UrlsId,
                     BankName = rate.Urls.Bank_Name,
                     Buy = rate.Buy,
                     Sell = rate.Sell,
                     Currency = rate.Urls.Currency,
                     DateTime = rate.DateTime
                 })
                 .ToListAsync();
            return rates
                 .Where(r => rate[r.UrlsId] == r.DateTime);
        }
    }
}
