
namespace ECommerce.Pages
{
    public partial class SearchPage : EShopPage
    {
        public SearchPage(Driver driver)
            : base(driver)
        {

        }

        public void SearchByKeywords(string keywords)
        {
            InputSearch.SendKeys(keywords);
            SearchButton.Click();
        }
    }
}