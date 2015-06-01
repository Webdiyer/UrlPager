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
using System.ComponentModel;
using System.Web.Routing;
using System.Web.UI;

namespace Webdiyer.WebControls
{
    public partial class UrlPager
    {
        #region fields

        private readonly string _copyrightText ="<!--"+UrlPagerResources.txtCopyright+"-->";
        private static readonly object EventPageChanged = new object();
        #endregion

        #region public properties

        /// <include file='Documentation.xml' path='UrlPagerDoc/Property[@name="RouteName"]'/>
        [ DefaultValue(null)]
        public string RouteName
        {
            get
            {
                object obj = ViewState["RouteName"];
                return (string)obj;
            }
            set { ViewState["RouteName"] = value; }
        }

        /// <include file='Documentation.xml' path='UrlPagerDoc/Property[@name="ContainerTag"]'/>
        [DefaultValue(HtmlTextWriterTag.Div)]
        public HtmlTextWriterTag ContainerTag
        {
            get
            {
                object obj = ViewState["ContainerTag"];
                return obj == null ? HtmlTextWriterTag.Div : (HtmlTextWriterTag)obj;
            }
            set { ViewState["ContainerTag"] = value; }
        }


        /// <include file='Documentation.xml' path='UrlPagerDoc/Property[@name="RouteValues"]'/>
        [DefaultValue(null)]
        public RouteValueDictionary RouteValues
        {
            get
            {
                object obj = ViewState["RouteValues"];
                return (RouteValueDictionary)obj;
            }
            set { ViewState["RouteValues"] = value; }
        }

        /// <include file='Documentation.xml' path='UrlPagerDoc/Property[@name="InheritsRouteValues"]'/>
        [ DefaultValue(false)]
        public bool InheritsRouteValues
        {
            get
            {
                object obj = ViewState["InheritsRouteValues"];
                return obj != null && (bool)obj;
            }
            set
            {
                ViewState["InheritsRouteValues"] = value;
            }
        }


        /// <include file='Documentation.xml' path='UrlPagerDoc/Property[@name="TotalItemCount"]'/>
        [Browsable(false)]
        public int TotalItemCount
        {
            get
            {
                object obj = ViewState["TotalItemCount"];
                return obj == null ? 0 : (int)obj;
            }
            set
            {
                int count = value;
                if (value < 0)
                    count = 0;
                ViewState["TotalItemCount"] = count;
            }
        }


        /// <include file='Documentation.xml' path='UrlPagerDoc/Property[@name="PageSize"]'/>
        [DefaultValue(10)]
        public int PageSize
        {
            get
            {
                object obj = ViewState["PageSize"];
                return obj == null ? 10 : (int)obj;
            }
            set
            {
                int pageSize = value;
                if (value <= 0)
                    pageSize = 10;
                ViewState["PageSize"] = pageSize;
            }
        }

        /// <include file='Documentation.xml' path='UrlPagerDoc/Property[@name="AutoHide"]'/>
        [DefaultValue(true)]
        public bool AutoHide
        {
            get
            {
                object obj = ViewState["AutoHide"];
                return obj == null || (bool)obj;
            }
            set
            {
                ViewState["AutoHide"] = value;
            }
        }


        /// <include file='Documentation.xml' path='UrlPagerDoc/Property[@name="PageIndexParameterName"]'/>
        [DefaultValue("pageIndex"),Browsable(true)]
        public string PageIndexParameterName
        {
            get
            {
                object obj = ViewState["PageIndexParameterName"];
                return obj == null ? "pageIndex" : (string)obj;
            }
            set { ViewState["PageIndexParameterName"] = value; }
        }
        

        /// <include file='Documentation.xml' path='UrlPagerDoc/Property[@name="PageNumberFormatString"]'/>
        [DefaultValue(null)]
        public string PageNumberFormatString
        {
            get{return (string)ViewState["PageNumberFormatString"];}
            set { ViewState["PageNumberFormatString"] = value; }
        }

        /// <include file='Documentation.xml' path='UrlPagerDoc/Property[@name="PagerItemTemplate"]'/>
        [DefaultValue(null)]
        public string PagerItemTemplate
        {
            get
            {
                return (string)ViewState["PagerItemTemplate"];
            }
            set
            {
                ViewState["PagerItemTemplate"] = value;
            }
        }


        /// <include file='Documentation.xml' path='UrlPagerDoc/Property[@name="CurrentPageNumberFormatString"]'/>
		[DefaultValue(null)]
        public string CurrentPageNumberFormatString
        {
            get{return (string)ViewState["CurrentPageNumberFormatString"];}
            set { ViewState["CurrentPageNumberFormatString"] = value; }
        }


