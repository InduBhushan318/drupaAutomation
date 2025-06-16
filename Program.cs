using System;
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
        page _page=new page();
        var login = new login(driver,logger);
        try
        {
            var desiredPageclicked = true;
            driver.Navigate().GoToUrl("https://draup.com/platformlogin/");
            login.loginIntoSystem();
            login.AcceptCookies();
            login.HandleOptionalPopup();
            login.ClickIndustryButtonInNav();
            login.SelectFromAccountsDropdown();
            //login.ScrolePageDown();
            //login.SelectResultPerPage100();
          

            _page.PageNumber = 1;//db_operations.getPage();
            if (_page.PageNumber > 1)
            {
                while (true)
                {
                    login.WaitForPageLoad();
                    login.ScrolePageDown();
                    desiredPageclicked = login.CheckandClickAllPageSpans(_page.PageNumber);


                    if (desiredPageclicked == true)
                    {
                        break;
                    }
                }
            }
            while (true)
            {
                var allrecoredProcessded = login.NavigateInsideAccount(_page);
                if (allrecoredProcessded)
                {
                    login.WaitForPageLoad();
                    login.ScrolePageDown();
                    login.CheckandClickAllPageSpans((_page.PageNumber+1));
                }
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