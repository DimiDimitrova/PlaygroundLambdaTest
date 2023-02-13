
namespace ECommerce
{
    public partial class AccountVoucherPage : EShopPage
    {
        public AccountVoucherPage(Driver driver)
            : base(driver)
        {
        }

        public void FillPurchaseGiftData(string recipientName, string recipientMail, GiftSertificateTheme theme, double amount)
        {
            RecipientName.SendKeys(recipientName);
            RecipientMail.SendKeys(recipientMail);
            GiftSertificateTheme(theme.ToString()).Click();
            Amount.Clear();
            Amount.SendKeys(amount.ToString());
            AgreeCheckbox.Click();
        }
    }
}