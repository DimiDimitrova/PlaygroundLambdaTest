
namespace ECommerce.Pages
{
    public partial class AccountOrderPage : EShopPage
    {
        public AccountOrderPage(Driver driver)
            : base(driver)
        {
        }

        public void AccountOrderHistoryIsDisplayed()
        {
            Assert.AreEqual(BreadcrumbSection.PageTitle.Text, "Order History",Utils.ORDER_HISTORY_ERROR);
            Assert.IsTrue(OrderTable.Displayed);
            Assert.IsNotEmpty(OrderTable.Text);
        }
    }
}