using ECommerce.Sections;

namespace ECommerce
{
    [TestFixture]
    public class CheckoutTests
    {
        private static LoginPage _loginPage;
        private static ProductPage _productPage;
        private static QuickViewPage _quickViewPage;
        private static CheckoutPage _checkoutPage;
        private static ConfirmPage _confirmPage;
        private static SuccessPage _successPage;
        private static HomePage _homePage;
        private static DriverFacade _driver;
        private static CartPage _cartPage;
        private Generator _generator;
        private MainNavigationSections _mainNavigationSections;

        [SetUp]
        public void TestInit()
        {
            _driver = new BasePage(new WebDriverSetUp());
            _driver.Start(Browser.Chrome);
            _loginPage = new LoginPage(_driver);
            _productPage = new ProductPage(_driver);
            _quickViewPage = new QuickViewPage(_driver);
            _confirmPage = new ConfirmPage(_driver);
            _checkoutPage = new CheckoutPage(_driver);
            _successPage = new SuccessPage(_driver);
            _homePage = new HomePage(_driver);
            _cartPage = new CartPage(_driver);
            _generator = new Generator();
            _mainNavigationSections = new MainNavigationSections(_driver);
        }

        [Test]
        public void MakePurchaseSuccessfullyWithLoggedUser()
        {
            _loginPage.LogIn(AccountInfo.Email, AccountInfo.Password);
            _mainNavigationSections.OpenMenu(MainMenu.Special);
            _productPage.SelectQuickView(Product.iPodTouch);
            _quickViewPage.SetQuantityOnItem(2);
            _productPage.BuyNow();
            _checkoutPage.TermsCheckbox.Click();
            _checkoutPage.ContinuePurchase();

            _confirmPage.AssertTotalSum();
            _confirmPage.ConfirmOrder();

            _successPage.AsserThatPurchaseIsMade();
        }

        [Test]
        public void MakePurchaseSuccessfullyLikeGuest()
        {
            _homePage.SearchByCategory(CategoryInSearchBox.Components);
            _productPage.ListViewButton.Click();
            _productPage.AddItemToCartDirectly(Product.PalmTreoPro);
            _cartPage.CheckoutButton.Click();
            var person = _generator.CreatePersonInfo();
            var paymentAddress = _generator.CreatePaymentAddress();

            _checkoutPage.FillAccountDetails(person, paymentAddress, Account.GuestAccount);
            var prices = _checkoutPage.GetPrices();
            _checkoutPage.ContinuePurchase();

            _confirmPage.VerifyPriceList(prices);
            _confirmPage.AssertPaymentInfo(paymentAddress);
            _confirmPage.ConfirmOrder();

            _successPage.AsserThatPurchaseIsMade();
        }

        [Test]
        public void MakePurchaseSuccessfullyLikeNewUser()
        {
            _homePage.SearchByCategory(CategoryInSearchBox.Components);
            _productPage.ListViewButton.Click();
            _productPage.AddItemToCartDirectly(Product.PalmTreoPro);

            _cartPage.CheckoutButton.Click();
            var person = _generator.CreatePersonInfo();

            var paymentAddress = _generator.CreatePaymentAddress();
            _checkoutPage.FillAccountDetails(person, paymentAddress, Account.RegisterAccount);
            _checkoutPage.FillPasswordFields(_generator.CreateText());
            _checkoutPage.ContinuePurchase();

            _confirmPage.AssertPaymentInfo(paymentAddress);
            _confirmPage.ConfirmOrder();

            _successPage.AsserThatPurchaseIsMade();
        }

        [Test]
        public void MadePurchaseSuccessfullyWithLoginOption()
        {
            _homePage.SearchByCategory(CategoryInSearchBox.Components);
            _productPage.ListViewButton.Click();
            _productPage.AddItemToCartDirectly(Product.PalmTreoPro);
            _cartPage.CheckoutButton.Click();
            var person = _generator.GetRegisteredUser();

            PaymentAddressInfo paymentAddress = new PaymentAddressInfo();
            _checkoutPage.FillAccountDetails(person, paymentAddress, Account.Login);
            _checkoutPage.ContinuePurchase();

            _confirmPage.AssertTotalSum();
            _confirmPage.ConfirmOrder();

            _successPage.AsserThatPurchaseIsMade();
        }

