
namespace ECommerce.Sections
{
    public partial class BreadcrumbSection
    {
        public void AssertMenuIsOpen(string expectedResult)
        {
            Assert.IsTrue(ActivePageTitle.Text.Equals(expectedResult),Utils.PAGE_ERROR);
        }
    }
}