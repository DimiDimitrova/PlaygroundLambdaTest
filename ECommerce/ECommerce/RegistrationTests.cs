using ECommerce.Sections;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace ECommerce
{
    [TestFixture]
    public class RegistrationTests
    {
        private static RegisterPage _registerPage;
        private static SuccessPage _successPage;
        private static HomePage _homePage;
        private static DriverFacade _driver;
        private Generator _generator;
        private MainNavigationSections _mainNavigationSections;
        private MyAccountDropDownSection _accountDropDownSection;

        [SetUp]
        public void TestInit()
        {
            _driver = new BasePage(new WebDriverSetUp());
            _driver.Start(Browser.Chrome);
            _registerPage = new RegisterPage(_driver);
            _successPage = new SuccessPage(_driver);
            _homePage = new HomePage(_driver);
            _generator = new Generator();
            _mainNavigationSections = new MainNavigationSections(_driver);
            _accountDropDownSection = new MyAccountDropDownSection(_driver);
        }

        [Test]
        [Description("Users should be able to register successfully when filling all required fields")]
        public void MakeRegistrationSuccessfully()
        {
            var person = _generator.CreatePersonInfo();

            _registerPage.DoRegistration(person.Email, person.Password, person);

            _successPage.AssertThatRegistrationIsMade();
        }

        [Test]
        [Description(("Users should not be able to register when using an already registered email"))]
        public void RegistrationFailed_When_UseAlreadyRegisteredEmail()
        {
            var person = _generator.CreatePersonInfo();

            _registerPage.DoRegistration(AccountInfo.Email, person.Password, person);

            _registerPage.AssertThatRegistrationFailedWithExistEmail(AccountInfo.Email);
        }

        [Test]
        [Description("Users should not be able to register when all required fields are empty")]
        public void RegistrationFailed_When_AllFieldsAreEmpty()
        {
            _driver.GoToUrl(_homePage.Url);
            _mainNavigationSections.MoveToMainMenu(MainMenu.MyAccount);
            _accountDropDownSection.MyAccountMenu(MyAccountDropDown.Register).Click();

            _registerPage.ContinueButon.Click();

            _registerPage.AssertThatRegistrationFailedWithoutData();
        }

        [TestCase(0)]
        [Description("Users should not be able to register when first name has less symbols than min size")]
        public void RegistrationFailed_When_FirstNameFieldIsLessSymbols_Than_MinSize(int firstName)
        {
            var person = _generator.CreatePersonWithSpecificFirstName(firstName);

            _registerPage.DoRegistration(person.Email, person.Password, person);

            _registerPage.AssertRegistrationFailedWithIncorrectFirstName(person.FirstName);
        }

        [TestCase(33)]
        [TestCase(34)]
        [Description("Users should not be able to register when first name has more symbols than max size")]
        public void RegistrationFailed_When_FirstNameFieldIsMoreSymbols_Than_MaxSize(int sizeFirstName)
        {
            var person = _generator.CreatePersonWithSpecificFirstName(sizeFirstName);

            _registerPage.DoRegistration(person.Email, person.Password, person);

            _registerPage.AssertRegistrationFailedWithIncorrectFirstName(person.FirstName);
        }

        [TestCase(33)]
        [TestCase(34)]
        [Description("Users should not be able to register when last name has more symbols than max size")]
        public void RegistrationFailed_When_LastNameFieldIsMoreSymbols_Than_MaxSize(int sizeLastName)
        {
            var person = _generator.CreatePersonWithSpecificLastName(sizeLastName);

            _registerPage.DoRegistration(person.Email, person.Password, person);

            _registerPage.AssertRegistrationFailedWithIncorrectLastName(person.LastName);
        }

        [TestCase(0)]
        [Description("Users should not be able to register when last name has less symbols than min size")]
        public void RegistrationFailed_When_LastNameFieldIsLessSymbols_Than_MinSize(int size)
        {
            var person = _generator.CreatePersonWithSpecificLastName(size);

            _registerPage.DoRegistration(person.Email, person.Password, person);

            _registerPage.AssertRegistrationFailedWithIncorrectLastName(person.LastName);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [Description("Users should not be able to register when telephone has less symbols than min size")]
        public void RegistrationFailed_When_TelephoneFieldIsLessSymbols_Than_MinSize(int telephoneSize)
        {
            var person = _generator.CreatePersonWithSpecificTelephone(telephoneSize);

            _registerPage.DoRegistration(person.Email, person.Password, person);

            _registerPage.AssertRegistrationFailedWithIncorrectTelephone(person.Telephone);
        }

        [TestCase(33)]
        [TestCase(34)]
        [TestCase(35)]
        [Description("Users should not be able to register when telephone has more symbols than max size")]
        public void RegistrationFailed_When_TelephoneFieldIsMoreSymbols_Than_MaxSize(int telephoneSize)
        {
            var person = _generator.CreatePersonWithSpecificTelephone(telephoneSize);

            _registerPage.DoRegistration(person.Email, person.Password, person);

            _registerPage.AssertRegistrationFailedWithIncorrectTelephone(person.Telephone);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [Description("Users should not be able to register when password has less symbols than min size")]
        public void RegistrationFailed_When_PasswordFieldIsLessSymbols_Than_MinSize(int passwordSize)
        {
            var person = _generator.CreatePersonWithSpecificPassword(passwordSize);

            _registerPage.DoRegistration(person.Email, person.Password, person);

            _registerPage.AssertRegistrationFailedWithIncorrectPassword(person.Password);
        }

        [TestCase(21)]
        [TestCase(22)]
        [TestCase(23)]
        [Description("Users should not be able to register when password has more symbols than max size")]
        public void RegistrationFailed_When_PasswordFieldIsMoreSymbols_Than_MaxSize(int passwordSize)
        {
            var person = _generator.CreatePersonWithSpecificPassword(passwordSize);

            _registerPage.DoRegistration(person.Email, person.Password, person);

            _registerPage.AssertRegistrationFailedWithIncorrectPassword(person.Password);
        }

        [TestCase()]
        [Description("Users should not be able to register successfully when confirm password is incorrect")]
        public void RegistrationFailed_When_ConfirmPasswordIsIncorrect()
        {
            var person = _generator.CreatePersonInfo();

            _registerPage.DoRegistration(person.Email, _generator.CreateText(), person);

            _registerPage.AssertRegistrationFailedWithIncorrectPasswordConfirmation();
        }

        [Test]
        public void RegistrationFailed_When_AgreeInformationIsNotChecked()
        {
            var person = _generator.CreatePersonInfo();
            _driver.GoToUrl(_registerPage.RegisterUrl);

            _registerPage.FillRegistrationForm(person);
            _registerPage.ContinueButon.Click();

            _registerPage.AssertThatRegistrationFailedWithoutCheckedAgree();
        }

        [TearDown]
        public void Quit()
        {
            _driver.Quit();
        }
    }
}
