using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Helpers
{
    public class ConstantsResponse
    {
        public const string Success = "The process was completed successfully.";
        public const string Unsuccess = " process was not completed successfully.";
        public const string ErrorInputData = "Invalid Data Provide";
        public const string NotFound = " was not found.";
        public const string Exit = " already exists.";
        public const string HasChild = " cannot be deleted because it has associated records.";

    }
}
