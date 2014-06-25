using Microsoft.Framework.Runtime;
using System;
using System.Runtime.Versioning;

namespace kdoc
{
    /// <summary>
    /// Summary description for FakeApplicationEnvironment
    /// </summary>
    public class ApplicationEnvironment : IApplicationEnvironment
    {
        public string ApplicationBasePath{get; set;}

        public string ApplicationName { get; set; }

        public FrameworkName TargetFramework { get; set; }

        public string Version { get; set; }

        public static ApplicationEnvironment Create(IApplicationEnvironment env)
        {
            return new ApplicationEnvironment
            {
                ApplicationBasePath = env.ApplicationBasePath,
                ApplicationName = env.ApplicationName,
                TargetFramework = env.TargetFramework,
                Version = env.Version
            };
        }
    }
}