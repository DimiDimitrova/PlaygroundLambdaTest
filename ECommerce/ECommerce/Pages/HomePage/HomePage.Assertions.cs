
namespace ECommerce.Pages
{
    public partial class HomePage
    {
        public void AssertCorrectPageIsOpen(string page)
        {
            string temp = page.Split(" ")[0];
            Assert.AreEqual(temp, BreadcrumbSection.ActivePageTitle.Text, "Page does not exist!");
        }

        public void AssertThatItemIsAddedToCartMessageAppeared()
        {
            Assert.That(SuccessMessageForAddItem.Displayed);
        }

        public void AssertModuleInformationPresent(HomePageModulTitle modul)
        {
            Assert.That(InformationForModul(modul).Displayed,Utils.HOME_PAGE_MODUL_ERROR);
        }

        public void AssertHomePageIsLoaded()
        {
            Assert.IsTrue(_driver.GetCurrentUrl().EndsWith("common/home"));
        }
    }
}