using ECommerce.Sections;

namespace ECommerce
{
    public class ShoppingCartTests
    {
        private static HomePage _homePage;
        private static ProductPage _productPage;
        private static LoginPage _loginPage;
        private static DriverFacade _driver;
        private static CartPage _cartPage;
        private static CheckoutPage _checkoutPage;
        private static AccountVoucherPage _accountVoucherPage;
        private static SuccessPage _successPage;
        private static AccountPage _accountPage;
        private static ConfirmPage _confirmPage;
        private MainHeaderSection _mainHeaderSection;
        public MainNavigationSections _mainNavigationSections;

        [SetUp]
        public void TestInit()
        {
            _driver = new BasePage(new WebDriverSetUp());
            _driver.Start(Browser.Chrome);
            _homePage = new HomePage(_driver);
            _productPage = new ProductPage(_driver);
            _cartPage = new CartPage(_driver);
            _checkoutPage = new CheckoutPage(_driver);
            _loginPage= new LoginPage(_driver);
            _successPage = new SuccessPage(_driver);
            _accountPage = new AccountPage(_driver);
            _accountVoucherPage = new AccountVoucherPage(_driver);
            _confirmPage = new ConfirmPage(_driver);
            _mainHeaderSection = new MainHeaderSection(_driver);
            _mainNavigationSections = new MainNavigationSections(_driver);   
        }

        [Test]
        public void OpenShoppingCartSuccessfully()
        {
            _driver.GoToUrl(_homePage.Url);
            _mainHeaderSection.MainHeaderNavigation(MainHeader.cart).Click();
            _driver.WaitUntilPageLoadsCompletely();
            _cartPage.CartButton.Click();

            _cartPage.AssertCartPageIsOpen();
        }

        [Test]
        public void ProductInformationPresentInShoppingCart()
        {
            _homePage.SearchByCategory(CategoryInSearchBox.Components);
            _productPage.ImageItem(Product.PalmTreoPro).Click();
            var product = _productPage.SetProductInfo();
            _productPage.AddToCartItemButton.Click();

            _cartPage.ViewCartButton.Click();

            _cartPage.AssertProductAddedToCart(product);
        }

        [Test]
        public void UpdateProductQuantityInCartSuccessfully()
        {
            _homePage.SearchByCategory(CategoryInSearchBox.Components);
            _productPage.ImageItem(Product.iPodTouch).Click();
            var product = _productPage.SetProductInfo();
            _productPage.AddToCartItemButton.Click();
            _cartPage.ViewCartButton.Click();

            _cartPage.UpdateProductQuantityInCart(3, product.ProductName);

            _cartPage.AssertQuantityAndTotalAreCorrect(3, product);
        }

        [Test]
        public void RemoveProductSuccessfully()
        {
            _homePage.SearchByCategory(CategoryInSearchBox.Components);
            _productPage.ImageItem(Product.iPodTouch).Click();
            var product = _productPage.SetProductInfo();
            _productPage.AddToCartItemButton.Click();
            _cartPage.ViewCartButton.Click();

            _cartPage.RemoveButton(product.ProductName).Click();

            _cartPage.AssertMessageForEmptyCartIsDisplayed();
        }

        [Test]
        public void AddItemToShoppingCart()
        {
            _homePage.SearchByCategory(CategoryInSearchBox.Components);
            _productPage.AddItemToCart(Product.SonyVAIO);

            _homePage.AssertThatItemIsAddedToCartMessageAppeared();
        }

        [Test]
        public void ContinueShoppingSuccessfully()
        {
            _homePage.SearchByCategory(CategoryInSearchBox.Components);
            _productPage.AddItemToCart(Product.SonyVAIO);
            _cartPage.ViewCartButton.Click();

            _cartPage.ContinueShoppingButton.Click();

            _homePage.AssertHomePageIsLoaded();
        }

        [Test]
        public void NavigateToCheckoutSuccessfully()
        {
            _homePage.SearchByCategory(CategoryInSearchBox.Components);
            _productPage.AddItemToCart(Product.SonyVAIO);
            _cartPage.ViewCartButton.Click();

            _cartPage.CheckoutButton.Click();

            _checkoutPage.CheckoutPageIsLoaded();
        }

        [Test]
        public void EstimateShippingTaxesSuccessfully()
        {
            _homePage.SearchByCategory(CategoryInSearchBox.Components);
            _productPage.AddItemToCart(Product.SonyVAIO);
            _cartPage.ViewCartButton.Click();

            _cartPage.OpenAccordion(CartAccordion.EstimateShippingTaxes).Click();
            _cartPage.FillEstimateShippingTaxes(Country.Bulgaria, "1000");
            _cartPage.GetQuotesButton.Click();
            var flatRate = _cartPage.FlatRateInformation.Text;
            _cartPage.FlatRate.Click();
            _cartPage.ApplyShippingButton.Click();

            _cartPage.AssertFlatRateIsApplied(flatRate);
        }

