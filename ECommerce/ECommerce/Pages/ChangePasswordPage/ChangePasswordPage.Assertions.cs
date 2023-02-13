
namespace ECommerce.Pages
{
    public partial class ChangePasswordPage
    {
        private const int PASSWORD_MIN_SIZE = 4;
        private const int PASSWORD_MAX_SIZE = 20;

        public void AssertChangePasswordFailedWithIncorrectPassword()
        {
            Assert.IsTrue(BreadcrumbSection.ActivePageTitle.Text.Equals("Change Password"),
               String.Format(Utils.UPDATED_ACCOUNT_MESSAGE));

            Assert.IsTrue(_accountPage.WarningMessage.Text.Equals(String.Format(Utils.PASSWORD_ERROR, PASSWORD_MIN_SIZE, PASSWORD_MAX_SIZE)),
                String.Format(Utils.NOT_EQUAL_ERROR, _accountPage.WarningMessage.Text,
                String.Format(Utils.PASSWORD_ERROR, PASSWORD_MIN_SIZE, PASSWORD_MAX_SIZE)));
        }
    }
}