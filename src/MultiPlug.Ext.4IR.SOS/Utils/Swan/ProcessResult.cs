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
    /// <summary>
    /// Represents the text of the standard output and standard error
    /// of a process, including its exit code.
    /// </summary>
    internal class ProcessResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessResult" /> class.
        /// </summary>
        /// <param name="exitCode">The exit code.</param>
        /// <param name="standardOutput">The standard output.</param>
        /// <param name="standardError">The standard error.</param>
        internal ProcessResult(int exitCode, string standardOutput, string standardError)
        {
            ExitCode = exitCode;
            StandardOutput = standardOutput;
            StandardError = standardError;
        }

        /// <summary>
        /// Gets the exit code.
        /// </summary>
        /// <value>
        /// The exit code.
        /// </value>
        internal int ExitCode { get; }

        /// <summary>
        /// Gets the text of the standard output.
        /// </summary>
        /// <value>
        /// The standard output.
        /// </value>
        internal string StandardOutput { get; }

        /// <summary>
        /// Gets the text of the standard error.
        /// </summary>
        /// <value>
        /// The standard error.
        /// </value>
        internal string StandardError { get; }

        internal string GetOutput()
        {
            return ExitCode == 0 ? StandardOutput : StandardError;
        }

        internal bool Okay()
        {
            return (ExitCode == 0);
        }
    }
}