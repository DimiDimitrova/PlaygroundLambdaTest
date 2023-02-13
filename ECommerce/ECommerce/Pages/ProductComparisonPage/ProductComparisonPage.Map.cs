
namespace ECommerce.Pages
{
    public partial class ProductComparisonPage
    {
        public IWebElement ModifiedMessage => _driver.FindElement(By.XPath("//*[contains(@class,'alert-success')]"));
        public IWebElement EmptyCompareContext => _driver.FindElement(By.XPath("//*[@id='content']/p"));

        public List<IWebElement> TableRowsContent
        {
            get
            {
                return _driver.FindElements(By.XPath("//table[contains(@class,'table-responsive')]//tbody[1]/tr"));
            }
        }

        public IWebElement ProductName(string productName)
        {
            return _driver.FindElement(By.XPath($"//*[@id='content']/table/tbody[1]/tr[1]//a/strong[contains(text(),'{productName}')]"));
        }

        public IWebElement AddToCart(string productName)
        {
            return _driver.FindElement(RelativeBy.WithLocator(By.TagName("input")).Below(ProductName(productName)));
        }

        public IWebElement RemoveButton(string productName)
        {
            return _driver.FindElement(RelativeBy.WithLocator(By.TagName("a")).Below(AddToCart(productName)));
        }      
    }
}