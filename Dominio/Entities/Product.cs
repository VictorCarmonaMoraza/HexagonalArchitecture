// Namespace donde vive la clase.
// En este caso, la entidad pertenece a la capa de Dominio (Domain Layer),
// dentro de la carpeta 'Entities', que contiene las entidades de negocio puras.
namespace Dominio.Entities
{
    // Clase que representa un producto en el dominio de la aplicación.
    // Es una entidad porque tiene una identidad única (Guid Id)
    // y su valor puede cambiar a lo largo del tiempo.
    public class Product
    {
        // Campos privados de respaldo para las propiedades públicas Name y Price.
        // Se usan para poder aplicar lógica de validación dentro de los setters.
        private string _name { get; set; }
        private decimal _price { get; set; }

        // Propiedad pública que identifica de forma única cada producto.
        // En dominios reales, se suele generar automáticamente o venir del repositorio.
        public Guid Id { get; set; }

        // Propiedad que representa el nombre del producto.
        // Tiene lógica de validación en el setter para asegurar que no sea nulo o vacío.
        public string Name
        {
            get => _name;  // Devuelve el valor actual del campo privado _name.
            set
            {
                // Validación: el valor no puede ser nulo ni vacío.
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("El nombre no puede ir vacío");
                }
                _name = value;
            }
        }

        // Propiedad que representa el precio del producto.
        // También tiene validación para impedir valores negativos.
        public decimal Price
        {
            get => _price; // Devuelve el valor actual del campo privado _price.
            set
            {
                // Validación: el precio no puede ser negativo.
                if (value < 0)
                {
                    throw new ArgumentException("El precio debe ser mayor que cero");
                }
                _price = value;
            }
        }

        // Constructor de la clase Product.
        // Recibe los valores iniciales para Id, Name y Price.
        public Product(Guid id, string name, decimal price)
        {
            // CORRECCIÓN: aquí se debe usar 'Id = id;', no 'id = id;'.
            // La asignación actual no tiene efecto si se usa mal el nombre de la propiedad.
            Id = id;

            // Al asignar Name y Price, se ejecutan las validaciones de los setters.
            Name = name;
            Price = price;
        }
    }
}
