using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IPropertyForSaleRepository
    {
        List<PropertyForSaleDTO> GetAll();
        List<PropertyForSaleDTO> GetPropertiesByEmail(string email);
        PropertyForSaleDTO GetById(int id);
        void Add(PropertyForSaleDTO propertyForSale);
        void Update(PropertyForSaleDTO propertyForSale);
        void Delete(int propertyForSale);
        void Save();
    }
}
