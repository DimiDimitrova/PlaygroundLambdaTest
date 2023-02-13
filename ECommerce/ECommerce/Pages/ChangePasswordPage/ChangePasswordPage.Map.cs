
namespace ECommerce.Pages
{
    public partial class ChangePasswordPage
    {
        public IWebElement PasswordInput => _driver.FindElement(By.Id("input-password"));
        public IWebElement ConfirmPasswordInput => _driver.FindElement(By.Id("input-confirm"));
        public IWebElement ContinueButton => _driver.FindElement(By.XPath("//*[@class='buttons clearfix']//input"));
    }
}