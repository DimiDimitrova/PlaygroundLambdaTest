
namespace ECommerce
{
    [TestFixture]
    public class ConfirmPageTests
    {
        private static DriverFacade _driver;
        private HomePage _homePage;
        private ProductPage _productPage;
        private CheckoutPage _checkoutPage;
        private ConfirmPage _confirmPage;
        private SuccessPage _successPage;
        private Generator _generator;

        [SetUp]
        public void TestInit()
        {
            _driver = new BasePage(new WebDriverSetUp());
            _driver.Start(Browser.Chrome);
            _successPage = new SuccessPage(_driver);
            _homePage = new HomePage(_driver);
            _productPage = new ProductPage(_driver);
            _checkoutPage = new CheckoutPage(_driver);
            _confirmPage = new ConfirmPage(_driver);
            _generator = new Generator();
        }

        [Test]
        public void ProductInformationIsInConfirmPageCorrectly()
        {
            _homePage.SearchByManufacturer(Brand.Apple);
            _productPage.ImageItem(Product.MacBookPro).Click();
            var product = _productPage.SetProductInfo();
            _productPage.BuyNowButton.Click();

            var person = _generator.GetRegisteredUser();
            var paymentAddress = new PaymentAddressInfo();
            _checkoutPage.FillAccountDetails(person, paymentAddress, Account.Login);
            _checkoutPage.ContinuePurchase();

            _confirmPage.AssertProductContentIsCorrect(product);
        }

        [Test]
        public void TotalSumIsCorrect()
        {
            _homePage.SearchByManufacturer(Brand.Apple);
            _productPage.ImageItem(Product.MacBookPro).Click();
            _productPage.BuyNowButton.Click();

            var person = _generator.GetRegisteredUser();
            var paymentAddress = new PaymentAddressInfo();
            _checkoutPage.FillAccountDetails(person, paymentAddress, Account.Login);
            _checkoutPage.ContinuePurchase();

            _confirmPage.AssertTotalSum();
        }

        [Test]
        public void PaymentAddressIsCorrect()
        {
            _homePage.SearchByManufacturer(Brand.Apple);
            _productPage.ImageItem(Product.MacBookPro).Click();
            _productPage.BuyNowButton.Click();

            var person = _generator.CreatePersonInfo();
            var paymentAddress = _generator.CreatePaymentAddress();
            _checkoutPage.FillAccountDetails(person, paymentAddress, Account.GuestAccount);
            _checkoutPage.ContinuePurchase();

            _confirmPage.AssertPaymentInfo(paymentAddress);
        }

        [Test]
        public void EditOrderSuccessfully()
        {
            _homePage.SearchByManufacturer(Brand.Apple);
            _productPage.ImageItem(Product.MacBookPro).Click();
            _productPage.BuyNowButton.Click();
            var person = _generator.GetRegisteredUser();
            var paymentAddress = new PaymentAddressInfo();

            _checkoutPage.FillAccountDetails(person, paymentAddress, Account.Login);
            _checkoutPage.ContinuePurchase();

            _confirmPage.ConfirmPageIsLoaded();
            _confirmPage.EditButton.Click();

            _checkoutPage.CheckoutPageIsLoaded();

            _checkoutPage.UpdateProductQuantity(10, Product.MacBookPro);
            _checkoutPage.TermsCheckbox.Click();
            _checkoutPage.ContinuePurchase();

            _confirmPage.AssertProductQuantityIsEdited(10, Product.MacBookPro);
        }

        [Test]
        public void ConfirmOrderSuccessfully()
        {
            _homePage.SearchByManufacturer(Brand.Apple);
            _productPage.ImageItem(Product.MacBookPro).Click();
            _productPage.BuyNowButton.Click();

            var person = _generator.GetRegisteredUser();
            var paymentAddress = new PaymentAddressInfo();
            _checkoutPage.FillAccountDetails(person, paymentAddress, Account.Login);
            _checkoutPage.ContinuePurchase();
            _confirmPage.ConfirmOrderButton.Click();

            _successPage.AsserThatPurchaseIsMade();
        }

        [Test]
        public void ContinueSuccessfullyAfterConfirmPurchase()
        {
            _homePage.SearchByManufacturer(Brand.Apple);
            _productPage.ImageItem(Product.MacBookPro).Click();
            _productPage.BuyNowButton.Click();
            var person = _generator.GetRegisteredUser();
            var paymentAddress = new PaymentAddressInfo();

            _checkoutPage.FillAccountDetails(person, paymentAddress, Account.Login);
            _checkoutPage.ContinuePurchase();
            _confirmPage.ConfirmOrderButton.Click();

            _successPage.AsserThatPurchaseIsMade();
            _successPage.ContinueButton.Click();
            _homePage.AssertHomePageIsLoaded();
        }

        [TearDown]
        public void Quit()
        {
            _driver.Quit();
        }
    }
}