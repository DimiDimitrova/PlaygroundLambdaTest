
namespace ECommerce
{
    [TestFixture]
    public class SearchTests
    {
        private static HomePage _homePage;
        private static SearchPage _searchPage;
        private static DriverFacade driver;
        private MegaMenuSection _megaMenuSection;

        [SetUp]
        public void TestInit()
        {
            driver = new BasePage(new WebDriverSetUp());
            driver.Start(Browser.Chrome);
            _homePage = new HomePage(driver);
            _searchPage = new SearchPage(driver);
        }

        [TestCase(CategoryInSearchBox.Cameras)]
        [TestCase(CategoryInSearchBox.MP3Players)]
        [TestCase(CategoryInSearchBox.PhonesPDAs)]
        [TestCase(CategoryInSearchBox.Desktops)]
        [TestCase(CategoryInSearchBox.Components)]
        public void SearchByCategory(CategoryInSearchBox category)
        {
            _homePage.SearchByCategory(category);

            _searchPage.AssertBySearchedCategory(category);
        }

        [TestCase(Brand.Xiomi)]
        [TestCase(Brand.HTC)]
        [TestCase(Brand.Apple)]
        [TestCase(Brand.Nokia)]
        [TestCase(Brand.LG)]
        public void SearchByManufacturer(Brand brand)
        {
            _homePage.SearchByManufacturer(brand);

            _searchPage.AssertBySearchedManufacturer(brand);
        }

        [TestCase(Categories.Airconditioner)]
        [TestCase(Categories.MP3Players)]
        [TestCase(Categories.WebCameras)]
        [TestCase(Categories.FashionandAccessories)]
        public void SearchWithTopCategoryFilter(Categories category)
        {
            _homePage.SearchByTopCategory(category);

            _megaMenuSection.AssertThatCategoryPresentInThePage(category);
        }

        [Test]
        public void SearchWithSearchInput()
        {
            string search = "canon";

            _homePage.Search(search);

            _searchPage.AssertBySearchedInput(search);
        }

        [Test]
        public void SearchWithKeywords()
        {
            string search = "n";
            driver.GoToUrl(_homePage.Url);
            _homePage.SearchButton.Click();

            _searchPage.SearchByKeywords(search);

            _searchPage.AssertBySearchedInput(search);
        }

        [TearDown]
        public void Quit()
        {
            driver.Quit();
        }
    }
}