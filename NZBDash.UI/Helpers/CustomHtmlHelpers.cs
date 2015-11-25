using System.Web;
using System.Web.Mvc;

namespace NZBDash.UI.Helpers
{
    public static class CustomHtmlHelpers
    {
        public static IHtmlString LoadingSpinner(this HtmlHelper html)
        {
            return html.Raw("<i class='fa fa-refresh fa-spin fa-5x'></i>");
        }

        public static IHtmlString AjaxPanel(this HtmlHelper html)
        {
            return html.Raw(string.Format(@"<div class='panel panel-default'>
                                <div class='panel-heading'>
                                </div>
                                <div class='panel-body'>
                                    <div class='col-md-12'>
                                        <div class='col-md-12 h6 text-center'>{0}
                                        </div>
                                    </div>
                                </div>
                            </div>", LoadingSpinner(html)));
        }

        public static IHtmlString AjaxPanel(this HtmlHelper html, int maxCol)
        {
            return html.Raw(
                string.Format(
                    @"<div class='panel panel-default'>
                        <div class='panel-heading'>
                        </div>
                        <div class='panel-body'>
                            <div class='col-md-{1}'>
                                <div class='col-md-{1} h6 text-center'>{0}
                                </div>
                            </div>
                        </div>
                     </div>", LoadingSpinner(html), maxCol));

        }
    }
}