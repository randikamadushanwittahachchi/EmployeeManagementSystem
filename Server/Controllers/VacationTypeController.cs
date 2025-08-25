using BaseLibrary.Entities;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositores.Contracts;

namespace Server.Controllers;

public class VacationTypeController : GenericController<VacationType>
{
    public VacationTypeController(IGenericRepositoryInterface<VacationType> vacationTypeRepository) : base(vacationTypeRepository) { }
}
