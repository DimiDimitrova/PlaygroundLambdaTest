
namespace ECommerce.Pages
{
    public partial class RegisterPage
    {
        private const int PASSWORD_MIN_SIZE = 4;
        private const int PASSWORD_MAX_SIZE = 20;
        private const int FIRST_NAME_MIN_SIZE = 1;
        private const int FIRST_NAME_MAX_SIZE = 32;
        private const int LAST_NAME_MIN_SIZE = 1;
        private const int LAST_NAME_MAX_SIZE = 32;
        private const int TELEPHONE_MIN_SIZE = 3;
        private const int TELEPHONE_MAX_SIZE = 32;

        public void AssertThatRegistrationFailedWithExistEmail(string email)
        {
            Assert.IsTrue(BreadcrumbSection.ActivePageTitle.Text.Equals(PageTitle.Register.ToString()),
                String.Format(Utils.ACCOUNT_CREATED_WITH_EXISTS_EMAIL, email));

            Assert.That(EmailWarningMessage.Displayed);
        }

        public void AssertThatRegistrationFailedWithoutData()
        {
            Assert.IsTrue(BreadcrumbSection.ActivePageTitle.Text.Equals(PageTitle.Register.ToString()),
                String.Format(Utils.NOT_EQUAL_ERROR, BreadcrumbSection.ActivePageTitle.Text, PageTitle.Register.ToString()));

            Assert.That(AgreeWithPrivacyMessage.Displayed);
        }

        public void AssertRegistrationFailedWithIncorrectPassword(string password)
        {
            Assert.IsTrue(BreadcrumbSection.ActivePageTitle.Text.Equals(PageTitle.Register.ToString()),
                String.Format(Utils.ACCOUNT_HAS_BEEN_CREATED, password.Length) + " " +
                String.Format(Utils.PASSWORD_ERROR, PASSWORD_MIN_SIZE, PASSWORD_MAX_SIZE));

            Assert.IsTrue(_accountPage.WarningMessage.Text.Equals(String.Format(Utils.PASSWORD_ERROR, PASSWORD_MIN_SIZE, PASSWORD_MAX_SIZE)),
                String.Format(Utils.NOT_EQUAL_ERROR, _accountPage.WarningMessage.Text,
                String.Format(Utils.PASSWORD_ERROR, PASSWORD_MIN_SIZE, PASSWORD_MAX_SIZE)));
        }

        public void AssertRegistrationFailedWithIncorrectFirstName(string firstName)
        {
            Assert.IsTrue(BreadcrumbSection.ActivePageTitle.Text.Equals("Register"),
                String.Format(Utils.ACCOUNT_HAS_BEEN_CREATED, firstName.Length) + " " +
                String.Format(Utils.FIRST_NAME_ERROR, FIRST_NAME_MIN_SIZE, FIRST_NAME_MAX_SIZE));

            Assert.IsTrue(_accountPage.WarningMessage.Text.Equals(String.Format(Utils.FIRST_NAME_ERROR, FIRST_NAME_MIN_SIZE, FIRST_NAME_MAX_SIZE)),
                String.Format(Utils.NOT_EQUAL_ERROR, _accountPage.WarningMessage.Text, String.Format(Utils.FIRST_NAME_ERROR, FIRST_NAME_MIN_SIZE, FIRST_NAME_MAX_SIZE)));
        }

        public void AssertRegistrationFailedWithIncorrectLastName(string lastName)
        {
            Assert.IsTrue(BreadcrumbSection.ActivePageTitle.Text.Equals("Register"),
                String.Format(Utils.ACCOUNT_HAS_BEEN_CREATED, lastName.Length) + " " +
                String.Format(Utils.LAST_NAME_ERROR, LAST_NAME_MIN_SIZE, LAST_NAME_MAX_SIZE));

            Assert.IsTrue(_accountPage.WarningMessage.Text.Equals(String.Format(Utils.LAST_NAME_ERROR, LAST_NAME_MIN_SIZE, LAST_NAME_MAX_SIZE)),
                String.Format(Utils.NOT_EQUAL_ERROR, _accountPage.WarningMessage.Text, String.Format(Utils.LAST_NAME_ERROR, LAST_NAME_MIN_SIZE, LAST_NAME_MAX_SIZE)));
        }

        public void AssertRegistrationFailedWithIncorrectTelephone(string telephone)
        {
            Assert.IsTrue(BreadcrumbSection.ActivePageTitle.Text.Equals("Register"),
                String.Format(Utils.ACCOUNT_HAS_BEEN_CREATED, telephone.Length) + " " +
                String.Format(Utils.TELEPHONE_ERROR, TELEPHONE_MIN_SIZE, TELEPHONE_MAX_SIZE));

            Assert.IsTrue(_accountPage.WarningMessage.Text.Equals(String.Format(Utils.TELEPHONE_ERROR, TELEPHONE_MIN_SIZE, TELEPHONE_MAX_SIZE)),
                String.Format(Utils.NOT_EQUAL_ERROR, _accountPage.WarningMessage.Text, String.Format(Utils.TELEPHONE_ERROR, TELEPHONE_MIN_SIZE, TELEPHONE_MAX_SIZE)));
        }

        public void AssertRegistrationFailedWithIncorrectPasswordConfirmation()
        {
            Assert.IsTrue(BreadcrumbSection.ActivePageTitle.Text.Equals("Register"),
                String.Format(Utils.NOT_EXISTS_IN_PAGE_ERROR, "Register"));

            Assert.IsTrue(_accountPage.WarningMessage.Text.Equals(Utils.PASSWORD_CONFIRMATION_ERROR), 
                String.Format(Utils.NOT_EQUAL_ERROR, _accountPage.WarningMessage.Text, Utils.PASSWORD_CONFIRMATION_ERROR));
        }

        public void AssertThatRegistrationFailedWithoutCheckedAgree()
        {
            Assert.That(AgreeWithPrivacyMessage.Displayed,Utils.ACCOUNT_HAS_BEEN_CREATED_WITHOUT_AGREE_INFORMATION);

            Assert.IsTrue(BreadcrumbSection.ActivePageTitle.Text.Equals("Register"),
                String.Format(Utils.NOT_EQUAL_ERROR, BreadcrumbSection.ActivePageTitle.Text, "Register"));
        }
    }
}