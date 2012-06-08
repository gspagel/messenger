using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Sitecore.SharedSource.Messenger
{
    public class MessageWindowRenderer
    {
        public void Process(Sitecore.Pipelines.PipelineArgs args)
        {
            if (Sitecore.Context.ClientPage.IsEvent)
            {
                return;
            }

            HttpContext context = HttpContext.Current;

            if (context == null)
            {
                return;
            }

            Page page = context.Handler as Page;

            if (page == null)
            {
                return;
            }

            this.RenderWindow(page);
        }

        private void RenderWindow(Page page)
        {
            // Load the jQuery framework, if not done already
            HtmlGenericControl script = new HtmlGenericControl("script");
            script.Attributes.Add("type", "text/javascript");
            script.Attributes.Add("src", "http://ajax.googleapis.com/ajax/libs/jquery/1/jquery.min.js");

            page.Header.Controls.Add(script);

            // Add the script that renders the messenger window
            // in the content editor
            page.ClientScript.RegisterClientScriptResource(this.GetType(), "Sitecore.SharedSource.Messenger.Resources.message.window.js");
        }
    }
}