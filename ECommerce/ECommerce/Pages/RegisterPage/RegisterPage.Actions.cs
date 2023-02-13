
namespace ECommerce.Pages
{
    public partial class RegisterPage : EShopPage
    {
        private AccountPage _accountPage;
        public RegisterPage(Driver driver)
            : base(driver)
        {
            _accountPage = new AccountPage(driver);
        }

        protected string Url => "https://ecommerce-playground.lambdatest.io";

        public string RegisterUrl => "https://ecommerce-playground.lambdatest.io/index.php?route=account/register";

        public void FillRegistrationForm(string email, string confirmPassword, PersonInfo personInfo)
        {
            FirstNameInput.SendKeys(personInfo.FirstName);
            LastNameInput.SendKeys(personInfo.LastName);
            EmailInput.SendKeys(email);
            TelephoneInput.SendKeys(personInfo.Telephone);
            PasswordInput.SendKeys(personInfo.Password);
            ConfirmPassword.SendKeys(confirmPassword);
        }

        public void FillRegistrationForm(PersonInfo personInfo)
        {
            FirstNameInput.SendKeys(personInfo.FirstName);
            LastNameInput.SendKeys(personInfo.LastName);
            EmailInput.SendKeys(personInfo.Email);
            TelephoneInput.SendKeys(personInfo.Telephone);
            PasswordInput.SendKeys(personInfo.Password);
            ConfirmPassword.SendKeys(personInfo.Password);
        }

        public void DoRegistration(string email, string confirmPassword, PersonInfo person)
        {
            _driver.GoToUrl(Url);
            MainNavigationSections.MoveToMainMenu(MainMenu.MyAccount);
            _driver.WaitUntilPageLoadsCompletely();
            MyAccountDropDownSection.MyAccountMenu(MyAccountDropDown.Register).Click();
            _driver.WaitForAjax();
            FillRegistrationForm(email, confirmPassword, person);
            AgreeCheckbox.Click();
            ContinueButon.Click();
            _driver.WaitForAjax();
        }
    }
}