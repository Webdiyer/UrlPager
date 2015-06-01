using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using Xunit;

namespace Webdiyer.WebControls.Test
{
    public class UrlPagerTest:IDisposable
    {
        private const string LinkFormat = "<a href=\"/urlpager/test/{0}\">{1}</a>";
        private const string RouteName = "testRoute1";

        public UrlPagerTest()
        {
            var reqeust = new HttpRequest("~/test.aspx", "http://en.webdiyer.com/urlpager/", null);
            HttpContext.Current = new HttpContext(reqeust, new HttpResponse(null));
            if (RouteTable.Routes[RouteName] == null)
            {
                RouteTable.Routes.MapPageRoute(RouteName,
                    "{controller}/{action}/{id}",
                    "~/test.aspx", false,
                    new RouteValueDictionary {{"controller", "urlpager"}, {"action", "test"}, {"id", 1}},
                    new RouteValueDictionary {{"controller", "UrlPager"}, {"action", "test"}});
            }
        }


        [Fact]
        public void DataPropertyValues_ShouldBeCorrect_WhenCurrentPageIndexIs1()
        {
            var pager=new UrlPager();
            pager.TotalItemCount = 228;
            Assert.Equal(pager.StartItemIndex,1);
            Assert.Equal(pager.EndItemIndex,10);
            Assert.Equal(pager.TotalPageCount,23);
        }

        [Fact]
        public void DataPropertyValues_ShouldBeCorrect__WhenPageSizeIs8()
        {
            var pager = new UrlPager {TotalItemCount = 228, PageSize = 8};
            Assert.Equal(pager.StartItemIndex, 1);
            Assert.Equal(pager.EndItemIndex, 8);
            Assert.Equal(pager.TotalPageCount, 29);
        }


        [Fact]
        public void DataPropertyValues_ShouldBeCorrect_CurrentPageIndexIsLastPage()
        {
            var pager = new UrlPager {TotalItemCount = 222, CurrentPageIndex = 23};
            Assert.Equal(pager.StartItemIndex, 221);
            Assert.Equal(pager.EndItemIndex, 222);
            Assert.Equal(pager.TotalPageCount, 23);
        }


        [Fact]
        public void DefaultSettings_ShouldGenerateCorrectHtml()
        {
            var pager = new UrlPager {TotalItemCount = 168, PageIndexParameterName = "id", RouteName = "testRoute1"};
            var sb = new StringBuilder(TestHelper.CopyrightEn).Append("\r\n<div>\r\n");
            sb.Append("FirstPrev1");
            for (int i = 2; i <= 10; i++)
            {
                sb.AppendFormat(LinkFormat, i, i);
            }
            sb.AppendFormat(LinkFormat, 11, "...");
            sb.AppendFormat(LinkFormat, 2, "Next");
            sb.AppendFormat(LinkFormat, 17, "Last");
            sb.Append("\r\n</div>\r\n");
            sb.Append(TestHelper.CopyrightEn);
            Assert.Equal(TestHelper.RenderControl(pager), sb.ToString());
        }

        [Fact]
        public void NumericPagerItemCountIs5_CurrentPageIndexIs1_ShouldGenerateCorrectHtml()
        {
            var pager = new UrlPager { TotalItemCount = 168, PageIndexParameterName = "id", RouteName = "testRoute1", NumericPagerItemCount = 5 };
            var sb = new StringBuilder(TestHelper.CopyrightEn).Append("\r\n<div>\r\n");
            sb.Append("FirstPrev1");
            for (int i = 2; i <= 5; i++)
            {
                sb.AppendFormat(LinkFormat, i, i);
            }
            sb.AppendFormat(LinkFormat, 6, "...");
            sb.AppendFormat(LinkFormat, 2, "Next");
            sb.AppendFormat(LinkFormat, 17, "Last");
            sb.Append("\r\n</div>\r\n");
            sb.Append(TestHelper.CopyrightEn);
            Assert.Equal(TestHelper.RenderControl(pager), sb.ToString());
        }

