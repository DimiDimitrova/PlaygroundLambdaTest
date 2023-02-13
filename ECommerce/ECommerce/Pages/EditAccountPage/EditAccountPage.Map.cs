
namespace ECommerce.Pages
{
    public partial class EditAccountPage
    {
        public IWebElement FirstNameInput => _driver.FindElement(By.Id("input-firstname"));
        public IWebElement LastNameInput => _driver.FindElement(By.Id("input-lastname"));
        public IWebElement EmailInput => _driver.FindElement(By.Id("input-email"));
        public IWebElement TelephoneInput => _driver.FindElement(By.Id("input-telephone"));
        public IWebElement ContinueButon => _driver.FindElement(By.XPath("//input[@value='Continue']"));
    }
}