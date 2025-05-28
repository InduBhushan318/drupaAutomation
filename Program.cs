using System;
using drupAuto.events;
using NUnit.Framework;
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
            var login = new login(driver);
            login.loginIntoSystem();
            login.HandleOptionalPopup();
            login.ClickIndustryButtonInNav();
            login.SelectFromAccountsDropdown();

            Console.ReadLine(); // Wait for user input before closing
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        finally
        {
            driver.Quit();
        }

        //Console.ReadLine();
    }


    //[Test]
    //public void SeleniumTest()
    //{
    //    IWebDriver driver = new ChromeDriver();

    //    try
    //    {

    //        driver.Navigate().GoToUrl("https://draup.com/platformlogin/");
    //        var login = new login(driver);
    //        login.loginIntoSystem();
    //        login.HandleOptionalPopup();
    //        login.SelectFromAccountsDropdown();

            
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine("Error: " + ex.Message);
    //    }
    //    finally
    //    {
    //        driver.Quit();
    //    }

    //    //Console.ReadLine();
    //}
}