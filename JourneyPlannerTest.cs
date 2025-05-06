namespace Moovit;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

//[TestFixture]
public class JourneyPlannerTest: BaseTest
{
    public JourneyPlannerPage journeyPage;

    //[Test]
    public void JourneySearchTest()
    {
        journeyPage = new JourneyPlannerPage(driver);
        
        journeyPage.enterTripPoints(config.StartPoint, config.Destination);
        Helpers.WaitUntilElementDisplayed(driver,journeyPage.suggestedRouts);
        
    }
    
   

}