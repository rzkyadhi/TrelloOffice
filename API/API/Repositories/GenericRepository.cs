using API.Context;
using API.Repository.Interface;
using System.Collections.Generic;
using System.Linq;

namespace API.Repositories
{
    public class GenericRepository<TModel> : IGeneralRepository<TModel>
        where TModel : class
    {
        private readonly MyContext myContext;

        public GenericRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }

        /*public virtual List<TModel> Get()
        {
            var data = myContext.Set<TModel>().ToList();
            return data;
        }*/

        public TModel Get(int id)
        {
            var data = myContext.Set<TModel>().Find(id);
            return data;
        }

        public virtual int Post(TModel model)
        {
            myContext.Set<TModel>().Add(model);
            var data = myContext.SaveChanges();
            return data;
        }

        public virtual int Put(TModel model)
        {
            myContext.Set<TModel>().Update(model);
            var data = myContext.SaveChanges();
            return data;
        }

        public int Delete(TModel model)
        {
            myContext.Set<TModel>().Remove(model);
            var data = myContext.SaveChanges();
            return data;
        }
    }
}
