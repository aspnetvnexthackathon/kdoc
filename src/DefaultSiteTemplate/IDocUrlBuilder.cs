using System;
using Microsoft.Framework.Runtime;

namespace DefaultSiteTemplate
{
    [AssemblyNeutral]
    public interface IDocUrlBuilder
    {
	    string Url(string docId);
    }
}