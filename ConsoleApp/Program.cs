
using Dominio.Ports.Primary;
using Dominio.Ports.Secundary;
using Dominio.Services;
using JsonRepository;
using Microsoft.Extensions.DependencyInjection;

string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "product.json");



var services = new ServiceCollection();
services.AddTransient<IRepository>(provider => new ProductRepository(path));

services.AddTransient<IService, ProductService>();

var serviceProvider = services.BuildServiceProvider();
var productService = serviceProvider.GetRequiredService<IService>();

while (true)
{
    try
    {
        Console.WriteLine("\nSeleccione una opcion");
        Console.WriteLine("1 - Agregar un producto");
        Console.WriteLine("2 - Mostrar productos almacenados");
        Console.WriteLine("3 - Salir");
        Console.Write("Opcion: ");

        string option = Console.ReadLine();
        switch (option)
        {
            case "1":
                Console.Write("Nombre del producto: ");
                string name = Console.ReadLine();
                Console.Write("Precio del producto: ");
                decimal price = decimal.Parse(Console.ReadLine() ?? "0");
                productService.Register(name, price);
                break;
            case "2":
                Console.WriteLine("\nProductos almacenados:");
                foreach (var product in productService.GetAll())
                {
                    Console.WriteLine($"- {product.Name}: {product.Price} euros");
                }
                break;
            case "3":
                Console.WriteLine("Saliendo del sistema...");
                return;
            default:
                Console.WriteLine("Opcion no valida");
                break;
        }

    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}


