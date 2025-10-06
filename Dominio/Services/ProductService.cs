using Dominio.Entities;
using Dominio.Ports.Primary;
using Dominio.Ports.Secundary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Services
{
    public class ProductService :IService
    {
        private readonly IRepository _repository;

        public ProductService(IRepository repository)
        {
            _repository = repository;
        }

        public void Register(string name, decimal price)
        {
            var product = new Entities.Product(Guid.NewGuid(), name, price);
            _repository.Save(product);
        }

        public IEnumerable<Product> GetAll()
        {
            return _repository.GetAll();
        }
    }
}
