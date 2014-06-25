using Microsoft.Framework.Runtime;
using System;
using System.Runtime.Versioning;

namespace kdoc
{
    /// <summary>
    /// Summary description for FakeApplicationEnvironment
    /// </summary>
    public class FakeApplicationEnvironment : IApplicationEnvironment
    {
        public string ApplicationBasePath{get; set;}

        public string ApplicationName { get; set; }

        public FrameworkName TargetFramework { get; set; }

        public string Version { get; set; }

        public static FakeApplicationEnvironment Create(IApplicationEnvironment env)
        {
            return new FakeApplicationEnvironment
            {
                ApplicationBasePath = env.ApplicationBasePath,
                ApplicationName = env.ApplicationName,
                TargetFramework = env.TargetFramework,
                Version = env.Version
            };
        }
    }
}