using System.Collections.Generic;
using System.Threading.Tasks;
using PortalApp.API.Models;

namespace PortalApp.API.Data
{
    public interface IDocumentRepository
    {
        Task<DocumentConfigVs> GetDocumentConfig(string documentName);

        Task SaveDocumentConfig(DocumentConfig conf);

        Task<List<DocumentConfig>> GetDocumentConfigs();


    }
}