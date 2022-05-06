using Proyecto_DesarrolloWeb_API.Dominio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_DesarrolloWeb_API.Dominio.Core.Interfaces
{
    public interface IPetAdoptionRepository
    {
        Task<bool> RegisterPetAdoption(PetAdoption petAdoption);
    }
}