        [TestCase(0)]
        public void MadePurchaseWithRegisterAccountFailed_When_FirstNameHasLessSymbols_Than_MinSize(int firstNameSize)
        {
            _homePage.SearchByCategory(CategoryInSearchBox.Components);
            _productPage.ListViewButton.Click();
            _productPage.AddItemToCartDirectly(Product.PalmTreoPro);

            _cartPage.CheckoutButton.Click();
            var person = _generator.CreatePersonWithSpecificFirstName(firstNameSize);
            var paymentAddress = _generator.CreatePaymentAddress();
            _checkoutPage.FillAccountDetails(person, paymentAddress, Account.RegisterAccount);
            _checkoutPage.FillPasswordFields(_generator.CreateText());
            _checkoutPage.ContinuePurchase();

            _checkoutPage.AssertMakePurchaseFailedWithIncorrectFirstName();
        }

        [TestCase(33)]
        [TestCase(34)]
        [TestCase(35)]
        public void MadePurchaseWithRegisterAccountFailed_When_FirstNameHasMoreSymbols_Than_MaxSize(int sizeFirstName)
        {
            _homePage.SearchByCategory(CategoryInSearchBox.Components);
            _productPage.ListViewButton.Click();
            _productPage.AddItemToCartDirectly(Product.PalmTreoPro);
            _cartPage.CheckoutButton.Click();
            var person = _generator.CreatePersonWithSpecificFirstName(sizeFirstName);

            var paymentAddress = _generator.CreatePaymentAddress();
            _checkoutPage.FillAccountDetails(person, paymentAddress, Account.RegisterAccount);
            _checkoutPage.FillPasswordFields(_generator.CreateText());
            _checkoutPage.ContinuePurchase();

            _checkoutPage.AssertMakePurchaseFailedWithIncorrectFirstName();
        }

        [TestCase(0)]
        public void MakePurchaseWithRegisterAccountFailed_When_LastNameHasLessSymbols_Than_MinSize(int lastNameSize)
        {
            _homePage.SearchByCategory(CategoryInSearchBox.Components);
            _productPage.ListViewButton.Click();
            _productPage.AddItemToCartDirectly(Product.PalmTreoPro);

            _cartPage.CheckoutButton.Click();
            var person = _generator.CreatePersonWithSpecificLastName(lastNameSize);
            var paymentAddress = _generator.CreatePaymentAddress();

            _checkoutPage.FillAccountDetails(person, paymentAddress, Account.RegisterAccount);
            _checkoutPage.FillPasswordFields(_generator.CreateText());
            _checkoutPage.ContinuePurchase();

            _checkoutPage.AssertMakePurchaseFailedWithIncorrectLastName();
        }

        [TestCase(34)]
        [TestCase(33)]
        public void MakePurchaseWithRegisterAccountFailed_When_LastNameHasMoreSymbols_Than_MaxSize(int sizeLastName)
        {
            _homePage.SearchByCategory(CategoryInSearchBox.Desktops);
            _productPage.ListViewButton.Click();
            _productPage.AddItemToCartDirectly(Product.NikonD300);
            _cartPage.CheckoutButton.Click();

            var person = _generator.CreatePersonWithSpecificLastName(sizeLastName);
            var paymentAddress = _generator.CreatePaymentAddress();

            _checkoutPage.FillAccountDetails(person, paymentAddress, Account.RegisterAccount);
            _checkoutPage.FillPasswordFields(_generator.CreateText());
            _checkoutPage.ContinuePurchase();

            _checkoutPage.AssertMakePurchaseFailedWithIncorrectLastName();
        }

