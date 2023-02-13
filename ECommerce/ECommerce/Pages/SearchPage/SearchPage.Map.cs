
namespace ECommerce.Pages
{
    public partial class SearchPage
    {
        public IWebElement InputSearch => _driver.FindElement(By.Id("input-search"));
        public IWebElement SearchButton => _driver.FindElement(By.Id("button-search"));
        public IWebElement SearchValueInResult => _driver.FindElement(By.XPath("//*[contains(@class,'title')]/a"));
     
        public IWebElement SearchCategoryDropDown(int categoryId)
        {
            return _driver.FindElement(By.XPath($"//*[@name='category_id']/option[@value='{categoryId}' and @selected='selected']"));
        }
      
        public IWebElement SearchCriteriaInput(string searchedCriteria)
        {
            return _driver.FindElement(By.XPath($"//input[@id='input-search' and @value='{searchedCriteria}']"));
        }
    }
}