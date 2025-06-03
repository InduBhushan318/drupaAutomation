using drupAuto.Models;
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


        public void AcceptCookies()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                // Try to find a button or span with text 'Accept' or 'Accept Cookies'
                var acceptButton = wait.Until(ExpectedConditions.ElementToBeClickable(
                    By.XPath("//button[@aria-label='Accept']")
                ));
                acceptButton.Click();
                Thread.Sleep(500);
                Console.WriteLine("Cookie accept button clicked.");
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("Cookie accept button not found, proceeding without clicking.");
            }

        }

        public void loginIntoSystem()
        {
            //AcceptCookies();
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
            Thread.Sleep(30000);
        }

        public void HandleOptionalPopup()
        {
            Console.WriteLine("Handling optional popup if present...");
            Actions actions = new Actions(driver);
            try
            {
                // Wait for the page to load and the popup to appear
                By skipbtn = By.XPath("//span[text()='Skip']");
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(120));
                IWebElement skipButton = wait.Until(ExpectedConditions.ElementExists(skipbtn));
                Console.WriteLine("displayed the text of skip button");
                Console.WriteLine(skipButton.Text);
                actions.SendKeys(Keys.Escape).Perform();
                Console.WriteLine("Skip button Press.");
            }
            catch (WebDriverTimeoutException)
            {
                Thread.Sleep(30000);
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
                Console.WriteLine("Scrolling down the page... from scroll page down function");
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

        public void NavigateInsideAccount(page _page)
        {
            int counter = 0;
            while (true)
            {
                Console.WriteLine("inside NavigateInsideAccount");
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
                wait.Until(ExpectedConditions.ElementExists(By.CssSelector(".react-grid-Row")));

                var rows = driver.FindElements(By.CssSelector(".react-grid-Row"));

                Console.WriteLine("Total rows found: " + rows.Count);

                var cells = rows[counter].FindElements(By.TagName("div"));
                Console.WriteLine("totals cells in each row:-" + cells.Count());
                if (counter >= 10)
                {
                    Actions actions = new Actions(driver);
                    for (int z = 0; z <= counter; z++)
                    {
                        Console.WriteLine("scrolling event is called from inside the NavigateInsideAccount");
                        actions.SendKeys(Keys.ArrowDown).Perform();
                        Thread.Sleep(100); // Optional pause to simulate natural scroll
                    }
                }



                if (cells.Count > 0)
                {
                    string accountName = cells[43].Text.Trim();
                    Console.WriteLine(accountName);
                    cells[43].Click();
                    wait = new WebDriverWait(driver, TimeSpan.FromSeconds(120));
                    var downloadImage = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(@class, 'ds-download-dropdown new-download-btn')]//i")));
                    downloadImage.Click();

                    Thread.Sleep(1000); // Wait for the dropdown to appear
                    var pptAccountPlanElements = driver.FindElements(By.XPath("//li[text()='PPT Account Plan']"));
                    if (pptAccountPlanElements.Count > 0 && pptAccountPlanElements[0].Displayed)
                    {
                        pptAccountPlanElements[0].Click();
                        wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[text()='Get PPT via Email']")));
                        OpenTabOptions();
                        IWebElement pptEmailButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[text()='Get PPT via Email']")));
                        pptEmailButton.Click();

                        // clik on send email button and wait for 2000 milliseconds
                    }
                    else
                    {
                        Console.WriteLine("'PPT Account Plan' option not found in the DOM or not visible.");
                    }

                    wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));


                    IWebElement dropdownbutton = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("nav-dropdown")));
                    dropdownbutton.Click();
                    Console.WriteLine("Click on Industry dropdown.");

                    IWebElement chooseAccounts = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"navbar-collapse-first\"]/ul[1]/div/div/li[2]/a")));
                    chooseAccounts.Click();
                    Console.WriteLine("account option choosen. NavigateInsideAccount");


                    // end here 
                    _page.Accounts.Add(new AccountsModel
                    {
                        AccountName = accountName,
                        isProcessed = true
                    });
                }

                counter = counter + 1;
                if (counter >= 100)
                {
                    Console.WriteLine("All accounts processed for this page.");
                    break; // Exit the loop if all accounts are processed
                }

            }




        }

        public void OpenTabOptions()
        {

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var tabOptions = new Dictionary<string, List<string>>
        {
            { "Priorities", new List<string> { "Select All" } },
            { "Tech Stack", new List<string> { "Select Top 48 Categories" } },
            { "Globalization Footprint", new List<string> { "Select All" } },
            { "Hiring", new List<string> { "IT", "Management & Strategy", "Procurement", "Customer Support & Service", "Financial Services Operations" } },
            { "Signals", new List<string> { "Select All" } },
            { "Service Providers", new List<string> { "Software Testing", "DevOps", "Artificial Intelligence & Data Science", "Application Development & Maintenance", "Data Engineering", "Legacy Modernization", "Infrastructure Management System", "Cybersecurity", "Product Installation, Maintenance & Technical Support", "System Integration" } },
            { "Key Executives", new List<string> { } }
        };

            foreach (var tab in tabOptions)
            {
                string tabName = tab.Key;
                List<string> checkboxes = tab.Value;

                Console.WriteLine($"Navigating to tab: {tabName}");
                var tabElement = wait.Until(d => d.FindElement(By.XPath($"//span[@class='general-400 text' and text()='{tabName}']")));
                tabElement.Click();
                Thread.Sleep(1000); // wait for content to load

                foreach (var checkboxLabel in checkboxes)
                {
                    SelectCheckboxByLabel(driver, checkboxLabel);
                }
            }

            Console.WriteLine("✅ Done selecting checkboxes from all tabs.");
            driver.Quit();
        }

        public void SelectCheckboxByLabel(IWebDriver driver, string labelText)
        {
            try
            {
                var label = driver.FindElement(By.XPath($"//span[text()='{labelText}']"));
                var checkbox = label.FindElement(By.XPath($"//ancestor-or-self::div[@class='category-lists']//input[@type= 'checkbox']"));

                if (!checkbox.Selected)
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", checkbox);
                    Thread.Sleep(300); // brief pause between selections
                    Console.WriteLine($"✔ Selected: {labelText}");
                }
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine($"⚠ Checkbox not found: {labelText}");
            }
        }

    }

}



