namespace DBTest;

class Program
{
    static void Main(string[] args)
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
            bool success = Helper.SingleUserTransaction();
            if (success)
            {
                Console.WriteLine("Transaction complete");
            } else
            {
                Console.WriteLine("Transaction failed. Insufficient balance.");
            }
            foreach (BankUserModel user in checkedUsers)
            {
                // när raden nedan körs, vad händer?
                //user.accounts = PostgresDataAccess.GetUserAccounts(user.id);
                List<BankAccountModel> tempAccounts = user.accounts;
                Console.WriteLine($"Logged in as {user.first_name} your pincode is {user.pin_code} and the id is {user.id}");
                Console.WriteLine($"role_id: {user.role_id} branch_id: {user.branch_id}");
                Console.WriteLine($"is_admin: {user.is_admin} is_client: {user.is_client}");
                Console.WriteLine($"User account list length: {tempAccounts}");
                Console.WriteLine("\nPlease select an account from the list");
                

                //users.account[0].balance = users.account[0].balance - 100
                //users.account[1].balance = users.account[1].balance + 100


                if (user.accounts.Count > 0)
                {
                    List<int> choiceAccountMap = new List<int> { };

                    for (int i = 0; i < tempAccounts.Count; i++)
                    {
                        // här vet jag vilken "position" jag är på i menyn
                        // Lägg till kontots id i Listan
                        choiceAccountMap.Add(tempAccounts[i].id);

                        // [1, 2, 5, 7]
                        //Console.WriteLine($"i: {i}, accounts[i].id: {user.accounts[i].id}");
                        //foreach (BankAccountModel account in user.accounts)
                        //{
                        Console.WriteLine($"\n{i + 1}. ID: {tempAccounts[i].id} Account name: {tempAccounts[i].name} Balance: {tempAccounts[i].balance}");
                        //Console.WriteLine($"Currency: {account.currency_name} Exchange rate: {account.currency_exchange_rate}");
                    }
                    string choice = Console.ReadLine();
                    Console.WriteLine($"You chose selection number {choice}");
                    // choice är en sträng, och ett högre än det sanna värdet (indexet i choiceAccontMap)

                    // titta i userChoiceMap på index int(choice) - 1
                    // plocka ut det värdet som är en int redan, detta är ditt ID

                    // konvertera choice till en int, dra bort 1
                    int choiceNumber = int.Parse(choice) - 1;

                    int userAccountID = choiceAccountMap[choiceNumber];
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
