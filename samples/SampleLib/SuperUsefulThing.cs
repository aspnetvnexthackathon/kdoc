using System;

namespace SampleLib
{
    /// <summary>
    /// Represents a thing that does a useful thing.
    /// </summary>
    public class SuperUsefulThing
    {
        /// <summary>
        /// Creates a new SuperUsefulThing.
        /// </summary>
        public SuperUsefulThing()
        {
            Salutation = "Hello";
        }

        /// <summary>
        /// The greeting to use when saying hello.
        /// </summary>
        public string Salutation { get; set; }

        /// <summary>
        /// Says 'Hello'.
        /// </summary>
        /// <param name="name">The name of the person to say hello to.</param>
        /// <returns>The hello message.</returns>
        /// <example ref="docs\apiref\SampleLib\SetupUsefulThing.SayHello.md" />
        public string SayHello(string name)
        {
            return string.Format("{0} {1}", Salutation, name);
        }
    }
}
