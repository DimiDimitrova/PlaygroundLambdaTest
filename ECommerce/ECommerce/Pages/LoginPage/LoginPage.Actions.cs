using ECommerce.Sections;

namespace ECommerce.Pages
{
    public partial class LoginPage : EShopPage
    {
        public LoginPage(Driver driver)
            :base(driver)
        {
        }

        protected string Url => "https://ecommerce-playground.lambdatest.io";

        public void LogIn(string email, string password)
        {
            _driver.GoToUrl(Url);
            MainNavigationSections.MoveToMainMenu(MainMenu.MyAccount);
            _driver.WaitUntilPageLoadsCompletely();
            MyAccountDropDownSection.MyAccountMenu(MyAccountDropDown.Login).Click();
            EmailInput.SendKeys(email);
            PasswordInput.SendKeys(password);
            LoginButton.Click();
            _driver.WaitForAjax();
        }
    }
}