using MultiPlug.Base.Http;
using MultiPlug.Extension.Core.Attribute;

namespace MultiPlug.Ext._4IR.SOS.Controllers.Settings
{
    [Name("SOS")]
    [HttpEndpointType(HttpEndpointType.Settings)]
    [ViewAs(ViewAs.Partial)]
    public class SettingsApp : Controller
    {
    }
}
