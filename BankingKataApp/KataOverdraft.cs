using BankingKata;

namespace BankingKataApp
{
    public class KataOverdraft : IOverdraft
    {
        public void Apply(IAccount account, DebitEntry withdrawal)
        {
            var charge = new Money(10);
            var unarrangeOverdraftLimit = new Money(-4000);
            var overdraftLimit = new Money(-1500);

            var balanceAfterWithdrawal = withdrawal.ApplyTo(account.CalculateBalance());

            if (balanceAfterWithdrawal < unarrangeOverdraftLimit)
            {
                account.Withdraw(withdrawal.CreateOverdraftCharge(charge));
                throw new OverdraftLimitReachedException();
            }

            new ArrangedOverdraft(overdraftLimit, charge).Apply(account, withdrawal);
        }
    }    
}
