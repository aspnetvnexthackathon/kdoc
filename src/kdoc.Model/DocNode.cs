using System;
using System.Linq;
using System.Xml.Linq;

namespace kdoc.Model
{
    public abstract class DocNode
    {
        public string DocId { get; private set; }
        public string Name { get; private set; }
        public string Summary { get; set; }
        public string Remarks { get; set; }
        public string Examples { get; set; }
        public XElement DocXml { get; private set; }
        public DocNode Parent { get; set; }

        protected DocNode(string docId, string name)
        {
            DocId = docId;
            Name = name;
        }

        public void MergeXml(XElement docXml)
        {
            DocXml = docXml;

            if (docXml == null)
            {
                return;
            }

            var summary = docXml.Descendants(XName.Get("summary")).FirstOrDefault();
            if (summary != null)
            {
                Summary = summary.Value.Trim();
            }

            var remarks = docXml.Descendants(XName.Get("remarks")).FirstOrDefault();
            if (remarks != null)
            {
                Remarks = remarks.Value.Trim();
            }

            var examples = docXml.Descendants(XName.Get("example")).FirstOrDefault();
            if (examples != null)
            {
                Examples = examples.Value.Trim();
            }
        }
    }
}