        [Fact]
        public void NumericPagerItemCountIs5_CurrentPageIndexIs5_ShouldGenerateCorrectHtml()
        {
            var pager = new UrlPager { TotalItemCount = 168, PageIndexParameterName = "id", RouteName = "testRoute1", NumericPagerItemCount = 5,CurrentPageIndex = 5};
            var sb = new StringBuilder(TestHelper.CopyrightEn).Append("\r\n<div>\r\n");
            sb.AppendFormat("<a href=\"/\">{0}</a>", "First");
            sb.AppendFormat(LinkFormat, 4, "Prev");
            sb.AppendFormat(LinkFormat, 2, "...");
            for (int i = 3; i <= 7; i++)
            {
                if (i == 5)
                    sb.Append(i);
                else
                    sb.AppendFormat(LinkFormat, i, i);
            }
            sb.AppendFormat(LinkFormat, 8, "...");
            sb.AppendFormat(LinkFormat, 6, "Next");
            sb.AppendFormat(LinkFormat, 17, "Last");
            sb.Append("\r\n</div>\r\n");
            sb.Append(TestHelper.CopyrightEn);
            Assert.Equal(TestHelper.RenderControl(pager), sb.ToString());
        }

        [Fact]
        public void NumericPagerItemCountIsLessThan1_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(
                () =>
                    new UrlPager
                    {
                        TotalItemCount = 168,
                        PageIndexParameterName = "id",
                        RouteName = "testRoute1",
                        NumericPagerItemCount = 0
                    });
        }

        [Fact]
        public void DefaultSettingsWithAttributes_ShouldGenerateCorrectHtml()
        {
            var pager = new UrlPager { TotalItemCount = 168, PageIndexParameterName = "id", RouteName = "testRoute1",CssClass = "pagination",ID="pager1"};
            pager.Style.Add("width","100%");
            var sb = new StringBuilder(TestHelper.CopyrightEn).Append("\r\n<div id=\"pager1\" class=\"pagination\" style=\"width:100%;\">\r\n");
            sb.Append("FirstPrev1");
            for (int i = 2; i <= 10; i++)
            {
                sb.AppendFormat(LinkFormat, i, i);
            }
            sb.AppendFormat(LinkFormat, 11, "...");
            sb.AppendFormat(LinkFormat, 2, "Next");
            sb.AppendFormat(LinkFormat, 17, "Last");
            sb.Append("\r\n</div>\r\n");
            sb.Append(TestHelper.CopyrightEn);
            Assert.Equal(TestHelper.RenderControl(pager), sb.ToString());
        }

        [Fact]
        public void HideDisabledPagedItemsAndMorePageButtons_ShouldGenerateCorrectHtml()
        {
            var pager = new UrlPager { TotalItemCount = 168, PageIndexParameterName = "id", RouteName = "testRoute1",ShowDisabledPagerItems = false,ShowMorePagerItems = false};
            var sb = new StringBuilder(TestHelper.CopyrightEn).Append("\r\n<div>\r\n");
            sb.Append("1");
            for (int i = 2; i <= 10; i++)
            {
                sb.AppendFormat(LinkFormat, i, i);
            }
            sb.AppendFormat(LinkFormat, 2, "Next");
            sb.AppendFormat(LinkFormat, 17, "Last");
            sb.Append("\r\n</div>\r\n");
            sb.Append(TestHelper.CopyrightEn);
            Assert.Equal(TestHelper.RenderControl(pager), sb.ToString());
        }

        [Fact]
        public void CurrentPageIndexIs2_ShouldGenerateCorrectHtml()
        {
            var pager = new UrlPager { TotalItemCount = 168, PageIndexParameterName = "id", RouteName = "testRoute1",CurrentPageIndex = 2};
            const string firstPageUrlFormat = "<a href=\"/\">{0}</a>";
            var sb = new StringBuilder(TestHelper.CopyrightEn).Append("\r\n<div>\r\n");
            sb.AppendFormat(firstPageUrlFormat, "First");
            sb.AppendFormat(firstPageUrlFormat, "Prev");
            for (int i = 1; i <= 10; i++)
            {
                if (i == 1)
                {
                    sb.AppendFormat(firstPageUrlFormat, 1);
                }
                else if (i == 2)
                    sb.Append(i);
                else
                    sb.AppendFormat(LinkFormat, i, i);
            }
            sb.AppendFormat(LinkFormat, 11, "...");
            sb.AppendFormat(LinkFormat, 3, "Next");
            sb.AppendFormat(LinkFormat, 17, "Last");
            sb.Append("\r\n</div>\r\n");
            sb.Append(TestHelper.CopyrightEn);
            Assert.Equal(TestHelper.RenderControl(pager), sb.ToString());
        }

