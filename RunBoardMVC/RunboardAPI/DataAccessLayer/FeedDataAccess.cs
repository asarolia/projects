using Microsoft.Practices.Unity;
using RunboardAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RunboardAPI.DataAccessLayer
{
    public class FeedDataAccess : IDataAccessLayer<FeedData>
    {
        public IEnumerable<FeedData> Get()
        {
            throw new NotImplementedException();
        }

        public FeedData GetByType(string id)
        {
            throw new NotImplementedException();
        }

        public FeedData GetByTypeDt(string id, DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}