        [Test]
        public void MakePurchaseWithRegisterAccountFailed_When_UseAlreadyRegisteredEmail()
        {
            _homePage.SearchByCategory(CategoryInSearchBox.Desktops);
            _productPage.ListViewButton.Click();
            _productPage.AddItemToCartDirectly(Product.NikonD300);
            _cartPage.CheckoutButton.Click();

            var person = _generator.GetRegisteredUser();
            var paymentAddress = _generator.CreatePaymentAddress();
            _checkoutPage.FillAccountDetails(person, paymentAddress, Account.RegisterAccount);

            _checkoutPage.ContinuePurchase();

            _checkoutPage.AssertMakePurchaseFailedWithExistEmail();
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(0)]
        public void MakePurchaseWithRegisterAccountFailed_When_TelephoneSymbolsAreLess_Than_MinSize(int telephoneSize)
        {
            _homePage.SearchByCategory(CategoryInSearchBox.Components);
            _productPage.ListViewButton.Click();
            _productPage.AddItemToCartDirectly(Product.iPodTouch);
            _cartPage.CheckoutButton.Click();

            var person = _generator.CreatePersonWithSpecificTelephone(telephoneSize);
            var paymentAddress = _generator.CreatePaymentAddress();

            _checkoutPage.FillAccountDetails(person, paymentAddress, Account.RegisterAccount);
            _checkoutPage.FillPasswordFields(person.Password);
            _checkoutPage.ContinuePurchase();

            _checkoutPage.AssertMakePurchaseFailedWithIncorrectTelephone();
        }

        [TestCase(33)]
        [TestCase(34)]
        [TestCase(35)]
        public void MakePurchaseWithRegisterAccountFailed_When_TelephoneSymbolsAreMore_Than_MaxSize(int telephoneSize)
        {
            _homePage.SearchByCategory(CategoryInSearchBox.Components);
            _productPage.ListViewButton.Click();
            _productPage.AddItemToCartDirectly(Product.iPodTouch);
            _cartPage.CheckoutButton.Click();

            var person = _generator.CreatePersonWithSpecificTelephone(telephoneSize);
            var paymentAddress = _generator.CreatePaymentAddress();

            _checkoutPage.FillAccountDetails(person, paymentAddress, Account.RegisterAccount);
            _checkoutPage.FillPasswordFields(person.Password);
            _checkoutPage.ContinuePurchase();

            _checkoutPage.AssertMakePurchaseFailedWithIncorrectTelephone();
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(0)]
        [TestCase(3)]
        public void MakePurchaseWithRegisterAccountFailed_When_PasswordSymbolsAreLess_Than_MinSize(int password)
        {
            _homePage.SearchByCategory(CategoryInSearchBox.Components);
            _productPage.ListViewButton.Click();
            _productPage.AddItemToCartDirectly(Product.iPodTouch);
            _cartPage.CheckoutButton.Click();

            var person = _generator.CreatePersonWithSpecificPassword(password);
            var paymentAddress = _generator.CreatePaymentAddress();

            _checkoutPage.FillAccountDetails(person, paymentAddress, Account.RegisterAccount);
            _checkoutPage.FillPasswordFields(person.Password);
            _checkoutPage.ContinuePurchase();

            _checkoutPage.AssertMakePurchaseFailedWithIncorrectPassword();
        }

        [TestCase(22)]
        [TestCase(21)]
        public void MakePurchaseWithRegisterAccountFailed_When_PasswordSymbolsAreMore_Than_MaxSize(int password)
        {
            _homePage.SearchByCategory(CategoryInSearchBox.Components);
            _productPage.ListViewButton.Click();
            _productPage.AddItemToCartDirectly(Product.iPodTouch);
            _cartPage.CheckoutButton.Click();

            var person = _generator.CreatePersonWithSpecificPassword(password);
            var paymentAddress = _generator.CreatePaymentAddress();

            _checkoutPage.FillAccountDetails(person, paymentAddress, Account.RegisterAccount);
            _checkoutPage.FillPasswordFields(person.Password);
            _checkoutPage.ContinuePurchase();

            _checkoutPage.AssertMakePurchaseFailedWithIncorrectPassword();
        }

