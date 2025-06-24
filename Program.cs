using System;
using System.Linq;
using System.Threading;
using drupAuto;
using drupAuto.events;
using drupAuto.Models;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Interactions;


class SeleniumDemo
{

    static void Main(string[] args)
    {
        Guid guid = Guid.NewGuid();
        var logger = new SimpleLogger(guid.ToString());
        logger.Log("Application started.");
        var options = new ChromeOptions();
        options.AddArgument("start-maximized");
        WebDriver driver = new ChromeDriver(options);

        var login = new login(driver, logger);
        try
        {
            var desiredPageclicked = true;
            var _page = db_operations.getPage();
            driver.Navigate().GoToUrl("https://draup.com/platformlogin/");
            login.loginIntoSystem();
            login.AcceptCookies();
            login.HandleOptionalPopup();
            login.ClickIndustryButtonInNav();
            login.SelectFromAccountsDropdown();
            //login.ScrolePageDown();
            //login.SelectResultPerPage100();

            
            var pagetoBeselected = _page.FirstOrDefault(x => x.isprocessed == false);
            

            //if (pagetoBeselected.PageNumber > 1)
            //{
            //    while (true)
            //    {
            //        login.WaitForPageLoad();
            //        login.ScrolePageDown();
            //        desiredPageclicked = login.CheckandClickAllPageSpans(pagetoBeselected.PageNumber);

            //        if (desiredPageclicked == true)
            //        {
            //            break;
            //        }
            //    }
            //}
            for (int i = 0; i < _page.Count(); i++)
            {
                logger.Log(message: $"Processing page number: {_page[i].PageNumber}");
                if (_page[i].PageNumber != 1 && _page[i].PageNumber > 0)
                {
                    logger.Log(message: $"inside the if condition");
                    login.WaitForPageLoad();
                    login.ScrolePageDown();
                    login.CheckandClickAllPageSpans((_page[i].PageNumber));
                    Thread.Sleep(10000);
                }
                
                login.NavigateInsideAccount(_page[i].PageNumber);
                Thread.Sleep(15000); // Wait for the page to load
                login.ScrolePageDown();
                _page[i].isprocessed = true; // Mark the page as processed
                db_operations.UpdatePage(_page[i].id, _page[i].isprocessed);
            }
            Console.ReadLine(); // Wait for user input before closing
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        //finally
        //{
        //    driver.Quit();
        //}

        //Console.ReadLine();
    }
}