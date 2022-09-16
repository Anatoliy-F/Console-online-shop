using System;

namespace ConsoleShop.Dal.Exception
{
    /// <summary>
    /// Represent errors that occur during execution when trying when trying to save entities that violate the integrity of the application
    /// </summary>
    [Serializable]
	public class CustomRepoException : System.Exception
    {
        /// <summary>
        /// Initializes a new instance of CustomRepoException without additional information about exception
        /// </summary>
        public CustomRepoException() { }

        /// <summary>
        /// Initializes a new instance of CustomRepoException with a specified error message
        /// </summary>
        /// <param name="message">string message</param>
		public CustomRepoException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of CustomRepoException with a specified error message and a reference to the inner exception that is the cause of this exception
        /// </summary>
        /// <param name="message">string message</param>
        /// <param name="inner">System.Exception inner</param>
		public CustomRepoException(string message, System.Exception inner) : base(message, inner) { }

        /// <summary>
        /// Initializes a new instance of CustomRepoException with serialized data
        /// </summary>
        /// <param name="info">System.Runtime.Serialization.SerializationInfo</param>
        /// <param name="context">System.Runtime.Serialization.StreamingContext</param>
		protected CustomRepoException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
