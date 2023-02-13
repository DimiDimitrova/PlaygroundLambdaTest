
namespace ECommerce
{
    public abstract class DriverFacade : Driver
    {
        public Driver Driver;

        protected DriverFacade(Driver driver)
        {
            Driver = driver;
        }

        public override void Start(Browser browser)
        {
            Driver?.Start(browser);
        }

        public override void Quit()
        {
            Driver?.Quit();
        }

        public override void GoToUrl(string url)
        {
            Driver?.GoToUrl(url);
        }

        public override IWebElement FindElement(By locator)
        {
            return Driver?.FindElement(locator);
        }

        public override List<IWebElement> FindElements(By locator)
        {
            return Driver?.FindElements(locator);
        }

        public override void WaitForAjax()
        {
            Driver?.WaitForAjax();
        }

        public override void WaitUntilPageLoadsCompletely()
        {
            Driver?.WaitUntilPageLoadsCompletely();
        }

        public override void MoveToElement(IWebElement element)
        {
            Driver?.MoveToElement(element);
        }

        public override string GetCurrentUrl()
        {
            return Driver?.GetCurrentUrl();
        }

        public override void ScrollToVisible(IWebElement element)
        {
            try
            {
                ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
            }
            catch (ElementNotInteractableException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}