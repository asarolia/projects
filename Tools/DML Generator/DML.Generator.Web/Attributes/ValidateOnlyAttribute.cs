using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DML.Generator.Web.Attributes
{
    public class ValidateOnlyAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// This is a list of attributes whose errors will not be removed even if they are not in the current model collection
        /// </summary>
        private List<string> attributesToValidate;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateOnlyAttribute"/> class.
        /// </summary>
        public ValidateOnlyAttribute()
            : base()
        {
            this.attributesToValidate = new List<string>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateOnlyAttribute"/> class.
        /// </summary>
        /// <param name="attributesToValidate">A list of attributes whose errors will not be removed even if they are not in the current model collection</param>
        public ValidateOnlyAttribute(string[] attributesToValidate)
            : base()
        {
            this.attributesToValidate = new List<string>(attributesToValidate);
        }

        /// <summary>
        /// Called before the action method is invoked.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var modelState = filterContext.Controller.ViewData.ModelState;
            var incomingValues = filterContext.Controller.ValueProvider;

            var keys = modelState.Keys.Where(x => !this.attributesToValidate.Contains(x));

            foreach (var key in keys)
            {
                modelState[key].Errors.Clear();
            }

            base.OnActionExecuting(filterContext);
        }
    }
}