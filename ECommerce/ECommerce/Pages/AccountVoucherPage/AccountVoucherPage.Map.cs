
namespace ECommerce
{
    public partial class AccountVoucherPage
    {
        public IWebElement RecipientName => _driver.FindElement(By.Id("input-to-name"));
        public IWebElement RecipientMail => _driver.FindElement(By.Id("input-to-email"));      
        public IWebElement AgreeCheckbox => _driver.FindElement(By.XPath("//div[@class='float-right']/input[@type='checkbox']"));
        public IWebElement ContinueButton => _driver.FindElement(By.XPath("//input[contains(@class,'btn-primary')]"));
        public IWebElement Amount => _driver.FindElement(By.Id("input-amount"));
        public IWebElement RecipientNameError => _driver.FindElement(RelativeBy.WithLocator(By.TagName("div")).Below(RecipientName));
        public IWebElement ErrorMessage => _driver.FindElement(By.XPath("//*[@class='text-danger']"));

        public IWebElement GiftSertificateTheme(string theme)
        {
            return _driver.FindElement(By.XPath($"//label[contains(text(),'{theme}')]/input"));
        }
    }
}