using CachingRuntime.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;

namespace CachingRuntime.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var Appalias = this.AppID;
            var data=  GetCategories(Appalias);
            return View(data);
        }


        public List<Category> GetCategories(string AppAlias)
        {
            List<Category> result = new List<Category>();
            try
            {
                var cache = MemoryCache.Default;
             

                if (cache.Get(AppAlias + "MemoryCache_GetCategories") == null)
                {
                    var cachePolicty = new CacheItemPolicy();
                    cachePolicty.AbsoluteExpiration = DateTime.Now.AddHours(200);
                  

                    using (var db = new NorthwindEntities())
                    {
                        result = db.Categories.ToList();

                        cache.Add(AppAlias + "MemoryCache_GetCategories", result, cachePolicty);
                    }
                }
                else
                {
                    result = (List<Category>)cache.Get(AppAlias + "MemoryCache_GetCategories");
                }


            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }


    }
}