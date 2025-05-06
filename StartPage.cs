using OpenQA.Selenium;

namespace Moovit;

public class StartPage
{
    private readonly IWebDriver driver;
    public StartPage(IWebDriver driver)
    {
        this.driver = driver;
    }
    
    public IWebElement ContactUs => driver.FindElement(By.XPath("//a[text()='Contact Us']"));
    public By ContactUsBy => By.XPath("//a[text()='Contact Us']");
    public IWebElement MoovitTickets => driver.FindElement(By.XPath("//a[text()='Moovit Tickets']"));
    public By MoovitTicketsBy => By.XPath("//a[text()='Moovit Tickets']");
}