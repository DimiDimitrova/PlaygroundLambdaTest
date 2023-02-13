
namespace ECommerce.Pages
{
    public partial class CheckoutPage
    {
        public IWebElement FirstNameField => _driver.FindElement(By.Id("input-payment-firstname"));
        public IWebElement LastNameField => _driver.FindElement(By.Id("input-payment-lastname"));
        public IWebElement EmailField => _driver.FindElement(By.Id("input-payment-email"));
        public IWebElement TelephoneField => _driver.FindElement(By.Id("input-payment-telephone"));
        public IWebElement BillingPaymentFirstAddressField => _driver.FindElement(By.Id("input-payment-address-1"));
        public IWebElement BillingPaymentCityField => _driver.FindElement(By.Id("input-payment-city"));
        public IWebElement BillingPaymentPostCodeField => _driver.FindElement(By.Id("input-payment-postcode"));
        public IWebElement BillingPaymentCountryDropDown => _driver.FindElement(By.Id("input-payment-country"));
        public IWebElement BillingPaymentRegionField => _driver.FindElement(By.Id("input-payment-zone"));
        public IWebElement ContinueButton => _driver.FindElement(By.Id("button-save"));
        public IWebElement PaymentPassword => _driver.FindElement(By.Id("input-payment-password"));
        public IWebElement PaymentConfirmPassword => _driver.FindElement(By.Id("input-payment-confirm"));
        public IWebElement TermsCheckbox => _driver.FindElement(By.XPath("//*[@id='input-agree']//parent::div//child::label"));
        public IWebElement PrivacyPoliceCheckbox => _driver.FindElement(By.XPath("//*[@id='input-account-agree']//parent::div//child::label"));
        public IWebElement LoginEmail => _driver.FindElement(By.Id("input-login-email"));
        public IWebElement LoginPassword => _driver.FindElement(By.Id("input-login-password"));
        public IWebElement LoginButton => _driver.FindElement(By.Id("button-login"));
        public IWebElement ExistingPaymentAddress => _driver.FindElement(By.XPath("//*[@id='input-payment-address-existing']//parent::div//child::label"));
        public List<IWebElement> CheckoutTableContent => _driver.FindElements(By.XPath("//div[@class='table-responsive']//tbody//tr"));
        public List<IWebElement> TableProductQuantity => _driver.FindElements(By.XPath("//div[@class='table-responsive']//tbody//tr//div"));
        public List<IWebElement> TableProductUnitPrice => _driver.FindElements(By.XPath("//div[@class='table-responsive']//tbody//tr//td[@class='text-right'][1]"));
        public IWebElement Total => _driver.FindElement(By.XPath("//table[@id='checkout-total']//td[text()='Total:']//following::strong"));
        public IWebElement FirstNameError => _driver.FindElement(By.XPath("//input[@id='input-payment-firstname']//following::div[contains(@class,'invalid-feedback')]"));
        public IWebElement LastNameError => _driver.FindElement(By.XPath("//input[@id='input-payment-lastname']//following::div[contains(@class,'invalid-feedback')]"));
        public IWebElement EmailError => _driver.FindElement(By.XPath("//input[@id='input-payment-email']//following::div[contains(@class,'invalid-feedback')]"));
        public IWebElement TelephoneError => _driver.FindElement(By.XPath("//input[@id='input-payment-telephone']//following::div[contains(@class,'invalid-feedback')]"));
        public IWebElement PasswordError => _driver.FindElement(By.XPath("//input[@id='input-payment-password']//following::div[contains(@class,'invalid-feedback')]"));
        public IWebElement PasswordConfirmationError => _driver.FindElement(By.XPath("//input[@id='input-payment-confirm']//following::div[contains(@class,'invalid-feedback')]"));
        public IWebElement AddressError => _driver.FindElement(By.XPath("//input[@id='input-payment-address-1']//following::div[contains(@class,'invalid-feedback')]"));
        public IWebElement CityError => _driver.FindElement(By.XPath("//input[@id='input-payment-city']//following::div[contains(@class,'invalid-feedback')]"));
        public IWebElement CountryError => _driver.FindElement(By.XPath("//select[@id='input-payment-country']//following::div[contains(@class,'invalid-feedback')]"));
        public IWebElement RegionError => _driver.FindElement(By.XPath("//input[@id='input-payment-zone']//following::div[contains(@class,'invalid-feedback')]"));
        public IWebElement PostCodeError => _driver.FindElement(By.XPath("//input[@id='input-payment-postcode']//following::div[contains(@class,'invalid-feedback')]"));
        public IWebElement TersmWarningMessage => _driver.FindElement(By.XPath("//*[@id='form-checkout']/div"));

        public IWebElement ConfirmTablePrice(TablePrice price)
        {
            return _driver.FindElement(By.XPath($"//table[contains(@class,'table-bordered')]//tfoot//strong[contains(text(),'{price.GetEnumDescription()}')]//following::td"));
        }

        public IWebElement AccountOption(Account option)
        {
            return _driver.FindElement(By.XPath($"//*[@id='input-account-{option.GetEnumDescription()}']/following-sibling::label"));
        }

        public IWebElement SelectCheckoutTotalInfo(TablePrice checkout)
        {
            return _driver.FindElement(By.XPath($"//table[@id='checkout-total']//td[contains(text(),'{checkout.GetEnumDescription()}')]//following::strong"));
        }

        public IWebElement UnitPrice(Product product)
        {
            return _driver.FindElement(By.XPath($"//a[contains(text(),'{product.GetEnumDescription()}')]//following::td//button[@data-original-title='Update']//following::td[1]"));
        }

        public IWebElement TotalOnProduct(Product product)
        {
            return _driver.FindElement(By.XPath($"//a[contains(text(),'{product.GetEnumDescription()}')]//following::td//button[@data-original-title='Update']//following::td[2]"));
        }

        public IWebElement QuentityField(Product product)
        {
            return _driver.FindElement(By.XPath($"//div[@id='checkout-cart']//td/a[text()='{product.GetEnumDescription()}']//following::td[@class='text-left']//input"));
        }

        public IWebElement UpdateButton(string productName)
        {
            return _driver.FindElement(By.XPath($"//tbody//td/a[contains(text(),'{productName}')]//following::i[contains(@class,'fa-sync-alt')]"));
        }

        public IWebElement RemoveButton(Product product)
        {
            return _driver.FindElement(RelativeBy.WithLocator(By.TagName("button")).RightOf(UpdateButton(product.GetEnumDescription())));
        }

        public IWebElement CountryOption(Country country)
        {
            return _driver.FindElement(By.XPath($"//*[@id='input-payment-country']/option[contains(text(),'{country.GetEnumDescription()}')]"));
        }

        public IWebElement RegionOption(string option)
        {
            return _driver.FindElement(By.XPath($"//*[@id='input-payment-zone']/option[contains(@value,'{option}')]"));
        }
    }
}