using System;
using System.Threading.Tasks;
using PortalApp.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace PortalApp.API.Data
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly DataContext _context;
        public DocumentRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<DocumentConfigVs> GetDocumentConfig(string documentType)
        {
            var config = await _context.DocumentConfigs.FirstOrDefaultAsync(x=>x.DocumentType == documentType);
            IAsyncEnumerable<DocumentConfigVs> theLatestVersionOfDocumentConfig = _context.DocumentConfigVs.OrderByDescending(x=>x.Created).FirstOrDefaultAsync().ToAsyncEnumerable();
            var docV = await theLatestVersionOfDocumentConfig.SingleOrDefault();
            return docV;
        }

        public async Task SaveDocumentConfig(DocumentConfig conf)
        {
            var config = await _context.DocumentConfigs.SingleOrDefaultAsync(x=>x.DocumentType == conf.DocumentType);
            if(config != null)
            {
                _context.DocumentConfigs.Remove(config);
            }

            if(conf.Id == Guid.Empty)
            {
                conf.Id = Guid.NewGuid();    
            }

            _context.DocumentConfigs.Add(conf);
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception ex){
                throw;
            }
        }

        public async Task<List<DocumentConfig>> GetDocumentConfigs(){
            return await _context.DocumentConfigs.ToListAsync();
        } 
    }
}