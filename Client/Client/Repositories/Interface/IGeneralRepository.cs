using System.Collections.Generic;
using System.Net;

namespace Client.Repositories.Interface
{
    public interface IGeneralRepository<TModel>
        where TModel : class
    {
        List<TModel> Get();
        TModel Get(int id);
        HttpStatusCode Put(TModel model);
        HttpStatusCode Post(TModel model);
        HttpStatusCode Delete(TModel model);
    }
}
