
using Dominio.Ports.Primary;
using Dominio.Ports.Secundary;
using Dominio.Services;
using JsonRepository;
using Microsoft.Extensions.DependencyInjection;

string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"product.json");



var services = new ServiceCollection();
services.AddTransient<IRepository>(provider => new ProductRepository(path));

services.AddTransient<IService,ProductService>();

var serviceProvider =services.BuildServiceProvider();
var productService = serviceProvider.GetRequiredService<IService>();


