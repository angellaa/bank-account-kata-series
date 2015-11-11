using System;
using System.Collections.Generic;
using BankingKata;

namespace BankingKataApp
{
    class Program
    {
        static readonly Dictionary<char, Action<AccountWithOverdraft, ConsolePrinter>> s_MenuOptions = new Dictionary<char, Action<AccountWithOverdraft, ConsolePrinter>>
        {
            { '1', MakeDeposit },
            { '2', MakeWithdrawal },
            { '3', PrintLastTransaction }
        };

        static void Main(string[] args)
        {
            var account = SetupAccount();
            var consolePrinter = new ConsolePrinter();

            Console.WriteLine("Welcome to your account. \n");
            account.PrintBalance(consolePrinter);

            while (true)
            {
                PrintMenu();

                var userOption = GetUserOption();

                s_MenuOptions[userOption.KeyChar].Invoke(account, consolePrinter);
            }
        }

        private static ConsoleKeyInfo GetUserOption()
        {
            var userOption = Console.ReadKey();
            return userOption;
        }

        private static void PrintLastTransaction(AccountWithOverdraft account, ConsolePrinter consolePrinter)
        {
            Console.WriteLine();
            Console.WriteLine();

            account.PrintLastTransaction(consolePrinter);
        }

        private static void MakeWithdrawal(AccountWithOverdraft account, ConsolePrinter consolePrinter)
        {
            Console.WriteLine("\n\nEnter an amount to withdraw in pounds:");
            var amountToWithdraw = Console.ReadLine();

            account.Withdraw(new ATMDebitEntry(DateTime.Now, new Money(decimal.Parse(amountToWithdraw))));

            Console.WriteLine();
            account.PrintBalance(consolePrinter);
        }

        private static void MakeDeposit(AccountWithOverdraft account, ConsolePrinter consolePrinter)
        {
            Console.WriteLine("\n\nEnter an amount to deposit in pounds:");
            var amountToDeposit = Console.ReadLine();

            account.Deposit(DateTime.Now, new Money(decimal.Parse(amountToDeposit)));

            Console.WriteLine();
            account.PrintBalance(consolePrinter);
        }

        private static void PrintMenu()
        {
            Console.WriteLine(@"
Press a key to choose an option:

    1. Cash deposit
    2. Cash withdrawal
    3. Print last transaction
            ");
        }

        private static AccountWithOverdraft SetupAccount()
        {
            var account = new Account();
            account.Deposit(DateTime.Now, new Money(1000m));

            var accountWithOverdraft = new AccountWithOverdraft(account, new ArrangedOverdraft(new Money(-1500m), new Money(10m)));
            return accountWithOverdraft;
        }
    }
}
