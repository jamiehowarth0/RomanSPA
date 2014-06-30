namespace RomanSPA.AngularJS {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;
    using RomanSPA.Core;

    public class AngularHtmlString : IHtmlString {

        internal const string ngPrefix = "data-ng-";
        internal const string ngApp = "app";
        internal const string ngBind = "bind";
        internal const string ngBindHtml = "bind-html";
        internal const string ngBindTemplate = "bind-template";
        internal const string ngBlur = "blur";
        internal const string ngChange = "change";
        internal const string ngChecked = "checked";
        internal const string ngClass = "class";
        internal const string ngClassEven = "class-even";
        internal const string ngClassOdd = "class-odd";
        internal const string ngClick = "click";
        internal const string ngCloak = "cloak";
        internal const string ngController = "controller";
        internal const string ngCopy = "copy";
        // internal const string ngCsp = "csp";
        internal const string ngCut = "cut";
        internal const string ngDblclick = "dblclick";
        internal const string ngDisabled = "disabled";
        internal const string ngFocus = "focus";
        internal const string ngForm = "form";
        internal const string ngHide = "hide";
        internal const string ngHref = "href";
        internal const string ngIf = "if";
        internal const string ngInclude = "include";
        internal const string ngInit = "init";
        internal const string ngKeydown = "keydown";
        internal const string ngKeypress = "keypress";
        internal const string ngKeyup = "keyup";
        internal const string ngList = "list";
        internal const string ngModel = "model";
        internal const string ngModelOptions = "model-options";
        internal const string ngMousedown = "mousedown";
        internal const string ngMouseenter = "mouseenter";
        internal const string ngMouseleave = "mouseleave";
        internal const string ngMousemove = "mousemove";
        internal const string ngMouseover = "mouseover";
        internal const string ngMouseup = "mouseup";
        internal const string ngNonBindable = "non-bindable";
        internal const string ngOpen = "open";
        internal const string ngPaste = "paste";
        internal const string ngPluralize = "pluralize";
        internal const string ngReadonly = "readonly";
        internal const string ngRepeat = "repeat";
        internal const string ngSelected = "selected";
        internal const string ngShow = "show";
        internal const string ngSrc = "src";
        internal const string ngSrcset = "srcset";
        internal const string ngStyle = "style";
        internal const string ngSubmit = "submit";
        internal const string ngSwitch = "switch";
        internal const string ngTransclude = "transclude";
        internal const string ngValue = "value";
        internal const string TagFormat= "<{0} {1}>{2}</{0}>";
        internal const string AttributeFormat = "{0}=\"{1}\"";
        internal const string AttributeKeyFormat = "{0}{1}";

        internal static class CheckBox {
            internal const string TrueValue = "true-value";
            internal const string FalseValue = "false-value";
        }

        private static string TagName;

        private static HttpContextBase _context;
        
        private Dictionary<string, string> ParamValues { get; set; }

        private Func<IHtmlString> RenderServer { get; set; }

        public string ToHtmlString() {
                return _context.IsRomanViewRequest() ?
                    String.Format(TagFormat, TagName, ParseDictionary(), String.Empty) : 
                    String.Format(TagFormat, TagName, null, RenderServer().ToHtmlString());
        }

        internal string WriteAngularAttribute(string attr) {
            return String.Format(AttributeKeyFormat, ngPrefix, attr);
        }

        internal AngularHtmlString WriteParam(string key, string value) {
            ParamValues.Add(key, value);
            return this;
        }

        internal string ParseDictionary() {
            var sb = new StringBuilder();
            foreach (var key in ParamValues.Keys) sb.Append(String.Format(AttributeFormat, key, ParamValues[key]));
            return sb.ToString().Trim();
        }

        public AngularHtmlString ServerResponse(Func<IHtmlString> method) {
            RenderServer = method;
            return this;
        }

        static AngularHtmlString() {
            _context = new HttpContextWrapper(HttpContext.Current);
        }

        internal AngularHtmlString() {
            ParamValues = new Dictionary<string, string>();
        }

        public AngularHtmlString Tag(string tagName) {
            TagName = tagName;
            return this;
        }

        public AngularHtmlString App(string propToBind) {
            return WriteParam(WriteAngularAttribute(ngApp), propToBind);
        }

        public AngularHtmlString Bind(string propToBind) {
            return WriteParam(WriteAngularAttribute(ngBind), propToBind);
        }

        public AngularHtmlString BindHtml(string propToBind) {
            return WriteParam(WriteAngularAttribute(ngBindHtml), propToBind);
        }

        public AngularHtmlString BindTemplate(string propToBind) {
            return WriteParam(WriteAngularAttribute(ngBindTemplate), propToBind);
        }

        public AngularHtmlString Blur(string propToBind) {
            return WriteParam(WriteAngularAttribute(ngBlur), propToBind);
        }

        public AngularHtmlString Change(string propToBind) {
            return WriteParam(WriteAngularAttribute(ngChange), propToBind);
        }

        public AngularHtmlString Checked(string propToBind) {
            return WriteParam(WriteAngularAttribute(ngChecked), propToBind);
        }

        public AngularHtmlString Class(string propToBind) {
            return WriteParam(WriteAngularAttribute(ngClass), propToBind);
        }

        public AngularHtmlString ClassEven(string propToBind) {
            return WriteParam(WriteAngularAttribute(ngClassEven), propToBind);
        }

        public AngularHtmlString ClassOdd(string propToBind) {
            return WriteParam(WriteAngularAttribute(ngClassOdd), propToBind);
        }

        public AngularHtmlString Click(string propToBind) {
            return WriteParam(WriteAngularAttribute(ngClick), propToBind);
        }

        public AngularHtmlString Cloak(string propToBind) {
            return WriteParam(WriteAngularAttribute(ngCloak), propToBind);
        }

        public AngularHtmlString Controller(string propToBind) {
            return WriteParam(WriteAngularAttribute(ngController), propToBind);
        }

        public AngularHtmlString Copy(string propToBind) {
            return WriteParam(WriteAngularAttribute(ngCopy), propToBind);
        }

        public AngularHtmlString Cut(string propToBind) {
            return WriteParam(WriteAngularAttribute(ngCut), propToBind);
        }

        public AngularHtmlString Dblclick(string propToBind) {
            return WriteParam(WriteAngularAttribute(ngDblclick), propToBind);
        }

        public AngularHtmlString Disabled(string propToBind) {
            return WriteParam(WriteAngularAttribute(ngDisabled), propToBind);
        }

        public AngularHtmlString Focus(string propToBind) {
            return WriteParam(WriteAngularAttribute(ngFocus), propToBind);
        }

        public AngularHtmlString Form(string propToBind) {
            return WriteParam(WriteAngularAttribute(ngForm), propToBind);
        }

        public AngularHtmlString Hide(string propToBind) {
            return WriteParam(WriteAngularAttribute(ngHide), propToBind);
        }

        public AngularHtmlString Href(string propToBind) {
            return WriteParam(WriteAngularAttribute(ngHref), propToBind);
        }

        public AngularHtmlString If(string propToBind) {
            return WriteParam(WriteAngularAttribute(ngIf), propToBind);
        }

        public AngularHtmlString Include(string propToBind) {
            return WriteParam(WriteAngularAttribute(ngInclude), propToBind);
        }

        public AngularHtmlString Init(string propToBind) {
            return WriteParam(WriteAngularAttribute(ngInit), propToBind);
        }

        public AngularHtmlString Keydown(string propToBind) {
            return WriteParam(WriteAngularAttribute(ngKeydown), propToBind);
        }

        public AngularHtmlString Keypress(string propToBind) {
            return WriteParam(WriteAngularAttribute(ngKeypress), propToBind);
        }

        public AngularHtmlString Keyup(string propToBind) {
            return WriteParam(WriteAngularAttribute(ngKeyup), propToBind);
        }

        public AngularHtmlString List(string propToBind) {
            return WriteParam(WriteAngularAttribute(ngList), propToBind);
        }

        public AngularHtmlString Model(string propToBind) {
            return WriteParam(WriteAngularAttribute(ngModel), propToBind);
        }

        public AngularHtmlString ModelOptions(string propToBind) {
            return WriteParam(WriteAngularAttribute(ngModelOptions), propToBind);
        }

        public AngularHtmlString Mousedown(string propToBind) {
            return WriteParam(WriteAngularAttribute(ngMousedown), propToBind);
        }

        public AngularHtmlString Mouseenter(string propToBind) {
            return WriteParam(WriteAngularAttribute(ngMouseenter), propToBind);
        }

        public AngularHtmlString Mouseleave(string propToBind) {
            return WriteParam(WriteAngularAttribute(ngMouseleave), propToBind);
        }

        public AngularHtmlString Mousemove(string propToBind) {
            return WriteParam(WriteAngularAttribute(ngMousemove), propToBind);
        }

        public AngularHtmlString Mouseover(string propToBind) {
            return WriteParam(WriteAngularAttribute(ngMouseover), propToBind);
        }

        public AngularHtmlString Mouseup(string propToBind) {
            return WriteParam(WriteAngularAttribute(ngMouseup), propToBind);
        }

        public AngularHtmlString NonBindable(string propToBind) {
            return WriteParam(WriteAngularAttribute(ngNonBindable), propToBind);
        }

        public AngularHtmlString Open(string propToBind) {
            return WriteParam(WriteAngularAttribute(ngOpen), propToBind);
        }

        public AngularHtmlString Paste(string propToBind) {
            return WriteParam(WriteAngularAttribute(ngPaste), propToBind);
        }

        public AngularHtmlString Pluralize(string propToBind) {
            return WriteParam(WriteAngularAttribute(ngPluralize), propToBind);
        }

        public AngularHtmlString Readonly(string propToBind) {
            return WriteParam(WriteAngularAttribute(ngReadonly), propToBind);
        }

        public AngularHtmlString Repeat(string propToBind) {
            return WriteParam(WriteAngularAttribute(ngRepeat), propToBind);
        }

        public AngularHtmlString Selected(string propToBind) {
            return WriteParam(WriteAngularAttribute(ngSelected), propToBind);
        }

        public AngularHtmlString Show(string propToBind) {
            return WriteParam(WriteAngularAttribute(ngShow), propToBind);
        }

        public AngularHtmlString Src(string propToBind) {
            return WriteParam(WriteAngularAttribute(ngSrc), propToBind);
        }

        public AngularHtmlString Srcset(string propToBind) {
            return WriteParam(WriteAngularAttribute(ngSrcset), propToBind);
        }

        public AngularHtmlString Style(string propToBind) {
            return WriteParam(WriteAngularAttribute(ngStyle), propToBind);
        }

        public AngularHtmlString Submit(string propToBind) {
            return WriteParam(WriteAngularAttribute(ngSubmit), propToBind);
        }

        public AngularHtmlString Switch(string propToBind) {
            return WriteParam(WriteAngularAttribute(ngSwitch), propToBind);
        }

        public AngularHtmlString Transclude(string propToBind) {
            return WriteParam(WriteAngularAttribute(ngTransclude), propToBind);
        }

        public AngularHtmlString Value(string propToBind) {
            return WriteParam(WriteAngularAttribute(ngValue), propToBind);
        }
    }
}