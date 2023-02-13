using ECommerce.Sections;
using NUnit.Framework;

namespace ECommerce
{
    [TestFixture]
    public class AccountTests
    {
        private static LoginPage _loginPage;
        private static AccountPage _accountPage;
        private static AccountOrderPage _accountOrderPage;
        private static AccountVoucherPage _accountVoucherPage;
        private static DriverFacade _driver;
        private MainNavigationSections _mainNavigationSections;
        private MainHeaderSection _mainHeaderSection;
        private CartPage _cartPage;
        private CheckoutPage _checkoutPage;
        private ConfirmPage _confirmPage;
        private ChangePasswordPage _changePasswordPage;
        private static SuccessPage _successPage;
        private static Generator _generator;
        private static EditAccountPage _editAccountPage;
        private static RegisterPage _registerPage;

        [SetUp]
        public void TestInit()
        {
            _driver = new BasePage(new WebDriverSetUp());
            _driver.Start(Browser.Chrome);
            _loginPage = new LoginPage(_driver);
            _accountPage = new AccountPage(_driver);
            _accountOrderPage = new AccountOrderPage(_driver);
            _accountVoucherPage = new AccountVoucherPage(_driver);
            _successPage = new SuccessPage(_driver);
            _cartPage = new CartPage(_driver);
            _checkoutPage = new CheckoutPage(_driver);
            _confirmPage = new ConfirmPage(_driver);
            _editAccountPage = new EditAccountPage(_driver);
            _registerPage = new RegisterPage(_driver);
            _changePasswordPage = new ChangePasswordPage(_driver);
            _generator = new Generator();
            _mainNavigationSections = new MainNavigationSections(_driver);
            _mainHeaderSection = new MainHeaderSection(_driver);
        }

        [Test]
        [Description("As a registered user, I should be able to view orders history")]
        public void ViewOrderHistorySuccessfully()
        {
            _loginPage.LogIn(AccountInfo.Email, AccountInfo.Password);

            _accountPage.OpenMenuFromNavbar(Navbar.OrderHistory);

            _accountOrderPage.AccountOrderHistoryIsDisplayed();
        }

        [Test]
        [Description("Users should be able to logout successfully")]
        public void LogoutSuccessfully()
        {
            _loginPage.LogIn(AccountInfo.Email, AccountInfo.Password);

            _accountPage.OpenMenuFromNavbar(Navbar.Logout);

            _accountPage.AssertThatUserIsNotLogged();
        }

        [TestCase(GiftSertificateTheme.General)]
        [TestCase(GiftSertificateTheme.Birthday)]
        [TestCase(GiftSertificateTheme.Christmas)]
        [Description("Users should be able to add voucher to the cart")]
        public void AddVoucherSuccessfully(GiftSertificateTheme gift)
        {
            var recipient = _generator.CreateRecipient();
            double amount = 1;
            _loginPage.LogIn(AccountInfo.Email, AccountInfo.Password);
            _mainNavigationSections.MoveToMainMenu(MainMenu.MyAccount);
            _mainNavigationSections.OpenMenu(MainMenu.Voucher);

            _accountVoucherPage.FillPurchaseGiftData(recipient.RecipientName, recipient.RecipientEmail, gift, amount);
            _accountVoucherPage.ContinueButton.Click();

            _successPage.AssertVoucherIsPurchased();

            _mainHeaderSection.MainHeaderNavigation(MainHeader.cart).Click();
            _cartPage.CheckoutButtonInCart.Click();
            _checkoutPage.CheckConditions(Account.GuestAccount);
            _checkoutPage.ContinueButton.Click();
            _confirmPage.ConfirmOrderButton.Click();

            _successPage.AsserThatPurchaseIsMade();
        }

        [TestCase(0)]
        [Description("Users should not be able to add voucher to the cart when recipient's name is less than min size")]
        public void AddVoucherFailed_When_RecipientNameIsLess_Than_MinSize(int nameSize)
        {
            var recipient = _generator.CreateRecipientWithSpecificName(nameSize);
            double amount = 10;

            _loginPage.LogIn(AccountInfo.Email, AccountInfo.Password);
            _mainNavigationSections.MoveToMainMenu(MainMenu.MyAccount);
            _mainNavigationSections.OpenMenu(MainMenu.Voucher);

            _accountVoucherPage.FillPurchaseGiftData(recipient.RecipientName, recipient.RecipientEmail, GiftSertificateTheme.Birthday, amount);
            _accountVoucherPage.ContinueButton.Click();

            _accountVoucherPage.AssertPurchaseVoucherFailedWithIncorrectRecipientName();
        }

