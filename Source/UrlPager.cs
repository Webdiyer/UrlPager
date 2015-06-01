/* UrlPager source code
This file is part of UrlPager.
Copyright 2003-2015 Webdiyer(http://en.webdiyer.com)
Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at
    http://www.apache.org/licenses/LICENSE-2.0
Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.Routing;
using System.Web.UI.WebControls;

namespace Webdiyer.WebControls
{
    ///<include file='Documentation.xml' path='UrlPagerDoc/Class[@name="UrlPager"]'/>
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
    [DefaultProperty("PageSize")]
    [DefaultEvent("PageChanged")]
    [ParseChildren(false)]
    [PersistChildren(false)]
    [Designer(typeof(UrlPagerDesigner))]
    [System.Drawing.ToolboxBitmap(typeof(UrlPager), "UrlPager.bmp")]
    [ToolboxData("<{0}:UrlPager runat=server></{0}:UrlPager>")]
    public partial class UrlPager : WebControl,INamingContainer
    {
        #region control rendering logic

        protected override HtmlTextWriterTag TagKey
        {
            get { return ContainerTag; }
        }

        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            writer.WriteLine(_copyrightText);
            base.RenderBeginTag(writer);
        }

        public override void RenderEndTag(HtmlTextWriter writer)
        {
            base.RenderEndTag(writer);
            writer.WriteLine();
            writer.Write(_copyrightText);
        }
        
        protected override void OnLoad(EventArgs e)
        {
            int pageIndex;
            object obj = Page.RouteData.Values[PageIndexParameterName];
            if (obj != null)
                int.TryParse(obj.ToString(), out pageIndex);
            else
                int.TryParse(Page.Request.QueryString[PageIndexParameterName], out pageIndex);
            if (pageIndex >= 1)
                CurrentPageIndex = ReversePageIndex ? TotalPageCount - pageIndex + 1 : pageIndex;
            if (!Page.IsPostBack)
                OnPageChanged(EventArgs.Empty);
            base.OnLoad(e);
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            output.Indent = 0;
            if (TotalItemCount <= PageSize && AutoHide)
                return;
            if (RouteValues == null)
                RouteValues = new RouteValueDictionary();
            var rq =DesignMode?new NameValueCollection{{"id","test"}}: HttpContext.Current.Request.QueryString;
            if (rq.Count > 0)
            {
                foreach (string key in rq.Keys)
                {
                    if (!RouteValues.ContainsKey(key))
                        RouteValues[key] = rq[key];
                }
            }
            if (InheritsRouteValues)
            {
                var currentValue = DesignMode?null:Page.RouteData.Values;
                if (currentValue != null && currentValue.Count > 0)
                {
                    foreach (KeyValuePair<string, object> routeElement in currentValue)
                    {
                        if (!RouteValues.ContainsKey(routeElement.Key))
                            RouteValues.Add(routeElement.Key, routeElement.Value);
                    }
                }
            }

            int startIndex =(CurrentPageIndex-1)-(NumericPagerItemCount/2); 
            if (startIndex < 0)
                startIndex = 0;
            else if (startIndex + NumericPagerItemCount > TotalPageCount - 1)
                startIndex = TotalPageCount - NumericPagerItemCount;
            int endIndex = ((startIndex + NumericPagerItemCount) > TotalPageCount)? TotalPageCount: (startIndex + NumericPagerItemCount);

            AddNavigationPageItem(output, 1, FirstPageText, PagerItemType.FirstPage);
            AddNavigationPageItem(output, CurrentPageIndex - 1, PrevPageText, PagerItemType.PrevPage);

            if (ShowMorePagerItems && startIndex > 0)
                AddMorePageItem(output, startIndex);

            if (AlwaysShowFirstLastPageNumber && startIndex > 1)
                AddNumericPagerItem(output, 1);

            if (ShowNumericPagerItems)
            {
                for (int i = startIndex + 1; i <= endIndex; i++)
                {
                    AddNumericPagerItem(output, i);
                }
            }
            if (ShowMorePagerItems && TotalPageCount > NumericPagerItemCount && endIndex < TotalPageCount)
                AddMorePageItem(output, endIndex + 1);

            if (AlwaysShowFirstLastPageNumber && endIndex < TotalPageCount)
                AddNumericPagerItem(output, TotalPageCount);

            AddNavigationPageItem(output, CurrentPageIndex + 1, NextPageText, PagerItemType.NextPage);
            AddNavigationPageItem(output, TotalPageCount, LastPageText, PagerItemType.LastPage);
        }

        private void AddNumericPagerItem(HtmlTextWriter writer, int pageIndex)
        {
            string linkText;
            if (CurrentPageIndex == pageIndex)
            {
                linkText = CurrentPageNumberFormatString == null
                               ? pageIndex.ToString()
                               : string.Format(CurrentPageNumberFormatString, pageIndex);
                writer.Write(CurrentPagerItemTemplate == null
                    ? linkText : string.Format(CurrentPagerItemTemplate, linkText));
            }
            else
            {
                linkText = PageNumberFormatString != null
                               ? string.Format(PageNumberFormatString, pageIndex)
                               : pageIndex.ToString();
                if (ReversePageIndex)
                    pageIndex = TotalPageCount - pageIndex + 1;
                if (RouteValues.ContainsKey(PageIndexParameterName))
                    RouteValues[PageIndexParameterName] = pageIndex;
                else
                    RouteValues.Add(PageIndexParameterName, pageIndex);
                var itemBuilder = new StringBuilder("<a href=\"");
                itemBuilder.Append(DesignMode ? "http://www.webdiyer.com/" : GetRouteUrl(RouteName, RouteValues));
                itemBuilder.Append("\">");
                itemBuilder.Append(linkText);
                itemBuilder.Append("</a>");
                writer.Write(NumericPagerItemTemplate == null
                                 ? itemBuilder.ToString()
                                 : string.Format(NumericPagerItemTemplate, itemBuilder));
            }
        }

        private void AddMorePageItem(HtmlTextWriter writer, int pageIndex)
        {
            if (ReversePageIndex)
                pageIndex = TotalPageCount - pageIndex + 1;
            if (RouteValues.ContainsKey(PageIndexParameterName))
                RouteValues[PageIndexParameterName] = pageIndex;
            else
                RouteValues.Add(PageIndexParameterName, pageIndex);
            var itemBuilder = new StringBuilder("<a href=\"");
            itemBuilder.Append(DesignMode ? "http://www.webdiyer.com/" : GetRouteUrl(RouteName, RouteValues));
            itemBuilder.Append("\">").Append(MorePageText);
            itemBuilder.Append("</a>");
            writer.Write(MorePagerItemTemplate == null
                             ? itemBuilder.ToString()
                             : string.Format(MorePagerItemTemplate, itemBuilder));
        }

        private void AddNavigationPageItem(HtmlTextWriter writer, int pageIndex, string linkText,
            PagerItemType pagerItemType)
        {
            if (ReversePageIndex)
                pageIndex = TotalPageCount - pageIndex + 1;
            if (!ShowFirstLast && (pagerItemType == PagerItemType.FirstPage || pagerItemType == PagerItemType.LastPage))
                return;
            if (!ShowPrevNext && (pagerItemType == PagerItemType.PrevPage || pagerItemType == PagerItemType.NextPage))
                return;
            string anchorElement;
            bool disabled = false;
            if (pagerItemType == PagerItemType.FirstPage || pagerItemType == PagerItemType.PrevPage)
            {
                if (CurrentPageIndex <= 1)
                {
                    if (!ShowDisabledPagerItems)
                        return;
                    disabled = true;
                }
            }

            if (pagerItemType == PagerItemType.NextPage || pagerItemType == PagerItemType.LastPage)
            {
                if (CurrentPageIndex >= TotalPageCount)
                {
                    if (!ShowDisabledPagerItems)
                        return;
                    disabled = true;
                }
            }
            if (disabled)
                anchorElement = linkText;
            else
            {
                if (RouteValues.ContainsKey(PageIndexParameterName))
                    RouteValues[PageIndexParameterName] = pageIndex;
                else
                    RouteValues.Add(PageIndexParameterName, pageIndex);
                anchorElement = string.Format("<a href=\"{0}\">{1}</a>",
                    DesignMode ? "http://www.webdiyer.com/" : GetRouteUrl(RouteName, RouteValues),
                    linkText);
            }
            if (disabled)
            {
                writer.Write(DisabledPagerItemTemplate == null
                   ? anchorElement
                   : string.Format(DisabledPagerItemTemplate, anchorElement));
            }
            else
            {
                writer.Write(NavigationPagerItemTemplate == null
                    ? anchorElement
                    : string.Format(NavigationPagerItemTemplate, anchorElement));
            }
        }


        #endregion

        #region methods
        protected virtual void OnPageChanged(EventArgs e)
        {
            var handler = (EventHandler)Events[EventPageChanged];
            if (handler != null)
                handler(this, e);
        }
        #endregion

        #region events

        public event EventHandler PageChanged
        {
            add
            {
                Events.AddHandler(EventPageChanged, value);
            }
            remove
            {
                Events.RemoveHandler(EventPageChanged, value);
            }
        }

        #endregion
    }
}
