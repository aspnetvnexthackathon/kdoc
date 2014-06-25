using System;
using Microsoft.AspNet.Mvc;

namespace DefaultSiteTemplate
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}