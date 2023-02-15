using System;
namespace DBTest
{
	public static class Helper
	{
		public static bool SingleUserTransaction()
		{
			Console.WriteLine("Överför pengar, egna konton");
			// Meny här,välj konto från
			//meny här, väljkonto till
			// fråga här, ange belopp
			return PostgresDataAccess.TransferMoney(1, 7, 2, 1000);
		}

        public static decimal ConvertCurrency(decimal amountInSEK, string boughtCurrency)
		{
			// This function takes an amount in SEK, and a currency name you wish to buy (exchange to)

            // boughtCurrency and soldCurrency are different

            List<BankCurrencyModel> currencies = PostgresDataAccess.GetExchangeRates();

            double exchange_rate = currencies.Find(c => c.name == boughtCurrency).exchange_rate;
            Console.WriteLine($"1 SEK = {1 / exchange_rate} EUR");
            return Math.Round(amountInSEK / (decimal)exchange_rate, 2);
        }

    }
}

