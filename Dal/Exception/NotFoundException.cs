using System;

namespace ConsoleShop.Dal.Exception
{
    /// <summary>
    /// Represent errors that occur during execution when the related entity is not in the store
    /// </summary>
    [Serializable]
    public class NotFoundException : System.Exception
    {
        /// <summary>
        /// Initializes a new instance of NotFoundException without additional information about exception
        /// </summary>
        public NotFoundException() { }

        /// <summary>
        /// Initializes a new instance of NotFoundException with a specified error message
        /// </summary>
        /// <param name="message">string message</param>
        public NotFoundException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of NotFoundException with a specified error message and a reference to the inner exception that is the cause of this exception
        /// </summary>
        /// <param name="message">string message</param>
        /// <param name="inner">System.Exception inner</param>
        public NotFoundException(string message, System.Exception inner) : base(message, inner) { }

        /// <summary>
        /// Initializes a new instance of NotFoundException with serialized data
        /// </summary>
        /// <param name="info">System.Runtime.Serialization.SerializationInfo</param>
        /// <param name="context">System.Runtime.Serialization.StreamingContext</param>
        protected NotFoundException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

}
