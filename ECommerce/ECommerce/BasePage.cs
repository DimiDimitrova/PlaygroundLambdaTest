
namespace ECommerce
{
    public class BasePage : DriverFacade
    {
        public BasePage(Driver driver) 
            : base(driver)
        {
        }

        public override void Start(Browser browser)
        {
            Console.WriteLine($"Start browser = {Enum.GetName(typeof(Browser), browser)}");
            Driver?.Start(browser);
        }

        public override void Quit()
        {
            Console.WriteLine("Close browser");
            Driver?.Quit();
        }

        public override void GoToUrl(string url)
        {
            Console.WriteLine($"Go to URL = {url}");
            Driver?.GoToUrl(url);
        }

        public override IWebElement FindElement(By locator)
        {
            Console.WriteLine("Find Element");
            return Driver?.FindElement(locator);
        }

        public override List<IWebElement> FindElements(By locator)
        {
            Console.WriteLine("Find elements");
            return Driver?.FindElements(locator);
        }

        public override string GetCurrentUrl()
        {
            return base.GetCurrentUrl();
        }

        public override void ScrollToVisible(IWebElement element)
        {
            try
            {
                ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true)", element);
            }
            catch (ElementNotInteractableException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}