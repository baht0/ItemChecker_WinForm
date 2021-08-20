using ItemChecker.Model;
using ItemChecker.Net;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Diagnostics;
using ItemChecker.Support;
using System;

namespace ItemChecker.Presenter
{
    public class ProjectInfoPresenter
    {
        public static void getCurrentVersion()
        {
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();

                ProjectInfo.info.Add(assembly.GetName().Version.ToString());

                assembly = Assembly.LoadFrom(Application.StartupPath + "ItemChecker.Net.dll");
                ProjectInfo.info.Add(assembly.GetName().Version.ToString());

                assembly = Assembly.LoadFrom(Application.StartupPath + "ItemChecker.Support.dll");
                ProjectInfo.info.Add(assembly.GetName().Version.ToString());

                assembly = Assembly.LoadFrom(Application.StartupPath + "Newtonsoft.Json.dll");
                ProjectInfo.info.Add(assembly.GetName().Version.ToString());

                assembly = Assembly.LoadFrom(Application.StartupPath + "WebDriver.dll");
                ProjectInfo.info.Add(assembly.GetName().Version.ToString());

                assembly = Assembly.LoadFrom(Application.StartupPath + "WebDriver.Support.dll");
                ProjectInfo.info.Add(assembly.GetName().Version.ToString());

                ICapabilities capabilities = ((RemoteWebDriver)Main.Browser).Capabilities;
                var chromedriver = (capabilities.GetCapability("chrome") as Dictionary<string, object>)["chromedriverVersion"].ToString().Split(' ');
                ProjectInfo.info.Add(chromedriver[0]);
            }
            catch (Exception exp)
            {
                string currMethodName = MethodBase.GetCurrentMethod().Name;
                Exceptions.errorLog(exp, Main.assemblyVersion);
                Exceptions.errorMessage(exp, currMethodName);
            }            
        }
        public static void checkUpdate()
        {
            try
            {
                ProjectInfo._clear();

                XmlDocument xDoc = new();
                xDoc.LoadXml(Post.RequestDropbox());
                XmlElement xRoot = xDoc.DocumentElement;

                foreach (XmlNode xnode in xRoot)
                {
                    if (xnode.Name == "PropertyGroup")
                        foreach (XmlNode childnode in xnode.ChildNodes)
                        {
                            if (childnode.Name == "Version")
                                ProjectInfo.latest.Add(childnode.InnerText);
                            else if (childnode.Name == "Net.Version")
                                ProjectInfo.latest.Add(childnode.InnerText);
                            else if (childnode.Name == "Support.Version")
                                ProjectInfo.latest.Add(childnode.InnerText);
                        }
                    if (xnode.Name == "ItemGroup")
                        foreach (XmlNode childnode in xnode.ChildNodes)
                        {
                            XmlNode Include = childnode.Attributes.GetNamedItem("Include");
                            if (Include.Value == "Newtonsoft.Json")
                                ProjectInfo.latest.Add(childnode.Attributes.GetNamedItem("Version").Value);
                            else if (Include.Value == "Selenium.WebDriver")
                                ProjectInfo.latest.Add(childnode.Attributes.GetNamedItem("Version").Value);
                            else if (Include.Value == "Selenium.Support")
                                ProjectInfo.latest.Add(childnode.Attributes.GetNamedItem("Version").Value);
                            else if (Include.Value == "Selenium.WebDriver.ChromeDriver")
                                ProjectInfo.latest.Add(childnode.Attributes.GetNamedItem("Version").Value);
                        }
                }
                if (ProjectInfo.latest[0] != ProjectInfo.info[0])
                {
                    ProjectInfo.update.Add(true);
                    for (int i = 1; i < 7; i++)
                    {
                        if (ProjectInfo.latest[i] != ProjectInfo.info[i])
                            ProjectInfo.update.Add(true);
                        else
                            ProjectInfo.update.Add(false);
                    }
                }
            }
            catch (Exception exp)
            {
                string currMethodName = MethodBase.GetCurrentMethod().Name;
                Exceptions.errorLog(exp, Main.assemblyVersion);
                Exceptions.errorMessage(exp, currMethodName);
            }
        }
        public static void update()
        {
            string args = null;
            foreach (bool update in ProjectInfo.update)
                args += $"{update} ";

            ProcessStartInfo updater = new();
            updater.FileName = Application.StartupPath + "ItemChecker.Updater.exe";
            updater.Arguments = args;
            Process.Start(updater);

            MainPresenter.exit();
        }

        public static void createCurrentVersion()
        {
            if (!File.Exists("info.xml"))
                File.Delete(Application.StartupPath + "info.xml");

            new XDocument(
                new XElement("Project",
                    new XElement("PropertyGroup",
                        new XElement("Version", ProjectInfo.info[0]),
                        new XElement("Net.Version", ProjectInfo.info[1]),
                        new XElement("Support.Version", ProjectInfo.info[2])
                    ),
                    new XElement("ItemGroup",
                        new XElement("PackageReference", new XAttribute("Include", "Newtonsoft.Json"), new XAttribute("Version", ProjectInfo.info[3])),
                        new XElement("PackageReference", new XAttribute("Include", "Selenium.WebDriver"), new XAttribute("Version", ProjectInfo.info[4])),
                        new XElement("PackageReference", new XAttribute("Include", "Selenium.Support"), new XAttribute("Version", ProjectInfo.info[5])),
                        new XElement("PackageReference", new XAttribute("Include", "Selenium.WebDriver.ChromeDriver"), new XAttribute("Version", ProjectInfo.info[6]))
                    )
                )
            ).Save("info.xml");
        }
    }
}