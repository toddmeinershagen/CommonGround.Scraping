using System;
using System.Diagnostics;
using CommonGround.Scrape.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;

namespace CommonGround.Scrape
{
    public class EligibilityRunner
    {
        private readonly IWebDriver _driver;

        public EligibilityRunner()
            : this(new PhantomJSDriver())
        {
            
        }

        public EligibilityRunner(IWebDriver driver)
        {
            _driver = driver;
        }

        public void Run(EligibilityScrapeRequest request)
        {
            //NOTE:  required dependencies down below
            //PhantomJS
            //Selenium.Support
            //Selenium.WebDriver

            using (_driver)
            {
                var uri = new Uri("https://provider.bcbssc.com/wps/portal/hcp/providers/home");
                _driver.Navigate().GoToUrl(uri);

                var usernameTextBox = _driver.FindElement(By.Name("CustomLoginFormID"));
                usernameTextBox.Clear();
                usernameTextBox.SendKeys("AccessEd");

                var passwordTextBox = _driver.FindElement(By.Name("CustomLoginFormPassword"));
                passwordTextBox.Clear();
                passwordTextBox.SendKeys("123456789");

                var loginButton = _driver.FindElement(By.Name("LoginPortletFormSubmit"));
                loginButton.Click();

                var title = _driver.Title;
                var expectedTitle = "My Insurance Manager - Pre Load Process";

                if (title != expectedTitle)
                {
                    throw new Exception($"The title did not match the expected value of '{expectedTitle}'.");
                }

                Debug.Write("Done.");
            }
        }
    }
}