        [Test]
        public void MakePurchaseFailedWithRegisterAccount_When_ConfirmPasswordIsIncorrect()
        {
            _homePage.SearchByCategory(CategoryInSearchBox.Components);
            _productPage.ListViewButton.Click();
            _productPage.AddItemToCartDirectly(Product.iPodTouch);
            _cartPage.CheckoutButton.Click();

            var person = _generator.CreatePersonInfo();
            var paymentAddress = _generator.CreatePaymentAddress();

            _checkoutPage.FillAccountDetails(person, paymentAddress, Account.RegisterAccount);
            _checkoutPage.FillPasswordFields(person.Password, _generator.CreateText());
            _checkoutPage.ContinuePurchase();

            _checkoutPage.AssertMakePurchaseFailedWithNotMatchesPassword();
        }

        [Test]
        public void MakePurchaseFailedWithRegisterAccount_When_ConfirmPasswordIsEmpty()
        {
            _homePage.SearchByCategory(CategoryInSearchBox.Components);
            _productPage.ListViewButton.Click();
            _productPage.AddItemToCartDirectly(Product.iPodTouch);
            _cartPage.CheckoutButton.Click();

            var person = _generator.CreatePersonInfo();
            var paymentAddress = _generator.CreatePaymentAddress();

            _checkoutPage.FillAccountDetails(person, paymentAddress, Account.RegisterAccount);
            _checkoutPage.FillPasswordFields(person.Password, string.Empty);
            _checkoutPage.ContinuePurchase();

            _checkoutPage.AssertMakePurchaseFailedWithEmptyConfirmPasswordField();
        }

        [TestCase(2)]
        [TestCase(1)]
        [TestCase(0)]
        public void MakePurchasefailedWithRegisterAccount_When_AddressHasSymbolsLess_Than_MinSize(int addressSize)
        {
            _homePage.SearchByCategory(CategoryInSearchBox.Components);
            _productPage.ListViewButton.Click();
            _productPage.AddItemToCartDirectly(Product.iPodTouch);
            _cartPage.CheckoutButton.Click();

            var person = _generator.CreatePersonInfo();
            var paymentAddress = _generator.CreatePaymentWithSpecificAddress(addressSize);

            _checkoutPage.FillAccountDetails(person, paymentAddress, Account.RegisterAccount);
            _checkoutPage.FillPasswordFields(person.Password);
            _checkoutPage.ContinuePurchase();

            _checkoutPage.AssertMakePurchaseFailedWithIncorrectAddress();
        }

        [TestCase(200)]
        [TestCase(130)]
        [TestCase(129)]
        public void MakePurchasefailedWithRegisterAccount_When_AddressHasSymbolsMore_Than_MaxSize(int addressSize)
        {
            _homePage.SearchByCategory(CategoryInSearchBox.Components);
            _productPage.ListViewButton.Click();
            _productPage.AddItemToCartDirectly(Product.iPodTouch);
            _cartPage.CheckoutButton.Click();

            var person = _generator.CreatePersonInfo();
            var paymentAddress = _generator.CreatePaymentWithSpecificAddress(addressSize);

            _checkoutPage.FillAccountDetails(person, paymentAddress, Account.RegisterAccount);
            _checkoutPage.FillPasswordFields(person.Password);
            _checkoutPage.ContinuePurchase();

            _checkoutPage.AssertMakePurchaseFailedWithIncorrectAddress();
        }

