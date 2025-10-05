using BaseLibrary.Entities;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositores.Contracts;

namespace Server.Controllers;

public class DoctorTypeController : GenericController<DoctorType>
{
    public DoctorTypeController(IGenericRepositoryInterface<DoctorType> doctorTypeRespository) : base(doctorTypeRespository)
    {

    }
}
