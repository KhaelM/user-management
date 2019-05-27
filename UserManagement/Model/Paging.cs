using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserManagement.Model
{
    public class Paging
    {

        public int MaxPagination { get; set; }
        public int PagesAvailable { get; set; }
        public int CurrentPage { get; set; }
        public int PagingBeginningIndex { get; set; }
        public int PagingEndIndex { get; set; }

        public Paging(int maxPagination, int pagesAvailable, int currentPage)
        {
            MaxPagination = maxPagination;
            PagesAvailable = pagesAvailable;
            CurrentPage = currentPage;
        }
        
        public void ManagePagination()
        {
            int maxPageRightAndLeft = MaxPagination / 2;
            int remainingRightPage = PagesAvailable - CurrentPage;
            int remainingLeftPage = CurrentPage - 1;

            if (PagesAvailable >= MaxPagination)
            {
                int nearestMidddle = CurrentPage;

                if (remainingRightPage < maxPageRightAndLeft)
                {
                    nearestMidddle = PagesAvailable - maxPageRightAndLeft;
                }
                if (remainingLeftPage < maxPageRightAndLeft)
                {
                    nearestMidddle = 1 + maxPageRightAndLeft;
                }

                PagingBeginningIndex = nearestMidddle - maxPageRightAndLeft;
                PagingEndIndex = nearestMidddle + maxPageRightAndLeft;
            }
            else
            {
                if (PagesAvailable > 1)
                {
                    PagingBeginningIndex = 1;
                    PagingEndIndex = PagesAvailable;
                }
                else
                {
                    PagingBeginningIndex = 0;
                }
            }
        }
    }
}