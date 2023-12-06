using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExchangeRatesWebapi.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataUrls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insert data into Urls table
            migrationBuilder.InsertData(
                table: "Urls",
                columns: new[] { "Bank_Name", "Bank_Id", "Bank_Url", "Currency", "Buy_Xpath", "Sell_Xpath" },
                values: new object[,]
                {
            { "AmeriaBank", 1, "https://www.ameriabank.am", "USD", "//table[@id='dnn_ctr16862_View_grdRates']//tr[3]/td[2]", "//table[@id='dnn_ctr16862_View_grdRates']//tr[3]/td[3]" },
            { "AmeriaBank", 1, "https://www.ameriabank.am", "EUR", "//table[@id='dnn_ctr16862_View_grdRates']//tr[4]/td[2]", "//table[@id='dnn_ctr16862_View_grdRates']//tr[4]/td[3]" },
            { "HSBCBank", 2, "https://www.hsbc.am/help/rates/", "USD", "//*[@id='content_main_basicTable_1']/table/tbody/tr[11]/td[2]", "//*[@id='content_main_basicTable_1']/table/tbody/tr[11]/td[3]" },
            { "HSBCBank", 2, "https://www.hsbc.am/help/rates/", "EUR", "//*[@id='content_main_basicTable_1']/table/tbody/tr[6]/td[2]", "//*[@id='content_main_basicTable_1']/table/tbody/tr[6]/td[3]" },
            { "ArdshinBank", 3, "https://www.ardshinbank.am/en", "USD", "//*[@id='cash']/table/tbody/tr[2]/td[2]/span[1]", "//*[@id='cash']/table/tbody/tr[2]/td[3]/span[1]" },
            { "ArdshinBank", 3, "https://www.ardshinbank.am/en", "EUR", "//*[@id='cash']/table/tbody/tr[3]/td[2]/span[1]", "//*[@id='cash']/table/tbody/tr[3]/td[3]/span[1]" },
                });
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Urls");

            migrationBuilder.DropTable(
                name: "Rates");
        }
    }
}
