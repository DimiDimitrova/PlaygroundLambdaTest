using ECommerce.Sections;

namespace ECommerce
{
    [TestFixture]
    public class MenuTests
    {
        private static HomePage _homePage;
        private static ProductPage _productPage;
        private static DriverFacade _driver;
        private MainNavigationSections _mainNavigationSections;
        private MegaMenuSection _megaMenuSection;
        private BreadcrumbSection _breadcrumbSection;

        [SetUp]
        public void TestInit()
        {
            _driver = new BasePage(new WebDriverSetUp());
            _driver.Start(Browser.Chrome);
            _homePage = new HomePage(_driver);
            _productPage = new ProductPage(_driver);
            _mainNavigationSections = new MainNavigationSections(_driver);
            _megaMenuSection = new MegaMenuSection(_driver);
            _breadcrumbSection= new BreadcrumbSection(_driver);
        }

        [Test]
        public void OpenMegaMenuSuccessfully()
        {
            _mainNavigationSections.OpenMenu(MainMenu.MegaMenu);

            _homePage.AssertCorrectPageIsOpen(_homePage.MegaMenuButton.Text);
        }

        [TestCase(Brand.LG)]
        [TestCase(Brand.Apple)]
        [TestCase(Brand.Nokia)]
        public void SelectByBrandInMenuSuccessfully(Brand brand)
        {
            _driver.GoToUrl(_homePage.Url);
            _homePage.SelectByBrandInMegaMenu(brand);

            _megaMenuSection.AssertMenuIsLoaded(brand.ToString());
        }

        [TestCase(SoundSystem.BluetoothSpeaker)]
        [TestCase(SoundSystem.DTH)]
        [TestCase(SoundSystem.HomeAudio)]
        public void SelectMenuInSoundSectionSuccessfully(SoundSystem system)
        {
            _driver.GoToUrl(_homePage.Url);

            _homePage.SelectBySoundSystemInMegaMenu(system);

            _megaMenuSection.AssertMenuIsLoaded(system.ToString());
        }

        [Test]
        public void OpenSpecialMenuSuccessfully()
        {
            _driver.GoToUrl(_homePage.Url);

            _mainNavigationSections.OpenMenu(MainMenu.Special);

            _breadcrumbSection.AssertMenuIsOpen("Special Offers");
        }

        [Test]
        public void SelectProductFromSpecialMenu()
        {
            _driver.GoToUrl(_homePage.Url);
            _mainNavigationSections.OpenMenu(MainMenu.Special);

            _productPage.ImageItem(Product.iPodTouch).Click();

            _productPage.AssertProductInformationIsDisplayed();
        }

        [TearDown]
        public void Quit()
        {
            _driver.Quit();
        }
    }
}