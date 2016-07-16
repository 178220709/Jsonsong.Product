using
    System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver.Linq;

namespace Jsonsong.Dal.Common.MongoDb
{
    public class Pager
    {
        public Pager()
        {
            this.PageIndex = 1;
            this.PageSize = 20;
        }

        public Pager(int pageIndex, int pageSize)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
        }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int Skip => this.PageSize*(this.PageIndex - 1);
    }

    public interface IQueryCollection<T>
    {
        IQueryable<T> Query(IQueryable<T> queryable);
    }

    public static class PagerEx
    {
        public static IQueryable<T> PagingQueryable<T>(this IQueryable<T> queryable,Pager pager)
        {
            return queryable.Skip(pager.Skip).Take(pager.PageSize);
        }

        public static IEnumerable<T> PagingEnumerable<T>(this IEnumerable<T> enumerable, Pager pager)
        {
            return enumerable.Skip(pager.Skip).Take(pager.PageSize).ToList();
        } 
    }
}