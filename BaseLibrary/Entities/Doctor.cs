using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.Entities
{
    public class Doctor:OtherBaseEntity
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter Valid Doctor Name")]
        public String DoctorName { get; set; } = String.Empty;
        [Required(ErrorMessage = "Data is Required")]
        public DateTime Date { get; set; }
        [Required( AllowEmptyStrings =false,ErrorMessage = "Medical Diagnose is required")]
        [StringLength(200, ErrorMessage = "Medical Diagnose is too long (max 200 chars).")]
        public String MedicalDiagnose { get; set; } = string.Empty;
        [Required(AllowEmptyStrings = false,ErrorMessage = "Medical Recommendation is required")]
        public String MedicalRecommndation {  get; set; } = string.Empty;

        // One to Many Relation Ship
        [Required(ErrorMessage ="Need Doctor Type")]
        [Range(1, int.MaxValue, ErrorMessage = "Enter Valid Doctor Type")]
        public int DoctorTypeId { get; set; }
        public DoctorType? DoctorType { get; set; }


    }
}
