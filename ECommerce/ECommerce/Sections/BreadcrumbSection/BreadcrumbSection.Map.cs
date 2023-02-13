
namespace ECommerce.Sections
{
    public partial class BreadcrumbSection
    {
        private readonly Driver _driver;

        public BreadcrumbSection(Driver driver)
        {
            _driver = driver;
        }

        public IWebElement PageTitle => _driver.FindElement(By.XPath("//*[contains(@class,'page-title')]"));
        public IWebElement ActivePageTitle => _driver.FindElement(By.XPath("//li[@aria-current='page']"));
    }
}