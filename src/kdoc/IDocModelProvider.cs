using System;
using kdoc.Model;
using Microsoft.Framework.Runtime;

namespace DefaultSiteTemplate
{
    [AssemblyNeutral]
    public interface IDocModelProvider
    {
        DocNode GetDocModel(string docId);
    }

    public class DocModelProvider : IDocModelProvider
    {
        private readonly DocModel _model;
        public DocModelProvider(DocModel model)
        {
            _model = model;
        }

        public DocNode GetDocModel(string docId)
        {
            return _model.Nodes[docId];
        }
    }
}