        /// <include file='Documentation.xml' path='UrlPagerDoc/Property[@name="NumericPagerItemTemplate"]'/>
        [DefaultValue(null)]
        public string NumericPagerItemTemplate
        {
            get
            {
                object obj = ViewState["NumericPagerItemTemplate"];
                return obj == null ? PagerItemTemplate : (string)obj;
            }
            set { ViewState["NumericPagerItemTemplate"] = value; }
        }

        /// <include file='Documentation.xml' path='UrlPagerDoc/Property[@name="DisabledPagerItemTemplate"]'/>
        [DefaultValue(null)]
        public string DisabledPagerItemTemplate
        {
            get
            {
                object obj = ViewState["DisabledPagerItemTemplate"];
                return obj == null ? NavigationPagerItemTemplate : (string)obj;
            }
            set { ViewState["DisabledPagerItemTemplate"] = value; }
        }

        /// <include file='Documentation.xml' path='UrlPagerDoc/Property[@name="CurrentPagerItemTemplate"]'/>
        [DefaultValue(null)]
        public string CurrentPagerItemTemplate
        {
            get
            {
                object obj = ViewState["CurrentPagerItemTemplate"];
                return obj == null ? NumericPagerItemTemplate : (string)obj;
            }
            set { ViewState["CurrentPagerItemTemplate"] = value; }
        }

        /// <include file='Documentation.xml' path='UrlPagerDoc/Property[@name="NavigationPagerItemTemplate"]'/>
        [DefaultValue(null)]
        public string NavigationPagerItemTemplate
        {
            get
            {
                object obj = ViewState["NavigationPagerItemTemplate"];
                return obj == null ? PagerItemTemplate : (string)obj;
            }
            set { ViewState["NavigationPagerItemTemplate"] = value; }
        }

        /// <include file='Documentation.xml' path='UrlPagerDoc/Property[@name="MorePagerItemTemplate"]'/>
        [DefaultValue(null)]
        public string MorePagerItemTemplate
        {
            get
            {
                object obj = ViewState["MorePagerItemTemplate"];
                return obj == null ? PagerItemTemplate : (string)obj;
            }
            set { ViewState["MorePagerItemTemplate"] = value; }
        }
        

        /// <include file='Documentation.xml' path='UrlPagerDoc/Property[@name="AlwaysShowFirstLastPageNumber"]'/>
        [DefaultValue(false)]
        public bool AlwaysShowFirstLastPageNumber
        {
            get
            {
                object obj = ViewState["AlwaysShowFirstLastPageNumber"];
                return obj != null && (bool)obj;
            }
            set
            {
                ViewState["AlwaysShowFirstLastPageNumber"] = value;
            }
        }

        /// <include file='Documentation.xml' path='UrlPagerDoc/Property[@name="NumericPagerItemCount"]'/>
        [DefaultValue(10)]
        public int NumericPagerItemCount
        {
            get
            {
                object obj = ViewState["NumericPagerItemCount"];
                return obj == null ? 10 : (int)obj;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(UrlPagerResources.errNumericPagerItemCountLessThan1);
                }
                ViewState["NumericPagerItemCount"] = value;
            }
        }

        /// <include file='Documentation.xml' path='UrlPagerDoc/Property[@name="ShowPrevNext"]'/>
        [DefaultValue(true)]
        public bool ShowPrevNext
        {
            get
            {
                object obj = ViewState["ShowPrevNext"];
                return obj == null || (bool)obj;
            }
            set
            {
                ViewState["ShowPrevNext"] = value;
            }
        }

        /// <include file='Documentation.xml' path='UrlPagerDoc/Property[@name="PrevPageText"]'/>
        [DefaultValue("Prev")]
        public string PrevPageText
        {
            get
            {
                string txt = (string)ViewState["PrevPageText"];
                return string.IsNullOrEmpty(txt) ? UrlPagerResources.txtPrevPage : txt;
            }
            set { ViewState["PrevPageText"] = value; }
        }

        /// <include file='Documentation.xml' path='UrlPagerDoc/Property[@name="NextPageText"]'/>
        [DefaultValue("Next")]
        public string NextPageText
        {
            get
            {
                string txt = (string)ViewState["NextPageText"];
                return string.IsNullOrEmpty(txt) ? UrlPagerResources.txtNextPage : txt;
            }
            set { ViewState["NextPageText"] = value; }
        }

