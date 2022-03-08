namespace FirstCSharp.Api.Server.Controller
{
    using FirstCSharp.Domain.Model;
    using FirstCSharp.Domain.Repository;
    using Newtonsoft.Json;
    using NLog;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    class MemberController : ApiController 
    {
        private ILogger logger = LogManager.GetLogger("FirstCSharp.Api.Server");

        private IMemberRepository repo;

        public MemberController(IMemberRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public HttpResponseMessage Get(int? sujectId)
        {
            try
            {
                var queryResult = this.repo.Query();

                if (queryResult.exception != null)
                {
                    throw queryResult.exception;
                }

                var result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new StringContent(JsonConvert.SerializeObject(queryResult.members));
                return result;
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, $"{this.GetType().Name} Get Exception sujectId:{sujectId}");
                return this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] MemberAddDto input)
        {
            try
            {
                //執行持久層
                var addResult = this.repo.Insert(new Member() {
                    f_name = input.MemberName,
                    f_price = input.MemberPrice,
                    f_descript = input.MemberDescrip
                });

                if (addResult.exception != null)
                {
                    throw addResult.exception;
                }

                var result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new StringContent(JsonConvert.SerializeObject(addResult.member));
                return result;
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, $"{this.GetType().Name} Post Exception Request:{input.ToString()}");
                return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var delResult = this.repo.Delete(id);

                if (delResult.exception != null)
                {
                    throw delResult.exception;
                }

                var result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new StringContent(JsonConvert.SerializeObject(delResult.member));
                return result;
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, $"{this.GetType().Name} Delete Exception id:{id}");
                return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
