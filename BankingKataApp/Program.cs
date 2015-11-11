using BankingKata;

namespace BankingKataApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var account = new Account();

            account.PrintBalance(new ConsolePrinter());
        }
    }
}
