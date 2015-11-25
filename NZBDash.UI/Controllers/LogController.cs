using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;

using NLog;

using NZBDash.UI.Models;

using ILogger = NZBDash.Common.Interfaces.ILogger;

namespace NZBDash.UI.Controllers
{
    public class LogController : BaseController
    {
        public LogController(ILogger logger)
            : base(logger)
        {

        }

        // GET: Log
        public ActionResult Index()
        {
            var model = new LogViewModel { LogLevel = LoggingLevel.Warn };
            if (Session["LogLevel"] != null)
            {
                model.LogLevel = (LoggingLevel)Session["LogLevel"];
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(LogViewModel model)
        {
            Session["LogLevel"] = model.LogLevel;
            ReconfigureLogLevel(model.LogLevel);
            return RedirectToAction("Index");
        }

        internal IEnumerable<string> GetLog(string path)
        {
            if (!System.IO.File.Exists(path))
            {
                return new List<string> { "File doesn't exist" };
            }
            var lines = System.IO.File.ReadLines(path, Encoding.UTF8);
            return lines;
        }

        internal IEnumerable<string> LogFiles()
        {
            var directory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/NZBDash/";

            return Directory.GetFiles(directory).ToList();
        }

        public static void ReconfigureLogLevel(LoggingLevel level)
        {
            foreach (var rule in LogManager.Configuration.LoggingRules)
            {
                // Remove all levels
                rule.DisableLoggingForLevel(LogLevel.Trace);
                rule.DisableLoggingForLevel(LogLevel.Info);
                rule.DisableLoggingForLevel(LogLevel.Debug);
                rule.DisableLoggingForLevel(LogLevel.Warn);
                rule.DisableLoggingForLevel(LogLevel.Error);
                rule.DisableLoggingForLevel(LogLevel.Fatal);

                switch (level)
                {
                    case LoggingLevel.Trace:
                        rule.EnableLoggingForLevel(LogLevel.Trace);
                        rule.EnableLoggingForLevel(LogLevel.Info);
                        rule.EnableLoggingForLevel(LogLevel.Debug);
                        rule.EnableLoggingForLevel(LogLevel.Warn);
                        rule.EnableLoggingForLevel(LogLevel.Error);
                        rule.EnableLoggingForLevel(LogLevel.Fatal);
                        break;
                    case LoggingLevel.Info:
                        rule.EnableLoggingForLevel(LogLevel.Info);
                        rule.EnableLoggingForLevel(LogLevel.Debug);
                        rule.EnableLoggingForLevel(LogLevel.Warn);
                        rule.EnableLoggingForLevel(LogLevel.Error);
                        rule.EnableLoggingForLevel(LogLevel.Fatal);
                        break;
                    case LoggingLevel.Debug:
                        rule.EnableLoggingForLevel(LogLevel.Debug);
                        rule.EnableLoggingForLevel(LogLevel.Warn);
                        rule.EnableLoggingForLevel(LogLevel.Error);
                        rule.EnableLoggingForLevel(LogLevel.Fatal);
                        break;
                    case LoggingLevel.Warn:
                        rule.EnableLoggingForLevel(LogLevel.Warn);
                        rule.EnableLoggingForLevel(LogLevel.Error);
                        rule.EnableLoggingForLevel(LogLevel.Fatal);
                        break;
                    case LoggingLevel.Error:
                        rule.EnableLoggingForLevel(LogLevel.Error);
                        rule.EnableLoggingForLevel(LogLevel.Fatal);
                        break;
                    case LoggingLevel.Fatal:
                        rule.EnableLoggingForLevel(LogLevel.Fatal);
                        break;
                }
            }

            //Call to update existing Loggers created with GetLogger() or
            //GetCurrentClassLogger()
            LogManager.ReconfigExistingLoggers();
        }
    }
}