using ECommerce.Sections;

namespace ECommerce
{
    // Make functional web tests using WebDriver. 
    // Imagine that your organization is creating an eCommerce project and your task is to create
    // automated tests.https://ecommerce-playground.lambdatest.io.
    // a.Create enough tests that in your opinion will assert the main business logic. 
    // b.Organize the test logic in page objects.
    // c.Pass the verification info through a DTO class.
    // d.Pick and use the best way for organizing your tests.
    // e.Try to document your tests so that the product owner will understand what have you done.
    // f.Try to describe what you leave for the next phase as a second priority.
    [TestFixture]
    public class LoginTests
    {
        private const int ALLOWED_NUMBER_OF_LOGIN_ATTEMPTS = 5;
        private static LoginPage _loginPage;
        private static AccountPage _accountPage;
        private static HomePage _homePage;
        private static DriverFacade _driver;
        private static Generator _generator;
        private MainNavigationSections _mainNavigationSections;

        [SetUp]
        public void TestInit()
        {
            _driver = new BasePage(new WebDriverSetUp());
            _driver.Start(Browser.Chrome);
            _loginPage = new LoginPage(_driver);
            _accountPage = new AccountPage(_driver);
            _homePage = new HomePage(_driver);
            _generator = new Generator();
            _mainNavigationSections = new MainNavigationSections(_driver);
        }

        [Test]
        [Description("Users should be able to login successfully with valid credentials")]
        public void LogInSuccessfully()
        {
            _loginPage.LogIn(AccountInfo.Email, AccountInfo.Password);

            _accountPage.AssertThatUserIsLogged();
        }

        [Test]
        [Description("Users should not be able to login with wrong credentials")]
        public void LogInFailedWithWrongEmailCredential()
        {
            var data = _generator.CreateText();

            _loginPage.LogIn(data, AccountInfo.Password);

            _loginPage.AssertThatInvalidCredentialsMessageIsDisplayed(data);
        }

        [Test]
        [Description("Users should not be able to login without credentials")]
        public void LogInFailedWithEmptyCredentials()
        {
            _driver.GoToUrl(_homePage.Url);
            _mainNavigationSections.SelectMenu(MainMenu.MyAccount).Click();

            _loginPage.LoginButton.Click();

            _loginPage.AssertThatInvalidCredentialsMessageIsDisplayed(string.Empty);
            _accountPage.AssertThatUserIsNotLogged();
        }

        [Test]
        [Description("Users should not be able to login when does not have account")]
        public void LogInFailedWithoutRegisteredAccount()
        {
            var data = _generator.CreateText();

            _loginPage.LogIn(data, data);

            _loginPage.AssertThatInvalidCredentialsMessageIsDisplayed(data);
        }

        [Test]
        [Description("User should not be able to login before one hour after exceeded allowed number of login attempts")]
        public void LogInFailed_When_TryToEnterCredentialsAfterAllowedNumberOfLoginAttemptBeforeOneHour()
        {
            for (int i = 0; i < ALLOWED_NUMBER_OF_LOGIN_ATTEMPTS; i++)
            {
                _loginPage.LogIn(AccountInfo.Email, _generator.CreateText());
                _loginPage.AssertThatInvalidCredentialsMessageIsDisplayed(AccountInfo.Email);
            }

            _loginPage.LogIn(AccountInfo.Email, _generator.CreateText());
            _loginPage.AssertThatMessageForExceededAllowedNumberOfLoginIsDisplayed();

            _loginPage.LogIn(AccountInfo.Email, AccountInfo.Password);
            _loginPage.AssertThatMessageForExceededAllowedNumberOfLoginIsDisplayed();           
        }

        [TearDown]
        public void Quit()
        {
            _driver.Quit();
        }
    }
}