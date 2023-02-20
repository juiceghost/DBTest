namespace DBTest;

class Program
{
    static async Task Main(string[] args)
    {

        /*
        Console.Write("Please enter FirstName: ");
        string firstName = Console.ReadLine();
        Console.Write("Please enter LastName: ");
        string lastName = Console.ReadLine();
        Console.Write("Please enter PinCode: ");
        string pinCode = Console.ReadLine();
        BankUserModel newUser = new BankUserModel
        {
            first_name = firstName,
            last_name = lastName,
            pin_code = pinCode
        };
        PostgresDataAccess.SaveBankUser(newUser);
        */
        ApiHelper.InitializeClient();

        /*
         * public DateTime Sunrise { get; set; }
        public DateTime Sunset { get; set; }
        */
        SunModel SunriseSunset = await SunProcessor.LoadSunInformation();

        
        Console.WriteLine($"Today the sun rose at {SunriseSunset.Sunrise.ToLocalTime()} and will set at {SunriseSunset.Sunset.ToLocalTime()}");
        Console.WriteLine($"Today is {SunriseSunset.Day_Length.Hours.ToString()} hours long");
        Console.ReadLine();


        SunModel TestSun = new SunModel
        {
            Sunrise = DateTime.Parse("7:42:42 AM"),
            Sunset = DateTime.Parse("8:12:12 PM"),
            Day_Length = TimeSpan.Parse("05:05:05")
        };
        Console.WriteLine($"Today the sun rose at {TestSun.Sunrise} and will set at {TestSun.Sunset}");
        Console.WriteLine($"Today is {TestSun.Day_Length.Hours.ToString()} hours long");
        Console.ReadLine();

        List<BankUserModel> users = PostgresDataAccess.LoadBankUsers();
        Console.WriteLine($"users length: {users.Count}");
        foreach (BankUserModel user in users)
        {
            Console.WriteLine($"Hello {user.first_name} your pincode is {user.pin_code}");
        }
        while (true)
        {
            Console.WriteLine("\nPlease login.");
            Console.Write("Please enter FirstName: ");
            string firstName = Console.ReadLine();
       
            Console.Write("Please enter PinCode: ");
            string pinCode = Console.ReadLine();
            List<BankUserModel> checkedUsers = PostgresDataAccess.CheckLogin(firstName, pinCode);
            if (checkedUsers.Count < 1)
            {
                Console.WriteLine("Login failed, please try again");
                continue;
            }
            /*
            bool success = Helper.SingleUserTransaction();
            if (success)
            {
                Console.WriteLine("Transaction complete");
            } else
            {
                Console.WriteLine("Transaction failed. Insufficient balance.");
            }*/
        foreach (BankUserModel user in checkedUsers)
            {
                // när raden nedan körs, vad händer?

                Console.WriteLine($"Logged in as {user.first_name} your pincode is {user.pin_code} and the id is {user.id}");
                Console.WriteLine($"role_id: {user.role_id} branch_id: {user.branch_id}");
                Console.WriteLine($"is_admin: {user.is_admin} is_client: {user.is_client}");
                Console.WriteLine($"User account list length: {user.GetAccounts().Count}");
                Console.WriteLine("\nPlease select an account from the list");

                decimal maxLoanAmount = 0; // After this loop is finished, this variable should contain the sum of all balances of all user accounts
                // in SEK, for example: Account 1 has 1000 SEK, account 2 has 100 USD. let's say 1 USD = 10 SEK.
                // Max loan amount is 1000 + (100 * 10) = 2000. User is allowed to borrow 5 times total value of accounts. 2000 * 5 = 10000.
                // max loan amount is 10000 SEK or 1000 USD
                // maxLoanAmount is the amount in SEK

                if (user.accounts.Count > 0)
                {
                    for (int i = 0; i < user.accounts.Count; i++)
                    {
                        var currA = user.accounts[i];
                        Console.Write($"\n{i + 1}. ID: {currA.id} Account name: {currA.name} Balance: {currA.balance} {currA.currency_name} ");
                        if (currA.currency_name != "SEK")
                        {
                            Console.Write($"(Value in SEK: {currA.balance * (decimal)currA.currency_exchange_rate})");
                            maxLoanAmount = maxLoanAmount + currA.balance * (decimal)currA.currency_exchange_rate;
                        }
                        else
                        {
                            maxLoanAmount = maxLoanAmount + currA.balance;
                        }
                        Console.Write("\n");
                        currA.GetTransactionsByAccountId(currA.id).ForEach(currT =>
                        {
                            var accountString = currT.to_account_id == currA.id ? $"{currT.name} från konto {currT.from_account_id} " : $"{currT.name} till konto {currT.to_account_id} ";
                            Console.WriteLine($" {accountString}: {currT.GetSignedAmount(currA.id)}");
                        });
                        
                    }
                    maxLoanAmount = maxLoanAmount * 5;
                    var maxLoanEUR = Helper.ConvertCurrency(maxLoanAmount, "EUR");
                    var maxLoanUSD = Helper.ConvertCurrency(maxLoanAmount, "USD");
                    Console.WriteLine($"You are allowed to borrow {maxLoanAmount} SEK, {maxLoanEUR} EUR, {maxLoanUSD} USD");
                    //Console.WriteLine(Helper.ConvertCurrency(100, "EUR"));
                    string choice = Console.ReadLine();
                    Console.WriteLine($"You chose selection number {choice}");
                    // choice är en sträng, och ett högre än det sanna värdet (indexet i choiceAccontMap)

                    // titta i userChoiceMap på index int(choice) - 1
                    // plocka ut det värdet som är en int redan, detta är ditt ID

                    // konvertera choice till en int, dra bort 1
                    int choiceNumber = int.Parse(choice) - 1;

                    int userAccountID = user.GetAccounts()[choiceNumber].id;
                    BankAccountModel chosenAccount = PostgresDataAccess.GetAccountById(userAccountID);

                    Console.WriteLine($"Account chosen is: {chosenAccount.id}: {chosenAccount.name}");
                }


            }
        }

        Console.WriteLine("Menu system");
        bool mainMenu = true;
        while (mainMenu)
        {
            Console.WriteLine("Welcome to Monsters Inc. There are {0} monsters in the database.");
            Console.WriteLine("Please select one of the following");
            Console.WriteLine("1. List all monsters");
            Console.WriteLine("2. Create new monster");
            Console.WriteLine("3. Update new monster");
            Console.WriteLine("4. Have 2 monsters fight");
            Console.WriteLine("E. Exit");
            Console.Write("----> ");
            string? choice = Console.ReadLine().ToUpper();
            switch (choice)
            {
                case "1":
                    Console.WriteLine("Listing all monsters");
                    
                    break;
                case "2":
                    Console.WriteLine("Creating new monster");
                    
                    break;
                case "3":
                    Console.WriteLine("Update selected");
                    
                    break;
                case "4":
                    
                    break;

                case "E":
                    Console.WriteLine("E selected");
                    Console.WriteLine();
                    mainMenu = false;
                    break;
                default:
                    Console.WriteLine("Please type either 1 or E and press enter");
                    Console.WriteLine();
                    break;

            }
        }
    }
}