        [TestCase(1)]
        [TestCase(0)]
        public void MakePurchaseFailedWithRegisterAccount_When_CityHasSymbolsLess_Than_MinSize(int citySize)
        {
            _homePage.SearchByCategory(CategoryInSearchBox.Components);
            _productPage.ListViewButton.Click();
            _productPage.AddItemToCartDirectly(Product.iPodTouch);
            _cartPage.CheckoutButton.Click();

            var person = _generator.CreatePersonInfo();
            var paymentAddress = _generator.CreatePaymentWithSpecificCity(citySize);

            _checkoutPage.FillAccountDetails(person, paymentAddress, Account.RegisterAccount);
            _checkoutPage.FillPasswordFields(person.Password);
            _checkoutPage.ContinuePurchase();

            _checkoutPage.AssertMakePurchaseFailedWithIncorrectCity();
        }

        [TestCase(130)]
        [TestCase(129)]
        public void MakePurchaseFailedWithRegisterAccount_When_CityHasSymbolsMore_Than_MaxSize(int citySize)
        {
            _homePage.SearchByCategory(CategoryInSearchBox.Components);
            _productPage.ListViewButton.Click();
            _productPage.AddItemToCartDirectly(Product.iPodTouch);
            _cartPage.CheckoutButton.Click();

            var person = _generator.CreatePersonInfo();
            var paymentAddress = _generator.CreatePaymentWithSpecificCity(citySize);

            _checkoutPage.FillAccountDetails(person, paymentAddress, Account.RegisterAccount);
            _checkoutPage.FillPasswordFields(person.Password);
            _checkoutPage.ContinuePurchase();

            _checkoutPage.AssertMakePurchaseFailedWithIncorrectCity();
        }

        [TestCase(1)]
        [TestCase(0)]
        public void MakePurchaseFailedWithRegisterAccount_When_PostCodeHasSymbolsLess_Than_MinSize(int postCodeSize)
        {
            _homePage.SearchByCategory(CategoryInSearchBox.Components);
            _productPage.ListViewButton.Click();
            _productPage.AddItemToCartDirectly(Product.iPodTouch);
            _cartPage.CheckoutButton.Click();

            var person = _generator.CreatePersonInfo();
            var paymentAddress = _generator.CreatePaymentWithSpecificPostCode(postCodeSize);

            _checkoutPage.FillAccountDetails(person, paymentAddress, Account.RegisterAccount);
            _checkoutPage.FillPasswordFields(person.Password);
            _checkoutPage.ContinuePurchase();

            _checkoutPage.AssertMakePurchaseFailedWithIncorrectPostCode();
        }

        [TestCase(11)]
        [TestCase(12)]
        public void MakePurchaseFailedWithRegisterAccount_When_PostCodeHasSymbolsMore_Than_MaxSize(int postCodeSize)
        {
            _homePage.SearchByCategory(CategoryInSearchBox.Components);
            _productPage.ListViewButton.Click();
            _productPage.AddItemToCartDirectly(Product.iPodTouch);
            _cartPage.CheckoutButton.Click();

            var person = _generator.CreatePersonInfo();
            var paymentAddress = _generator.CreatePaymentWithSpecificPostCode(postCodeSize);

            _checkoutPage.FillAccountDetails(person, paymentAddress, Account.RegisterAccount);
            _checkoutPage.FillPasswordFields(person.Password);
            _checkoutPage.ContinuePurchase();

            _checkoutPage.AssertMakePurchaseFailedWithIncorrectPostCode();
        }

        [Test]
        public void MakePurchaseFailedWithRegisterAccount_When_Country_Is_PleaseSelect()
        {
            _homePage.SearchByCategory(CategoryInSearchBox.Components);
            _productPage.ListViewButton.Click();
            _productPage.AddItemToCartDirectly(Product.iPodTouch);
            _cartPage.CheckoutButton.Click();

            var person = _generator.CreatePersonInfo();
            var paymentAddress = _generator.CreatePaymentWithSelectCountryOption();

            _checkoutPage.FillAccountDetails(person, paymentAddress, Account.RegisterAccount);
            _checkoutPage.FillPasswordFields(person.Password);
            _checkoutPage.ContinuePurchase();

            _checkoutPage.AssertMakePurchaseFailedWithIncorrectCountry();
        }