        /// <include file='Documentation.xml' path='UrlPagerDoc/Property[@name="ShowNumericPagerItems"]'/>
        [DefaultValue(true)]
        public bool ShowNumericPagerItems
        {
            get
            {
                object obj = ViewState["ShowNumericPagerItems"];
                return obj == null || (bool)obj;
            }
            set
            {
                ViewState["ShowNumericPagerItems"] = value;
            }
        }

        /// <include file='Documentation.xml' path='UrlPagerDoc/Property[@name="ShowFirstLast"]'/>
        [DefaultValue(true)]
        public bool ShowFirstLast
        {
            get
            {
                object obj = ViewState["ShowFirstLast"];
                return obj == null || (bool)obj;
            }
            set
            {
                ViewState["ShowFirstLast"] = value;
            }
        }

        /// <include file='Documentation.xml' path='UrlPagerDoc/Property[@name="FirstPageText"]'/>
        [DefaultValue("First")]
        public string FirstPageText
        {
            get
            {
                string txt = (string)ViewState["FirstPageText"];
                return string.IsNullOrEmpty(txt) ? UrlPagerResources.txtFirstPage : txt;
            }
            set { ViewState["FirstPageText"] = value; }
        }
        
        /// <include file='Documentation.xml' path='UrlPagerDoc/Property[@name="LastPageText"]'/>
        [DefaultValue("Last")]
        public string LastPageText
        {
            get
            {
                string txt = (string)ViewState["LastPageText"];
                return string.IsNullOrEmpty(txt) ? UrlPagerResources.txtLastPage : txt;
            }
            set { ViewState["LastPageText"] = value; }
        }

        /// <include file='Documentation.xml' path='UrlPagerDoc/Property[@name="ShowMorePagerItems"]'/>
        [DefaultValue(true)]
        public bool ShowMorePagerItems
        {
            get
            {
                object obj = ViewState["ShowMorePagerItems"];
                return obj == null || (bool)obj;
            }
            set
            {
                ViewState["ShowMorePagerItems"] = value;
            }
        }

        /// <include file='Documentation.xml' path='UrlPagerDoc/Property[@name="MorePageText"]'/>
        [DefaultValue("...")]
        public string MorePageText
        {
            get
            {
                string txt = (string)ViewState["MorePageText"];
                return string.IsNullOrEmpty(txt) ? "..." : txt;
            }
            set { ViewState["MorePageText"] = value; }
        }

        /// <include file='Documentation.xml' path='UrlPagerDoc/Property[@name="ShowDisabledPagerItems"]'/>
        [DefaultValue(true)]
        public bool ShowDisabledPagerItems
        {
            get
            {
                object obj = ViewState["ShowDisabledPagerItems"];
                return obj == null || (bool)obj;
            }
            set
            {
                ViewState["ShowDisabledPagerItems"] = value;
            }
        }

        /// <include file='Documentation.xml' path='UrlPagerDoc/Property[@name="ReversePageIndex"]'/>
        [DefaultValue(false)]
        public bool ReversePageIndex
        {
            get
            {
                object obj = ViewState["ReversePageIndex"];
                return obj != null && (bool) obj;
            }
            set { ViewState["ReversePageIndex"] = value; }
        }

        /// <include file='Documentation.xml' path='UrlPagerDoc/Property[@name="CurrentPageIndex"]'/>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CurrentPageIndex
        {
            get
            {
                object cpage = ViewState["CurrentPageIndex"];
                int pindex = (cpage == null) ? 1 : (int)cpage;
                if (pindex > TotalPageCount && TotalPageCount > 0)
                    return TotalPageCount;
                if (pindex < 1)
                    return 1;
                return pindex;
            }
            set
            {
                int cpage = value;
                if (cpage < 1)
                    cpage = 1;
                else if (cpage > TotalPageCount)
                    cpage = TotalPageCount;
                ViewState["CurrentPageIndex"] = cpage;
            }
        }

        /// <include file='Documentation.xml' path='UrlPagerDoc/Property[@name="StartItemIndex"]'/>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int StartItemIndex
        {
            get
            {
                return (CurrentPageIndex - 1) * PageSize + 1;
            }
        }

        /// <include file='Documentation.xml' path='UrlPagerDoc/Property[@name="EndItemIndex"]'/>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int EndItemIndex
        {
            get
            {
                if (CurrentPageIndex < TotalPageCount)
                    return (CurrentPageIndex * PageSize);
                return TotalItemCount;
            }
        }

        /// <include file='Documentation.xml' path='UrlPagerDoc/Property[@name="TotalPageCount"]'/>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int TotalPageCount
        {
            get
            {
                if (TotalItemCount == 0)
                    return 1;
                return (int)Math.Ceiling((double)TotalItemCount / (double)PageSize);
            }
        }
        #endregion
    }
}
