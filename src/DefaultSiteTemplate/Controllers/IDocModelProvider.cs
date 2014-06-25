using kdoc;
using Microsoft.Framework.Runtime;

namespace DefaultSiteTemplate
{
    [AssemblyNeutral]
    public interface IDocModelProvider
    {
        DocNode GetDocModel(string docId);
    }
}