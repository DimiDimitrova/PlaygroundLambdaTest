
namespace ECommerce.Pages
{
    public partial class LoginPage
    {
        public void AssertThatInvalidCredentialsMessageIsDisplayed(string email)
        {
            Assert.IsTrue(BreadcrumbSection.ActivePageTitle.Text.Equals("Login"),
                String.Format(Utils.LOGIN_MESSAGE, email));

            Assert.IsTrue(LoginWarningMessage.Text.Equals(Utils.INVALID_CREDENTIALS_MESSAGE),
                String.Format(Utils.NOT_EQUAL_ERROR, LoginWarningMessage.Text, Utils.INVALID_CREDENTIALS_MESSAGE));
        }

        public void AssertThatMessageForExceededAllowedNumberOfLoginIsDisplayed()
        {
            Assert.IsTrue(BreadcrumbSection.ActivePageTitle.Text.Equals("Login"),
                Utils.LOGGED_MESSAGE);

            Assert.IsTrue(LoginWarningMessage.Text.Equals(Utils.ALLOWED_NUMBER_OF_LOGIN_ERROR),
                String.Format(Utils.NOT_EQUAL_ERROR, LoginWarningMessage.Text, Utils.ALLOWED_NUMBER_OF_LOGIN_ERROR));
        }
    }
}