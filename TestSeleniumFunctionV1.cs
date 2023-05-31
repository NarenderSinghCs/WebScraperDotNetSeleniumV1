using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PuppeteerSharp;
using System.Linq;
using Selenium.WebDriver.UndetectedChromeDriver;
using OpenQA.Selenium;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace SeleriumFunctionV1code
{
    public static class TestSeleniumFunctionV1
    {
        [FunctionName("selenium")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
                var username="BallardiDrive";
                var password="Post12345!";
                using (var driver = UndetectedChromeDriver.Instance())   
                          
            
        {  
            driver.DriverArguments.Add("--no-sandbox");
            driver.DriverArguments.Add("enable-automation");
            driver.DriverArguments.Add("--disable-dev-shm-usage");
            driver.DriverArguments.Add("--disable-gpu");
            driver.DriverArguments.Add("--disable-extensions");
            driver.DriverArguments.Add("--headless");
            driver.DriverArguments.Add("--disable-infobars");
            driver.DriverArguments.Add("--disable-notifications");
            driver.DriverArguments.Add("--disable-browser-side-navigation");
            driver.DriverArguments.Add("--disable-popup-blocking");
            driver.DriverArguments.Add("--ignore-certificate-errors");
            driver.DriverArguments.Add("--mute-audio");

            
            
            driver.Manage().Window.Maximize();
            System.Console.WriteLine("Window Maximized");
            driver.Navigate().GoToUrl("https://www.ups.com/us/en/Home.page?");                     
            
            System.Console.WriteLine("Url Searched");
            // string htmlContent = driver.PageSource;
            // Console.WriteLine(htmlContent);    
            IWebElement userIcon = driver.FindElement(By.XPath("//*[@id='ups-navContainer']/header/div/div[3]/a"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", userIcon);
            await Task.Delay(3000);        
            var emailInput = driver.FindElement(By.XPath("//*[@id='email']"));
                //var productNames = wait.Until(driver => driver.FindElements(By.XPath("_4rR01T")));
                emailInput.SendKeys(username);
                var passwordInput = driver.FindElement(By.XPath("//*[@id='pwd']"));
                passwordInput.SendKeys(password);
                var loginButton =  driver.FindElement(By.XPath("//*[@id='submitBtn']"));
                loginButton.Click();
                await Task.Delay(3000);
                System.Console.WriteLine("Logged In!");
                var accountButton = driver.FindElement(By.XPath("//*[@id='dropdownMenuButton']"));
                accountButton.Click();
                await Task.Delay(3000);
                var viewBill = driver.FindElement(By.XPath("//*[@id='ups-navContainer']/header/div/div[3]/ul/li[6]/a"));
                viewBill.Click();
                await Task.Delay(3000);
                IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
                jsExecutor.ExecuteScript("document.body.style.zoom = '50%';");
                await Task.Delay(12000);
                
                // var myInvoices = driver.FindElement(By.XPath("//*[@id='side-nav-link-my-invoices']"));
                // myInvoices.Click();
                IWebElement myInvoices = driver.FindElement(By.XPath("//*[@id='side-nav-link-my-invoices']"));
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", myInvoices);
                await Task.Delay(3000);
                IWebElement calClick = driver.FindElement(By.XPath("//*[@id='invoice-table_wrapper']/div[1]/div[2]/div/button[3]"));
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", calClick);
                await Task.Delay(3000);
                

                IWebElement thirtyDays = driver.FindElement(By.XPath("//*[@id='calendarOptionType_LAST_30_DAYS']"));
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", thirtyDays);
                await Task.Delay(3000);               

                IWebElement applyButton = driver.FindElement(By.XPath("//*[@id='invoice-search-date-btn-apply']"));
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", applyButton);
                await Task.Delay(3000);                               
                ReadOnlyCollection<IWebElement> table = driver.FindElements(By.XPath("//table[contains(@class, 'table-bordered-simple')]"));
                if (table.Count > 0)
                {
                IWebElement tbody = table[0].FindElement(By.TagName("tbody"));            
                ReadOnlyCollection<IWebElement> rowss = tbody.FindElements(By.TagName("tr"));          
                foreach (IWebElement row in rowss)
                {
                    ReadOnlyCollection<IWebElement> _tdList = row.FindElements(By.TagName("td"));
                    ReadOnlyCollection<IWebElement> invoiceNum = row.FindElements(By.TagName("th"));
                
                    var rowData=_tdList[0];
                    var innerDiv=  rowData.FindElement(By.TagName("div"));
                    IWebElement checkBox=innerDiv.FindElement(By.TagName("input"));
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", checkBox);                 
                    }
                }        
                await Task.Delay(3000);
                System.Console.WriteLine("Filter Applied !"); 
                IWebElement downloadClick = driver.FindElement(By.XPath("//*[@id='invoice-table_wrapper']/div[1]/div[2]/div/button[1]"));
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", downloadClick);
                await Task.Delay(3000);
                IWebElement csvClick = driver.FindElement(By.XPath("//*[@id='downloadOptionType_csv']"));
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", csvClick);
                await Task.Delay(3000);
                // var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                // var screenshotPath = "D:\\UPS.png";
                // screenshot.SaveAsFile(screenshotPath, ScreenshotImageFormat.Png);
                await Task.Delay(3000);             
                               

                IWebElement finalDownload = driver.FindElement(By.Id("download-multiple-invoice-btn-download"));               
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", finalDownload);
                await Task.Delay(5000);
                System.Console.WriteLine("Downloaded successfully !"); 
                
                IWebElement closeDownloadPopup = driver.FindElement(By.XPath("//*[@id='modal-root']/div/div[2]/div/div/div[1]/button"));
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", closeDownloadPopup);
                await Task.Delay(2000);

                IWebElement arrow = driver.FindElement(By.XPath("//*[@id='main-nav-bar']/div/ul/li/div/div/a/i[2]"));
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", arrow);
                await Task.Delay(2000);
                IWebElement logOut = driver.FindElement(By.XPath("//*[@id='main-nav-bar']/div/ul/li/div/div/div/div/button"));
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", logOut);                     
                System.Console.WriteLine("logged Out !"); 
                await Task.Delay(5000);
                driver.Quit();
        }

        return new OkObjectResult("Scraping Done!");

        }
    }
}

