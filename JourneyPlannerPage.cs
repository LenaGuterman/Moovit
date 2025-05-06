namespace Moovit;
using OpenQA.Selenium;

public class JourneyPlannerPage
{
    private readonly IWebDriver driver;
    public JourneyPlannerPage(IWebDriver driver)
    {
        this.driver = driver;
    }

    public IWebElement startPoint => driver.FindElement(By.Id("from-input"));
    public IWebElement destination => driver.FindElement(By.Id("to-input"));
    public IWebElement searchBtn => driver.FindElement(By.XPath("//button[@title='Search']"));

    public By suggestedRouts => By.ClassName("suggested-routes-content");

    public void enterTripPoints(string startPointName, string destinationName)
    {
        Helpers.NoAdSendKeys(driver,startPoint,startPointName);
        startPoint.SendKeys(Keys.ArrowDown);
        startPoint.SendKeys(Keys.Enter);
        Helpers.NoAdSendKeys(driver,startPoint,destinationName);
        destination.SendKeys(Keys.ArrowDown);
        destination.SendKeys(Keys.Enter);
        Helpers.NoAdClick(driver,searchBtn);
    }
}