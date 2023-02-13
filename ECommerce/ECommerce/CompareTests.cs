using ECommerce.Sections;

namespace ECommerce
{
    public class CompareTests 
    {
        private static ProductPage _productPage;
        private static ProductComparisonPage _productComparisonPage;
        private static CartPage _cartPage;
        private static HomePage _homePage;
        private static DriverFacade _driver;
        private MainNavigationSections _mainNavigationSections;
        private MainHeaderSection _mainHeaderSection;

        [SetUp]
        public void TestInit()
        {
            _driver = new BasePage(new WebDriverSetUp());
            _driver.Start(Browser.Chrome);
            _productPage = new ProductPage(_driver);
            _productComparisonPage = new ProductComparisonPage(_driver);
            _cartPage = new CartPage(_driver);
            _homePage = new HomePage(_driver);   
        }

        [Test]
        public void AddProductToCartSuccessfully()
        {
            _mainNavigationSections.OpenMenu(MainMenu.Special);
            _productPage.ImageItem(Product.iPodTouch).Click();
            var product = _productPage.SetProductInfo();

            _productPage.CompareButton.Click();
            _mainHeaderSection.MainHeaderNavigation(MainHeader.compare).Click();
            _productComparisonPage.AddToCart(product.ProductName).Click();
            _mainHeaderSection.MainHeaderNavigation(MainHeader.cart).Click();
            _cartPage.CartButton.Click();

            _cartPage.AssertProductAddedToCart(product);
        }

        [Test]
        public void RemoveProductOfCompare()
        {
            _driver.GoToUrl(_homePage.Url);
            _mainNavigationSections.OpenMenu(MainMenu.Special);
            _productPage.ImageItem(Product.iPodTouch).Click();

            var product = _productPage.SetProductInfo();
            _productPage.CompareButton.Click();
            _mainHeaderSection.MainHeaderNavigation(MainHeader.compare).Click();
            _productComparisonPage.RemoveButton(product.ProductName).Click();

            _productComparisonPage.AssertProductIsRemoved();
        }

        [Test]
        public void CompareProductsSuccessfully()
        {
            var products = new List<ProductInfo>();

            _homePage.SearchByManufacturer(Brand.Apple);
            products.Add(_productPage.AddProductForCompare(Product.iPodShuffle));
            _homePage.SearchByManufacturer(Brand.HTC);
            products.Add(_productPage.AddProductForCompare(Product.HTCTouchHD));

            _productPage.CompareLink.Click();

            _productComparisonPage.ProductIsInComparePage(products);
        }

        [TearDown]
        public void Quit()
        {
            _driver.Quit();
        }
    }
}