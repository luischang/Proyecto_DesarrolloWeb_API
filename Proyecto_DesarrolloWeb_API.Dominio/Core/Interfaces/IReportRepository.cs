using Proyecto_DesarrolloWeb_API.Dominio.Core.DTOs;
using Proyecto_DesarrolloWeb_API.Dominio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_DesarrolloWeb_API.Dominio.Core.Interfaces
{
    public interface IReportRepository
    {
        Task<IEnumerable<ReportUserDTO>> GetUserReport();
        Task<IEnumerable<ReportUserDTO>> GetUserReportDate(DateTime startDate, DateTime endDate);
        Task<IEnumerable<ReportUserDTO>> GetUserReportName(string text);
        Task<IEnumerable<ReportPetDTO>> GetPetReport(DateTime startDate, DateTime endDate,
           string owner, string petName, string status, int idPetType, int idPetAge, bool images);
    }
}
