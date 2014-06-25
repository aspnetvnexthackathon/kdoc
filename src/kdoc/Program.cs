using System;
using Microsoft.Framework.Runtime;

namespace kdoc
{
    public class Program
    {
        private readonly IApplicationEnvironment _appEnvironment;

        public Program(IApplicationEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }

        public void Main(string[] args)
        {
            Console.WriteLine(string.Format("kdoc will do amazing things for {0} in {1}", _appEnvironment.ApplicationName, _appEnvironment.ApplicationBasePath));
            Console.ReadLine();
        }
    }
}