        [Fact]
        public void CurrentPageIndexIs11_ShouldGenerateCorrectHtml()
        {
            var pager = new UrlPager { TotalItemCount = 168, PageIndexParameterName = "id", RouteName = "testRoute1", CurrentPageIndex = 11 };
            const string firstPageUrlFormat = "<a href=\"/\">{0}</a>";
            var sb = new StringBuilder(TestHelper.CopyrightEn).Append("\r\n<div>\r\n");
            sb.AppendFormat(firstPageUrlFormat, "First");
            sb.AppendFormat(LinkFormat, 10, "Prev");
            sb.AppendFormat(LinkFormat, 5, "...");
            for (int i = 6; i <= 15; i++)
            {
                if (i == 11)
                    sb.Append(i);
                else
                    sb.AppendFormat(LinkFormat, i, i);
            }
            sb.AppendFormat(LinkFormat, 16, "...");
            sb.AppendFormat(LinkFormat, 12, "Next");
            sb.AppendFormat(LinkFormat, 17, "Last");
            sb.Append("\r\n</div>\r\n");
            sb.Append(TestHelper.CopyrightEn);
            Assert.Equal(TestHelper.RenderControl(pager), sb.ToString());
        }


        [Fact]
        public void CurrentPageIndexIsLastPage_ShouldGenerateCorrectHtml()
        {
            var pager = new UrlPager { TotalItemCount = 168, PageIndexParameterName = "id", RouteName = "testRoute1", CurrentPageIndex = 17 };
            const string firstPageUrlFormat = "<a href=\"/\">{0}</a>";
            var sb = new StringBuilder(TestHelper.CopyrightEn).Append("\r\n<div>\r\n");
            sb.AppendFormat(firstPageUrlFormat, "First");
            sb.AppendFormat(LinkFormat, 16, "Prev");
            sb.AppendFormat(LinkFormat, 7, "...");
            for (int i = 8; i <= 16; i++)
            {
                    sb.AppendFormat(LinkFormat, i, i);
            }
            sb.Append("17NextLast");
            sb.Append("\r\n</div>\r\n");
            sb.Append(TestHelper.CopyrightEn);
            Assert.Equal(TestHelper.RenderControl(pager), sb.ToString());
        }
        [Fact]
        public void CustomrizedPagerItemText_ShouldGenerateCorrectHtml()
        {
            var pager = new UrlPager { TotalItemCount = 168, PageIndexParameterName = "id", RouteName = "testRoute1",FirstPageText = "<<",PrevPageText = "<",NextPageText = ">",LastPageText = ">>",MorePageText = "more"};
            var sb = new StringBuilder(TestHelper.CopyrightEn).Append("\r\n<div>\r\n");
            sb.Append("<<<1");
            for (int i = 2; i <= 10; i++)
            {
                sb.AppendFormat(LinkFormat, i, i);
            }
            sb.AppendFormat(LinkFormat, 11, "more");
            sb.AppendFormat(LinkFormat, 2, ">");
            sb.AppendFormat(LinkFormat, 17, ">>");
            sb.Append("\r\n</div>\r\n");
            sb.Append(TestHelper.CopyrightEn);
            Assert.Equal(TestHelper.RenderControl(pager), sb.ToString());
        }

        [Fact]
        public void ShowNumericPagerItemsIsFalse_ShouldGenerateCorrectHtml()
        {
            var pager = new UrlPager { TotalItemCount = 168, PageIndexParameterName = "id", RouteName = "testRoute1", ShowNumericPagerItems=false };
            var sb = new StringBuilder(TestHelper.CopyrightEn).Append("\r\n<div>\r\n");
            sb.Append("FirstPrev");
            sb.AppendFormat(LinkFormat, 11, "...");
            sb.AppendFormat(LinkFormat, 2, "Next");
            sb.AppendFormat(LinkFormat, 17, "Last");
            sb.Append("\r\n</div>\r\n");
            sb.Append(TestHelper.CopyrightEn);
            Assert.Equal(TestHelper.RenderControl(pager), sb.ToString());
        }

        [Fact]
        public void ShowFirstLastIsFalse_ShouldGenerateCorrectHtml()
        {
            var pager = new UrlPager { TotalItemCount = 168, PageIndexParameterName = "id", RouteName = "testRoute1", ShowFirstLast = false };
            var sb = new StringBuilder(TestHelper.CopyrightEn).Append("\r\n<div>\r\n");
            sb.Append("Prev1");
            for (int i = 2; i <= 10; i++)
            {
                sb.AppendFormat(LinkFormat, i, i);
            }
            sb.AppendFormat(LinkFormat, 11, "...");
            sb.AppendFormat(LinkFormat, 2, "Next");
            sb.Append("\r\n</div>\r\n");
            sb.Append(TestHelper.CopyrightEn);
            Assert.Equal(TestHelper.RenderControl(pager), sb.ToString());
        }

