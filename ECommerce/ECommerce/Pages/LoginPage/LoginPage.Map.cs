
namespace ECommerce.Pages
{
    public partial class LoginPage
    {
        public IWebElement LoginWarningMessage => _driver.FindElement(By.XPath("//div[contains(@class,'alert-dismissible')]"));
        public IWebElement EmailInput => _driver.FindElement(By.Id("input-email"));
        public IWebElement PasswordInput => _driver.FindElement(By.Id("input-password"));
        public IWebElement LoginButton => _driver.FindElement(By.XPath("//div//input[@value='Login']"));
    }
}