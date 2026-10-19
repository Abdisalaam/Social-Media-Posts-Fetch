using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrandWatchElaqat.Models
{
    public partial class ClientProjectQuerie
    {
        public static List<ClientProjectQuerie> GetAllClientProjectQuerie()
        {
            var db = new DBModelBrandWatchContainer();

            return db.ClientProjectQueries.ToList(); ;

        }

        public static ClientProjectQuerie GetClientProjectQuerieDetails(int clientProjectQuerieID)
        {
            var db = new DBModelBrandWatchContainer();

            return db.ClientProjectQueries.Find(clientProjectQuerieID);
        }
    }
}