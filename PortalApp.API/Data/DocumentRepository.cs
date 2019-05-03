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
        public async Task<DocumentConfig> GetDocumentConfig(string documentType)
        {
            return await _context.DocumentConfigs
            .Include(x => x.DocumentConfigVs)
            .FirstOrDefaultAsync(d => d.DocumentType == documentType);
        }

        public async Task<DocumentConfig> GetDocumentConfigById(Guid id)
        {
            return await _context.DocumentConfigs
            .Include(x => x.DocumentConfigVs)
            .FirstOrDefaultAsync(d => d.Id == id);
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
                throw ex;
            }
        }

        public async Task<List<DocumentConfig>> GetDocumentConfigs(){
            
            var conf =  await _context.DocumentConfigs.Include(x=>x.DocumentConfigVs).ToListAsync();
            return conf;
        } 
        public async Task<List<DocumentConfigVs>> GetDocumentConfigVs(){
            
            return await _context.DocumentConfigVs.ToListAsync();
        }

        public async Task<DocumentConfigVs> GetDocumentConfigVs(string docType)
        {
            var conf = await _context.DocumentConfigs.Where(x=>x.DocumentType==docType).SingleOrDefaultAsync();
            var confV = await _context.DocumentConfigVs.Where(x=>x.DocumentConfigId == conf.Id).OrderByDescending(x=>x.Created).FirstOrDefaultAsync();
            return confV;
        }
        public async Task<DocumentConfigVs> GetDocumentConfigVsById(Guid id)
        {
            var confV = await _context.DocumentConfigVs.FirstOrDefaultAsync(x=>x.Id == id);
            return confV;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
}
}