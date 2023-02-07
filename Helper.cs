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
			return PostgresDataAccess.TransferMoney(1, 7, 2, 1300);


		}
	}
}

