using ECommerce.Sections;

namespace ECommerce
{
    public abstract class EShopPage
    {
        protected readonly Driver _driver;
        protected WebDriverSetUp _webDriverSetUp;

        protected EShopPage(Driver driver)
        {
            _driver = driver;
            MegaMenuSection = new MegaMenuSection(driver);
            MainNavigationSections = new MainNavigationSections(driver);
            MainHeaderSection = new MainHeaderSection(driver);
            MyAccountDropDownSection = new MyAccountDropDownSection(driver);
            BreadcrumbSection = new BreadcrumbSection(driver);
            _webDriverSetUp = new WebDriverSetUp();
        }

        public MegaMenuSection MegaMenuSection { get; }
        public MainNavigationSections MainNavigationSections { get; }
        public MainHeaderSection MainHeaderSection { get; }
        public MyAccountDropDownSection MyAccountDropDownSection { get; }
        public BreadcrumbSection BreadcrumbSection { get; }
    }
}