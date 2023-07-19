using MultiPlug.Base.Http;
using MultiPlug.Base.Attribute;

namespace MultiPlug.Ext._4IR.SOS.Controllers.Settings.Home
{
    [Route("")]
    public class HomeController : SettingsApp
    {
        public Response Get()
        {
            return new Response
            {
                Template = "MultiPlug.Ext.4IR.SOS_Home",
            };
        }

        public Response Post()
        {
            return new Response
            {
                StatusCode = System.Net.HttpStatusCode.Moved,
                Location = Context.Referrer
            };
        }
    }
}
