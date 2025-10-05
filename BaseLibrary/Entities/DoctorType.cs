using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BaseLibrary.Entities;

public class DoctorType : BaseEntity
{
    [JsonIgnore]
    public List<Doctor> Doctors { get; set; } = new();
}
