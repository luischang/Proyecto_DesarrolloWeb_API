using Proyecto_DesarrolloWeb_API.Dominio.Core.DTOs;
using Proyecto_DesarrolloWeb_API.Dominio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_DesarrolloWeb_API.Dominio.Core.Interfaces
{
    public interface ILostPetRepository
    {
        Task<bool> Insert(LostPet lostPet);
        Task<IEnumerable<LostPetJoinDTO>> GetLostPets(int userId);
        Task<bool> RegisterPetFound(LostPet lostPet);
    }
}
