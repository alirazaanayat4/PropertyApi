using Repository.EFModels;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{
    public class PropertyForSaleRepository : IPropertyForSaleRepository
    {
        private readonly PropertyDatabaseContext _context;
        public PropertyForSaleRepository()
        {
            _context = new PropertyDatabaseContext();
        }
        public void Add(PropertyForSaleDTO propertyForSale)
        {
            _context.PropertyForSales.Add(propertyForSale.ToEntity());
        }

        public void Delete(int id)
        {
            var entity = _context.PropertyForSales.FirstOrDefault(e => e.Id == id);
            if (entity != null)
            {
                _context.PropertyForSales.Remove(entity);
            }
        }

        public List<PropertyForSaleDTO> GetAll()
        {
            List<PropertyForSaleDTO> propertyForSales = new List<PropertyForSaleDTO>();

            var properties = _context.PropertyForSales.ToList();
            properties.ForEach(x => propertyForSales.Add(x.ToDto()));

            return propertyForSales;
        }
        public List<PropertyForSaleDTO> GetPropertiesByEmail(string email)
        {
            var properties = _context.PropertyForSales.Where(e => e.OwnerEmail == email).ToList();

            List<PropertyForSaleDTO> propertyForSales = new List<PropertyForSaleDTO>();
            properties.ForEach(x => propertyForSales.Add(x.ToDto()));

            return propertyForSales;
        }

        public PropertyForSaleDTO GetById(int id)
        {
            var entity = _context.PropertyForSales.FirstOrDefault(e => e.Id == id);
            if (entity != null)
            {
                return entity.ToDto();
            }
            return null;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(PropertyForSaleDTO propertyForSale)
        {
            _context.PropertyForSales.Update(propertyForSale.ToEntity());
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
