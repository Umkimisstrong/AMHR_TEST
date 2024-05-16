using AMHR_WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace AMHR_WEB.GlobalAttribute
{
    public static class GlobalHtmlAttributes
    {
         public static MvcHtmlString Pagination(this HtmlHelper helper, int currentPage, int totalPageCount, int totalResults, string pageUrlParamater, string isCustom)
        {
            var builder = new StringBuilder();
            var strResults = string.Empty;


            if (totalPageCount == 1)
            {
                strResults = "<li class='page-item active'> <a class='page-link' href='#'>1</a> </li>";
            }
            else if (totalPageCount == 0)
            {
                strResults = "";
            }
            else
            {
                int displayPageNuber = 1;
                for (int i = 0; i < totalPageCount + 2; i++)
                {
                    if (i == 0)
                    {
                        if (currentPage == displayPageNuber)
                        {
                            strResults += "<li class=\"page-item disabled\"><a class=\"page-link\" href=\"#\">&laquo;</a></li>";
                        }
                        else
                        {
                            strResults += "<li class=\"page-item\"><a class=\"page-link\" href=\"#\">&laquo;</a></li>";
                        }
                    }
                    else if (i > 0 && i <= totalPageCount)
                    {
                        if (currentPage == displayPageNuber)
                        {
                            strResults += "<li class=\"page-item active\"><a class=\"page-link\" href=\"#\">" +
                                            displayPageNuber.ToString() +
                                            "</a></li>";
                        }
                        else
                        {
                            strResults += "<li class=\"page-item\"><a class=\"page-link\" href=\"#\">" +
                                            displayPageNuber.ToString() +
                                            "</a></li>";
                        }
                        displayPageNuber++;
                    }
                    else if (i == totalPageCount + 2 - 1)
                    {
                        if (currentPage == displayPageNuber)
                        {
                            strResults += "<li class=\"page-item disabled\"><a class=\"page-link\" href=\"#\">&raquo;</a></li>";
                        }
                        else
                        {
                            strResults += "<li class=\"page-item\"><a class=\"page-link\" href=\"#\">&raquo;</a></li>";
                        }
                    }
                }
            }
            builder.Append(strResults);
            return new MvcHtmlString(builder.ToString());
        }

    }
}