namespace RomanSPA.Core {
    using System;
    using System.Web.Mvc;
    using RomanSPA.Core.Models;
    
    public class RomanActionAttribute : ActionFilterAttribute {

        #region Properties

        public Type Factory {
            get { return ModelFactory != null ? ModelFactory.GetType() : null; }
            set { _factory = value; InvokeFactory(); }
        }

        public IModelFactory ModelFactory { get { return _modelFactory; } }
        
        public string ControllerName { get; set; }

        public string ViewPath { get; set; }

        #endregion

        #region Fields

        private Type _factory;
        private IModelFactory _modelFactory;

        #endregion

        #region .ctors

        public RomanActionAttribute() : this(null, String.Empty, String.Empty) { }

        public RomanActionAttribute(string viewPath) : this(null, String.Empty, viewPath) { }

        public RomanActionAttribute(string controllerName, string viewPath) : this(null, controllerName, viewPath) { }

        public RomanActionAttribute(Type factory, string viewPath) : this(factory, String.Empty, viewPath) { }

        public RomanActionAttribute(Type factory, string controllerName, string viewPath) {
            Factory = factory; ControllerName = controllerName; ViewPath = viewPath;
            if (Factory != null) {
                InvokeFactory();
            }
        }

        #endregion
        
        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            if (filterContext.HttpContext.IsRomanModelRequest()) {
                filterContext.Result = new JsonResult() {
                    Data = ((ModelFactory != null) ? ModelFactory.Execute() : new object()),
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            } else {
                base.OnActionExecuting(filterContext);
            }
        }

        private void InvokeFactory() {
            if (_factory != null) {
                try {
                    _modelFactory = (IModelFactory)Activator.CreateInstance(_factory);
                } catch (Exception ex) {
                    throw new ArgumentException(String.Format("Could not create factory for type '{0}'", _factory.Name), ex);
                }
            }
        }
    }
}
