using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RunboardAPI.DataAccessLayer
{
    public interface IDataAccessLayer<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Get();
        TEntity GetByType(string id);

        TEntity GetByTypeDt(string id, DateTime date);

        //TEntity Get(TPrimaryKey id);
        //void Post(TEntity entity);
        //void Put(TPrimaryKey id, TEntity entity);
        //void Delete(TPrimaryKey id);


    }
}