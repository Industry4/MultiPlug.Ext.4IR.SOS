using System;
using System.IO;
using System.Linq;
using System.Globalization;
using MultiPlug.Base.Http;
using MultiPlug.Base.Attribute;
using MultiPlug.Ext._4IR.SOS.Models.Settings.Home;
using MultiPlug.Ext._4IR.SOS.Models;

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

            string Version = SOS.InstalledVersion();

            Model.ServiceInstalledVersion = SOS.InstalledVersion();
            Model.ServiceInstalled = Model.ServiceInstalledVersion != string.Empty;

            string SnapshotPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "snapshots");

            if (Directory.Exists(SnapshotPath))
            {
                var Files = Directory.GetFiles(SnapshotPath).Select(FullPath =>
                {
                    return new FileProp
                    {
                        Path = FullPath,
                        Created = DateTime.ParseExact(Path.GetFileNameWithoutExtension(FullPath), "MM-dd-yyyy-HH-mm-ss", CultureInfo.InvariantCulture)
                    };
                }).ToList();

                Files = Files.OrderBy(x => x.Created).ToList();

                Model.Files = Files.Select(f => Path.GetFileName(f.Path)).ToArray();
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
