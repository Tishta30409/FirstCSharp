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

    public class MemberController : ApiController 
    {
        private ILogger logger = LogManager.GetLogger("FirstCSharp.Api.Server");

        private IMemberRepository repo;

        public MemberController(IMemberRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        [Route("api/Member/Test")]
        public HttpResponseMessage Get()
        {
            try
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK);
                return result;
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, $"{this.GetType().Name} Get Exception");
                return this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        [Route("api/Member/AddMember")]
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
                this.logger.Error(ex, $"{this.GetType().Name} AddMember Request:{input.ToString()}");
                return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("api/Member/AddMembers")]
        public HttpResponseMessage Post([FromBody] MembersAddDto input)
        {
            try
            {
                //執行持久層
                var addResult = this.repo.BatchInsert(input.Members);

                if (addResult.exception != null)
                {
                    throw addResult.exception;
                }

                var result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new StringContent(JsonConvert.SerializeObject(addResult.members));
                return result;
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, $"{this.GetType().Name} Post Exception Request:{input.ToString()}");
                return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPut]
        [Route("api/Member/UpdateMember")]
        public HttpResponseMessage Put([FromBody] MemberAddDto input)
        {
            try
            {
                var updateResult = this.repo.Update(new Member()
                {
                    f_id = input.MemberID,
                    f_name = input.MemberName,
                    f_price = input.MemberPrice,
                    f_descript = input.MemberDescrip
                });

                if (updateResult.exception != null)
                {
                    throw updateResult.exception;
                }

                var result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new StringContent(JsonConvert.SerializeObject(updateResult.member));
                return result;
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, $"{this.GetType().Name} Put Exception Request:{input.ToString()}");
                return this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpDelete]
        [Route("api/Member/DeleteMember")]
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
