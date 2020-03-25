namespace AccounterApplication.Web.ViewModels
{
    public class MessageAlertViewModel
    {
        public MessageAlertViewModel(string cssClass, string buttonCssClass, string title, string message)
        {
            this.CssClass = cssClass;
            this.ButtonCssClass = buttonCssClass;
            this.Title = title;
            this.Message = message;
        }

        public string CssClass { get;  private set; }

        public string ButtonCssClass { get; private set; }

        public string Title { get; private set; }

        public string Message { get; private set; }
    }
}
