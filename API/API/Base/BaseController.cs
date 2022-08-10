using API.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<TModel, TRepository> : ControllerBase
        where TModel : class
        where TRepository : IGeneralRepository<TModel>
    {
        TRepository repository;

        public BaseController(TRepository repository)
        {
            this.repository = repository;
        }

        #region GET
        [HttpGet]
        public ActionResult<List<TModel>> Get()
        {
            var result = repository.Get();
            if (result != null)
                return Ok(new
                {
                    status = 200,
                    message = "SUCCESS",
                    data = result
                });
            return NotFound(new
            {
                status = 404,
                message = "NOT FOUND"
            });
        }
        #endregion GET

        #region GetId
        [HttpGet("{id}")]
        public virtual ActionResult<TModel> Get(int id)
        {
            var result = repository.Get(id);
            if (result != null)
                return Ok(new
                {
                    status = 200,
                    message = "SUCCESS",
                    data = result
                });
            return NotFound(new
            {
                status = 404,
                message = "NOT FOUND"
            });
        }
        #endregion GetId

        #region Post
        [HttpPost]
        public ActionResult<int> Post(TModel model)
        {
            var data = repository.Post(model);
            if (data > 0)
                return Created(nameof(Get), new
                {
                    status = 200,
                    message = "Created"
                });
            return BadRequest(new
            {
                status = 400,
                message = "Bad Request"
            });
        }
        #endregion Post

        #region Put
        [HttpPut]
        public ActionResult<int> Put(TModel model)
        {
            var data = repository.Put(model);
            if (data > 0)
                return Ok(new
                {
                    status = 200,
                    message = "Updated"
                });
            return BadRequest(new
            {
                status = 400,
                message = "Bad Request"
            });
        }
        #endregion Put

        #region Delete
        [HttpDelete]
        public ActionResult<int> Delete(TModel model)
        {
            var data = repository.Delete(model);
            if (data > 0)
                return Ok(new
                {
                    status = 200,
                    message = "Delete"
                });
            return BadRequest(new
            {
                status = 400,
                message = "Bad Request"
            });
        }
        #endregion Delete
    }
}
