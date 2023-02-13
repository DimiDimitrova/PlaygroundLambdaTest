
namespace ECommerce.Sections
{
    public partial class MainNavigationSections
    {
        public IWebElement SelectMenu(MainMenu menu)
        {
            return _driver.FindElement(By.XPath($"//ul[@class='navbar-nav horizontal']//a[contains(@href,'{menu.GetEnumDescription()}')]"));
        }
    }
}