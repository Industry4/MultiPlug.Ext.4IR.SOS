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
            CopySingleFileFromExtensions(folderpath, "MultiPlug.Ext.Recipe.File", "/usr/local/bin/multiplug/extensions/MultiPlug.Ext.Recipe.File/base.json");

            string MultiPlugHomeDirectory = "/usr/local/bin/multiplug/";

            if (Directory.Exists(MultiPlugHomeDirectory))
            {
                string CollectionMono = Path.Combine(folderpath, "Mono");

                if (!Directory.Exists(CollectionMono))
                {
                    Directory.CreateDirectory(CollectionMono);
                }

                foreach (var path in Directory.GetFiles(MultiPlugHomeDirectory))
                {
                    var FileName = Path.GetFileName(path);

                    try
                    {
                        if (FileName.EndsWith("exe"))
                        {
                            continue;
                        }

                        if (FileName.EndsWith("dll"))
                        {
                            continue;
                        }

                        if (FileName.EndsWith("lock"))
                        {
                            continue;
                        }

                        if (FileName.EndsWith("json"))
                        {
                            if ( ! FileName.StartsWith("mono") )
                            {
                                continue;
                            }
                        }

                        if (FileName.EndsWith("config"))
                        {
                            continue;
                        }

                        File.Copy(path, Path.Combine(CollectionMono, Path.GetFileName(path)), true);
                    }
                    catch
                    {
                        using (StreamWriter outputFile = new StreamWriter(Path.Combine(folderpath, "Errors.txt"), true))
                        {
                            outputFile.WriteLine(path);
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
                string ExtensionFolder = Path.Combine(theCollectionFolderPath, theExtensionName);

                if (!Directory.Exists(ExtensionFolder))
                {
                    Directory.CreateDirectory(ExtensionFolder);
                }

                foreach (var file in Directory.GetFiles(thePathToTheExtensionFolder))
                {
                    try
                    {
                        File.Copy(file, Path.Combine(ExtensionFolder, Path.GetFileName(file)), true);
                    }
                    catch
                    {
                        using (StreamWriter outputFile = new StreamWriter(Path.Combine(ExtensionFolder, "Errors.txt"), true))
                        {
                            outputFile.WriteLine(file);
                        }
                    }
                }
            }
        }

        private static void CopySingleFileFromExtensions(string theCollectionFolderPath, string theExtensionName, string theFileNameAndPath)
        {
            if (File.Exists(theFileNameAndPath))
            {
                string ExtensionFolder = Path.Combine(theCollectionFolderPath, theExtensionName);

                if (!Directory.Exists(ExtensionFolder))
                {
                    Directory.CreateDirectory(ExtensionFolder);
                }

                try
                {
                    File.Copy(theFileNameAndPath, Path.Combine(ExtensionFolder, Path.GetFileName(theFileNameAndPath)), true);
                }
                catch
                {
                    using (StreamWriter outputFile = new StreamWriter(Path.Combine(ExtensionFolder, "Errors.txt"), true))
                    {
                        outputFile.WriteLine(theFileNameAndPath);
                    }
                }
            }
        }
    }
}