        [Fact]
        public void ShowPrevNextIsFalse_ShouldGenerateCorrectHtml()
        {
            var pager = new UrlPager { TotalItemCount = 168, PageIndexParameterName = "id", RouteName = "testRoute1", ShowPrevNext = false };

            var sb = new StringBuilder(TestHelper.CopyrightEn).Append("\r\n<div>\r\n");
            sb.Append("First1");
            for (int i = 2; i <= 10; i++)
            {
                sb.AppendFormat(LinkFormat, i, i);
            }
            sb.AppendFormat(LinkFormat, 11, "...");
            sb.AppendFormat(LinkFormat, 17, "Last");
            sb.Append("\r\n</div>\r\n");
            sb.Append(TestHelper.CopyrightEn);
            Assert.Equal(TestHelper.RenderControl(pager), sb.ToString());
        }
        
        [Fact]
        public void AlwaysShowFirstLastPageNumberIsTrue_ShouldGenerateCorrectHtml()
        {
            var pager = new UrlPager { TotalItemCount = 168, PageIndexParameterName = "id", RouteName = "testRoute1",AlwaysShowFirstLastPageNumber=true };

            var sb = new StringBuilder(TestHelper.CopyrightEn).Append("\r\n<div>\r\n");
            sb.Append("FirstPrev1");
            for (int i = 2; i <= 10; i++)
            {
                sb.AppendFormat(LinkFormat, i, i);
            }
            sb.AppendFormat(LinkFormat, 11, "...");
            sb.AppendFormat(LinkFormat, 17, "17");
            sb.AppendFormat(LinkFormat, 2, "Next");
            sb.AppendFormat(LinkFormat, 17, "Last");
            sb.Append("\r\n</div>\r\n");
            sb.Append(TestHelper.CopyrightEn);
            Assert.Equal(TestHelper.RenderControl(pager), sb.ToString());
        }
        

        [Fact]
        public void PagerItemTemplate_ShouldGenerateCorrectHtml()
        {
            var pager = new UrlPager { TotalItemCount = 168, PageIndexParameterName = "id", RouteName = "testRoute1" };
            pager.CurrentPagerItemTemplate = "<li class=\"active\"><a href=\"#\">{0}</a></li>";
            pager.PagerItemTemplate="<li>{0}</li>";

            const string itemFormat = "<li>" + LinkFormat + "</li>";
            var sb = new StringBuilder(TestHelper.CopyrightEn).Append("\r\n<div>\r\n");
            sb.Append("<li>First</li><li>Prev</li><li class=\"active\"><a href=\"#\">1</a></li>");
            for (int i = 2; i <= 10; i++)
            {
                sb.AppendFormat(itemFormat, i, i);
            }
            sb.AppendFormat(itemFormat, 11, "...");
            sb.AppendFormat(itemFormat, 2, "Next");
            sb.AppendFormat(itemFormat, 17, "Last");
            sb.Append("\r\n</div>\r\n");
            sb.Append(TestHelper.CopyrightEn);
            Assert.Equal(TestHelper.RenderControl(pager), sb.ToString());
        }

        [Fact]
        public void SpecifiedPagerItemTemplate_ShouldOverridePagerItemTemplate()
        {
            var pager = new UrlPager { TotalItemCount = 168, PageIndexParameterName = "id", RouteName = "testRoute1" };
            pager.CurrentPagerItemTemplate = "<li class=\"active\"><a href=\"#\">{0}</a></li>";
            pager.NavigationPagerItemTemplate = "<li><strong>{0}</strong></li>";
            pager.MorePagerItemTemplate = "<li><i>{0}</i></li>";
            pager.NumericPagerItemTemplate = "<span>{0}</span>";
            pager.PagerItemTemplate = "<li>{0}</li>";
            pager.DisabledPagerItemTemplate = "<li class=\"disabled\">{0}</li>";

            const string numFormat = "<span>" + LinkFormat + "</span>";
            const string navFormat = "<li><strong>" + LinkFormat + "</strong></li>";
            const string moreFormat = "<li><i>" + LinkFormat + "</i></li>";
            var sb = new StringBuilder(TestHelper.CopyrightEn).Append("\r\n<div>\r\n");
            sb.Append("<li class=\"disabled\">First</li><li class=\"disabled\">Prev</li><li class=\"active\"><a href=\"#\">1</a></li>");
            for (int i = 2; i <= 10; i++)
            {
                sb.AppendFormat(numFormat, i, i);
            }
            sb.AppendFormat(moreFormat, 11, "...");
            sb.AppendFormat(navFormat, 2, "Next");
            sb.AppendFormat(navFormat, 17, "Last");
            sb.Append("\r\n</div>\r\n");
            sb.Append(TestHelper.CopyrightEn);
            Assert.Equal(TestHelper.RenderControl(pager), sb.ToString());
        }

