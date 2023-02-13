
namespace ECommerce.Pages
{
    public partial class MegaMenuSection
    {
        public IWebElement SearchCategoryHeader => _driver.FindElement(By.XPath("//*[contains(@class,'content-title')]/h1"));
    }
}