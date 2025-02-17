using System;
using System.IO;
using System.Linq;
using System.Globalization;
using System.Threading.Tasks;
using MultiPlug.Ext._4IR.SOS.Models;
using MultiPlug.Ext._4IR.SOS.Properties;
using MultiPlug.Extension.Core;
using MultiPlug.Extension.Core.Http;
using MultiPlug.Ext._4IR.SOS.Utils.Swan;

namespace MultiPlug.Ext._4IR.SOS
{
    public class SOS : MultiPlugExtension
    {
        private bool m_RunOnce = true;
        public override RazorTemplate[] RazorTemplates
        {
            get
            {
                return new RazorTemplate[]
                {
                    new RazorTemplate("MultiPlug.Ext.4IR.SOS_Home",Resources.Home)
                };
            }
        }

        public override void Initialise()
        {
            if (m_RunOnce)
            {
                m_RunOnce = false;

                Task.Run(() =>
                {
                    string Version = InstalledVersion();

                    if (Version != string.Empty && Version != "1.0.1") //Upgrade
                    {
                        if (RemoveService() == null)
                        {
                            if (InstallService() == null)
                            {
                                Utils.Hardware.Instance.RebootUserPrompt = true;
                            }
                        }
                    }

                    // Delete Old Snapshots

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

                        if (Files.Count > 4)
                        {
                            Files = Files.OrderByDescending(x => x.Created).ToList();
                            Files = Files.Skip(4).ToList();

                            foreach (var file in Files)
                            {
                                try
                                {
                                    File.Delete(file.Path);
                                }
                                catch
                                { }
                            }
                        }
                    }
                });
            }
        }

        internal static string InstalledVersion()
        {
            if (Utils.Hardware.Instance.isRunningRaspberryPi)
            {
                var Task = ProcessRunner.GetProcessResultAsync("apt", "list -a adaptorsos");
                Task.Wait();

                if (Task.Result.Okay())
                {
                    var OutPut = Task.Result.GetOutput();

                    var Installed = OutPut.Contains("installed");

                    if (Installed)
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
                                if (Index < OutPut.Length)
                                {
                                    Index = OutPut.IndexOfAny(VersionNumbers.ToCharArray(), Index);
                                }
                                else
                                {
                                    Index = -1;
                                }
                            }
                            if (StartIndex != EndIndex)
                            {
                                EndIndex++;
                                return OutPut.Substring(StartIndex, EndIndex - StartIndex);
                            }
                        }
                    }
                }
            }
            return string.Empty;
        }

        internal static string InstallService()
        {
            string Result = null;
            try
            {
                if (Utils.Hardware.Instance.isRunningRaspberryPi)
                {
                    var Task = ProcessRunner.GetProcessResultAsync("apt-get", "-qq install /usr/local/bin/multiplug/extensions/MultiPlug.Ext.4IR.SOS/adaptorsos_1.0.1_all.deb");
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

            return Result;
        }

        internal static string RemoveService()
        {
            string Result = null;

            try
            {
                if (Utils.Hardware.Instance.isRunningRaspberryPi)
                {
                    var Task = ProcessRunner.GetProcessResultAsync("apt-get", "-qq remove adaptorsos");
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

            return Result;
        }
    }
}
