
namespace ECommerce.Pages
{
    public partial class SuccessPage : EShopPage
    {
        private AccountPage AccountPage;
        public SuccessPage(Driver driver)
            : base(driver)
        {
            AccountPage = new AccountPage(driver);
        }

        public void AssertThatRegistrationIsMade()
        {
            Assert.That(AccountPage.AccountNavbar(Navbar.Logout).Enabled,
                String.Format(Utils.NOT_EXISTS_IN_PAGE_ERROR,Navbar.Logout.ToString()));
        }

        public void AsserThatPurchaseIsMade()
        {
            _driver.WaitForAjax();
            Assert.That(BreadcrumbSection.PageTitle.Text.Equals(Utils.ORDER_IS_MADE),
                String.Format(Utils.NOT_EQUAL_ERROR, BreadcrumbSection.PageTitle.Text, Utils.ORDER_IS_MADE));
        }

        public void AssertVoucherIsPurchased()
        {
            Assert.That(BreadcrumbSection.PageTitle.Text.Equals(Utils.PURCHASE_GIFT_IS_MADE),
                String.Format(Utils.NOT_EQUAL_ERROR, BreadcrumbSection.PageTitle.Text, Utils.PURCHASE_GIFT_IS_MADE));
        }

        public void AssertAccountIsUpdated()
        {
            Assert.IsTrue(BreadcrumbSection.ActivePageTitle.Text.Equals(PageTitle.Account.ToString()),
                String.Format(Utils.PAGE_ERROR, PageTitle.Account.ToString()));

            Assert.That(Utils.UPDATED_ACCOUNT_MESSAGE, Is.EqualTo(AlertMessage.Text),
                String.Format(Utils.NOT_EQUAL_ERROR, AlertMessage.Text, Utils.UPDATED_ACCOUNT_MESSAGE));
        }
    }
}