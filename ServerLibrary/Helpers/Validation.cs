using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Helpers
{
    public static class Validation
    {
        public static List<String> ValidateModel<T>(T model) where T : class
        {
            var context = new ValidationContext(model!);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(model!, context, results, true);
            return results.Select(r => r.ErrorMessage!).ToList();
        }
    }
}
