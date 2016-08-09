namespace DML.Generator.Web.Controllers
{
    using System.Web.Mvc;
    using System.Web.Routing;
    using DML.Generator.Web.Models;

    public class BaseController : Controller
    {
        /// <summary>
        /// FileUpload view model
        /// </summary>
        public FileUploadViewModel FileUploadViewModel;
        
        /// <summary>
        /// Session Key
        /// </summary>
        public const string sessionKey = "FileUploadModel";
        
        

        /// <summary>
        /// Called before the action method is invoked.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (Session[sessionKey] != null)
            {
                this.FileUploadViewModel = (FileUploadViewModel)Session[sessionKey];
            }
        }

        /// <summary>
        /// Called before the action result that is returned by an action method is executed.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action result</param>
        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
            Session[sessionKey] = this.FileUploadViewModel;
        }

        /// <summary>
        /// Called when an unhandled exception occurs in the action.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected override void OnException(ExceptionContext filterContext)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(filterContext.Exception);
            base.OnException(filterContext);
        }
    }
}
