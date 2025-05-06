using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;



namespace Moovit;

public class BaseTest
{
    public IWebDriver driver;
    public StartPage startPage;
    public AdsContacUsPage adsContactUsPage;
    public Helpers.ConfigReader.TestSettings config;
    
    
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        driver = new ChromeDriver();

        driver.Manage().Window.Maximize();
        config = Helpers.ConfigReader.LoadConfig();

        driver!.Navigate().GoToUrl(config.Url);
        
    }

    [TearDown]
    public void TearDown()
    {
        driver!.Navigate().GoToUrl(config.Url);
    }
    

    
    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        driver!.Quit();
    }
}