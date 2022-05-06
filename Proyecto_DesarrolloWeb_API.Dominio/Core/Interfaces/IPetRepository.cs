using Proyecto_DesarrolloWeb_API.Dominio.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto_DesarrolloWeb_API.Dominio.Core.Interfaces
{
    public interface IPetRepository
    {
        Task<bool> Delete(int id);
        Task<IEnumerable<Pet>> GetPets();
        Task<IEnumerable<Pet>> GetUserPets(int userId);
        Task<IEnumerable<PetType>> GetPetsType();
        Task<IEnumerable<PetAge>> GetPetsAge();
        Task<IEnumerable<PetBreed>> GetPetsBreed();
        Task<IEnumerable<PetImage>> GetPetsImage();
        Task<IEnumerable<PetSize>> GetPetsSize();
        Task<Pet> GetPetById(int id);
        Task<bool> Insert(Pet pet);
        Task<bool> Update(Pet pet);
    }
}