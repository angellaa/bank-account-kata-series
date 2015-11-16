using System;
using BankingKata;
using NUnit.Framework;
using BankingKataApp;

namespace BankingKataAppTests
{
    public class KataOverdraftTests
    {
        [TestCase(1000, 1500, -500)]
        [TestCase(1000, 2700, -1710)]
        [TestCase(1000, 5000, -4010)]
        public void WithdrawalAcceptedTests(int initialBalance, int withdrawalAmount, int expectedBalance)
        {
            var kataOverdraft = new KataOverdraft();
            var accountWithOverdraft = new AccountWithOverdraft(new Account(), kataOverdraft);
            accountWithOverdraft.Deposit(DateTime.Now, new Money(initialBalance));

            var debitEntry = new ATMDebitEntry(DateTime.Now, new Money(withdrawalAmount));
            accountWithOverdraft.Withdraw(debitEntry);

            var balance = accountWithOverdraft.CalculateBalance();

            Assert.That(balance, Is.EqualTo(new Money(expectedBalance)));
        }

        [TestCase(1000, 5001, 990)]
        [TestCase(-4010, 1000, -4020)]
        [TestCase(-3999, 5, -4009)]
        public void WithdrawalDeniedTests(int initialBalance, int withdrawalAmount, int expectedBalance)
        {
            var kataOverdraft = new KataOverdraft();
            var accountWithOverdraft = new AccountWithOverdraft(new Account(), kataOverdraft);
            accountWithOverdraft.Deposit(DateTime.Now, new Money(initialBalance));

            var debitEntry = new ATMDebitEntry(DateTime.Now, new Money(withdrawalAmount));

            Assert.Throws<OverdraftLimitReachedException>(() => accountWithOverdraft.Withdraw(debitEntry));

            var balance = accountWithOverdraft.CalculateBalance();

            Assert.That(balance, Is.EqualTo(new Money(expectedBalance)));
        }
    }
}
