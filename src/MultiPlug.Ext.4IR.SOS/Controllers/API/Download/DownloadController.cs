using System;
using System.IO;
using System.IO.Compression;
using MultiPlug.Base.Attribute;
using MultiPlug.Base.Http;

namespace MultiPlug.Ext._4IR.SOS.Controllers.API.Download
{
    [Route("download/*")]
    public class DownloadController : APIEndpoint
    {
        public Response Get(string id)
        {
            string folderpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "collection");
            string zippath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, id);

            if (!Directory.Exists(folderpath))
            {
                Directory.CreateDirectory(folderpath);
            }

            foreach (var file in Directory.GetFiles("/usr/local/bin/multiplug/extensions/MultiPlug.Ext.Hermes/diag/"))
            {
                try
                {
                    File.Copy(file, Path.Combine(folderpath, Path.GetFileName(file)),true);
                }
                catch
                {
                    using (StreamWriter outputFile = new StreamWriter(Path.Combine(folderpath, "Errors.txt"), true))
                    {
                        outputFile.WriteLine(file);
                    }
                }
            }

            foreach (var file in Directory.GetFiles("/usr/local/bin/multiplug/extensions/MultiPlug.Ext.Network.Sockets/diag/"))
            {
                try
                {
                    File.Copy(file, Path.Combine(folderpath, Path.GetFileName(file)), true);
                }
                catch
                {
                    using (StreamWriter outputFile = new StreamWriter(Path.Combine(folderpath, "Errors.txt"), true))
                    {
                        outputFile.WriteLine(file);
                    }
                }
            }

            foreach (var file in Directory.GetFiles("/usr/local/bin/multiplug/extensions/MultiPlug.Ext.SerialPort/diag/"))
            {
                try
                {
                    File.Copy(file, Path.Combine(folderpath, Path.GetFileName(file)), true);
                }
                catch
                {
                    using (StreamWriter outputFile = new StreamWriter(Path.Combine(folderpath, "Errors.txt"), true))
                    {
                        outputFile.WriteLine(file);
                    }
                }
            }

            foreach (var file in Directory.GetFiles("/usr/local/bin/multiplug/diag/"))
            {
                try
                {
                    File.Copy(file, Path.Combine(folderpath, Path.GetFileName(file)), true);
                }
                catch
                {
                    using (StreamWriter outputFile = new StreamWriter(Path.Combine(folderpath, "Errors.txt"), true))
                    {
                        outputFile.WriteLine(file);
                    }
                }
            }

            foreach (var file in Directory.GetFiles("/usr/local/bin/multiplug/"))
            {
                try
                {
                    if( file.EndsWith("exe"))
                    {
                        continue;
                    }

                    if (file.EndsWith("dll"))
                    {
                        continue;
                    }

                    if (file.EndsWith("lock"))
                    {
                        continue;
                    }

                    File.Copy(file, Path.Combine(folderpath, Path.GetFileName(file)), true);
                }
                catch
                {
                    using (StreamWriter outputFile = new StreamWriter(Path.Combine(folderpath, "Errors.txt"), true))
                    {
                        outputFile.WriteLine(file);
                    }
                }
            }

            ZipFile.CreateFromDirectory(folderpath, zippath);

            byte[] readText = File.ReadAllBytes(zippath);

            File.Delete(zippath);

            foreach (var file in Directory.GetFiles(folderpath))
            {
                try
                {
                    File.Delete(file);
                }
                catch
                {
                }
            }

            Directory.Delete(folderpath, true);

            return new Response
            {
                MediaType = "application/zip",
                RawBytes = readText
            };

        }
    }
}
