namespace Moovit;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

[TestFixture]
public class ContactUsTest :BaseTest
{
    [Test]
    
    public void ContactUsFormTest()
    {
        startPage = new StartPage(driver);
        adsContactUsPage = new AdsContacUsPage(driver);

        IWebElement moovitAdsToHover = driver.FindElement(By.CssSelector("a[href='https://moovit.com/ads/']"));
        Actions action = new Actions(driver);
        action.MoveToElement(moovitAdsToHover).Perform();
        action.Build().Perform();
        Helpers.WaitUntilElementDisplayed(driver,startPage.ContactUsBy);
        startPage.ContactUs.Click();
        
        
        Helpers.WaitUntilElementDisplayed(driver,adsContactUsPage.FirstNameSelector);
        adsContactUsPage.FirstName.Click();
        adsContactUsPage.FirstName.Clear();
        Helpers.SafeSendKeys(driver,adsContactUsPage.FirstName,config.FirstName);
        Helpers.ChooseSelectBox(driver,adsContactUsPage.CountrySelectBox,config.Country);
        
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({ behavior: 'smooth', block: 'center' });",
            adsContactUsPage.SubmitBtn);
        Helpers.SafeClick(driver,adsContactUsPage.SubmitBtn);
        Assert.IsTrue(adsContactUsPage.ErrorLastName.Displayed);
        Helpers.WaitUntilElementDisplayed(driver,adsContactUsPage.LastNameSelector);
        adsContactUsPage.LastName.Click();
        adsContactUsPage.LastName.Clear();
        Helpers.SafeSendKeys(driver,adsContactUsPage.LastName,config.LastName);
        Helpers.SafeClick(driver,adsContactUsPage.SubmitBtn);
        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({ behavior: 'smooth', block: 'center' });",
            adsContactUsPage.LastName);
        var errorLastNameElements = driver.FindElements(By.XPath(adsContactUsPage.ErrorLastNameXPath));
        Assert.IsTrue(errorLastNameElements.Count == 0);
    }
}