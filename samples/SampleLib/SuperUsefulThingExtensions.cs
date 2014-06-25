using System;

namespace SampleLib
{
    /// <summary>
    /// Extesion methods for <see cref="SampleLib.SuperUsefulThing" />
    /// </summary>
    public static class SuperUsefulThingExtensions
    {
        /// <summary>
        /// Says 'Goodbye'.
        /// </summary>
        /// <param name="name">The name of the person to say goodbye to.</param>
        /// <returns>The goodbye message.</returns>
	    public static string SayGoodbye(string name)
        {
            return string.Format("Goodbye {0}", name);
        }
    }
}