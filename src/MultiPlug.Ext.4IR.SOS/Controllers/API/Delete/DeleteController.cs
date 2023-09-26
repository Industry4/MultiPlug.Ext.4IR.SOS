using System;
using System.IO;
using MultiPlug.Base.Attribute;
using MultiPlug.Base.Http;

namespace MultiPlug.Ext._4IR.SOS.Controllers.API.Delete
{
    [Route("delete/*")]
    public class DeleteController : APIEndpoint
    {
        public Response Post(string id)
        {
            string SnapshotPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "snapshots");
            string FullPath = SnapshotPath + Path.DirectorySeparatorChar + id;

            if (File.Exists(FullPath))
            {
                try
                {
                    File.Delete(FullPath);
                }
                catch
                {
                }
            }

            return new Response
            {
                StatusCode = System.Net.HttpStatusCode.Moved,
                Location = Context.Referrer
            };

        }
    }
}
