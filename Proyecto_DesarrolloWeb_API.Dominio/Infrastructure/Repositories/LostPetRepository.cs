using Microsoft.EntityFrameworkCore;
using Proyecto_DesarrolloWeb_API.Dominio.Core.DTOs;
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
    public class LostPetRepository:ILostPetRepository
    {
        private readonly PetRescueDBContext _context;

        public LostPetRepository(PetRescueDBContext context)
        {
            _context = context;
        }

        public async Task<bool> Insert(LostPet lostPet)
        {
            try
            {
                var lostPetNow = await _context.LostPet.Where(x => (x.IdPet == lostPet.IdPet && x.Status == "Lost")).FirstOrDefaultAsync();
                int countRows;
                if (lostPetNow == null)
                {
                    lostPet.Status = "Lost";
                    lostPet.RegistrationDate = DateTime.Now;
                    _context.LostPet.Add(lostPet);
                    countRows = await _context.SaveChangesAsync();

                }
                else
                {
                    //mascota ya esta registrada como perdida
                    countRows = 0;
                }
                
                return (countRows > 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException.Message);
                return false;
            }
        }

        public async Task<IEnumerable<LostPetJoinDTO>> GetLostPets(int userId)
        {

            var result = await _context.LostPet.Where(x => (x.IdUser == userId && x.Status == "Lost"))
            .Select(p => new LostPetJoinDTO
            {
                IdPet = p.IdPet,
                IdUser= p.IdUser,
                Name = _context.Pet.Where(x => x.Id == p.IdPet).Select(z => z.Name).First(),
                DateOfLoss = p.DateOfLoss,
                DescriptionOfLoss = p.DescriptionOfLoss
            }).ToListAsync();

            return result;




            //return await _context.LostPet.Where(x => (x.IdUser == userId && x.Status == "Lost")).ToListAsync();
        }

        //register pet found
        public async Task<bool> RegisterPetFound(LostPet lostPet)
        {
            try
            {

                var lostPetNow = await _context.LostPet.Where(x => x.IdPet == lostPet.IdPet && x.Status == "Lost").FirstOrDefaultAsync();
                lostPetNow.Status = "Found";
                lostPetNow.DateOfFound = lostPet.DateOfFound;
                lostPetNow.DescriptionOfFound = lostPet.DescriptionOfFound;

                int countRows = await _context.SaveChangesAsync();
                return (true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException.Message);
                return false;
            }
        }

    }
}
