using System;
using BankingKata;

namespace BankingKataApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var account = new Account();
            account.Deposit(DateTime.Now, new Money(1000m));

            Console.WriteLine("Welcome to your account. \n");
            account.PrintBalance(new ConsolePrinter());

            while (true)
            {
                Console.WriteLine(@"
Press a key to choose an option:

    1. Cash deposit
    2. Cash withdrawal
    3. Print last transaction
            ");

                var userOption = Console.ReadKey();

                if (userOption.KeyChar == '1')
                {
                    Console.WriteLine("\n\nEnter an amount to deposit in pounds:");
                    var amountToDeposit = Console.ReadLine();

                    account.Deposit(DateTime.Now, new Money(decimal.Parse(amountToDeposit)));

                    Console.WriteLine();
                    account.PrintBalance(new ConsolePrinter());
                }

                if (userOption.KeyChar == '2')
                {
                    Console.WriteLine("\n\nEnter an amount to withdraw in pounds:");
                    var amountToWithdraw = Console.ReadLine();

                    account.Withdraw(new ATMDebitEntry(DateTime.Now,  new Money(decimal.Parse(amountToWithdraw))));

                    Console.WriteLine();
                    account.PrintBalance(new ConsolePrinter());
                }

                if (userOption.KeyChar == '3')
                {
                    Console.WriteLine();
                    Console.WriteLine();

                    account.PrintLastTransaction(new ConsolePrinter());
                }
            }
        }
    }
}
