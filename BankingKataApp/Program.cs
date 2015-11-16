using System;
using System.Collections.Generic;
using BankingKata;

namespace BankingKataApp
{
    class Program
    {
        private static readonly Dictionary<char, Action> MenuOptions = new Dictionary<char, Action>
        {
            { '1', MakeDeposit },
            { '2', MakeWithdrawal },
            { '3', PrintLastTransaction }
        };

        private static AccountWithOverdraft m_Account;
        private static readonly ConsolePrinter m_Printer = new ConsolePrinter();

        static void Main(string[] args)
        {
            SetupAccount();

            Console.WriteLine("Welcome to your account. \n");
            m_Account.PrintBalance(m_Printer);

            while (true)
            {
                PrintMenu();

                var userOption = GetUserOption();

                MenuOptions[userOption.KeyChar].Invoke();
            }
        }

        private static ConsoleKeyInfo GetUserOption()
        {
            var userOption = Console.ReadKey();
            return userOption;
        }

        private static void PrintLastTransaction()
        {
            Console.WriteLine();
            Console.WriteLine();

            m_Account.PrintLastTransaction(m_Printer);
        }

        private static void MakeWithdrawal()
        {
            Console.WriteLine("\n\nEnter an amount to withdraw in pounds:");
            var amountToWithdraw = Console.ReadLine();

            m_Account.Withdraw(new ATMDebitEntry(DateTime.Now, new Money(decimal.Parse(amountToWithdraw))));

            Console.WriteLine();
            m_Account.PrintBalance(m_Printer);
        }

        private static void MakeDeposit()
        {
            Console.WriteLine("\n\nEnter an amount to deposit in pounds:");
            var amountToDeposit = Console.ReadLine();

            m_Account.Deposit(DateTime.Now, new Money(decimal.Parse(amountToDeposit)));

            Console.WriteLine();
            m_Account.PrintBalance(m_Printer);
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
