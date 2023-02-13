
namespace ECommerce.Pages
{
    public partial class AccountOrderPage
    {
        public IWebElement OrderTable => _driver.FindElement(By.XPath("//*[@id='content']//table[contains(@class,'table-bordered')]"));
    }
}