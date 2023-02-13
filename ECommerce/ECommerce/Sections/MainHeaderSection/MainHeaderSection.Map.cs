
namespace ECommerce.Sections
{
    public partial class MainHeaderSection
    {
        public IWebElement MainHeaderNavigation(MainHeader link)
        {
            return _driver.FindElement(By.XPath($"//div[@id='main-header']//a[contains(@href,'{link}')]"));
        }
    }
}