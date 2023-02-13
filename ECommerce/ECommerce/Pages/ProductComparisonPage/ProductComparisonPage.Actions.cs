
namespace ECommerce.Pages
{
    public partial class ProductComparisonPage : EShopPage
    {
        public ProductComparisonPage(Driver driver) 
            : base(driver)
        {
        }

        public List<string> TableContent()
        {
            var list = new List<string>();

            foreach (var item in TableRowsContent)
            {
                foreach (var cell in item.FindElements(By.TagName("td")))
                {
                    list.Add(cell.Text);
                }
            }

            return list;
        }
    }
}