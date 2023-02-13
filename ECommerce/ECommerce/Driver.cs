
namespace ECommerce
{
    public abstract class Driver
    {
        public abstract void Start(Browser browser);
        public abstract void Quit();
        public abstract void GoToUrl(string url);
        public abstract IWebElement FindElement(By locator);
        public abstract List<IWebElement> FindElements(By locator);
        public abstract void WaitForAjax();
        public abstract void WaitUntilPageLoadsCompletely();
        public abstract void MoveToElement(IWebElement element);
        public abstract string GetCurrentUrl();
        public abstract void ScrollToVisible(IWebElement element);
    }
}