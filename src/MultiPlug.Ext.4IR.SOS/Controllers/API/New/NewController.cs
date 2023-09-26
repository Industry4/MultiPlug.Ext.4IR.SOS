using System;
using System.IO;
using MultiPlug.Base.Attribute;
using MultiPlug.Base.Http;
using AdaptorSOSLib = _4IR.SOS.Adaptor.Retrieve;

namespace MultiPlug.Ext._4IR.SOS.Controllers.API.New
{
    [Route("new")]
    public class NewController : APIEndpoint
    {
        public Response Post()
        {
            string SnapshotPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "snapshots");
            string ZipFilePath = AdaptorSOSLib.LogFiles(DateTime.Now.ToString("MM-dd-yyyy-HH-mm-ss") + ".zip");
            if (!Directory.Exists(SnapshotPath))
            {
                Directory.CreateDirectory(SnapshotPath);
            }
            File.Move(ZipFilePath, SnapshotPath + Path.DirectorySeparatorChar + Path.GetFileName(ZipFilePath));

            return new Response
            {
                StatusCode = System.Net.HttpStatusCode.Moved,
                Location = Context.Referrer
            };

        }
    }
}
