using MultiPlug.Ext._4IR.SOS.Properties;
using MultiPlug.Extension.Core;
using MultiPlug.Extension.Core.Http;

namespace MultiPlug.Ext._4IR.SOS
{
    public class SOS : MultiPlugExtension
    {
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
    }
}
