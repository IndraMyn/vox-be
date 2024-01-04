using Microsoft.AspNetCore.Identity;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Vox.Utils
{
    public class ErrorException : Exception
    {
        public Error Error { get; set; }
        public ErrorException(Error error)
        {
            Error = error;
        }

    }
}
