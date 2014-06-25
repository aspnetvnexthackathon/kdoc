using System;

namespace SampleLib
{
    /// <summary>
    /// Represents a thing that does a useful thing.
    /// </summary>
    public class SuperUsefulThing
    {
        /// <summary>
        /// The default salutation.
        /// </summary>
        public static readonly string DefaultSalutation = "Hello";

        /// <summary>
        /// Creates a new SuperUsefulThing using the default salutation (<see cref="DefaultSalutation" />).
        /// </summary>
        public SuperUsefulThing()
            : this(DefaultSalutation)
        {
            
        }

        /// <summary>
        /// Creates a new SuperUsefulThing.
        /// </summary>
        /// <param name="salutation">The salutation to use.</param>
        public SuperUsefulThing(string salutation)
        {
            Salutation = salutation;
        }

        /// <summary>
        /// The greeting to use when saying hello.
        /// </summary>
        public string Salutation { get; set; }

        /// <summary>
        /// Raised when <see cref="SayHello(string)"/> is called.
        /// </summary>
        public event EventHandler Hello;

        /// <summary>
        /// Says 'Hello'.
        /// </summary>
        /// <param name="name">The name of the person to say hello to.</param>
        /// <returns>The hello message.</returns>
        public string SayHello(string name)
        {
            return SayHello(name, yell: false);
        }

        /// <summary>
        /// Says 'Hello'.
        /// </summary>
        /// <param name="name">The name of the person to say hello to.</param>
        /// <param name="yell">A <see cref="bool"/> indicating whether the message should be loud.</param>
        /// <returns>The hello message.</returns>
        public string SayHello(string name, bool yell)
        {
            OnHello();
            var message = string.Format("{0} {1}", Salutation, name);
            return yell ? message.ToUpper() : message;
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void OnHello()
        {
            if (Hello != null)
            {
                Hello(this, EventArgs.Empty);
            }
        }
    }
}
