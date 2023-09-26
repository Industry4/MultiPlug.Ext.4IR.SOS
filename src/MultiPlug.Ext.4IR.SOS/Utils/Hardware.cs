using System;
using System.Threading.Tasks;
using MultiPlug.Ext._4IR.SOS.Utils.Swan;

namespace MultiPlug.Ext._4IR.SOS.Utils
{
    public class Hardware
    {
        private static Hardware m_Instance = null;

        public bool isRunningRaspberryPi { get; private set; } = false;
        public bool isRunningMono { get; private set; } = false;
        public bool RebootUserPrompt { get; set; } = false;

        private Hardware()
        {
            if (Type.GetType("Mono.Runtime") != null)
            {
                isRunningMono = true;

                Task<ProcessResult> RaspberryPiModel = ProcessRunner.GetProcessResultAsync("cat", "/proc/device-tree/model");

                RaspberryPiModel.Wait();

                if (RaspberryPiModel.Result.Okay())
                {
                    isRunningRaspberryPi = true;
                }
            }
        }

        public static Hardware Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new Hardware();
                }
                return m_Instance;
            }
        }
    }
}
