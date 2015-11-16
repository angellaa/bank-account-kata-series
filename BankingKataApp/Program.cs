using System;
using System.Collections.Generic;
using BankingKata;

namespace BankingKataApp
{
    class Program
    {
        private static readonly Dictionary<char, Action<ConsolePrinter>> MenuOptions = new Dictionary<char, Action<ConsolePrinter>>
        {
            { '1', MakeDeposit },
            { '2', MakeWithdrawal },
            { '3', PrintLastTransaction }
        };

        private static AccountWithOverdraft m_Account;

        static void Main(string[] args)
        {
            SetupAccount();
            var consolePrinter = new ConsolePrinter();

            Console.WriteLine("Welcome to your account. \n");
            m_Account.PrintBalance(consolePrinter);

            while (true)
            {
                PrintMenu();

                var userOption = GetUserOption();

                MenuOptions[userOption.KeyChar].Invoke(consolePrinter);
            }
        }

        private static ConsoleKeyInfo GetUserOption()
        {
            var userOption = Console.ReadKey();
            return userOption;
        }

        private static void PrintLastTransaction(ConsolePrinter consolePrinter)
        {
            Console.WriteLine();
            Console.WriteLine();

            m_Account.PrintLastTransaction(consolePrinter);
        }

        private static void MakeWithdrawal(ConsolePrinter consolePrinter)
        {
            Console.WriteLine("\n\nEnter an amount to withdraw in pounds:");
            var amountToWithdraw = Console.ReadLine();

            m_Account.Withdraw(new ATMDebitEntry(DateTime.Now, new Money(decimal.Parse(amountToWithdraw))));

            Console.WriteLine();
            m_Account.PrintBalance(consolePrinter);
        }

        private static void MakeDeposit(ConsolePrinter consolePrinter)
        {
            Console.WriteLine("\n\nEnter an amount to deposit in pounds:");
            var amountToDeposit = Console.ReadLine();

            m_Account.Deposit(DateTime.Now, new Money(decimal.Parse(amountToDeposit)));

            Console.WriteLine();
            m_Account.PrintBalance(consolePrinter);
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

        private static void SetupAccount()
        {
            var account = new Account();
            account.Deposit(DateTime.Now, new Money(1000m));

            m_Account = new AccountWithOverdraft(account, new ArrangedOverdraft(new Money(-1500m), new Money(10m)));
        }
    }
}