        [Test]
        public void EstimateShippingTaxesFailed_When_NotFilledData()
        {
            _homePage.SearchByCategory(CategoryInSearchBox.Components);
            _productPage.AddItemToCart(Product.SonyVAIO);
            _cartPage.ViewCartButton.Click();

            _cartPage.OpenAccordion(CartAccordion.EstimateShippingTaxes).Click();
            _cartPage.GetQuotesButton.Click();

            _cartPage.AssertEstimateShippingTaxesErrorsPresent();
        }

        [Test, Combinatorial]
        public void EstimateShippingTaxesFailed_When_PostCodeSymbolsAreLess_Than_MinSize(
            [Values(Country.UnitedKingdom, Country.Bulgaria)] Country country,
            [Values("", "1")] string postCode)
        {
            _homePage.SearchByCategory(CategoryInSearchBox.Components);
            _productPage.AddItemToCart(Product.SonyVAIO);
            _cartPage.ViewCartButton.Click();

            _cartPage.OpenAccordion(CartAccordion.EstimateShippingTaxes).Click();
            _cartPage.FillEstimateShippingTaxes(country, postCode);
            _cartPage.GetQuotesButton.Click();

            _cartPage.AssertPostCodeEstimateTaxesErrorPresent();
        }

        [TestCase(GiftSertificateTheme.General,2)]
        [TestCase(GiftSertificateTheme.Birthday,3)]
        [TestCase(GiftSertificateTheme.Christmas,5)]
        public void UseGiftCertificateSuccessfullyLikeRegisteredUser(GiftSertificateTheme gift,double amount)
        {
            _loginPage.LogIn(AccountInfo.Email, AccountInfo.Password);
            _mainNavigationSections.MoveToMainMenu(MainMenu.MyAccount);
            _mainNavigationSections.OpenMenu(MainMenu.Voucher);
            RecipientInfo recipient = new RecipientInfo()
            {
                RecipientName = "Dimi",
                RecipientEmail = "d@abv.bg"
            };

            _accountVoucherPage.FillPurchaseGiftData(recipient.RecipientName, recipient.RecipientEmail, gift, amount);
            _accountVoucherPage.ContinueButton.Click();
            _successPage.AssertVoucherIsPurchased();

            _mainHeaderSection.MainHeaderNavigation(MainHeader.cart).Click();
            _cartPage.CheckoutButtonInCart.Click();
            _checkoutPage.TermsCheckbox.Click();
            _checkoutPage.ContinueButton.Click();
            _confirmPage.ConfirmOrderButton.Click();

            _successPage.AsserThatPurchaseIsMade();

            _accountPage.OpenMenuFromNavbar(Navbar.Logout);

            _loginPage.LogIn(recipient.RecipientEmail, "dimi");
            _homePage.SearchByCategory(CategoryInSearchBox.Components);
            _productPage.AddItemToCart(Product.SonyVAIO);
            _cartPage.ViewCartButton.Click();

            _cartPage.OpenAccordion(CartAccordion.UseGiftCertificate).Click();
            _cartPage.EnterGiftCertificate(gift);
            _cartPage.ApplyGiftCertificate.Click();

            _cartPage.AssertGiftCertificateIsApplied();
        }

        [Test]
        public void UserHaveCorrectInformationForCartEcoTax()
        {
            _homePage.SearchByCategory(CategoryInSearchBox.Components);
            _productPage.AddItemToCartDirectly(Product.PalmTreoPro);
            _productPage.AddItemToCartDirectly(Product.HTCTouchHD);

            _cartPage.ViewCartButton.Click();

            _cartPage.AssertEcoTaxIsCorrect();
        }

        [Test]
        public void UserHaveCorrectInformationForCartVAT()
        {
            _homePage.SearchByCategory(CategoryInSearchBox.Components);
            _productPage.AddItemToCartDirectly(Product.PalmTreoPro);

            _cartPage.ViewCartButton.Click();

            _cartPage.AssertVatIsCorrect();
        }

        [Test]
        public void UserHaveCorrectInformationForCartTotalPrice()
        {
            _homePage.SearchByCategory(CategoryInSearchBox.Components);
            _productPage.AddItemToCartDirectly(Product.PalmTreoPro);

            _cartPage.ViewCartButton.Click();

            _cartPage.AssertTotalPriceIsCorrect();
        }

        [TearDown]
        public void Quit()
        {
            {
                _driver.Quit();
            }
        }
    }
}