using BaseLibrary.Entities;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositores.Contracts;

namespace Server.Controllers
{
    public class OverTimeTypeController : GenericController<OverTimeType>
    {
        public OverTimeTypeController(IGenericRepositoryInterface<OverTimeType> overTimeRepository) : base(overTimeRepository) { }
    }
}