        [TestCase(65)]
        [TestCase(66)]
        [Description("Users should not be able to add voucher to the cart when recipient's name is more than max size")]
        public void AddVoucherFailed_When_RecipientNameIsMore_Than_MaxSize(int nameSize)
        {
            var recipient = _generator.CreateRecipientWithSpecificName(nameSize);
            double amount = 10;

            _loginPage.LogIn(AccountInfo.Email, AccountInfo.Password);
            _mainNavigationSections.MoveToMainMenu(MainMenu.MyAccount);
            _mainNavigationSections.OpenMenu(MainMenu.Voucher);

            _accountVoucherPage.FillPurchaseGiftData(recipient.RecipientName, recipient.RecipientEmail, GiftSertificateTheme.Birthday, amount);
            _accountVoucherPage.ContinueButton.Click();

            _accountVoucherPage.AssertPurchaseVoucherFailedWithIncorrectRecipientName(recipient.RecipientName);
        }

        [TestCase(6, "@")]
        [TestCase(0, "")]
        [Description("Users should not be able to add voucher to the cart when recipient's email is incorrect")]
        public void AddVoucherFailed_When_RecipientEmailIsIncorrect(int emailSize, string endEmail)
        {
            var recipient = _generator.CreateRecipientWithSpecificEmail(emailSize, endEmail);
            double amount = 10;

            _loginPage.LogIn(AccountInfo.Email, AccountInfo.Password);
            _mainNavigationSections.MoveToMainMenu(MainMenu.MyAccount);
            _mainNavigationSections.OpenMenu(MainMenu.Voucher);

            _accountVoucherPage.FillPurchaseGiftData(recipient.RecipientName, recipient.RecipientEmail, GiftSertificateTheme.Birthday, amount);
            _accountVoucherPage.ContinueButton.Click();

            _accountVoucherPage.AssertPurchaseVoucherFailedWithInvalidEmail(recipient.RecipientEmail);
        }

        [Test]
        [Description("As a registered user, I should be able to edit information for my account")]
        public void EditAccountInformationSuccessfully()
        {
            _loginPage.LogIn(AccountInfo.Email, AccountInfo.Password);

            _accountPage.OpenMenuFromNavbar(Navbar.EditAccount);

            _editAccountPage.FillAllAccountInformation(_generator.CreatePersonWithRegisteredCredentials());
            _editAccountPage.ContinueButon.Click();

            _successPage.AssertAccountIsUpdated();
        }

        [Test]
        [Description("As a registered user, I should be able to change password for my account")]
        public void ChangePasswordSuccessfully()
        {
            var person = _generator.CreatePersonInfo();
            _registerPage.DoRegistration(person.Email, person.Password, person);
            _accountPage.OpenMenuFromNavbar(Navbar.Logout);

            _loginPage.LogIn(person.Email, person.Password);
            _accountPage.OpenMenuFromNavbar(Navbar.Password);
            string newPassword = _generator.CreateTextWithSpecificLength(18);
            _changePasswordPage.ChangePassword(newPassword);
            _changePasswordPage.ContinueButton.Click();

            _accountPage.OpenMenuFromNavbar(Navbar.Logout);
            _loginPage.LogIn(person.Email, newPassword);

            _accountPage.AssertThatUserIsLogged();
        }

        [TestCase(3)]
        [TestCase(2)]
        [Description("As a registered user, I should not be able to change password when it has less symbols than min size")]
        public void ChangePasswordFailed_When_NewPasswordLengthHasLessSymbols_Than_MinSize(int passwordLength)
        {
            var person = _generator.CreatePersonInfo();
            _registerPage.DoRegistration(person.Email, person.Password, person);
            _accountPage.OpenMenuFromNavbar(Navbar.Logout);

            _loginPage.LogIn(person.Email, person.Password);
            _accountPage.OpenMenuFromNavbar(Navbar.Password);
            string newPassword = _generator.CreateTextWithSpecificLength(passwordLength);
            _changePasswordPage.ChangePassword(newPassword);
            _changePasswordPage.ContinueButton.Click();

            _changePasswordPage.AssertChangePasswordFailedWithIncorrectPassword();
        }

        [TestCase(22)]
        [TestCase(21)]
        [Description("As a registered user, I should not be able to change password when it has more symbols than max size")]
        public void ChangePasswordFailed_When_NewPasswordLengthHasMoreSymbols_Than_MaxSize(int passwordLength)
        {
            var person = _generator.CreatePersonInfo();
            _registerPage.DoRegistration(person.Email, person.Password, person);
            _accountPage.OpenMenuFromNavbar(Navbar.Logout);

            _loginPage.LogIn(person.Email, person.Password);
            _accountPage.OpenMenuFromNavbar(Navbar.Password);
            string newPassword = _generator.CreateTextWithSpecificLength(passwordLength);
            _changePasswordPage.ChangePassword(newPassword);
            _changePasswordPage.ContinueButton.Click();

            _changePasswordPage.AssertChangePasswordFailedWithIncorrectPassword();
        }

        [TearDown]
        public void Quit()
        {
            _driver.Quit();
        }
    }
}