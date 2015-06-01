using System;

namespace Webdiyer.UrlPagerDemo
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void PageChanged(object sender, EventArgs e)
        {
            lb_txt.Text = "PageChanged event fired, current page index is " + pager1.CurrentPageIndex;
        }
    }
}
