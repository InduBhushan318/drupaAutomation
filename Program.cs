using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;


class SeleniumDemo
{
    static void Main(string[] args)
    {
        IWebDriver driver = new ChromeDriver();

        try
        {
            driver.Navigate().GoToUrl("https://draup.com/platformlogin/");
            IWebElement searchBox = driver.FindElement(By.Id("emailsso"));
            searchBox.SendKeys("kevin.ferrao@testingxperts.com");

            IWebElement contubtn = driver.FindElement(By.Id("login-btnsso"));

            contubtn.Click();
            Console.Write("enter the code:- ");
            var code=Console.ReadLine();
            System.Threading.Thread.Sleep(30000);
            IWebElement first = driver.FindElement(By.Id("first"));
            
            first.SendKeys(code[0].ToString());
            System.Threading.Thread.Sleep(1500);

            Actions actions = new Actions(driver);
            actions.SendKeys(code[1].ToString()).Perform();
            System.Threading.Thread.Sleep(700);

            actions.SendKeys(code[2].ToString()).Perform();
            System.Threading.Thread.Sleep(700);

            actions.SendKeys(code[3].ToString()).Perform();
            System.Threading.Thread.Sleep(700);

            actions.SendKeys(code[4].ToString()).Perform();
            System.Threading.Thread.Sleep(700);

            actions.SendKeys(code[5].ToString()).Perform();
            System.Threading.Thread.Sleep(700);

            
            Console.WriteLine("Page Title: " + driver.Title);
            Console.ReadLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        finally
        {
            driver.Quit();
        }

        Console.ReadLine();
    }
}