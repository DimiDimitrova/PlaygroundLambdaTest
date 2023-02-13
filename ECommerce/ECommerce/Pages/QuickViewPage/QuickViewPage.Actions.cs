
namespace ECommerce.Pages
{
    public partial class QuickViewPage : EShopPage
    {
        public QuickViewPage(Driver driver)
            : base(driver)
        {
        }

        public void SetQuantityOnItem(int quantity)
        {
            string temp = quantity.ToString();
            _driver.WaitUntilPageLoadsCompletely();
            QuickViewQuantityInput.Clear();
            QuickViewQuantityInput.SendKeys(temp);
        }
    }
}