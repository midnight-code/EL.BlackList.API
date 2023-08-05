using EL.BlackList.API.Data;
using EL.BlackList.API.Models;
using EL.BlackList.API.Repositore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace EL.BlackList.API.Repositore.Repositore
{
    public class DocumentsRepositore : IDocumentRepositore
    {
        private readonly DataContext _dataContext;
        public DocumentsRepositore(DataContext dataContext) => _dataContext = dataContext;

        public async Task<bool> Delete(int id)
        {
            if (id > 0)
            {
                var result = _dataContext.Documents.FirstOrDefaultAsync(d => d.ID == id);
                if(result is not null)
                {
                    _dataContext.Remove(result);
                    await _dataContext.SaveChangesAsync();
                    return await Task.Run(() => true);
                }
            }
            return await Task.Run(() => false);
        }

        public async Task<Documents?> GetById(int id)
        {
            if (id > 0)
                return await _dataContext.Documents.FirstOrDefaultAsync(i => i.ID == id);
            return null;
        }

        public async Task<Documents?> GetDocumentByDriverIDAsync(int driverID, string imgType)
        {
            if (driverID > 0)
            {
                var uuu= await _dataContext.Documents.Where(id => (id.DriverID == driverID && id.ImgType == imgType)).FirstOrDefaultAsync();
                return await _dataContext.Documents.Where(id => (id.DriverID == driverID && id.ImgType == imgType)).FirstOrDefaultAsync();
            }
            return null;
        }

        public async Task<IEnumerable<Documents>?> GetListDocumentByDriverIDAsync(int driverID, string imgType)
        {
            if (driverID > 0)
            {
                return await Task.Run(() => _dataContext.Documents.Where(id => (id.DriverID == driverID && id.ImgType == imgType)));
            }
            return await Task.Run(() =>
            {
                List<Documents> enumerable = new();
                return enumerable;
            });
        }

        public async Task<int> Save(Documents intite)
        {
            if (intite is not null)
            {
                if (intite.ID > 0)
                    _dataContext.Documents.Update(intite);
                else
                  await  _dataContext.Documents.AddAsync(intite);

                await _dataContext.SaveChangesAsync();
                return await Task.Run(() => intite.ID);
            }
            return await Task.Run(() => 0);

        }

        public Task<IEnumerable<Documents>> Select()
        {
            throw new NotImplementedException();
        }
    }
}
