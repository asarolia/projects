namespace DML.Generator.Web.Attributes
{
    using System.Reflection;
    using System.Web.Mvc;

    /// <summary>
    /// Buttong attribute
    /// </summary>
    public class ButtonAttribute: ActionMethodSelectorAttribute
    {
        /// <summary>
        /// Gets or sets the name of the button.
        /// </summary>
        /// <value>
        /// The name of the button.
        /// </value>
        public string ButtonName { get; set; }

        /// <summary>
        /// Determines whether the action method selection is valid for the specified controller context.
        /// </summary>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="methodInfo">Information about the action method.</param>
        /// <returns>
        /// true if the action method selection is valid for the specified controller context; otherwise, false.
        /// </returns>
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            return controllerContext.Controller.ValueProvider.GetValue(ButtonName) != null;
        }
    }
}