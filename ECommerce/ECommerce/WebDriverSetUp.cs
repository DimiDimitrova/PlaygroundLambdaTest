using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace ECommerce
{
    public class WebDriverSetUp : Driver
    {
        private IWebDriver _webDriver;
        private WebDriverWait _webDriverWait;

        public override void Start(Browser browser)
        {
            switch (browser)
            {
                case Browser.Chrome:
                new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.Latest);
                _webDriver = new ChromeDriver();
                _webDriverWait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(30));
                _webDriver.Manage().Window.Maximize();
                break;
                default:
                throw new ArgumentOutOfRangeException(nameof(browser), browser, null);
            }
        }

        public override void Quit()
        {
            _webDriver.Quit();
        }

        public override void GoToUrl(string url)
        {
            _webDriver.Navigate().GoToUrl(url);
        }

        public override IWebElement FindElement(By locator)
        {
            var element = _webDriverWait.Until(ExpectedConditions.ElementExists(locator));
            ScrollToVisible(element);
            return element;
        }

        public override List<IWebElement> FindElements(By locator) => _webDriver.FindElements(locator).ToList();

        public override void WaitForAjax()
        {
            var js = (IJavaScriptExecutor)_webDriver;
            _webDriverWait.Until(wd => js.ExecuteScript("return jQuery.active").ToString() == "0");
        }

        public override void WaitUntilPageLoadsCompletely()
        {
            var js = (IJavaScriptExecutor)_webDriver;
            _webDriverWait.Until(wd => js.ExecuteScript("return document.readyState").ToString() == "complete");
        }

        public override void ScrollToVisible(IWebElement element)
        {
            try
            {
                ((IJavaScriptExecutor)_webDriver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
            }
            catch (ElementNotInteractableException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public override void MoveToElement(IWebElement element)
        {
            Actions action = new Actions(_webDriver);
            action.MoveToElement(element).Perform();
        }

        public override string GetCurrentUrl()
        {
            return _webDriver?.Url;
        }
    }
}