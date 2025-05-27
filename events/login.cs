using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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


        }

        public void HandleOptionalPopup()
        {
            By skipbtn = By.XPath("//span[text()='Skip']");

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            IWebElement skipButton = wait.Until(ExpectedConditions.ElementExists(skipbtn)); // Replace with actual locator

            skipButton.Click();

            // Skip button didn't appear - do nothing
            Console.WriteLine("Skip button not present, proceeding without skipping.");
        }
    }
}
}
