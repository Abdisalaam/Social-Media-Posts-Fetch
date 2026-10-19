using BrandWatchElaqat.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrandWatchElaqat.Models
{
    public partial class NewsBarDomain
    {
        public static List<NewsBarDomain> GetAllNewsBarFilterForProject()
        {
            var db = new DBModelBrandWatchContainer();
            
            return db.NewsBarDomains.ToList();
        }
        public static NewsBarDomain GetNewsBarFilterById(int newsBarFilterId)
        {
            var db = new DBModelBrandWatchContainer();
            return db.NewsBarDomains.Find(newsBarFilterId);
        }

        public static NewsBarFilterVM PrepareNewsBarFilterVMToBeEdit(int newsBarFilterId)
        {
            var db = new DBModelBrandWatchContainer();
            NewsBarDomain newsBarFilter = db.NewsBarDomains.Find(newsBarFilterId);
            if (newsBarFilter == null)
            {
                return null;
            }
           
            NewsBarFilterVM newsBarFilterVM = new NewsBarFilterVM
            {
                Id = newsBarFilter.Id,              
                PublicationName = newsBarFilter.PublicationName, 
                Domain = newsBarFilter.Domain,
                DomainURL = newsBarFilter.DomainURL,
                Favicon = newsBarFilter.Favicon
               

            };
            return newsBarFilterVM;

        }
        public static int UpdateNewsBarFilter(NewsBarFilterVM newsBarFilterVM)
        {
            var db = new DBModelBrandWatchContainer();

            if (newsBarFilterVM == null)
            {
                return 0;
            }
            NewsBarDomain newsBarDomain = db.NewsBarDomains.Find(newsBarFilterVM.Id);
            if (newsBarDomain == null)
            {
                return 0;
            }
           
            newsBarDomain.PublicationName = newsBarFilterVM.PublicationName;
           
            newsBarDomain.Domain = newsBarFilterVM.Domain;
            newsBarDomain.DomainURL = newsBarFilterVM.DomainURL;
           
            if (newsBarFilterVM.DomainFavIcon != null)
            {
                var data = new byte[newsBarFilterVM.DomainFavIcon.ContentLength];
                newsBarFilterVM.DomainFavIcon.InputStream.Read(data, 0, newsBarFilterVM.DomainFavIcon.ContentLength);
                newsBarDomain.Favicon = data;
            }

            db.Entry(newsBarDomain).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            var IncludedDomainsList = db.IncludedDomains.Where(e => e.DomainID == newsBarDomain.Id);

            if (IncludedDomainsList.Any())
            {
                foreach (var domain in IncludedDomainsList)
                {
                    domain.Domain = newsBarDomain.Domain;
                    domain.Favicon = newsBarDomain.Favicon;
                    db.Entry(domain).State = System.Data.Entity.EntityState.Modified;
                }
                db.SaveChanges();
            }
            return newsBarDomain.Id;
        }


       

        public static int AddNewsBarFilter(NewsBarFilterVM newsBarFilterVM)
        {
            var db = new DBModelBrandWatchContainer();
            if (newsBarFilterVM == null )
            {
                return 0;
            }
            NewsBarDomain newsBarFilter = new NewsBarDomain
            {
               
                PublicationName = newsBarFilterVM.PublicationName,               
                Domain = newsBarFilterVM.Domain,
                DomainURL = newsBarFilterVM.DomainURL,
               
                
            };
            if (newsBarFilterVM.DomainFavIcon !=null)
            {
                var data = new byte[newsBarFilterVM.DomainFavIcon.ContentLength];
                newsBarFilterVM.DomainFavIcon.InputStream.Read(data, 0, newsBarFilterVM.DomainFavIcon.ContentLength);
                newsBarFilter.Favicon = data;
            }

            db.NewsBarDomains.Add(newsBarFilter);
            db.SaveChanges();
            return newsBarFilter.Id;
        }

        public static bool DeleteNewsBarFilter(int newsBarFilterId)
        {
            var db = new DBModelBrandWatchContainer();
            NewsBarDomain newsBarDomain = db.NewsBarDomains.Find(newsBarFilterId);
            if (newsBarDomain == null)
            {
                return false;
            }


            var includedDomainsList = db.IncludedDomains.Where(e => e.DomainID == newsBarDomain.Id);
            db.NewsBarDomains.Remove(newsBarDomain);
           
            db.IncludedDomains.RemoveRange(includedDomainsList);
            db.SaveChanges();

            return true;
        }


        public static bool DeleteAllNewsBarFilterFromMentionFilter()
        {
            try
            {
                var db = new DBModelBrandWatchContainer();
                var NewsBarDomains = db.NewsBarDomains.ToList();
                var includedDomainsList = db.IncludedDomains.ToList();
                
                db.NewsBarDomains.RemoveRange(NewsBarDomains);
                db.IncludedDomains.RemoveRange(includedDomainsList);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
           
        }
    }
}