using System;
using kdoc.Model;
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

        public ActionResult Index(string docId = "SamplePackage", string templateName = "Index")
        {
            return View(templateName, _docModelProvider.GetDocModel(docId));
        }
    }
}