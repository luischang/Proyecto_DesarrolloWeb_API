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
    public class ReportRepository : IReportRepository
    {
        private readonly PetRescueDBContext _context;

        public ReportRepository(PetRescueDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ReportUserDTO>> GetUserReport()
        {
            var result = await _context.User
            .Select(p => new ReportUserDTO
            {
                Id = p.Id,
                Name = p.Name,
                LastName = p.LastName,
                RegistrationDate = p.RegistrationDate,
                petsCount = _context.Pet.Where(x => x.IdUser == p.Id).Count()
            }).ToListAsync();

            return result;
        }

        //Obtener usuarios en un rango de fechas
        public async Task<IEnumerable<ReportUserDTO>> GetUserReportDate(DateTime startDate, DateTime endDate)
        {
            //30/03/2021   30/08/2021 ......  30/09/2021
            var result = await _context.User
            .Where(x => (startDate <= x.RegistrationDate && endDate >= x.RegistrationDate))
            .Select(p => new ReportUserDTO
            {
                Id = p.Id,
                Name = p.Name,
                LastName = p.LastName,
                RegistrationDate = p.RegistrationDate,
                petsCount = _context.Pet.Where(x => x.IdUser == p.Id).Count()
            }).ToListAsync();

            return result;
        }

        //Obtener usuarios a partir del nombre
        public async Task<IEnumerable<ReportUserDTO>> GetUserReportName(string text)
        {
            var result = await _context.User
            .Where(x => (x.Name.Contains(text) || x.LastName.Contains(text)))
            .Select(p => new ReportUserDTO
            {
                Id = p.Id,
                Name = p.Name,
                LastName = p.LastName,
                RegistrationDate = p.RegistrationDate,
                petsCount = _context.Pet.Where(x => x.IdUser == p.Id).Count()
            }).ToListAsync();

            return result;
        }

        public async Task<IEnumerable<ReportPetDTO>> GetPetReport(DateTime startDate, DateTime endDate,
            /*DateTime startDate, DateTime endDate,*/ string owner, string petName, string status, int idPetType, int idPetAge, bool images)
        {
            //var query = from a in ctx.Articulos
            //            join s in ctx.Saldos on a.id equals s.id
            //            select new ArticuloSaldo
            //            {
            //                Id = a.id,
            //                NombreProducto = a.nombreProducto,
            //                Precio = a.Precio,
            //                Cantidad = s.Cantidad
            //            };
            //return query.ToList();

            //ejemplo de left outer join
            //var query = from person in people
            //            where person.ID == 4
            //            join pet in pets on person equals pet.Owner into personpets
            //            from petOrNull in personpets.DefaultIfEmpty()
            //            select new { Person = person, Pet = petOrNull };

            //otro ejemplo
            //from d in context.dc_tpatient_bookingd
            //join bookingm in context.dc_tpatient_bookingm
            //     on d.bookingid equals bookingm.bookingid into bookingmGroup
            //from m in bookingmGroup.DefaultIfEmpty()
            //join patient in dc_tpatient
            //     on m.prid equals patient.prid into patientGroup
            //from p in patientGroup.DefaultIfEmpty()

            var query = from pet in _context.Pet
                        join type in _context.PetType on pet.IdPetType equals type.Id
                        join user in _context.User on pet.IdUser equals user.Id
                        join age in _context.PetAge on pet.IdPetAge equals age.Id
                        join lost in _context.LostPet on pet.Id equals lost.IdPet into petLost
                        from lost in petLost.DefaultIfEmpty()
                        join adoption in _context.PetAdoption on pet.Id equals adoption.IdPet into petAdoption
                        from adoption in petAdoption.DefaultIfEmpty()
                        
                        where (
                        (user.Name.Contains(owner==null?"":owner) || user.LastName.Contains(owner == null ? "" : owner)) &&
                        (pet.Name.Contains(petName==null?"":petName)) &&
                        (pet.IdPetType.ToString().Contains(idPetType.ToString()=="0"?"":idPetType.ToString())) &&
                        (pet.IdPetAge.ToString().Contains(idPetAge.ToString()=="0"?"":idPetAge.ToString())) &&
                        (lost.Status.Contains(status == null ? "" : status) || adoption.Status.Contains(status == null ? "" : status)) &&
                        (startDate <= pet.RegistrationDate && endDate >= pet.RegistrationDate)
                        )
                        select new ReportPetDTO
                        {
                            IdPet = pet.Id,
                            Name = pet.Name,
                            Owner = user.Name + " "+ user.LastName,
                            RegistrationDate = pet.RegistrationDate,
                            Type = type.Name,
                            Age = age.AgeRange,
                            AdoptionStatus = adoption.Status==null?"No registra": adoption.Status,
                            LostStatus = lost.Status == null ? "No registra" : lost.Status,
                            Photo = images?_context.PetImage.Where(x => x.IdPet == pet.Id).Select(x=>x.Photo).ToList():null

                        };


            return await query.ToListAsync();

            //var result = await _context.Pet
            //.Where(x => 
            //(startDate <= x.RegistrationDate && endDate >= x.RegistrationDate)||
            //(x.Name.Contains(owner))
            //)
            //.Select(p => new ReportUserDTO
            //{
            //    Id = p.Id,
            //    Name = p.Name,
            //    LastName = p.LastName,
            //    RegistrationDate = p.RegistrationDate,
            //    petsCount = _context.Pet.Where(x => x.IdUser == p.Id).Count()
            //}).ToListAsync();

            //return result;
        }

    }
}
