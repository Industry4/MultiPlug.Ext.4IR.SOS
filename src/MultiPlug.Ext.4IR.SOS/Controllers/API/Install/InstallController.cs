using System;
using MultiPlug.Base.Attribute;
using MultiPlug.Base.Http;
using MultiPlug.Ext._4IR.SOS.Utils.Swan;

namespace MultiPlug.Ext._4IR.SOS.Controllers.API.Install
{
    [Route("install")]
    public class InstallController : APIEndpoint
    {
        public Response Post()
        {
            string Result = null;
            try
            {
                if (Utils.Hardware.Instance.isRunningRaspberryPi)
                {
                    var Task = ProcessRunner.GetProcessResultAsync("apt-get", "-qq install /usr/local/bin/multiplug/extensions/MultiPlug.Ext.4IR.SOS/adaptorsos_1.0.0_all.deb");
                    Task.Wait();

                    if (!Task.Result.Okay())
                    {
                        Result = Task.Result.StandardError;
                    }
                }
            }
            catch (Exception ex)
            {
                Result = ex.Message;
            }

            if (Result == null)
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