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
    public class PetAdoptionRepository : IPetAdoptionRepository
    {
        private readonly PetRescueDBContext _context;

        public PetAdoptionRepository(PetRescueDBContext context)
        {
            _context = context;
        }

        public async Task<bool> RegisterPetAdoption(PetAdoption petAdoption)
        {
            try
            {
                var petNow = await _context.PetAdoption.Where(x => (x.IdPet == petAdoption.IdPet && x.Status == "Registered")).FirstOrDefaultAsync();
                int countRows;
                if (petNow == null)
                {
                    petAdoption.Status = "Registered";
                    petAdoption.RegistrationDate = DateTime.Now;
                    _context.PetAdoption.Add(petAdoption);
                    countRows = await _context.SaveChangesAsync();

                }
                else
                {
                    //mascota ya esta registrada para adopcion
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
    }
}
