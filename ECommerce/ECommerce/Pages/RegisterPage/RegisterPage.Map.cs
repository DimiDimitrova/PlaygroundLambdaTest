
namespace ECommerce.Pages
{
    public partial class RegisterPage
    {
        public IWebElement FirstNameInput => _driver.FindElement(By.Id("input-firstname"));
        public IWebElement LastNameInput => _driver.FindElement(By.Id("input-lastname"));
        public IWebElement EmailInput => _driver.FindElement(By.Id("input-email"));
        public IWebElement TelephoneInput => _driver.FindElement(By.Id("input-telephone"));
        public IWebElement PasswordInput => _driver.FindElement(By.Id("input-password"));
        public IWebElement ConfirmPassword => _driver.FindElement(By.Id("input-confirm"));
        public IWebElement AgreeCheckbox => _driver.FindElement(By.XPath("//*[@id='input-agree']//parent::div"));
        public IWebElement ContinueButon => _driver.FindElement(By.XPath("//input[@value='Continue']"));   
        public IWebElement AgreeWithPrivacyMessage => _driver.FindElement(By.XPath("//div[@id='account-register']//div[contains(text(), 'Warning: You must agree to the Privacy Policy!')]"));       
        public IWebElement EmailWarningMessage => _driver.FindElement(By.XPath("//*[contains(@class,'fa-exclamation-circle')]"));
    }
}