using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace FillTSA
{
    class Program
    {
        public static IWebDriver driver;
        
        public StringBuilder verificationErrors = new StringBuilder();
        public static string baseURL = "https://gp.emea.csc.com/";
        public static bool acceptNextAlert = true;

        static void Main(string[] args)
        {
            DateTime day = DateTime.Now;
            string dayname = day.ToString("dddd");
            try
            {
                
                
              driver = new FirefoxDriver(new FirefoxBinary("C:\\Program Files (x86)\\Mozilla Firefox\\Firefox.exe"), new FirefoxProfile(), TimeSpan.FromMinutes(10));

              //  driver = new FirefoxDriver(new FirefoxBinary("C:\\Program Files (x86)\\Mozilla Firefox\\Firefox.exe"), new FirefoxProfile());


                //for direct opening of TSA
                driver.Navigate().GoToUrl(baseURL + "/siteminderagent/forms/login5.fcc?TYPE=33554433&REALMOID=06-00045132-730a-15d4-b018-80cd967d0000&GUID=&SMAUTHREASON=0&METHOD=GET&SMAGENTNAME=inportal_cscwebnoi001v_iis_agent&TARGET=-SM-HTTPS%3a%2f%2fin%2eportal%2ecsc%2ecom%2fCSCI%2ePortal%2fdefault%2easpx%3fmid%3d1");

                // open the C3 
                //    driver.Navigate().GoToUrl(baseURL + "/siteminderagent/forms/login5.fcc?TYPE=33554433&REALMOID=06-12a2fc84-51e3-1143-9e5f-83815e590000&GUID=&SMAUTHREASON=0&METHOD=GET&SMAGENTNAME=cscgplndc006_iis_agent&TARGET=-SM-HTTPS%3a%2f%2fgp%2eamer%2ecsc%2ecom%2fsiteminderagent%2fredirectjsp%2fredirectC3PHX%2ejsp%3fSPID%3durn-%3Ac3-%3Aprod%26RelayState%3dhttps-%3A-%2F-%2Fc3%2ecsc%2ecom-%2F-%252Findex%2ejspa%26SMPORTALURL%3dhttps-%3A-%2F-%2Fgp%2eamer%2ecsc%2ecom-%3A443-%2Faffwebservices-%2Fpublic-%2Fsaml2sso");

                //   driver.Navigate().GoToUrl("https://in.portal.csc.com/CSCI.Portal/default.aspx?mid=1");

                driver.FindElement(By.Id("USER")).Clear();
                driver.FindElement(By.Id("USER")).SendKeys("asarolia");
                driver.FindElement(By.Name("PASSWORD")).Clear();
                driver.FindElement(By.Name("PASSWORD")).SendKeys("Aviva@may16");
                driver.FindElement(By.Id("loginbtn")).Click();
                //driver.FindElement(By.LinkText("TSA")).Click();
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
                IWebElement myDynamicElement = wait.Until<IWebElement>(driver => driver.FindElement(By.PartialLinkText("TSA")));
                // driver.FindElement(By.XPath("//a[.='TSA']")).Click();
                myDynamicElement.Click();

                switch (dayname)
                {
                    case "Monday" :

                        new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ddlTask"))).SelectByText("FinesseQ2 | FinesseQ2 | UK-EXCEED-NU-D");
                        driver.FindElement(By.CssSelector("option[value=\"6$FinesseQ2 |  FinesseQ2 | EXCEED\"]")).Click();
                        new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ddlMjrActvty"))).SelectByText("Analysis (SE)");
                        driver.FindElement(By.CssSelector("option[value=\"202\"]")).Click();
                        new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ddlSubActvty"))).SelectByText("Detailed Analysis");
                        driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_btnIncROW")).Click();
                        new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ddlMjrActvty"))).SelectByText("Testing (SE)");
                        driver.FindElement(By.CssSelector("option[value=\"205\"]")).Click();
                        new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ddlSubActvty"))).SelectByText("Test Environment Setup");
                        driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_btnIncROW")).Click();
                        new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ddlTask"))).SelectByText("Organisational Activities | OA | UK-EXCEED-NU-D");
                        driver.FindElement(By.CssSelector("option[value=\"7$Organisational Activities |  OA | EXCEED\"]")).Click();
                        new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ddlMjrActvty"))).SelectByText("HR Related Activities (OA)");
                        driver.FindElement(By.CssSelector("option[value=\"504\"]")).Click();
                        new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ddlSubActvty"))).SelectByText("Interviewing / participation in recruitment cycle");
                        new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ddlMjrActvty"))).SelectByText("Non Project Training (OA)");
                        driver.FindElement(By.CssSelector("option[value=\"505\"]")).Click();
                        new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ddlSubActvty"))).SelectByText("Self learning / Learning new tools (non-project related)");
                        driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_btnIncROW")).Click();
                        new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ddlTask"))).SelectByText("Other Tasks | OT | UK-EXCEED-NU-D");
                        driver.FindElement(By.CssSelector("option[value=\"8$Other Tasks |  OT | EXCEED\"]")).Click();
                        new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ddlMjrActvty"))).SelectByText("Leave / Holiday");
                        driver.FindElement(By.CssSelector("option[value=\"601\"]")).Click();
                        new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ddlSubActvty"))).SelectByText("Holiday");
                        driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_btnIncROW")).Click();
                        new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ddlMjrActvty"))).SelectByText("Personal Time");
                        driver.FindElement(By.CssSelector("option[value=\"602\"]")).Click();
                        new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ddlSubActvty"))).SelectByText("Personal time");
                        driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_btnIncROW")).Click();
                        new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ddlTask"))).SelectByText("Software Generic | SG | UK-EXCEED-NU-D");
                        driver.FindElement(By.CssSelector("option[value=\"11$Software Generic |  SG | EXCEED\"]")).Click();
                        driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_dgTest_ctl02_txtMon")).Clear();
                        driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_dgTest_ctl02_txtMon")).SendKeys("9");
                        Assert.AreEqual("Please enter Only Numeric Value", CloseAlertAndGetItsText());
                        driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_dgTest_ctl06_txtMon")).Clear();
                        driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_dgTest_ctl06_txtMon")).SendKeys(".5");
                        Assert.AreEqual("Please enter Only Numeric Value", CloseAlertAndGetItsText());
                        driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_btnSubmit")).Click();
                        driver.FindElement(By.Id("ctl00_lnkLogout")).Click();
                        Assert.IsTrue(Regex.IsMatch(CloseAlertAndGetItsText(), "^Are you sure want to Logout[\\s\\S]$"));
                        driver.FindElement(By.LinkText("Logout")).Click();
                        Assert.AreEqual("Do you want to Logout", CloseAlertAndGetItsText());

                        break;
                    case "Tuesday":

                        driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_dgTest_ctl05_txtTue")).Clear();
                        driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_dgTest_ctl05_txtTue")).SendKeys("9");
                        Assert.AreEqual("Please enter Only Numeric Value", CloseAlertAndGetItsText());
                        driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_btnSubmit")).Click();
                        driver.FindElement(By.Id("ctl00_lnkLogout")).Click();
                        Assert.IsTrue(Regex.IsMatch(CloseAlertAndGetItsText(), "^Are you sure want to Logout[\\s\\S]$"));
                        driver.FindElement(By.LinkText("Logout")).Click();
                        Assert.AreEqual("Do you want to Logout", CloseAlertAndGetItsText());

                        break;              
                    case "Wednesday":

                        driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_dgTest_ctl05_txtWed")).Clear();
                        driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_dgTest_ctl05_txtWed")).SendKeys("9");
                        Assert.AreEqual("Please enter Only Numeric Value", CloseAlertAndGetItsText());
                        driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_btnSubmit")).Click();
                        driver.FindElement(By.Id("ctl00_lnkLogout")).Click();
                        Assert.IsTrue(Regex.IsMatch(CloseAlertAndGetItsText(), "^Are you sure want to Logout[\\s\\S]$"));
                        driver.FindElement(By.LinkText("Logout")).Click();
                        Assert.AreEqual("Do you want to Logout", CloseAlertAndGetItsText());

                        break;    
                    case "Thursday":

                        driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_dgTest_ctl05_txtThu")).Clear();
                        driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_dgTest_ctl05_txtThu")).SendKeys("9");
                        Assert.AreEqual("Please enter Only Numeric Value", CloseAlertAndGetItsText());
                        driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_btnSubmit")).Click();
                        driver.FindElement(By.Id("ctl00_lnkLogout")).Click();
                        Assert.IsTrue(Regex.IsMatch(CloseAlertAndGetItsText(), "^Are you sure want to Logout[\\s\\S]$"));
                        driver.FindElement(By.LinkText("Logout")).Click();
                        Assert.AreEqual("Do you want to Logout", CloseAlertAndGetItsText());

                        break;
                    case "Friday":

                        driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_dgTest_ctl05_txtFri")).Clear();
                        driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_dgTest_ctl05_txtFri")).SendKeys("9");
                        Assert.AreEqual("Please enter Only Numeric Value", CloseAlertAndGetItsText());
                        driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_btnSubmit")).Click();
                        driver.FindElement(By.Id("ctl00_lnkLogout")).Click();
                        Assert.IsTrue(Regex.IsMatch(CloseAlertAndGetItsText(), "^Are you sure want to Logout[\\s\\S]$"));
                        driver.FindElement(By.LinkText("Logout")).Click();
                        Assert.AreEqual("Do you want to Logout", CloseAlertAndGetItsText());

                        break;
                    default:
                        break;
                }

                //if (dayname == "Monday")
                //{

                //    new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ddlTask"))).SelectByText("FinesseQ2 | FinesseQ2 | UK-EXCEED-NU-D");
                //    driver.FindElement(By.CssSelector("option[value=\"6$FinesseQ2 |  FinesseQ2 | EXCEED\"]")).Click();
                //    new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ddlMjrActvty"))).SelectByText("Analysis (SE)");
                //    driver.FindElement(By.CssSelector("option[value=\"202\"]")).Click();
                //    new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ddlSubActvty"))).SelectByText("Detailed Analysis");
                //    driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_btnIncROW")).Click();
                //    new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ddlMjrActvty"))).SelectByText("Testing (SE)");
                //    driver.FindElement(By.CssSelector("option[value=\"205\"]")).Click();
                //    new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ddlSubActvty"))).SelectByText("Test Environment Setup");
                //    driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_btnIncROW")).Click();
                //    new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ddlTask"))).SelectByText("Organisational Activities | OA | UK-EXCEED-NU-D");
                //    driver.FindElement(By.CssSelector("option[value=\"7$Organisational Activities |  OA | EXCEED\"]")).Click();
                //    new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ddlMjrActvty"))).SelectByText("HR Related Activities (OA)");
                //    driver.FindElement(By.CssSelector("option[value=\"504\"]")).Click();
                //    new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ddlSubActvty"))).SelectByText("Interviewing / participation in recruitment cycle");
                //    new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ddlMjrActvty"))).SelectByText("Non Project Training (OA)");
                //    driver.FindElement(By.CssSelector("option[value=\"505\"]")).Click();
                //    new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ddlSubActvty"))).SelectByText("Self learning / Learning new tools (non-project related)");
                //    driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_btnIncROW")).Click();
                //    new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ddlTask"))).SelectByText("Other Tasks | OT | UK-EXCEED-NU-D");
                //    driver.FindElement(By.CssSelector("option[value=\"8$Other Tasks |  OT | EXCEED\"]")).Click();
                //    new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ddlMjrActvty"))).SelectByText("Leave / Holiday");
                //    driver.FindElement(By.CssSelector("option[value=\"601\"]")).Click();
                //    new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ddlSubActvty"))).SelectByText("Holiday");
                //    driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_btnIncROW")).Click();
                //    new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ddlMjrActvty"))).SelectByText("Personal Time");
                //    driver.FindElement(By.CssSelector("option[value=\"602\"]")).Click();
                //    new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ddlSubActvty"))).SelectByText("Personal time");
                //    driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_btnIncROW")).Click();
                //    new SelectElement(driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ddlTask"))).SelectByText("Software Generic | SG | UK-EXCEED-NU-D");
                //    driver.FindElement(By.CssSelector("option[value=\"11$Software Generic |  SG | EXCEED\"]")).Click();
                //    driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_dgTest_ctl02_txtMon")).Clear();
                //    driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_dgTest_ctl02_txtMon")).SendKeys("9");
                //    Assert.AreEqual("Please enter Only Numeric Value", CloseAlertAndGetItsText());
                //    driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_dgTest_ctl06_txtMon")).Clear();
                //    driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_dgTest_ctl06_txtMon")).SendKeys(".5");
                //    Assert.AreEqual("Please enter Only Numeric Value", CloseAlertAndGetItsText());
                //    driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_btnSubmit")).Click();
                //    driver.FindElement(By.Id("ctl00_lnkLogout")).Click();
                //    Assert.IsTrue(Regex.IsMatch(CloseAlertAndGetItsText(), "^Are you sure want to Logout[\\s\\S]$"));
                //    driver.FindElement(By.LinkText("Logout")).Click();
                //    Assert.AreEqual("Do you want to Logout", CloseAlertAndGetItsText());
                //    // ERROR: Caught exception [ERROR: Unsupported command [waitForPopUp | _parent | 30000]]
                //}

                //driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_dgTest_ctl05_txtTue")).Clear();
                //driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_dgTest_ctl05_txtTue")).SendKeys("9");
                //Assert.AreEqual("Please enter Only Numeric Value", CloseAlertAndGetItsText());
                //driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_btnSubmit")).Click();
                //driver.FindElement(By.Id("ctl00_lnkLogout")).Click();
                //Assert.IsTrue(Regex.IsMatch(CloseAlertAndGetItsText(), "^Are you sure want to Logout[\\s\\S]$"));
                //driver.FindElement(By.LinkText("Logout")).Click();
                //Assert.AreEqual("Do you want to Logout", CloseAlertAndGetItsText());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }


        }
        public bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        public static string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }

    }
    
}
