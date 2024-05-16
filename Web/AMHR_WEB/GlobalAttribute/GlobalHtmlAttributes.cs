﻿using AMHR_WEB.Models;
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

            int displayPageCount = 10;
            int startNumber = 1;
            
            int prevFirstNumber = 0;
            int nextFirstNumber = 0;
            // 현재 페이지 번호를 확인한다.
            // 기본은 1 ~ 10이므로 displayPageCount(10으로 현재 페이지가 나누어 떨어지는지 확인한다.)
            // 나머지가 1인 경우 1 ~ 10 / 11 ~ 20 등 앞번호이므로 Page를 그리는 시작번호를 요청한 번호로 해준다.. (11, 21 ..)

            displayPageCount = totalPageCount > displayPageCount ?
                                (
                                    (totalPageCount % 10 != 0 && totalPageCount % 10 < displayPageCount)
                                    ? totalPageCount % 10
                                    : displayPageCount
                                )
                                :
                                totalPageCount;


            // 0 ~ 9페이지
            if ((totalPageCount / 10) < 1)
            {
                startNumber = 1;
                prevFirstNumber = 0;
                nextFirstNumber = 11;
            }
            // 11 ~ 모든 페이지
            else
            {
                startNumber = startNumber + (((totalPageCount / 10) * 10)); // 35 : 1 + 30 = 31 / 11: 1 + 10 = 11
                prevFirstNumber = ((totalPageCount / 10) * 10) - 9;  // 35 : 30-9 = 21   / 11: 10-9 = 1
                nextFirstNumber = ((totalPageCount / 10) * 10) + 11; // 35 : 30+11 = 41   / 11: 10+11 = 21
            }


            if (displayPageCount < 10) // 10개 미만인 경우는 전체 페이지 카운트가 마지막 페이지 넘버가 된다.
            {
                nextFirstNumber = totalPageCount;
            }


            if (totalPageCount == 0)
            {
                strResults = "";
            }
            else if (totalPageCount == 1)
            {
                strResults = "<li class='page-item active'> <a class='page-link' href='javascript:void(0);' onclick='ChangeConstPageNumber(" + '0' + ")'>1</a> </li>";
            }
            else
            {
                int displayPageNuber = startNumber;
                int requestPageNumber = startNumber - 1;

                for (int i = 0; i < displayPageCount + 2; i++)
                {
                    if (i == 0)
                    {
                        // 맨 첫번째 화살표는 i 가 0일 때 DISABLED
                        // 아닌 경우는 항상 이전의 0번째 숫자를 바라보게 한다.
                        if (currentPage == displayPageNuber && i == 0)
                        {
                            strResults += "<li class=\"page-item disabled\"><a class=\"page-link\" href=\"#\">&laquo;</a></li>";
                        }
                        else
                        {
                            strResults += "<li class=\"page-item\"><a class=\"page-link\" href='javascript:void(0);' onclick='ChangeConstPageNumber(" + prevFirstNumber + ")'>&laquo;</a></li>";
                        }
                    }
                    else if (i > 0 && i <= displayPageCount)
                    {
                        if (currentPage == displayPageNuber)
                        {
                            strResults += "<li class=\"page-item active\"><a class=\"page-link\"  href='javascript:void(0);' onclick='ChangeConstPageNumber(" + requestPageNumber + ")'>" +
                                            displayPageNuber.ToString() +
                                            "</a></li>";
                        }
                        else
                        {
                            strResults += "<li class=\"page-item\"><a class=\"page-link\" href='javascript:void(0);' onclick='ChangeConstPageNumber(" + requestPageNumber + ")'>" +
                                            displayPageNuber.ToString() +
                                            "</a></li>";
                        }
                        displayPageNuber++;
                        requestPageNumber++;
                    }
                    else if (i == displayPageCount + 2 - 1)
                    {
                        // 맨 첫번째 화살표는 i 가 0일 때 DISABLED
                        // 아닌 경우는 항상 이후의 0번째 숫자 또는 해당 Level 의 마지막 번호를 바라보게 한다.
                        if (currentPage == totalPageCount)
                        {
                            strResults += "<li class=\"page-item disabled\"><a class=\"page-link\" href=\"#\">&raquo;</a></li>";
                        }
                        else
                        {
                            strResults += "<li class=\"page-item\"><a class=\"page-link\"  href='javascript:void(0);' onclick='ChangeConstPageNumber(" + nextFirstNumber + ")'>&raquo;</a></li>";
                        }
                    }
                }
            }
            builder.Append(strResults);
            return new MvcHtmlString(builder.ToString());
        }

    }
}