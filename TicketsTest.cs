namespace Moovit;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

[TestFixture]
public class TicketsTest: BaseTest
{
    public StartPage startPage;
    public TicketsPage ticketsPage;
    
    [SetUp]
    public void SetUp()
    {
        // Initialize page objects before each test
        startPage = new StartPage(driver);
        ticketsPage = new TicketsPage(driver);
    }
    

    [Test]
    public void ACompleteTicketSearchTest()
    {
        Helpers.WaitUntilElementDisplayed(driver, startPage.MoovitTicketsBy);
        Thread.Sleep(2000);
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({ behavior: 'smooth', block: 'center' });",
            startPage.MoovitTickets);
        Helpers.SafeClick(driver,startPage.MoovitTickets);
        Thread.Sleep(2000);
        Helpers.WaitUntilElementDisplayed(driver, ticketsPage.distributionSearchBy);
        ticketsPage.EnterToShadowInputField(config.DeparturePoint,ticketsPage.departureBy);
        string expectedDepartureValue = ticketsPage.ShadowElementValue(ticketsPage.departureBy);
        Assert.IsTrue(expectedDepartureValue.Contains(config.DeparturePoint));
        ticketsPage.EnterToShadowInputField(config.ArrivalPoint,ticketsPage.arrivalSelector);
        string expectedArrivalValue = ticketsPage.ShadowElementValue(ticketsPage.arrivalSelector);
        Assert.IsTrue(expectedArrivalValue.Contains(config.ArrivalPoint));
    }
    
    
    [Test]
    public void TicketsSearchTest()
    {
        Helpers.WaitUntilElementDisplayed(driver, startPage.MoovitTicketsBy);
        Thread.Sleep(2000);
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({ behavior: 'smooth', block: 'center' });",
            startPage.MoovitTickets);
        Helpers.SafeClick(driver,startPage.MoovitTickets);
        Thread.Sleep(2000);
        Helpers.WaitUntilElementDisplayed(driver, ticketsPage.distributionSearchBy);
        ticketsPage.EnterToShadowInputField(config.DeparturePoint,ticketsPage.departureBy);
        ticketsPage.EnterToShadowInputField(config.ArrivalPoint,ticketsPage.arrivalSelector);
        ticketsPage.ClickOnShadowElement(ticketsPage.submitSelector);
        Helpers.WaitUntilPageLoaded(driver);
        ticketsPage.GoToInsideIframe(driver);
        Assert.AreEqual(ticketsPage.departueFieldResults.GetAttribute("value"),config.DeparturePoint);
        Assert.AreEqual(ticketsPage.arrivalFieldResults.GetAttribute("value"),config.ArrivalPoint);

    }

    [Test]
    public void TicketsSwitchLocationsTest()
    {
        Helpers.WaitUntilElementDisplayed(driver, startPage.MoovitTicketsBy);
        Thread.Sleep(2000);
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({ behavior: 'smooth', block: 'center' });",
            startPage.MoovitTickets);
        Helpers.SafeClick(driver,startPage.MoovitTickets);
        Thread.Sleep(2000);
        Helpers.WaitUntilElementDisplayed(driver, ticketsPage.distributionSearchBy);
        ticketsPage.EnterToShadowInputField(config.DeparturePoint,ticketsPage.departureBy);
        ticketsPage.EnterToShadowInputField(config.ArrivalPoint,ticketsPage.arrivalSelector);
        ticketsPage.ClickOnShadowElement(ticketsPage.submitSelector);
        Helpers.WaitUntilPageLoaded(driver);
        ticketsPage.GoToInsideIframe(driver);
        ticketsPage.switchBtn.Click();
        Assert.AreEqual(ticketsPage.departueFieldResults.GetAttribute("value"),config.ArrivalPoint);
        Assert.AreEqual(ticketsPage.arrivalFieldResults.GetAttribute("value"),config.DeparturePoint);
    }

    [Test]
    public void IncreasePassengersNumTest()
    {
        Helpers.WaitUntilElementDisplayed(driver, startPage.MoovitTicketsBy);
        Thread.Sleep(2000);
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({ behavior: 'smooth', block: 'center' });",
            startPage.MoovitTickets);
        Helpers.SafeClick(driver,startPage.MoovitTickets);
        Thread.Sleep(2000);
        Helpers.WaitUntilElementDisplayed(driver, ticketsPage.distributionSearchBy);
        ticketsPage.EnterToShadowInputField(config.DeparturePoint,ticketsPage.departureBy);
        ticketsPage.EnterToShadowInputField(config.ArrivalPoint,ticketsPage.arrivalSelector);
        ticketsPage.ClickOnShadowElement(ticketsPage.submitSelector);
        Helpers.WaitUntilPageLoaded(driver);
        ticketsPage.GoToInsideIframe(driver);
        double priceFor1 = ticketsPage.GetRidePrice(ticketsPage.firstRideOptPrice);
        ticketsPage.passNumber.Click();
        Thread.Sleep(1000);
        ticketsPage.passPlus.Click();
        Assert.AreEqual("2", ticketsPage.passNumber.Text);
        ticketsPage.resultsSubmitBtn.Click();
        Helpers.WaitUntilElementDisplayed(driver,ticketsPage.CcyBy);
        double priceFor2 = ticketsPage.GetRidePrice(ticketsPage.firstRideOptPrice);
        Assert.Greater(priceFor2, priceFor1);
    }
}