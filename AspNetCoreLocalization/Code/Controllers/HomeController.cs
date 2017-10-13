﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreLocalization.Code.Utilities;
using AspNetCoreLocalization.Properties;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspNetCoreLocalization.Code.Controllers
{
    
    public class HomeController : Controller
    {
        private IStringLocalizer _localizer;

        public HomeController(IStringLocalizer<CommonResources> localizer)
        {
            _localizer = localizer;
        }

        [Route("")]
        // GET: /<controller>/
        public IActionResult Index()
        {            
            return View();
        }



        [HttpPost]
        [HttpGet]
        public IActionResult Cultures(PersonModel model = null)
        {
            //if (model != null)
            //{
            //    if (!ModelState.IsValid)
            //    {
            //        ModelState.
            //    }
            //}
        

            string culture = Request.Query["culture"];
            
            ViewBag.OriginalHello = _localizer["HelloWorld"];

            TempData["lang"] = culture;
            ViewBag.Culture = culture;

            var listLanguages = new[]
            {
                new SelectListItem {Text = _localizer["BrowserDefault"], Value = ""},
                new SelectListItem {Text = _localizer["EnglishUs"], Value = "en-US"},
                new SelectListItem {Text = _localizer["English"], Value = "en"},
                new SelectListItem {Text = _localizer["German"], Value = "de"},
                new SelectListItem {Text = _localizer["French"], Value = "fr"},
                new SelectListItem {Text = _localizer["Polish"], Value = "pl"}
            };            
            var sel = listLanguages.FirstOrDefault(li => li.Value == culture);
            if (sel != null)
                sel.Selected = true;
            ViewBag.Languages = listLanguages;


            var cl = new CultureInfo("de-DE"); // this has no effect
            Thread.CurrentThread.CurrentCulture = cl;
            Thread.CurrentThread.CurrentUICulture = cl;

            return View();
        }


        

        public IActionResult ApiLinks()
        {
            return View();
        }
    }
}