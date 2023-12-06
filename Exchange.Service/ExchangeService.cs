using Exchange.Repo.Entities;
using Exchange.Repo.Repositories;
using HtmlAgilityPack;

namespace Exchange.Service
{
    public class ExchangeService
    {
        private readonly ExchangeRepository exchangeRepository;

        public ExchangeService(ExchangeRepository exchangeRepository)
        {
            this.exchangeRepository = exchangeRepository;
        }

        public async Task Parse()
        {
            List<Urls> urls = exchangeRepository.GetBankXpath();

            foreach (var url in urls)
            {
                try
                {
                    using (var httpClient = new HttpClient())
                    {
                        string html = await httpClient.GetStringAsync(url.Bank_Url);
                        var doc = new HtmlDocument();
                        doc.LoadHtml(html);

                        var buy = doc.DocumentNode.SelectNodes(url.Buy_Xpath);
                        var sell = doc.DocumentNode.SelectNodes(url.Sell_Xpath);

                        if (buy != null && sell != null && buy.Count > 0 && sell.Count > 0 &&
                            url.Bank_Name != null && url.Currency != null)
                        {
                            Console.WriteLine((url.Bank_Name, buy[0].InnerText, sell[0].InnerText, url.Currency));

                            string b = buy[0].InnerText;
                            string s = sell[0].InnerText;

                            if (decimal.TryParse(b, out decimal buyValue) && decimal.TryParse(s, out decimal sellValue))
                            {
                                await exchangeRepository.AddRate(new Rates
                                {
                                    UrlsId = url.Id,
                                    Buy = buyValue,
                                    Sell = sellValue
                                });
                            }
                            else
                            {
                                Console.WriteLine($"Error parsing buy or sell value for bank {url.Bank_Name}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error: XPath data not found for bank " + url.Bank_Name);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing bank {url.Bank_Name}: {ex.Message}");
                }
            }
            Console.WriteLine("Processing complete.");
        }

        public async Task<dynamic> PostRates(string currencyName)
        {
            var lastRows = await exchangeRepository.PostRatesQuery(currencyName);

            return lastRows;
        }
    }
}
