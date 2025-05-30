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
        var options = new EdgeOptions();
        options.AddArgument("start-maximized");
        IWebDriver driver = new EdgeDriver(options);
        page _page=new page();
        var login = new login(driver);
        try
        {
            var desiredPageclicked = true;
            driver.Navigate().GoToUrl("https://draup.com/platformlogin/");
            login.loginIntoSystem();
            login.HandleOptionalPopup();
            login.ClickIndustryButtonInNav();
            login.SelectFromAccountsDropdown();
            login.ScrolePageDown();
            login.SelectResultPerPage100();
          

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
            login.NavigateInsideAccount(_page);

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