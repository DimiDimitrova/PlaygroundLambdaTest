
namespace ECommerce.Pages
{
    public partial class ProductComparisonPage
    {
        public void ProductIsInComparePage(List<ProductInfo> product)
        {
            var temp = TableContent();
            Assert.IsNotNull(temp);

            for (int i = 0; i < product.Count; i++)
            {
                Assert.IsTrue(temp.Contains(product[i].Price),
                    String.Format(Utils.NOT_EXISTS_IN_PAGE_ERROR, product[i].Price));
                Assert.IsTrue(temp.Contains(product[i].ProductName), 
                    String.Format(Utils.NOT_EXISTS_IN_PAGE_ERROR, product[i].ProductName));
                Assert.IsTrue(temp.Contains(product[i].Availability), 
                    String.Format(Utils.NOT_EXISTS_IN_PAGE_ERROR, product[i].Availability));
                Assert.IsTrue(temp.Contains(product[i].Model), 
                    String.Format(Utils.NOT_EXISTS_IN_PAGE_ERROR, product[i].Model));
            }
        }

        public void AssertProductIsRemoved()
        {
            Assert.IsTrue(EmptyCompareContext.Displayed);
            Assert.IsTrue(ModifiedMessage.Displayed);
        }
    }
}