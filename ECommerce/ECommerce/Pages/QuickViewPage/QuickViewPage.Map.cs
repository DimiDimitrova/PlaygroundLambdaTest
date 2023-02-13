
namespace ECommerce.Pages
{
    public partial class QuickViewPage
    {
        public IWebElement QuickViewQuantityInput => _driver.FindElement(By.XPath("//*[contains(@class,'content-quantity')]//input"));
        public IWebElement DecreaseQtyButton => _driver.FindElement(By.XPath("//button[contains(@aria-label,'Decrease quantity')]"));
        public IWebElement IncreaseQtyButton => _driver.FindElement(By.XPath("//button[contains(@aria-label,'Increase quantity')]"));
    }
}