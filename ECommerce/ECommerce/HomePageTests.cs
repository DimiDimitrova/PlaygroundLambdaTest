
namespace ECommerce
{
    public class HomePageTests
    {
        private static DriverFacade _driver;
        private static HomePage _homePage;
        private static ProductPage _productPage;

        [SetUp]
        public void TestInit()
        {
            _driver = new BasePage(new WebDriverSetUp());
            _driver.Start(Browser.Chrome);
            _homePage = new HomePage(_driver);
            _productPage = new ProductPage(_driver);
        }

        [Test]
        public void ShopNowFromHomePageSuccessfully()
        {
            _driver.GoToUrl(_homePage.Url);

            _homePage.ShopNowButton.Click();

            _productPage.AssertProductPageIsOpen();
        }

        [TestCase(HomePageModulTitle.TopColection)]
        [TestCase(HomePageModulTitle.TopProducts)]
        public void HomePageContainsInformationForModuls(HomePageModulTitle modulInfo)
        {
            _driver.GoToUrl(_homePage.Url);

            _homePage.AssertModuleInformationPresent(modulInfo);
        }

        [TearDown]
        public void Quit()
        {
            _driver.Quit();
        }
    }
}