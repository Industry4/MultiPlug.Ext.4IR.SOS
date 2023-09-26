using System;
using System.IO;

namespace AdaptorSOS
{
    class Program
    {
        static void Main(string[] args)
        {
            string SOSExtensionPath = "/usr/local/bin/multiplug/extensions/MultiPlug.Ext.4IR.SOS/";

            if( Directory.Exists(SOSExtensionPath))
            {
                string Snapshots = Path.Combine(SOSExtensionPath, "snapshots");

                if (!Directory.Exists(Snapshots))
                {
                    Directory.CreateDirectory(Snapshots);
                }

                string ZipPath = _4IR.SOS.Adaptor.Retrieve.LogFiles(DateTime.Now.ToString("MM-dd-yyyy-HH-mm-ss") + ".zip");

                if(File.Exists(ZipPath))
                {
                    File.Copy(ZipPath, Path.Combine(Snapshots, Path.GetFileName(ZipPath)), true);
                }

                try
                {
                    File.Delete(ZipPath);
                }
                catch
                {
                }
            }
        }
    }
}
