/// <summary>
/// SWAN: Stuff We All Need (Unosquare's collection of C# extension methods and classes)
/// https://github.com/unosquare/swan
/// MIT License
/// Repeating code and reinventing the wheel is generally considered bad practice. 
/// At Unosquare we are committed to beautiful code and great software. Swan is a 
/// collection of classes and extension methods that we (and other good developers)
/// have written and evolved over the years. We found ourselves copying and pasting
/// the same code for every project every time we started them. We decided to kill
/// that cycle once and for all. This is the result of that idea. Our philosophy is 
/// that Swan should have no external dependencies, it should be cross-platform, 
/// and it should be useful.
/// </summary>
namespace MultiPlug.Ext._4IR.SOS.Utils.Swan
{
    using System.Text;

    /// <summary>
    /// Contains useful constants and definitions.
    /// </summary>
    internal static partial class Definitions
    {
        /// <summary>
        /// The MS Windows codepage 1252 encoding used in some legacy scenarios
        /// such as default CSV text encoding from Excel.
        /// </summary>
        internal static readonly Encoding Windows1252Encoding;

        /// <summary>
        /// The encoding associated with the default ANSI code page in the operating 
        /// system's regional and language settings.
        /// </summary>
        internal static readonly Encoding CurrentAnsiEncoding;

        /// <summary>
        /// Initializes the <see cref="Definitions"/> class.
        /// </summary>
        static Definitions()
        {
            CurrentAnsiEncoding = Encoding.GetEncoding(default(int));
            try
            {
                Windows1252Encoding = Encoding.GetEncoding(1252);
            }
            catch
            {
                // ignore, the codepage is not available use default
                Windows1252Encoding = CurrentAnsiEncoding;
            }
        }
    }
}