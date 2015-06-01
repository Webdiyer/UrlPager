using System;
using System.Text;
using System.Web;
using System.Web.Routing;
using Xunit;

namespace Webdiyer.WebControls.Test
{
    public class RoutesTest : IDisposable
    {
        private const string Route1Name = "Route1";
        private const string Route2Name = "Route2";
        private const string Route3Name = "Route3";
        private const string Route4Name = "Route4";

        public RoutesTest()
        {
            if (RouteTable.Routes[Route1Name]==null)
            {
                RouteTable.Routes.MapPageRoute(Route1Name,
                    "{controller}/{action}/{page}",
                    "~/test.aspx", false,
                    new RouteValueDictionary { { "controller", "employees" }, { "action", "list" }, { "page", 1 } });
            }
            if (RouteTable.Routes[Route2Name] == null)
            {
                RouteTable.Routes.MapPageRoute("Route2",
                    "{controller}/{action}/{page}",
                    "~/urlpager.aspx", false,
                    new RouteValueDictionary { { "controller", "employees" }, { "action", "list" }, { "page", 1 } });
            }
            if (RouteTable.Routes[Route3Name] == null)
            {
                RouteTable.Routes.MapPageRoute("Route3",
                    "{controller}/{action}/{id}",
                    "~/test.aspx", false,
                    new RouteValueDictionary { { "controller", "urlpager" }, { "action", "test" }, { "id", 1 } },
                    new RouteValueDictionary { { "controller", "UrlPager" }, { "action", "test" } });
            }
            if (RouteTable.Routes[Route4Name] == null)
            {
                RouteTable.Routes.MapPageRoute("Route4",
                    "{author}/{city}/{pageIndex}",
                    "~/test.aspx");
            }
        }
        [Fact]
        public void UrlWithQueryString_ShouldGenerateCorrectHtml()
        {
            HttpContext.Current = new HttpContext(new HttpRequest("~/test.aspx", "http://en.webdiyer.com/urlpager/", "author=webdiyer&city=wuqi"), new HttpResponse(null));


            const string linkFormat = "<a href=\"/employees/list/{0}?author=webdiyer&city=wuqi\">{1}</a>";
            var pager = new UrlPager { TotalItemCount = 168, PageIndexParameterName = "page", RouteName = "Route1" };
            var sb = new StringBuilder(TestHelper.CopyrightEn).Append("\r\n<div>\r\n");
            sb.Append("FirstPrev1");
            for (int i = 2; i <= 10; i++)
            {
                sb.AppendFormat(linkFormat, i, i);
            }
            sb.AppendFormat(linkFormat, 11, "...");
            sb.AppendFormat(linkFormat, 2, "Next");
            sb.AppendFormat(linkFormat, 17, "Last");
            sb.Append("\r\n</div>\r\n");
            sb.Append(TestHelper.CopyrightEn);
            Assert.Equal(TestHelper.RenderControl(pager), sb.ToString());
        }


        [Fact]
        public void RouteWithAddedValues_ShouldGenerateCorrectHtml()
        {
            HttpContext.Current = new HttpContext(new HttpRequest("~/urlpager.aspx", "http://webdiyer.com/urlpager/", null), new HttpResponse(null));
            const string linkFormat = "<a href=\"/employees/list/{0}?author=webdiyer&city=wuqi\">{1}</a>";
            var pager = new UrlPager { TotalItemCount = 168, PageIndexParameterName = "page", RouteName = "Route2", RouteValues = new RouteValueDictionary { { "author", "webdiyer" }, { "city", "wuqi" } } };
            var sb = new StringBuilder(TestHelper.CopyrightEn).Append("\r\n<div>\r\n");
            sb.Append("FirstPrev1");
            for (int i = 2; i <= 10; i++)
            {
                sb.AppendFormat(linkFormat, i, i);
            }
            sb.AppendFormat(linkFormat, 11, "...");
            sb.AppendFormat(linkFormat, 2, "Next");
            sb.AppendFormat(linkFormat, 17, "Last");
            sb.Append("\r\n</div>\r\n");
            sb.Append(TestHelper.CopyrightEn);
            Assert.Equal(TestHelper.RenderControl(pager), sb.ToString());
        }

        [Fact]
        public void RouteWithNoMatchParam_ShouldGenerateCorrectHtml()
        {
            HttpContext.Current = new HttpContext(new HttpRequest("~/test.aspx", "http://webdiyer.com/urlpager/", null), new HttpResponse(null));
            const string linkFormat = "<a href=\"/urlpager/test/888?author=webdiyer&city=wuqi&page={0}\">{1}</a>";
            var pager = new UrlPager { TotalItemCount = 168, PageIndexParameterName = "page", RouteName = "Route3", RouteValues = new RouteValueDictionary { { "id", 888 }, { "author", "webdiyer" }, { "city", "wuqi" } } };
            var sb = new StringBuilder(TestHelper.CopyrightEn).Append("\r\n<div>\r\n");
            sb.Append("FirstPrev1");
            for (int i = 2; i <= 10; i++)
            {
                sb.AppendFormat(linkFormat, i, i);
            }
            sb.AppendFormat(linkFormat, 11, "...");
            sb.AppendFormat(linkFormat, 2, "Next");
            sb.AppendFormat(linkFormat, 17, "Last");
            sb.Append("\r\n</div>\r\n");
            sb.Append(TestHelper.CopyrightEn);
            Assert.Equal(TestHelper.RenderControl(pager), sb.ToString());
        }

        [Fact]
        public void CustomRouteParames_ShouldGenerateCorrectHtml()
        {
            HttpContext.Current = new HttpContext(new HttpRequest("~/test.aspx", "http://webdiyer.com/urlpager/", null), new HttpResponse(null));
            const string linkFormat = "<a href=\"/webdiyer/wuqi/{0}\">{1}</a>";
            var pager = new UrlPager { TotalItemCount = 168, RouteName = "Route4", RouteValues = new RouteValueDictionary { { "author", "webdiyer" }, { "city", "wuqi" } } };
            var sb = new StringBuilder(TestHelper.CopyrightEn).Append("\r\n<div>\r\n");
            sb.Append("FirstPrev1");
            for (int i = 2; i <= 10; i++)
            {
                sb.AppendFormat(linkFormat, i, i);
            }
            sb.AppendFormat(linkFormat, 11, "...");
            sb.AppendFormat(linkFormat, 2, "Next");
            sb.AppendFormat(linkFormat, 17, "Last");
            sb.Append("\r\n</div>\r\n");
            sb.Append(TestHelper.CopyrightEn);
            Assert.Equal(TestHelper.RenderControl(pager), sb.ToString());
        }

        public void Dispose()
        {
            HttpContext.Current = null;
            if (RouteTable.Routes[Route1Name] != null)
                RouteTable.Routes.Remove(RouteTable.Routes[Route1Name]);
            if (RouteTable.Routes[Route2Name] != null)
                RouteTable.Routes.Remove(RouteTable.Routes[Route2Name]);
            if (RouteTable.Routes[Route3Name] != null)
                RouteTable.Routes.Remove(RouteTable.Routes[Route3Name]);
            if (RouteTable.Routes[Route4Name] != null)
            RouteTable.Routes.Remove(RouteTable.Routes[Route4Name]);
        }
    }
}