        [Fact]
        public void CurrentPagerItemTemplate_ShouldInheritFromNumericPagerItemTemplate()
        {
            var pager = new UrlPager { TotalItemCount = 168, PageIndexParameterName = "id", RouteName = "testRoute1" };
            pager.NumericPagerItemTemplate = "<li>{0}</li>";

            const string numFormat = "<li>" + LinkFormat + "</li>";
            var sb = new StringBuilder(TestHelper.CopyrightEn).Append("\r\n<div>\r\n");
            sb.Append("FirstPrev<li>1</li>");
            for (int i = 2; i <= 10; i++)
            {
                sb.AppendFormat(numFormat, i, i);
            }
            sb.AppendFormat(LinkFormat, 11, "...");
            sb.AppendFormat(LinkFormat, 2, "Next");
            sb.AppendFormat(LinkFormat, 17, "Last");
            sb.Append("\r\n</div>\r\n");
            sb.Append(TestHelper.CopyrightEn);
            Assert.Equal(TestHelper.RenderControl(pager), sb.ToString());
        }

        [Fact]
        public void DisabledPagerItemTemplate_ShouldInheritFromNavigationPagerItemTemplate()
        {
            var pager = new UrlPager { TotalItemCount = 168, PageIndexParameterName = "id", RouteName = "testRoute1" };
            pager.NavigationPagerItemTemplate = "<li><strong>{0}</strong></li>";
            pager.PagerItemTemplate = "<li>{0}</li>";

            const string navFormat = "<li><strong>" + LinkFormat + "</strong></li>";
            const string linkFormat = "<li>" + LinkFormat + "</li>";
            var sb = new StringBuilder(TestHelper.CopyrightEn).Append("\r\n<div>\r\n");
            sb.Append("<li><strong>First</strong></li><li><strong>Prev</strong></li><li>1</li>");
            for (int i = 2; i <= 10; i++)
            {
                sb.AppendFormat(linkFormat, i, i);
            }
            sb.AppendFormat(linkFormat, 11, "...");
            sb.AppendFormat(navFormat, 2, "Next");
            sb.AppendFormat(navFormat, 17, "Last");
            sb.Append("\r\n</div>\r\n");
            sb.Append(TestHelper.CopyrightEn);
            Assert.Equal(TestHelper.RenderControl(pager), sb.ToString());
        }

        [Fact]
        public void ContainerTagSetting_ShouldGenerateCorrectHtml()
        {
            var pager = new UrlPager { TotalItemCount = 168, PageIndexParameterName = "id", RouteName = "testRoute1", ContainerTag = HtmlTextWriterTag.Ul };
            pager.CurrentPagerItemTemplate = "<li class=\"active\"><a href=\"#\">{0}</a></li>";
            pager.PagerItemTemplate = "<li>{0}</li>";

            const string itemFormat = "<li>" + LinkFormat + "</li>";
            var sb = new StringBuilder(TestHelper.CopyrightEn).Append("\r\n<ul>\r\n");
            sb.Append("<li>First</li><li>Prev</li><li class=\"active\"><a href=\"#\">1</a></li>");
            for (int i = 2; i <= 10; i++)
            {
                sb.AppendFormat(itemFormat, i, i);
            }
            sb.AppendFormat(itemFormat, 11, "...");
            sb.AppendFormat(itemFormat, 2, "Next");
            sb.AppendFormat(itemFormat, 17, "Last");
            sb.Append("\r\n</ul>\r\n");
            sb.Append(TestHelper.CopyrightEn);
            Assert.Equal(TestHelper.RenderControl(pager), sb.ToString());
        }

