using ECommerce.Sections;

namespace ECommerce
{
    [TestFixture]
    public class ProductTests
    {
        private static HomePage _homePage;
        private static ProductPage _productPage;
        private static CartPage _cartPage;
        private static CheckoutPage _checkoutPage;
        private static QuickViewPage _quickViewPage;
        private static DriverFacade driver;
        private MainHeaderSection mainHeaderSection;

        [SetUp]
        public void TestInit()
        {
            driver = new BasePage(new WebDriverSetUp());
            driver.Start(Browser.Chrome);
            _homePage = new HomePage(driver);
            _productPage = new ProductPage(driver);
            _cartPage = new CartPage(driver);
            _checkoutPage = new CheckoutPage(driver);
            _quickViewPage = new QuickViewPage(driver);
            mainHeaderSection = new MainHeaderSection(driver);
        }

        [Test]
        public void ViewProductInformation()
        {
            _homePage.SearchByManufacturer(Brand.Apple);

            _productPage.ImageItem(Product.iPodNano).Click();

            _productPage.AssertProductInformationIsDisplayed();
        }

        [Test]
        public void AddProductToCartSuccessfully()
        {
            _homePage.SearchByManufacturer(Brand.Apple);
            _productPage.ImageItem(Product.iPodNano).Click();
            var product = _productPage.SetProductInfo();

            _productPage.AddToCartItemButton.Click();
            mainHeaderSection.MainHeaderNavigation(MainHeader.cart).Click();
            driver.WaitUntilPageLoadsCompletely();
            _cartPage.CartButton.Click();

            _cartPage.AssertProductAddedToCart(product);
        }

        [Test]
        public void BuyProductSuccessfully()
        {
            _homePage.SearchByManufacturer(Brand.Apple);
            _productPage.ImageItem(Product.MacBookPro).Click();
            var product = _productPage.SetProductInfo();   
            
            _productPage.BuyNowButton.Click();

            _checkoutPage.CheckoutPageIsLoaded();
            _checkoutPage.AssertProductInCheckoutPage(product);
        }

        [Test]
        public void IncreaseQuantitySuccessfully_When_QuantityFieldIsEmpty()
        {
            _homePage.SearchByManufacturer(Brand.Apple);
            _productPage.SelectQuickView(Product.iPodShuffle);
            _quickViewPage.QuickViewQuantityInput.Clear();

            _quickViewPage.IncreaseQtyButton.Click();
            
            _quickViewPage.AssertIncreaseQuantityButtonWork(1);
        }

        [Test]
        public void DecreaseQuantitySuccessfully()
        {
            _homePage.SearchByManufacturer(Brand.Apple);
            _productPage.SelectQuickView(Product.iPodShuffle);
            _quickViewPage.IncreaseQtyButton.Click();

            _quickViewPage.DecreaseQtyButton.Click();

            _quickViewPage.AssertDecreaseQuantityButtonWork(1);
        }

        [TearDown]
        public void Quit()
        {
            driver.Quit();
        }
    }
}