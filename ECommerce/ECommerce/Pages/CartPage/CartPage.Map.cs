
namespace ECommerce.Pages
{
    public partial class CartPage
    {
        public IWebElement CartButton => _driver.FindElement(By.XPath("//a[contains(@href,'checkout/cart')]"));
        public IWebElement CheckoutButton => _driver.FindElement(By.XPath("//a[contains(text(),'Checkout')]"));
        public IWebElement CheckoutButtonInCart => _driver.FindElement(By.XPath("//a[contains(@href,'checkout/checkout')]"));
        public IWebElement ContinueShoppingButton => _driver.FindElement(By.XPath("//a[contains(text(),'Continue Shopping')]"));
        public IWebElement ShoppingCartIsEmptyMessage => _driver.FindElement(By.XPath("//h1[contains(@Class,'page-title')]/following-sibling::p"));
        public List<IWebElement> TableRowsContent => _driver.FindElements(By.XPath("//div[@class='table-responsive']//tbody//tr"));
        public IWebElement ViewCartButton => _driver.FindElement(By.XPath("//a[contains(text(),'View Cart')]"));
        public IWebElement CountryShippingTaxes => _driver.FindElement(By.Id("input-country"));
        public IWebElement RegionStateShippingTaxes => _driver.FindElement(By.Id("input-zone"));
        public IWebElement PostCodeSatateShippingTaxes => _driver.FindElement(By.Id("input-postcode"));
        public IWebElement GetQuotesButton => _driver.FindElement(By.Id("button-quote"));
        public IWebElement FlatRate => _driver.FindElement(By.XPath("//div[@class='form-check']//input"));
        public IWebElement FlatRateInformation => _driver.FindElement(By.XPath("//div[@class='form-check']//label"));
        public IWebElement ApplyShippingButton => _driver.FindElement(By.Id("button-shipping"));
        public IWebElement CountryInvalidMessage => _driver.FindElement(By.XPath("//*[@id='input-country']//following::div[@class='invalid-feedback']"));
        public IWebElement RegionInvalidMessage => _driver.FindElement(By.XPath("//*[@id='input-zone']//following::div[@class='invalid-feedback']"));
        public IWebElement PostCodeInvalidMessage => _driver.FindElement(By.XPath("//*[@id='input-postcode']//following::div[@class='invalid-feedback']"));
        public IWebElement VoucherInput => _driver.FindElement(By.Id("input-voucher"));
        public IWebElement ApplyGiftCertificate => _driver.FindElement(By.Id("button-voucher"));
        public IWebElement GiftError => _driver.FindElement(By.XPath("//*[contains(@class,'fa-exclamation-circle')]"));
        public List<IWebElement> TableProductQuantity => _driver.FindElements(By.XPath("//div[@class='table-responsive']//tbody//tr//div"));
        public List<IWebElement> TableProductUnitPrice => _driver.FindElements(By.XPath("//div[@class='table-responsive']//tbody//tr//td[@class='text-right'][1]"));

        public IWebElement RegionOption(string option = "None")
        {
            return _driver.FindElement(By.XPath($"//*[@id='input-zone']/option[contains(@value,'{option}')]"));
        }

        public IWebElement CountryOption(Country country)
        {
            return _driver.FindElement(By.XPath($"//*[@id='input-country']/option[contains(text(),'{country.GetEnumDescription()}')]"));
        }

        public IWebElement UpdateButton(string productName)
        {
            return _driver.FindElement(By.XPath($"//tbody//td/a[contains(text(),'{productName}')]//following::i[contains(@class,'fa-sync-alt')]"));
        }

        public IWebElement UnitPrice(string product)
        {
            return _driver.FindElement(By.XPath($"//*[contains(@class,'table-bordered')]//a[contains(text(),'{product}')]//following::td[3]"));
        }

        public IWebElement Total(string product)
        {
            return _driver.FindElement(By.XPath($"//*[contains(@class,'table-bordered')]//a[contains(text(),'{product}')]//following::td[4]"));
        }

        public IWebElement RemoveButton(string product)
        {
            return _driver.FindElement(RelativeBy.WithLocator(By.TagName("button")).RightOf(UpdateButton(product)));
        }

        public IWebElement QuentityField(string product)
        {
            return _driver.FindElement(RelativeBy.WithLocator(By.TagName("input")).LeftOf(UpdateButton(product)));
        }

        public IWebElement OpenAccordion(CartAccordion cartAccordion)
        {
            return _driver.FindElement(By.XPath($"//*[contains(text(),'{cartAccordion.GetEnumDescription()}')]/*[contains(@Class,'fa-plus')]"));
        }

        public IWebElement SelectCartPriceInformation(TablePrice name)
        {
            return _driver.FindElement(By.XPath($"//table[contains(@class,'m-0')]//td[contains(text(),'{name.GetEnumDescription()}')]//following::strong"));
        }

        public IWebElement SelectCartTotalInfo(TablePrice price)
        {
            return _driver.FindElement(By.XPath($"//table[contains(@class,'table-bordered')]//td[text()='{price.GetEnumDescription()}']//following-sibling::td[@class='text-right']/strong"));
        }
    }
}