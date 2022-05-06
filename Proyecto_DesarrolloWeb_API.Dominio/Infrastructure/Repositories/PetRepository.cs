using Microsoft.EntityFrameworkCore;
using Proyecto_DesarrolloWeb_API.Dominio.Core.Entities;
using Proyecto_DesarrolloWeb_API.Dominio.Core.Interfaces;
using Proyecto_DesarrolloWeb_API.Dominio.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_DesarrolloWeb_API.Dominio.Infrastructure.Repositories
{
    public class PetRepository : IPetRepository
    {
        private readonly PetRescueDBContext _context;

        public PetRepository(PetRescueDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Pet>> GetPets()
        {
            return await _context.Pet.ToListAsync();
        }
        public async Task<IEnumerable<Pet>> GetUserPets(int userId)
        {
            return await _context.Pet.Where(x => x.IdUser == userId).ToListAsync();
        }
        public async Task<IEnumerable<PetType>> GetPetsType()
        {
            return await _context.PetType.ToListAsync();
        }

        public async Task<IEnumerable<PetAge>> GetPetsAge()
        {
            return await _context.PetAge.ToListAsync();
        }
        public async Task<IEnumerable<PetBreed>> GetPetsBreed()
        {
            return await _context.PetBreed.ToListAsync();
        }
        public async Task<IEnumerable<PetImage>> GetPetsImage()
        {
            return await _context.PetImage.ToListAsync();
        }
        public async Task<IEnumerable<PetSize>> GetPetsSize()
        {
            return await _context.PetSize.ToListAsync();
        }



        public async Task<Pet> GetPetById(int id)
        {
            return await _context.Pet.FindAsync(id);
        }
        public async Task<bool> Insert(Pet pet)
        {
            try
            {
                pet.RegistrationDate = DateTime.Now;
                _context.Pet.Add(pet);
                int countRows = await _context.SaveChangesAsync();
                return (countRows > 0);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException.Message);
                return false;
            }
        }
        public async Task<bool> Update(Pet pet)
        {
            var petNow = await _context.Pet.FindAsync(pet.Id);
            petNow.Name = pet.Name;
            //petNow. = pet.LastName;
            //petNow.Country = pet.Country;
            //petNow.City = pet.City;
            //petNow.Phone = pet.Phone;

            int countRows = await _context.SaveChangesAsync();
            return (countRows > 0);
        }
        public async Task<bool> Delete(int id)
        {
            var pet = await _context.Pet.FindAsync(id);
            int countRows = 0;//await _context.Customer.Remove();
            return (countRows > 0);

        }
    }
}