        [Fact]
        public void PagerItemFormatStrings_ShouldGenerateCorrectHtml()
        {
            var pager = new UrlPager { TotalItemCount = 168, PageIndexParameterName = "id", RouteName = "testRoute1" };
            pager.CurrentPageNumberFormatString = "[{0}]";
            pager.PageNumberFormatString = "[{0}]";
            pager.CurrentPagerItemTemplate = "<li class=\"active\"><a href=\"#\">{0}</a></li>";
            pager.PagerItemTemplate = "<li>{0}</li>";

            const string itemFormat = "<li>" + LinkFormat + "</li>";
            var sb = new StringBuilder(TestHelper.CopyrightEn).Append("\r\n<div>\r\n");
            sb.Append("<li>First</li><li>Prev</li><li class=\"active\"><a href=\"#\">[1]</a></li>");
            for (int i = 2; i <= 10; i++)
            {
                sb.AppendFormat(itemFormat, i, "["+i+"]");
            }
            sb.AppendFormat(itemFormat, 11, "...");
            sb.AppendFormat(itemFormat, 2, "Next");
            sb.AppendFormat(itemFormat, 17, "Last");
            sb.Append("\r\n</div>\r\n");
            sb.Append(TestHelper.CopyrightEn);
            Assert.Equal(TestHelper.RenderControl(pager), sb.ToString());
        }

        [Fact]
        public void AutoHideIsTrue_ShouldGenerateCorrectHtml()
        {
            var pager = new UrlPager { TotalItemCount = 5, PageIndexParameterName = "id", RouteName = "testRoute1" };
            var sb = new StringBuilder(TestHelper.CopyrightEn).Append("\r\n<div>\r\n\r\n</div>\r\n").Append(TestHelper.CopyrightEn);
            Assert.Equal(TestHelper.RenderControl(pager), sb.ToString());
        }

        [Fact]
        public void AutoHideIsFalse_ShouldGenerateCorrectHtml()
        {
            var pager = new UrlPager { TotalItemCount = 5,AutoHide = false,PageIndexParameterName = "id", RouteName = "testRoute1" };
            var sb = new StringBuilder(TestHelper.CopyrightEn).Append("\r\n<div>\r\n");
            sb.Append("FirstPrev1NextLast");
            sb.Append("\r\n</div>\r\n").Append(TestHelper.CopyrightEn);
            Assert.Equal(TestHelper.RenderControl(pager), sb.ToString());
        }

        [Fact]
        public void ReverseUrlPageIndex_ShouldGenerateCorrectHtml()
        {
            var pager = new UrlPager { TotalItemCount = 168,ReversePageIndex = true,PageIndexParameterName = "id", RouteName = "testRoute1" };
            var sb = new StringBuilder(TestHelper.CopyrightEn).Append("\r\n<div>\r\n");
            sb.Append("FirstPrev1");
            for (int i = 16; i >7; i--)
            {
                sb.AppendFormat(LinkFormat, i, 17+1-i);
            }
            sb.AppendFormat(LinkFormat, 7, "...");
            sb.AppendFormat(LinkFormat, 16, "Next");
            sb.AppendFormat("<a href=\"/\">{0}</a>", "Last");
            sb.Append("\r\n</div>\r\n");
            sb.Append(TestHelper.CopyrightEn);
            Assert.Equal(TestHelper.RenderControl(pager), sb.ToString());
        }

        [Fact]
        public void CurrentUICultureIsZhCN_ShouldGenerateCorrectHtml()
        {
            var culture = Thread.CurrentThread.CurrentUICulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("zh-CN");
            var pager = new UrlPager { TotalItemCount = 168, PageIndexParameterName = "id", RouteName = "testRoute1" };
            var sb = new StringBuilder(TestHelper.CopyrightCn).Append("\r\n<div>\r\n");
            sb.Append("首页上页1");
            for (int i = 2; i <= 10; i++)
            {
                sb.AppendFormat(LinkFormat, i, i);
            }
            sb.AppendFormat(LinkFormat, 11, "...");
            sb.AppendFormat(LinkFormat, 2, "下页");
            sb.AppendFormat(LinkFormat, 17, "尾页");
            sb.Append("\r\n</div>\r\n");
            sb.Append(TestHelper.CopyrightCn);
            Assert.Equal(TestHelper.RenderControl(pager), sb.ToString());
            Thread.CurrentThread.CurrentUICulture = culture;
        }

        public void Dispose()
        {
            HttpContext.Current = null;
            RouteTable.Routes.Remove(RouteTable.Routes[RouteName]);
        }
    }
}
