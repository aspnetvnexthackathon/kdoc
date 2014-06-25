using System;
using Microsoft.AspNet.Mvc;

namespace DefaultSiteTemplate
{
    public class DocsController : Controller
    {
        private readonly IDocModelProvider _docModelProvider;

        public DocsController(IDocModelProvider docModelProvider)
        {
            _docModelProvider = docModelProvider;
        }

        public ActionResult Index(string docId = "SamplePackage", string templateName = "Package")
        {
            return View(templateName, _docModelProvider.GetDocModel(docId));
        }
    }
}