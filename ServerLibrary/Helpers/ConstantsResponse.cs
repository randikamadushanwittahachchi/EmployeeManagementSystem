using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Helpers
{
    public class ConstantsResponse
    {
        public static string Success { get; } = "Process completed successfully.";
        public static string NotFound { get; } = " was not found.";
        public static string Exit { get; } = " already exists";

    }
}
