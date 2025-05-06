using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Moovit;

public class Helpers
{
    public static void WaitUntilElementDisplayed(IWebDriver driver,By locator )
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        wait.Until(d =>
        {
            try
            {
                var element = d.FindElement(locator);
                return element.Displayed; // Returns true when element is visible

            }
            catch (NoSuchElementException)
            {
                return false; // If element is not found, keep waiting
            }
        });
    }
    public static void WaitUntilPageLoaded(IWebDriver driver, int timeoutInSeconds = 10)
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
        wait.Until(driver => ((IJavaScriptExecutor)driver)
            .ExecuteScript("return document.readyState").ToString() == "complete");
    }
    public static void ChooseSelectBox(IWebDriver driver,By selectBoxLocator, string option)
    {
        WebDriverWait wait;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        var selectBoxField = wait.Until(drv => drv.FindElement(selectBoxLocator));
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({behavior: 'smooth', block: 'center'});", selectBoxField);
        var selectBox = new SelectElement(selectBoxField);
        selectBoxField.Click();
        selectBox.SelectByText(option);
        // Click somewhere else
        IWebElement body = driver.FindElement(By.TagName("body"));
        body.Click();
    }

    public static void SafeClick(IWebDriver driver,IWebElement element)
    {
        AcceptCookiesIfPresent(driver); 
        element.Click();    
    }

    public static void SafeSendKeys(IWebDriver driver,IWebElement element, string text)
    {
        AcceptCookiesIfPresent(driver);
        element.SendKeys(text);
    }
    
    private static void AcceptCookiesIfPresent(IWebDriver driver)
    {
        try
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            var acceptButtons = driver.FindElements(By.Id("onetrust-accept-btn-handler"));
            if (acceptButtons.Count > 0 && acceptButtons[0].Displayed && acceptButtons[0].Enabled)
            {
                var acceptButton = acceptButtons[0];

                // Scroll into view and click
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", acceptButton);
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", acceptButton);
            }
        }
        catch (WebDriverTimeoutException)
        {
            Console.WriteLine("No cookies banner");
        }
    }
    
    private static void CloseAdIfPresent(IWebDriver driver)
    {
        try
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            var closeButton = wait.Until(driver => 
                driver.FindElements(By.CssSelector(".popup-close, .modal-close, .close-button"))
                    .FirstOrDefault(e => e.Displayed));
            closeButton?.Click();
        }
        catch (WebDriverTimeoutException)
        {
            Console.WriteLine("No ad");
        }
    }
    
    public static void NoAdClick(IWebDriver driver,IWebElement element)
    {
        CloseAdIfPresent(driver); 
        element.Click();    
    }

    public static void NoAdSendKeys(IWebDriver driver,IWebElement element, string text)
    {
        CloseAdIfPresent(driver);
        element.Clear();
        element.SendKeys(text);
    }
    
    public class ConfigReader
    {
        private static readonly string configFilePath = "appsettings.json"; // Ensure this is the correct path
        
        public static TestSettings LoadConfig()
        {
            string json = File.ReadAllText(configFilePath);
            var settings = JsonConvert.DeserializeObject<Configuration>(json);
            return settings.TestSettings;
        }

        public class Configuration
        {
            public TestSettings TestSettings { get; set; } = new TestSettings(); 
        }

        public class TestSettings
        {
            public string Url { get; set; } = string.Empty; 
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public string Country { get; set; } = string.Empty;
            public string DeparturePoint { get; set; } = string.Empty;
            public string ArrivalPoint { get; set; } = string.Empty;
            public string StartPoint { get; set; } = string.Empty;
            public string Destination { get; set; } = string.Empty;
        }
    }
}