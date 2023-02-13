
namespace ECommerce.Pages
{
    public partial class HomePage
    {
        public IWebElement SearchInput => _driver.FindElement(By.XPath("//div[@id='search']//child::input[@name='search']"));
        public IWebElement AllCategoriesDropDown => _driver.FindElement(By.XPath("//*[contains(@id,'search')]//button[contains(@class,'dropdown-toggle')]"));
        public IWebElement SearchButton => _driver.FindElement(By.XPath("//*[@id='search']/child::div[@class='search-button']/button"));       
        public IWebElement ShopByCategoryButton => _driver.FindElement(By.XPath("//div[@data-id='217832']/a"));
        public IWebElement MegaMenuButton => _driver.FindElement(By.XPath("//span[contains(text(),' Mega Menu')]//parent::div//parent::a"));        
        public IWebElement SuccessMessageForAddItem => _driver.FindElement(By.XPath("//p[contains(text(),'Success: You have added')]"));
        public IWebElement ShopNowButton => _driver.FindElement(By.XPath("//*[contains(text(),'SHOP NOW')]"));

        public IWebElement GetCategoryById(int id)
        {
            return _driver.FindElement(By.XPath($"//a[@data-category_id='{id}']"));
        }

        public IWebElement GetTopCategoryByName(string name)
        {
            _driver.WaitUntilPageLoadsCompletely();
            return _driver.FindElement(By.XPath($"//span[contains(text(),'{name}')]//parent::div//parent::a"));
        }

        public IWebElement GetMenuName(string section, string menu)
        {
            return _driver.FindElement(By.XPath($"//h3[contains(text(),'{section}')]//following-sibling::div/ul/li/a[@title='{menu}']"));
        }

        public IWebElement InformationForModul(HomePageModulTitle title)
        {
            return _driver.FindElement(By.XPath($"//h3[@class='module-title' and (contains(text(),'{title.GetEnumDescription()}'))]"));
        }
    }
}