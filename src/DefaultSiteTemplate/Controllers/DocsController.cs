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

        public ActionResult Index(string docId = "Pk:SamplePackage", string templateName = "Package")
        {
            var model = _docModelProvider.GetDocModel(docId);
            ViewBag.Title = GetTitleFromDocModel(model);
            return View(templateName, model);
        }

        private string GetTitleFromDocModel(DocNode node)
        {
            var title = node.Name;
            var currentNode = node.Parent;

            while (currentNode != null)
            {
                title = currentNode.Name;
                currentNode = currentNode.Parent;
            }

            return title;
        }
    }
}