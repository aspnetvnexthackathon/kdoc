using System;
using System.Linq;
using kdoc.Model;

namespace DefaultSiteTemplate
{
    public class DesignTimeDocUrlBuilder : IDocUrlBuilder
    {
        public string Url(string docId)
        {
            return string.Format("?docId={0}&templateName={1}", docId, GetTemplateName(docId));
        }

        private string GetTemplateName(string docId)
        {
            var idType = docId.Split(':').FirstOrDefault().ToUpperInvariant();

            switch (idType)
            {
                case "A":
                    return "Assembly";

                case "N":
                    return "Namespace";

                case "T":
                    return "Class";

                case "P":
                    return "Property";

                case "M":
                    return "Method";

                case "E":
                    return "Event";

                case "Pk":
                default:
                    return "Package";
            }
        }
    }
}