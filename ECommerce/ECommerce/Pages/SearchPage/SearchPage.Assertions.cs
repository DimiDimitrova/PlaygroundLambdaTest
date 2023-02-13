
namespace ECommerce.Pages
{
    public partial class SearchPage
    {
        public void AssertBySearchedCategory(CategoryInSearchBox category)
        {
            Assert.That(SearchCategoryDropDown((int)category).Displayed,
                String.Format(Utils.SEARCH_ERROR,category.ToString()));
        }

        public void AssertBySearchedManufacturer(Brand manufacturer)
        {
            Assert.That(SearchCriteriaInput(manufacturer.ToString()).Displayed,
                String.Format(Utils.SEARCH_ERROR,manufacturer.ToString()));
        }

        public void AssertBySearchedInput(string input)
        {
            Assert.IsTrue(SearchValueInResult.Text.ToLower().Contains(input),
                Utils.SEARCH_VALUE_ERROR);
        }
    }
}