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

        public void WaitForPageLoad(int timeoutSeconds = 30)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
            wait.Until(d =>
                ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").ToString() == "complete"
            );
        }

        public void MoveMouseToGrid()
        {
            By tableheaderPath = By.XPath("//*[@id=\"table_info_popover_Account\"]/div[1]");
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(tableheaderPath));
            Actions actions = new Actions(driver);
            actions.MoveToElement(element).Perform();
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

            Actions actions = new Actions(driver);
            actions.SendKeys(code[1].ToString()).Perform();
            actions.SendKeys(code[2].ToString()).Perform();
            actions.SendKeys(code[3].ToString()).Perform();
            actions.SendKeys(code[4].ToString()).Perform();
            actions.SendKeys(code[5].ToString()).Perform();

        }

        public void HandleOptionalPopup()
        {
            Actions actions = new Actions(driver);
            try
            {

                By skipbtn = By.XPath("//span[text()='Skip']");
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(120));
                IWebElement skipButton = wait.Until(ExpectedConditions.ElementExists(skipbtn));
                actions.SendKeys(Keys.Escape).Perform();
                Console.WriteLine("Skip button Press.");
            }
            catch (WebDriverTimeoutException)
            {
                Thread.Sleep(10000);
                actions.SendKeys(Keys.Escape).Perform();
                // Skip button didn't appear - do nothing
                Console.WriteLine("Skip button not present, proceeding without skipping.");
            }


        }


        public void ClickIndustryButtonInNav()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            IWebElement industryDropdown = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"navbar-collapse-first\"]/ul[1]/li[1]/a")));
            industryDropdown.Click();
            Console.WriteLine("Industry Button clicked.");
        }
        public void SelectFromAccountsDropdown()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));


            IWebElement dropdownbutton = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("nav-dropdown")));
            dropdownbutton.Click();
            Console.WriteLine("Click on Industry dropdown.");

            IWebElement chooseAccounts = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"navbar-collapse-first\"]/ul[1]/div/div/li[2]/a")));
            chooseAccounts.Click();
            Console.WriteLine("account option choosen.");

        }

        public void ScrolePageDown()
        {
            MoveMouseToGrid();
            By tableheaderPath = By.XPath("//*[@id=\"table_info_popover_Account\"]/div[1]");
            By tablePagerPath = By.XPath("//*[@id=\"root\"]/div/div/div/div/main/div/div/div[1]/div[2]/div/div[3]/div/div[2]/div/div[1]/span");
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(120));
            wait.Until(ExpectedConditions.ElementExists(tableheaderPath));
            Actions actions = new Actions(driver);
            while (true)
            {
                Console.WriteLine("Scrolling down the page...");
                actions.SendKeys(Keys.ArrowDown).Perform();
                Thread.Sleep(100); // Optional pause to simulate natural scroll

                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(120));
                IWebElement pagerText = wait.Until(ExpectedConditions.ElementExists(tablePagerPath));
                if (pagerText.Text.ToLower().Trim() == "results per page")
                {
                    Console.WriteLine("Reached the end of the page or Results per Page text found.");
                    break; // Exit the loop if the text is found
                }

            }
        }

        public void SelectResultPerPage100()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

            var dropdown = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("(//div[contains(@class, 'caret-icon')]//i)[2]")));
            dropdown.Click();

            var option100 = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[text()='100']")));

            option100.Click();
            Thread.Sleep(5000);
        }

        public bool CheckandClickAllPageSpans(int pagenumber)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            // Wait for the page container to be present
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector(".page-container")));

            // Find all page number spans (excluding prev/next)
            var pageSpans = driver.FindElements(By.CssSelector(".page-container > span.page"));
            var desiredPageclicked = false;
            int counter = 1;
            for (int i = 0; i < pageSpans.Count; i++)
            {

                int actuallPageNumber = Convert.ToInt32(pageSpans[i].Text.Trim());
                if (actuallPageNumber == pagenumber)
                {
                    Console.WriteLine("page number " + pageSpans[i] + "clicked on trageted page");
                    pageSpans[i].Click();
                    desiredPageclicked = true;
                    break;
                }
                else if (counter == 5)
                {
                    Console.WriteLine("page number " + pageSpans[i] + "clicked as last page in paging");
                    pageSpans[i].Click();
                }
                Console.WriteLine("page number " + pageSpans[i] + "skiped");
                counter = counter + 1;
                // Exit if the desired page number is found and clicked
            }

            return desiredPageclicked;


        }

        public void NavigateInsideAccount()
        {

            while (true)
            {
                // Get all account rows on the page
                var accountRows = driver.FindElements(By.XPath("//div[contains(@class,'display-flex align-items-center')]//a"));

                for (int i = 0; i < accountRows.Count; i++)
                {
                    // Re-find elements (DOM refreshes after navigation)
                    accountRows = driver.FindElements(By.XPath("//div[contains(@class,'display-flex align-items-center')]//a"));

                    // Click the specific row
                    accountRows[i].Click();
                    

                    // Wait for the account page to load
                    Thread.Sleep(8000); // Consider using WebDriverWait instead
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
                    var downloadImage = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(@class, 'ds-download-dropdown new-download-btn')]//i")));
                    downloadImage.Click();
                    //Thread.Sleep(3000);

                    var pptAcountPlanOption = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//li[text()='PPT Account Plan']")));
                    pptAcountPlanOption.Click();
                    //Thread.Sleep(4000);

                    var getPptcrossBtn = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='modal-close']//i")));
                    getPptcrossBtn.Click();
                    //Thread.Sleep(2000);
                    // Perform your data extraction or actions here

                    // Go back to the main list
                    driver.Navigate().Back();
                    Thread.Sleep(3000); // Wait for page to reload
                }

                // Check if "Next" button is enabled
                try
                {
                    var nextButton = driver.FindElement(By.XPath("//span[@class='next']//i"));
                    if (nextButton.GetAttribute("class").Contains("disabled"))
                        break;

                    nextButton.Click();
                    Thread.Sleep(8000); // Wait for the next page to load
                }
                catch (NoSuchElementException)
                {
                    // No "Next" button found; exit loop
                    break;
                }
            }

            driver.Quit();
        }
    }
}


