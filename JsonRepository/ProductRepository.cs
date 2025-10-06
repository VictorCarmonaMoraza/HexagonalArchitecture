using Dominio.Entities;
using Dominio.Ports.Secundary;
using System.Text.Json;

namespace JsonRepository
{
    public class ProductRepository : IRepository
    {
        private readonly string _path;
        public ProductRepository(string path)
        {
            _path = path;
        }

        public void Save(Product product)
        {
            //Obtenemos todos los productos
            var products = GetAll();

            //Añadimos el producto nuevo
            products.Add(product);

            //WriteIndented = true
            //Significa que cuando serialices(conviertas un objeto a JSON),
            //el resultado se escribirá con sangrías y saltos de línea — es decir, “bonito” o pretty printed
            var options = new JsonSerializerOptions { WriteIndented = true };

            //Serializamos la lista de productos a JSON
            string jsonString = JsonSerializer.Serialize(products, options);

            //Escribimos el JSON en el fichero
            File.WriteAllText(_path, jsonString);
        }

        public List<Product> GetAll()
        {
            //Si el fihceor no existe, devolvemos una lista vacía
            if (!File.Exists(_path))
            {
                return new List<Product>();
            }
            //Leemos el fichero
            string jsonString = File.ReadAllText(_path);
            //Deserailizamos el JSON a una lista de productos
            var json = JsonSerializer.Deserialize<List<Product>>(jsonString);

            //Retornar los productos obtenidoso una lista vacía si es nulo
            return json ?? new List<Product>();
        }
    }
}
