
namespace ECommerce.Pages
{
    public partial class ChangePasswordPage : EShopPage
    {
        private AccountPage _accountPage;
        public ChangePasswordPage(Driver driver)
            : base(driver)
        {
            _accountPage = new AccountPage(driver);
        }

        public void ChangePassword(string newPassword)
        {
            PasswordInput.SendKeys(newPassword);
            ConfirmPasswordInput.SendKeys(newPassword);
        }
    }
}