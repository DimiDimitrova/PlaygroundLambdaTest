
namespace ECommerce
{
    public partial class AccountVoucherPage
    {
        private const int RECIPIENT_NAME_MIN_SIZE = 1;
        private const int RECIPIENT_NAME_MAX_SIZE = 64;

        public void AssertPurchaseVoucherFailedWithIncorrectRecipientName()
        {
            Assert.IsTrue(BreadcrumbSection.ActivePageTitle.Text.Equals("Gift Certificate"),
                String.Format(Utils.RECIPIENT_NAME_ERROR, RECIPIENT_NAME_MIN_SIZE, RECIPIENT_NAME_MAX_SIZE));
            Assert.IsTrue(ErrorMessage.Text.Equals(String.Format(Utils.RECIPIENT_NAME_ERROR, RECIPIENT_NAME_MIN_SIZE, RECIPIENT_NAME_MAX_SIZE)));
        }

        public void AssertPurchaseVoucherFailedWithIncorrectRecipientName(string name)
        {
            Assert.IsTrue(BreadcrumbSection.ActivePageTitle.Text.Equals("Gift Certificate"),
                String.Format(Utils.PURCHASE_GIFT_MESSAGE, name.Length) + " " +
                String.Format(Utils.RECIPIENT_NAME_ERROR, RECIPIENT_NAME_MIN_SIZE, RECIPIENT_NAME_MAX_SIZE));
            Assert.IsTrue(ErrorMessage.Text.Equals(String.Format(Utils.RECIPIENT_NAME_ERROR, RECIPIENT_NAME_MIN_SIZE, RECIPIENT_NAME_MAX_SIZE)));
        }

        public void AssertPurchaseVoucherFailedWithInvalidEmail(string email)
        {
            Assert.IsTrue(ErrorMessage.Text.Equals(Utils.EMAIL_ERROR), String.Format(Utils.PURCHASE_GIFT_MESSAGE, email.Length));
        }
    }
}