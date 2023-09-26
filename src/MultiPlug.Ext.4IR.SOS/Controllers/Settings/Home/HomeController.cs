using System;
using System.IO;
using System.Linq;
using MultiPlug.Base.Http;
using MultiPlug.Base.Attribute;
using MultiPlug.Ext._4IR.SOS.Models.Settings.Home;
using MultiPlug.Ext._4IR.SOS.Utils.Swan;

namespace MultiPlug.Ext._4IR.SOS.Controllers.Settings.Home
{
    [Route("")]
    public class HomeController : SettingsApp
    {
        public Response Get()
        {
            var Model = new GetModel();
            Model.ServiceInstalledVersion = "Unknown";
            Model.RebootUserPrompt = Utils.Hardware.Instance.RebootUserPrompt;

            if (Utils.Hardware.Instance.isRunningRaspberryPi)
            {
                var Task = ProcessRunner.GetProcessResultAsync("apt", "list -a adaptorsos");
                Task.Wait();

                if (Task.Result.Okay())
                {
                    var OutPut = Task.Result.GetOutput();

                    Model.ServiceInstalled = OutPut.Contains("installed");

                    if(Model.ServiceInstalled)
                    {
                        string VersionNumbers = "0123456789";
                        int Index = OutPut.IndexOfAny(VersionNumbers.ToCharArray());
                        int StartIndex = 0;
                        int EndIndex = 0;

                        if (Index != -1)
                        {
                            StartIndex = Index;

                            while (Index != -1)
                            {
                                EndIndex = Index;
                                Index++;
                                if( Index < OutPut.Length)
                                {
                                    Index = OutPut.IndexOfAny(VersionNumbers.ToCharArray(), Index);
                                }
                                else
                                {
                                    Index = -1;
                                }
                            }
                            if(StartIndex != EndIndex)
                            {
                                EndIndex++;
                                Model.ServiceInstalledVersion = OutPut.Substring(StartIndex, EndIndex - StartIndex);
                            }
                        }
                    }
                }
            }

            string SnapshotPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "snapshots");

            if (Directory.Exists(SnapshotPath))
            {
                Model.Files = Directory.GetFiles(SnapshotPath).Select(f => Path.GetFileName(f)).ToArray();
            }
            else
            {
                Model.Files = new string[0];
            }

            return new Response
            {
                Model = Model,
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
