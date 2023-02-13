
namespace ECommerce.Pages
{
    public partial class ConfirmPage
    {
        public IWebElement ConfirmOrderButton => _driver.FindElement(By.Id("button-confirm"));
        public IWebElement PaymentTable => _driver.FindElement(By.XPath("//*[text()='Payment Address']//following::*[@Class='card-body']"));
        public IWebElement ShippingAddressTable => _driver.FindElement(By.XPath("//*[text()='Payment Address']//following::*[@Class='card-body'][2]"));
        public IWebElement TotalSum => _driver.FindElement(By.XPath("//div[@id='content']//child::tfoot//strong[text()='Total:']/following::td"));
        public IWebElement EditButton => _driver.FindElement(By.XPath("//*[contains(@class,'fa-caret-left')]//parent::a"));
        public List<IWebElement> ProductsContent => _driver.FindElements(By.XPath("//div[@id='content']//table/tbody/tr"));
        public List<IWebElement> QuantityContent => _driver.FindElements(By.XPath("//div[@id='content']//table/tbody/tr/td[3]"));
        public List<IWebElement> UnitPriceContent => _driver.FindElements(By.XPath("//div[@id='content']//table/tbody/tr/td[4]"));

        public IWebElement ProductQuantity(Product product)
        {
            return _driver.FindElement(By.XPath($"//div[@id='content']//table/tbody/tr/td[contains(text(),'{product.GetEnumDescription()}')]//following::td[2]"));
        }

        public IWebElement ConfirmPrice(TablePrice price)
        {
            return _driver.FindElement(By.XPath($"//div[@id='content']//child::tfoot//strong[contains(text(),'{price.GetEnumDescription()}')]/following::td"));
        }
    }
}