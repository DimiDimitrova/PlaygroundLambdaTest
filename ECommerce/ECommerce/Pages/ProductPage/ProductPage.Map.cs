
namespace ECommerce.Pages
{
    public partial class ProductPage
    {
        public IWebElement ProductName => _driver.FindElement(By.XPath("//div[contains(@class,'content-title')]//h1"));
        public IWebElement ProductImage => _driver.FindElement(By.XPath("//div[contains(@class,'image-gallery')]//img"));
        public IWebElement ProductPrice => _driver.FindElement(By.XPath("//div[@class='price']/h3"));
        public IWebElement CompareButton => _driver.FindElement(By.XPath("//button[contains(text(),'Compare this Product')]"));
        public IWebElement AddToCartItemButton => _driver.FindElement(By.XPath("//div[contains(@class,'entry-row')]//button[contains(@class,'button-cart')]"));
        public IWebElement BuyNowButton => _driver.FindElement(By.XPath("//button[contains(@class,'button-buynow')]"));
        public IWebElement CompareLink => _driver.FindElement(By.XPath("//*[contains(text(),'Success: You have added ')]//a[contains(@href,'compare')]"));
        public IWebElement ListViewButton => _driver.FindElement(By.Id("list-view"));

        public IWebElement CartButton(Product product)
        {
            return _driver.FindElement(By.XPath($"//*[@title='{product.GetEnumDescription()}']//following::button[contains(@class,'btn-cart')]"));
        }

        public IWebElement QuickViewButton(Product product)
        {
            return _driver.FindElement(By.XPath($"//div[@class='carousel-item active']//*[@title='{product.GetEnumDescription()}']//following::button[contains(@class,'btn-quick-view')]"));
        }

        public IWebElement WishListButton(Product product)
        {
            return _driver.FindElement(By.XPath($"//div[@class='carousel-item active']//*[@title='{product.GetEnumDescription()}']//following::button[contains(@class,'btn-wishlist')]"));
        }

        public IWebElement ImageItem(Product productName)
        {
            return _driver.FindElement(By.XPath($"//div[@class='carousel-item active']//*[@title='{productName.GetEnumDescription()}']"));
        }

        public IWebElement ProductExtraContent(ExtraProductContent label)
        {
            return _driver.FindElement(By.XPath($"//*[contains(@class,'content-extra')]//span[contains(text(),'{label.GetEnumDescription()}')]//following::span"));
        }
    }
}