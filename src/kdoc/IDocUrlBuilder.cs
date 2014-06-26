using System;
using Microsoft.Framework.Runtime;

namespace DefaultSiteTemplate
{
    [AssemblyNeutral]
    public interface IDocUrlBuilder
    {
	    string Url(string docId);
    }

    public class DocUrlBuilder : IDocUrlBuilder
    {
        public string Url(string docId)
        {
            return docId;
        }
    }
}