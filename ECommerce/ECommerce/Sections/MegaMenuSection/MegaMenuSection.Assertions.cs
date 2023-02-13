
namespace ECommerce.Pages
{
    public partial class MegaMenuSection
    {
        public void AssertThatCategoryPresentInThePage(Categories category)
        {
            string temp = category.GetEnumDescription();
            Assert.AreEqual(temp, SearchCategoryHeader.Text,
                String.Format(Utils.SEARCH_ERROR, category.GetEnumDescription()));
        }

        public void AssertMenuIsLoaded(string menu)
        {
            var temp = SearchCategoryHeader.Text;
            Assert.That(menu, Is.EqualTo(temp),
                String.Format(Utils.NOT_EQUAL_ERROR, temp, menu));
        }
    }
}