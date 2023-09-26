using System;
using System.IO;
using MultiPlug.Base.Attribute;
using MultiPlug.Base.Http;

namespace MultiPlug.Ext._4IR.SOS.Controllers.API.Download
{
    [Route("download/*")]
    public class DownloadController : APIEndpoint
    {
        public Response Get(string id)
        {
            string SnapshotPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "snapshots");
            string ZipFilePath = SnapshotPath + Path.DirectorySeparatorChar + id;

            if (Directory.Exists(SnapshotPath) && File.Exists(ZipFilePath) )
            {
                byte[] readText = File.ReadAllBytes(ZipFilePath);
                return new Response
                {
                    MediaType = "application/zip",
                    RawBytes = readText
                };
            }
            else
            {
                return new Response
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };
            }
        }
    }
}
