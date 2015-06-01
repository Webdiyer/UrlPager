using System;

namespace Webdiyer.UrlPagerDemo
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var title = Page.Title;
            var kw = Page.MetaKeywords;
            //Page.Title += " - UrlPager demo";
            Page.MetaKeywords = "UrlPager,UrlPager paging samples,UrlPager demos,ASP.NET WebForm route paging," + (string.IsNullOrWhiteSpace(kw) ? title : kw);
        }
    }
}
