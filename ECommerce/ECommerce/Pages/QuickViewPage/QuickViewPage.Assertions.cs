
namespace ECommerce.Pages
{
    public partial class QuickViewPage
    {
        public void AssertIncreaseQuantityButtonWork(int expectedQuantity)
        {
            Assert.AreEqual(expectedQuantity.ToString(), QuickViewQuantityInput.GetAttribute("value"));
        }

        public void AssertDecreaseQuantityButtonWork(int expectedQuantity)
        {
            Assert.AreEqual(expectedQuantity.ToString(), QuickViewQuantityInput.GetAttribute("value"));
        }
    }
}