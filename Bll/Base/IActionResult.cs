using System;
using System.Collections.Generic;
using System.Text;
using ConsoleShop.Model.BaseEntity;

namespace ConsoleShop.Controller.Base
{
    /// <summary>
    /// Represents possible response statuses
    /// </summary>
    public enum ActionResult
    {
        /// <summary>
        /// Request processed successfully
        /// </summary>
        Succes,
        /// <summary>
        /// An error occurred while executing the request
        /// </summary>
        Error,
        /// <summary>
        ///Input data does not match the required format
        /// </summary>
        Warning,
        /// <summary>
        /// Requested object not found
        /// </summary>
        NotFound
    }

    /// <summary>
    /// Define response object
    /// </summary>
    public interface IActionResult
    {
        /// <summary>
        /// Represents response status <see cref="ConsoleShop.Controller.Base.ActionResult"/>
        /// </summary>
        public ActionResult Result { get; }

        /// <summary>
        /// Represents response message
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Represents collection of requested entities
        /// </summary>
        public IEnumerable<IEntity> ResultBody { get; }

        /// <summary>
        /// This method determines the type of response form, for example: writing to a file,
        /// response over the network using JSON or html, etc.
        /// </summary>
        public void RenderResult();
    }
}
