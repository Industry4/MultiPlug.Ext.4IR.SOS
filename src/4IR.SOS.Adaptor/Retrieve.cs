using System;
using System.IO;
using System.IO.Compression;

namespace _4IR.SOS.Adaptor
{
    public static class Retrieve
    {
        /// <summary>
        /// Retrieves the Log Files of common Extensions of the SMEMA Hermes Adaptor and SMEMA Ethernet Adaptor
        /// </summary>
        /// <param name="theZipFileName">The Zip File Name</param>
        /// <returns>The Path to the Zip Folder</returns>
        public static string LogFiles( string theZipFileName)
        {
            string folderpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "collection");
            string zippath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, theZipFileName);

            if (!Directory.Exists(folderpath))
            {
                Directory.CreateDirectory(folderpath);
            }

            CopyFilesFromExtensions(folderpath, "MultiPlug.Ext.Hermes", "/usr/local/bin/multiplug/extensions/MultiPlug.Ext.Hermes/diag/");
            CopyFilesFromExtensions(folderpath, "MultiPlug.Ext.Hermes.Config", "/usr/local/bin/multiplug/extensions/MultiPlug.Ext.Hermes.Config/diag/");
            CopyFilesFromExtensions(folderpath, "MultiPlug.Ext.SMEMA", "/usr/local/bin/multiplug/extensions/MultiPlug.Ext.SMEMA/diag/");
            CopyFilesFromExtensions(folderpath, "MultiPlug.Ext.Network.Sockets", "/usr/local/bin/multiplug/extensions/MultiPlug.Ext.Network.Sockets/diag/");
            CopyFilesFromExtensions(folderpath, "MultiPlug.Ext.SerialPort", "/usr/local/bin/multiplug/extensions/MultiPlug.Ext.SerialPort/diag/");
            CopyFilesFromExtensions(folderpath, "MultiPlug.Ext.RasPi.Config", "/usr/local/bin/multiplug/extensions/MultiPlug.Ext.RasPi.Config/diag/");
            CopyFilesFromExtensions(folderpath, "MultiPlug.Ext.RasPi.GPIO", "/usr/local/bin/multiplug/extensions/MultiPlug.Ext.RasPi.GPIO/diag/");
            CopyFilesFromExtensions(folderpath, "MultiPlug.Ext.Nuget", "/usr/local/bin/multiplug/extensions/MultiPlug.Ext.Nuget/diag/");
            CopyFilesFromExtensions(folderpath, "MultiPlug", "/usr/local/bin/multiplug/diag/");

            string MultiPlugHomeDirectory = "/usr/local/bin/multiplug/";

            if (Directory.Exists(MultiPlugHomeDirectory))
            {
                string CollectionMono = Path.Combine(folderpath, "Mono");

                if (!Directory.Exists(CollectionMono))
                {
                    Directory.CreateDirectory(CollectionMono);
                }

                foreach (var file in Directory.GetFiles(MultiPlugHomeDirectory))
                {
                    try
                    {
                        if (file.EndsWith("exe"))
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

                        if (file.EndsWith("json"))
                        {
                            if( ! file.StartsWith("mono") )
                            {
                                continue;
                            }
                        }

                        if (file.EndsWith("config"))
                        {
                            continue;
                        }

                        File.Copy(file, Path.Combine(CollectionMono, Path.GetFileName(file)), true);
                    }
                    catch
                    {
                        using (StreamWriter outputFile = new StreamWriter(Path.Combine(folderpath, "Errors.txt"), true))
                        {
                            outputFile.WriteLine(file);
                        }
                    }
                }
            }

            ZipFile.CreateFromDirectory(folderpath, zippath);

            Directory.Delete(folderpath, true);

            return zippath;
        }

        private static void CopyFilesFromExtensions( string theCollectionFolderPath, string theExtensionName, string thePathToTheExtensionFolder)
        {
            if (Directory.Exists(thePathToTheExtensionFolder))
            {
                string CollectionHermes = Path.Combine(theCollectionFolderPath, theExtensionName);

                if (!Directory.Exists(CollectionHermes))
                {
                    Directory.CreateDirectory(CollectionHermes);
                }

                foreach (var file in Directory.GetFiles(thePathToTheExtensionFolder))
                {
                    try
                    {
                        File.Copy(file, Path.Combine(CollectionHermes, Path.GetFileName(file)), true);
                    }
                    catch
                    {
                        using (StreamWriter outputFile = new StreamWriter(Path.Combine(CollectionHermes, "Errors.txt"), true))
                        {
                            outputFile.WriteLine(file);
                        }
                    }
                }
            }
        }
    }
}
