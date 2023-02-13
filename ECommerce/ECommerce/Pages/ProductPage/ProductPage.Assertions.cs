
namespace ECommerce.Pages
{
    public partial class ProductPage
    {
        public void AssertProductInformationIsDisplayed()
        {
            Assert.IsTrue(ProductName.Displayed,String.Format(Utils.NOT_EXISTS_IN_PAGE_ERROR,ProductName.Text));
            Assert.IsTrue(ProductPrice.Displayed, String.Format(Utils.NOT_EXISTS_IN_PAGE_ERROR, ProductPrice.Text));
            Assert.IsTrue(ProductExtraContent(ExtraProductContent.Availability).Displayed);
            Assert.IsTrue(ProductExtraContent(ExtraProductContent.ProductCode).Displayed);
        }

        public void AssertProductPageIsOpen()
        {
            Assert.IsFalse(_driver.GetCurrentUrl().EndsWith("#"),Utils.REFRESH_PAGE_ERROR);
            Assert.IsTrue(_driver.GetCurrentUrl().EndsWith("product"));
        }
    }
}