using System.IO;
using System.Text;
using System.Web.UI;

namespace Webdiyer.WebControls.Test
{
    internal static class TestHelper
    {
        internal const string CopyrightEn = "<!--ASP.NET UrlPager 2.0 © 2009-2015 Webdiyer(http://en.webdiyer.com)-->";
        internal const string CopyrightCn = "<!--ASP.NET UrlPager 2.0 © 2009-2015 Webdiyer(http://www.webdiyer.com)-->";

        internal static string RenderControl(Control ctrl)
        {
            var sb = new StringBuilder();
            using (var sw = new StringWriter(sb))
            {
                using (var hw = new HtmlTextWriter(sw))
                {
                    ctrl.RenderControl(hw);
                    return sb.ToString();
                }
            }
        }
    }
}