        [Test]
        public void MakePurchaseFailedWhenConditionsAreNotCheck()
        {
            _homePage.SearchByCategory(CategoryInSearchBox.Components);
            _productPage.ListViewButton.Click();
            _productPage.AddItemToCartDirectly(Product.iPodTouch);
            _cartPage.CheckoutButton.Click();

            var person = _generator.CreatePersonInfo();
            var paymentAddress = _generator.CreatePaymentAddress();

            _checkoutPage.FillAccountDetails(person, paymentAddress, Account.RegisterAccount);
            _checkoutPage.FillPasswordFields(person.Password);

            _checkoutPage.AssertMakePurchaseFailedWithoutCheckedConditions();
        }

        [Test]
        public void UpdateProductQuantityInCheckoutSuccessfully()
        {
            _homePage.SearchByCategory(CategoryInSearchBox.Components);
            _productPage.ImageItem(Product.iPodTouch).Click();
            _productPage.AddToCartItemButton.Click();
            _cartPage.CheckoutButton.Click();

            _checkoutPage.UpdateProductQuantity(3, Product.iPodTouch);
            _checkoutPage.UpdateButton(Product.iPodTouch.GetEnumDescription()).Click();

            _checkoutPage.AssertQuantityAndTotalAreCorrect(3, Product.iPodTouch);
        }

        [Test]
        public void RemoveProductSuccessfully()
        {
            _homePage.SearchByCategory(CategoryInSearchBox.Components);
            _productPage.ListViewButton.Click();
            _productPage.AddItemToCartDirectly(Product.PalmTreoPro);

            _cartPage.CheckoutButton.Click();
            _driver.WaitForAjax();
            _checkoutPage.RemoveButton(Product.PalmTreoPro).Click();

            _cartPage.AssertShoppingCartIsEmpty();
        }

        [Test]
        public void UserHaveCorrectInformationForCheckoutEcoTax()
        {
            _homePage.SearchByCategory(CategoryInSearchBox.Components);
            _productPage.ListViewButton.Click();
            _productPage.AddItemToCartDirectly(Product.HTCTouchHD);
            _productPage.AddItemToCartDirectly(Product.iPodTouch);

            _cartPage.CheckoutButton.Click();

            _checkoutPage.AssertEcoTaxIsCorrect();
        }

        [Test]
        public void UserHaveCorrectInformationForCheckoutVAT()
        {
            _homePage.SearchByCategory(CategoryInSearchBox.Components);
            _productPage.ListViewButton.Click();
            _productPage.AddItemToCartDirectly(Product.iPodNano);

            _cartPage.CheckoutButton.Click();

            _checkoutPage.AssertVatIsCorrect();
        }

        [Test]
        public void UserHaveCorrectInformationForCheckoutTotalPrice()
        {
            _homePage.SearchByCategory(CategoryInSearchBox.Components);
            _productPage.ListViewButton.Click();
            _productPage.AddItemToCartDirectly(Product.PalmTreoPro);

            _cartPage.CheckoutButton.Click();

            _checkoutPage.AssertTotalPriceIsCorrect();
        }

        [Test]
        public void CheckoutContainsSelectedProduct()
        {
            _homePage.SearchByCategory(CategoryInSearchBox.Components);
            _productPage.ImageItem(Product.PalmTreoPro).Click();
            var product = _productPage.SetProductInfo();
            _productPage.AddToCartItemButton.Click();

            _cartPage.CheckoutButton.Click();

            _checkoutPage.AssertProductInCheckoutPage(product);
        }

        [Test]
        public void AddedProductPresentOnThePage()
        {
            _homePage.SearchByCategory(CategoryInSearchBox.Components);
            _productPage.ImageItem(Product.PalmTreoPro).Click();
            var product = _productPage.SetProductInfo();
            _productPage.AddToCartItemButton.Click();

            _cartPage.CheckoutButton.Click();

            _checkoutPage.AssertProductInCheckoutPage(product);
        }

        [TearDown]
        public void Quit()
        {
            _driver.Quit();
        }
    }
}