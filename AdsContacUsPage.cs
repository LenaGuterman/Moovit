using OpenQA.Selenium;

namespace Moovit;

public class AdsContacUsPage
{
    private readonly IWebDriver driver;
    public AdsContacUsPage(IWebDriver driver)
    {
        this.driver = driver;
    }
    
    //public IWebElement ContactUs => driver.FindElement(By.XPath("//a[text()='Contact Us']"));
    public IWebElement FirstName => driver.FindElement(FirstNameSelector);
    public By FirstNameSelector = By.XPath("//input[@name='contact-ads-first-name']");
    public By CountrySelectBox = By.ClassName("SlectBox");
    public IWebElement SubmitBtn => driver.FindElement(By.XPath("//input[@value='Submit']"));
    public IWebElement ErrorLastName => driver.FindElement(By.XPath("//span[@data-name='contact-ads-last-name']//span[@class='wpcf7-not-valid-tip']"));
    public string ErrorLastNameXPath => "//span[@data-name='contact-ads-last-name']//span[@class='wpcf7-not-valid-tip']";

    public IWebElement LastName => driver.FindElement(LastNameSelector);
    public By LastNameSelector = By.XPath("//input[@name='contact-ads-last-name']");

}