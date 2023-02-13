
namespace ECommerce.Pages
{
    public partial class SuccessPage
    {
        public IWebElement ContinueButton => _driver.FindElement(By.XPath("//div[@id='content']//a[contains(@href,'common/home')]"));
        public IWebElement AlertMessage => _driver.FindElement(By.XPath("//div[contains(@class,'alert-dismissible')]"));
    }
}