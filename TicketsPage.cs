using System.Globalization;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Moovit;

public class TicketsPage
{
    private readonly IWebDriver driver;
    public TicketsPage(IWebDriver driver)
    {
        this.driver = driver;
    }
    public IWebElement distributionSearch => driver.FindElement(distributionSearchBy);
    public By distributionSearchBy => By.XPath("//div[@class='content-wrapper']/div[@id='distribusion-search']");
    public string departureBy = "input[data-tag='departure']";
    public string arrivalSelector = "input[data-tag='arrival']";
    public string submitSelector = "button[type='submit']";
    public By journeyList => By.XPath("//div[@class='journey-list__cards']");
    public By virtualList => By.XPath("//div[@class='virtual-list__item']");
    public IWebElement departueFieldResults => driver.FindElement(By.XPath("//input[@class='ui-input' and @data-tag='departure']"));
    public IWebElement arrivalFieldResults => driver.FindElement(By.XPath("//input[@class='ui-input' and @data-tag='arrival']"));

    public IWebElement switchBtn => driver.FindElement(By.XPath("//span[contains(text(),'return')]"));
    public IWebElement passNumber => driver.FindElement(By.XPath("//div[@class='passenger-dropdown__description']"));

    public IWebElement passPlus => driver.FindElement(By.XPath("//span[contains(text(),'plus')]"));
    public IWebElement resultsSubmitBtn => driver.FindElement(By.XPath("//button[@type='submit']"));

    public IWebElement firstRideOptPrice => driver.FindElement(By.XPath("//div[@class='journey-card__footer-price-total']"));
    public By CcyBy => By.XPath("//div[contains(text(), '€')]");

    public double GetRidePrice(IWebElement priceElement)
    {
        string priceText = priceElement.Text.Trim();
        string numberPart = priceText.Substring(1);
        if (numberPart.Contains(","))
        {
            numberPart = numberPart.Replace(",", ".");
        }
        double price = double.Parse(numberPart, CultureInfo.InvariantCulture);
        return price;
    }
    
    public void GoToInsideIframe(IWebDriver driver)
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        IWebElement iframe = wait.Until(d => d.FindElement(By.TagName("iframe")));

        // Get the iframe source URL and Naviagate there
        string iframeSrc = iframe.GetAttribute("src");
        
        driver.Navigate().GoToUrl(iframeSrc);
        
        Helpers.WaitUntilElementDisplayed(driver,CcyBy);
    }
    
    public string ShadowElementValue(string selector)
    {
        IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
        ShadowRoot shadowRoot = (ShadowRoot)js.ExecuteScript("return arguments[0].shadowRoot", distributionSearch);
        IWebElement shadowInputField = (IWebElement)js.ExecuteScript(
            "return arguments[0].querySelector(arguments[1])", shadowRoot, selector);
        selector = shadowInputField.GetAttribute("value");
        
        return selector;
    }
    

    public void EnterToShadowInputField(string text, string selector)
    {
        IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
        ShadowRoot shadowRoot = (ShadowRoot)js.ExecuteScript("return arguments[0].shadowRoot", distributionSearch);
        IWebElement shadowInputField = (IWebElement)js.ExecuteScript(
            "return arguments[0].querySelector(arguments[1])", shadowRoot, selector);
        shadowInputField.Clear();
        shadowInputField.SendKeys(text);
        shadowInputField.Click();
        Thread.Sleep(2000);
        shadowInputField.SendKeys(Keys.ArrowDown);
        shadowInputField.SendKeys(Keys.Enter);
        Assert.IsTrue(shadowInputField.GetAttribute("value").Contains(text));
    }

    public void ClickOnShadowElement(string element)
    {
        IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
        ShadowRoot shadowRoot = (ShadowRoot)js.ExecuteScript("return arguments[0].shadowRoot", distributionSearch);
        IWebElement shadowElement = (IWebElement)js.ExecuteScript(
            "return arguments[0].querySelector(arguments[1])", shadowRoot, element);
        shadowElement.Click();
    }
}