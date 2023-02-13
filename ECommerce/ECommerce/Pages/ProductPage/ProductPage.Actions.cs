
namespace ECommerce.Pages
{
    public partial class ProductPage : EShopPage
    {
        private QuickViewPage QuickViewPage;

        public ProductPage(Driver driver)
            : base(driver)
        {
            QuickViewPage = new QuickViewPage(driver);
        }

        public void BuyNow()
        {
            BuyNowButton.Click();
            _driver.WaitForAjax();
        }

        public void AddItemToCart(Product product, int quantity = 2)
        {
            SelectQuickView(product);
            QuickViewPage.SetQuantityOnItem(quantity);
            AddToCartItemButton.Click();
        }

        public void AddItemToCartDirectly(Product product)
        {
            _driver.WaitForAjax();
            CartButton(product).Click();
        }

        public void SelectQuickView(Product productName)
        {
            _driver.MoveToElement(ImageItem(productName));
            _driver.WaitForAjax();
            QuickViewButton(productName).Click();
        }

        public ProductInfo SetProductInfo()
        {
            ProductInfo product = new ProductInfo();
            product.ProductName = ProductName.Text;
            product.Price = ProductPrice.Text;
            product.ImageName = ProductImage.GetAttribute("title");
            product.Model = ProductExtraContent(ExtraProductContent.ProductCode).Text;
            product.Availability = ProductExtraContent(ExtraProductContent.Availability).Text;

            return product;
        }

        public ProductInfo AddProductForCompare(Product product)
        {
            ImageItem(product).Click();

            var temp = SetProductInfo();
            CompareButton.Click();

            return temp;
        }
    }
}