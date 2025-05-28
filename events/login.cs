using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace drupAuto.events
{
    public class login
    {
        IWebDriver driver;
        public login(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void loginIntoSystem()
        {
            IWebElement searchBox = driver.FindElement(By.Id("emailsso"));
            searchBox.SendKeys("kevin.ferrao@testingxperts.com");

            IWebElement contubtn = driver.FindElement(By.Id("login-btnsso"));

            contubtn.Click();
            Console.Write("enter the code:- ");
            var code = Console.ReadLine();
            System.Threading.Thread.Sleep(1000);
            IWebElement first = driver.FindElement(By.Id("first"));

            first.SendKeys(code[0].ToString());
            //System.Threading.Thread.Sleep(500);


            Actions actions = new Actions(driver);
            actions.SendKeys(code[1].ToString()).Perform();
            //System.Threading.Thread.Sleep(700);

            actions.SendKeys(code[2].ToString()).Perform();
            //System.Threading.Thread.Sleep(700);

            actions.SendKeys(code[3].ToString()).Perform();
            //System.Threading.Thread.Sleep(700);

            actions.SendKeys(code[4].ToString()).Perform();
            //System.Threading.Thread.Sleep(700);

            actions.SendKeys(code[5].ToString()).Perform();
            //System.Threading.Thread.Sleep(700);


        }

        public void HandleOptionalPopup()
        {
            Actions actions = new Actions(driver);
            try
            {
                
                By skipbtn = By.XPath("//span[text()='Skip']");
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                IWebElement skipButton = wait.Until(ExpectedConditions.ElementExists(skipbtn));
                actions.SendKeys(Keys.Escape).Perform();
            }
            catch (WebDriverTimeoutException)
            {
                Thread.Sleep(10000);
                actions.SendKeys(Keys.Escape).Perform();
                // Skip button didn't appear - do nothing
                Console.WriteLine("Skip button not present, proceeding without skipping.");
            }

            //By skipbtn = By.XPath("//span[text()='Skip']");

            //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            //IWebElement skipButton = wait.Until(ExpectedConditions.ElementExists(skipbtn)); // Replace with actual locator

            //skipButton.Click();

            //// Skip button didn't appear - do nothing
            //Console.WriteLine("Skip button not present, proceeding without skipping.");
        }


        public void ClickIndustryButtonInNav()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement industryDropdown = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"navbar-collapse-first\"]/ul[1]/li[1]/a")));
            industryDropdown.Click();
        }
        public void SelectFromAccountsDropdown()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
           

            IWebElement dropdownbutton = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("nav-dropdown")));
            dropdownbutton.Click();

           
            IWebElement chooseAccounts = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"navbar-collapse-first\"]/ul[1]/div/div/li[2]/a")));
            chooseAccounts.Click();
            
        }

        public void SelectResultPerPage100()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            
            //IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            //js.ExecuteScript("window.scrollBy(0,1000);");

            //IWebElement element = driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div/div/div/main/div/div/div[1]/div[2]/div/div[3]/div/div[2]/div/div[1]/span"));
            //IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            //js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
            //Thread.Sleep(500);

            var dropdown = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"root\"]/div/div/div/div/main/div/div/div[1]/div[2]/div/div[3]/div/div[2]/div/div[1]/div/div/button/div[2]/i")));
            Thread.Sleep(500);
            dropdown.Click();

            var option100 = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"num_results-Number of results\"]/ul/li/div/ul/li[3]/div/div/div/div/div")));
            Thread.Sleep(500);
            option100.Click();
            Thread.Sleep(500);
        }




        //public void Pagination()
        //{
            
        //    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        //    List<string> allData = new List<string>();

        //    int currentPage = 1;
        //    int totalPages = 16000; // Suppose 100 records per page

        //    while (currentPage <= totalPages)
        //    {
        //        Console.WriteLine($"Scraping page {currentPage}...");

        //        // Wait for elements to load
        //        wait.Until(d => d.FindElements(By.CssSelector(".your-element-class")).Count > 0);

        //        // Collect data from each element on this page
        //        IList<IWebElement> elements = driver.FindElements(By.CssSelector(".your-element-class"));
        //        foreach (var element in elements)
        //        {
        //            allData.Add(element.Text); // Or element.GetAttribute("href") etc.
        //        }

        //        // OPTIONAL: Save to file after each page
        //        File.AppendAllLines("scraped_data.txt", allData);
        //        allData.Clear();

        //        // Click on the "Next" or go to the next page
        //        try
        //        {
        //            IWebElement nextButton = wait.Until(d => d.FindElement(By.CssSelector(".next-page-button")));
        //            nextButton.Click();
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine("Next button not found or error occurred: " + ex.Message);
        //            break;
        //        }

        //        // Optional sleep to reduce server load
        //        Thread.Sleep(1000);

        //        currentPage++;
        //    }

        //    driver.Quit();
        //    Console.WriteLine("Scraping complete.");
        //}
    }
}

