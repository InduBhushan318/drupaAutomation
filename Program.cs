using System;
using System.Threading;
using drupAuto;
using drupAuto.events;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;


class SeleniumDemo
{
    
    static void Main(string[] args)
    {
        var options = new ChromeOptions();
        options.AddArgument("start-maximized");
        IWebDriver driver = new ChromeDriver(options);
       
        var login = new login(driver);
        try
        {
            var desiredPageclicked = true;
            driver.Navigate().GoToUrl("https://draup.com/platformlogin/");
            login.loginIntoSystem();
            driver.Navigate().Refresh();
            login.HandleOptionalPopup();
            login.ClickIndustryButtonInNav();
            login.SelectFromAccountsDropdown();
            login.ScrolePageDown();
            login.SelectResultPerPage100();
            login.NavigateInsideAccount();

            //int getPage = 1;//db_operations.getPage();
            //while (true)
            //{
            //    login.WaitForPageLoad();
            //    login.ScrolePageDown();
            //    desiredPageclicked = login.CheckandClickAllPageSpans(getPage);


            //    if(desiredPageclicked==true)
            //    { 
            //        break; 
            //    }
            //}


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
}