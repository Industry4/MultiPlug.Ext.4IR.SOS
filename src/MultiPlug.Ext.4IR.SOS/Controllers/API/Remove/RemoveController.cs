using MultiPlug.Base.Attribute;
using MultiPlug.Base.Http;

namespace MultiPlug.Ext._4IR.SOS.Controllers.API.Remove
{
    [Route("remove")]
    public class RemoveController : APIEndpoint
    {
        public Response Post()
        {
            string Result = SOS.RemoveService();

            if ( Result == null)
            {
                Utils.Hardware.Instance.RebootUserPrompt = true;

                return new Response
                {
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
            else
            {
                return new Response
                {
                    Model = new { ErrorMessage = Result },
                    StatusCode = System.Net.HttpStatusCode.ServiceUnavailable,
                    MediaType = "application/json"
                };
            }
        }
    }
}