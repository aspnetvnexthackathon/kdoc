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

        }

        /// <summary>
        /// Says 'Hello'.
        /// </summary>
        /// <param name="name">The name of the person to say hello to.</param>
        /// <returns>The hello message.</returns>
        public string SayHello(string name)
        {
            return string.Format("Hello {0}", name);
        }
    }
}
