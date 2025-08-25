using BaseLibrary.Entities;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositores.Contracts;

namespace Server.Controllers;

public class OverTimeController : GenericController<OverTime>
{
    public OverTimeController(IGenericRepositoryInterface<OverTime> overTimeRepository) : base(overTimeRepository) { }
}
