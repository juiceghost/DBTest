using System;
namespace DBTest
{
	public class BankCurrencyModel
	{
        public int id { get; set; }

        public string name { get; set; }

        public double exchange_rate { get; set; }

        //public List<BankAccountModel> accounts { get; set; }

        //public List<BankAccountModel> GetAccounts(bool immediate = false)
        //{
        //    var ts = new TimeSpan(DateTime.UtcNow.Ticks - accounts_timestamp.Ticks);
        //    double delta = Math.Abs(ts.TotalSeconds);
        //    //Console.WriteLine("delta: " + delta);
        //    //accounts_timestamp = DateTime.UtcNow;
        //    if (delta > 45 | immediate)
        //    {
        //        //Console.WriteLine("Cache expired");
        //        accounts_timestamp = DateTime.UtcNow;
        //        accounts = PostgresDataAccess.GetUserAccounts(id);
        //        return accounts;
        //    }
        //    return accounts;
        //}
    }
}

