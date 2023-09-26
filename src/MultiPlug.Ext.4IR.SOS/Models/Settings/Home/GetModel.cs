
namespace MultiPlug.Ext._4IR.SOS.Models.Settings.Home
{
    public class GetModel
    {
        public string[] Files { get; set; }
        public bool RebootUserPrompt { get; internal set; }
        public bool ServiceInstalled { get; set; }
        public string ServiceInstalledVersion { get; set; }
    }
}
