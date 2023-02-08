using BotComers.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
namespace BotComers.Controllers
{
    public class BotComersController : Controller
    {
        // GET: BotComers
        public ActionResult Index()
        {

            var listarprodu = new List<ProductoNuevoViewModel>();
            listarprodu.Add(new ProductoNuevoViewModel()
            {
                //desc = "",

            });
            ProductoNuevoViewModel model = new ProductoNuevoViewModel()
            {
            };
            model.ListaProductoNuevo = new List<ProductoNuevoViewModel>() { };
            for (int i = 0; i < 7; i++)
            {
                model.ListaProductoNuevo.Add(new ProductoNuevoViewModel()
                {
                    ProductoID = i * 2,


                    NombreProducto = "Televisor " + i.ToString() + " LG Ultima Generacion",
                    Descripcion = "Negro mediano camara 14px",
                    PrecioActual = 75 * new Random().Next(1, 15),
                    PrecioOriginal = 75 * new Random().Next(1, 15),
                    Title = "Nuevo",
                    UrlImage = "https://falabella.scene7.com/is/image/Falabella/7335858_1?wid=1500&hei=1500&qlt=70",

                });
            }
            var BannerLista = new List<BannerProductoViewModel>();
            BannerLista.Add(new BannerProductoViewModel()
            {
                //descripcion_banner = "",
            });
            BannerProductoViewModel modelo = new BannerProductoViewModel()
            {
            };
            model.ListaBanner = new List<BannerProductoViewModel>() { };

            model.ListaBanner.Add(new BannerProductoViewModel()
            {
                Descripcion = "Cros Games",
                Precio = Convert.ToDecimal("99.90"),
                //nombre_producbanner="Televisor Pantalla Plana de 50 Pulgadas",
                UrlImage = "https://d13xymm0hzzbsd.cloudfront.net/1/20200123/15798220240922.png",

            });

            var ListaProductoCat = new List<CaracteristicaProductoViewModel>();

            ListaProductoCat.Add(new CaracteristicaProductoViewModel()
            {
                //desc = "",

            });

            var ListaProductoCa = new List<CaracteristicaProductoViewModel>();
            ListaProductoCa.Add(new CaracteristicaProductoViewModel()
            {
                //desc = "",

            });
            CaracteristicaProductoViewModel modela = new CaracteristicaProductoViewModel()
            {
            };
            modela.ListaCaracteristica = new List<CaracteristicaProductoViewModel> { };


            modela.ListaCaracteristica.Add(new CaracteristicaProductoViewModel()
            {
                NombreProducto = "Televisor LG Ultima Generacion",
                Descripcion = "Negro mediano camara 14px",
                PrecioActual = Convert.ToDecimal("1900.990"),
                PrecioOriginal = Convert.ToDecimal("2300.90"),
                Title = "Nuevo",
                UrlImage = "https://falabella.scene7.com/is/image/Falabella/8118653?wid=249&hei=249&qlt=70",

            });
            return View(model);
        }
        //Controlador Json  27/01/2020  16:00
        public ActionResult ValidarStock(ProductoNuevoViewModel request)
        {
            ProductoNuevoViewModel respuesta = new ProductoNuevoViewModel();
            respuesta.ProductoID = 1;
            respuesta.NombreProducto = "Televisor LG Ultima Generacion";
            respuesta.Descripcion = "Negro mediano camara 14px";
            respuesta.PrecioActual = Convert.ToDecimal("1900.990");
            respuesta.PrecioOriginal = Convert.ToDecimal("2300.90");
            respuesta.Title = "Nuevo";
            respuesta.UrlImage = "https://falabella.scene7.com/is/image/Falabella/8118653?wid=249&hei=249&qlt=70";

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        //Controlador catalogo producto     de mvc 25/01/2020 16:
        public ActionResult CatalogoProducto()
        {
            {
                CategoriaProductoViewModel categoria = new CategoriaProductoViewModel()
                {
                    ListaProducto = new List<CatalogoProductoViewModel>()

                };
                for (int i = 0; i < 5; i++)
                {
                    categoria.ListaProducto.Add(new CatalogoProductoViewModel()
                    {
                        Title = "Nuevo",

                        NombreProducto = "LG  Código: 7370231LED Nanoell 55 55SM8600 4K Ultra HD Smart TV",
                        PrecioActual = Convert.ToDecimal("1500.90"),
                        PrecioOriginal = Convert.ToDecimal("1600.90"),
                        Descripcion = "Peso: 18,7 kg Modelo(Internet): LED LG 55SM8600 Resolución: 4K Super Ultra HD",
                        UrlImage = "https://falabella.scene7.com/is/image/Falabella/7370231_1?wid=1500&hei=1500&qlt=70",

                        Imagen = new List<CatalogoProductoViewModel.Images>()
                        {
                     new CatalogoProductoViewModel.Images()
                     {
                         Descripcion ="imagen1"
                     }
                 }
                    });
                }
                List<CategoriaProductoViewModel> modelList = new List<CategoriaProductoViewModel>();
                modelList.Add(categoria);
                return View("CatalogoProducto", modelList);
            }
        }
        public ActionResult DetalleProducto()
        {
            CatalogoProductoViewModel DetalleCatalogo = new CatalogoProductoViewModel()
            {
                Imagen = new List<CatalogoProductoViewModel.Images>()

            };
            DetalleCatalogo.Imagen.Add(new CatalogoProductoViewModel.Images()
            {

                UrlImage = "https://falabella.scene7.com/is/image/Falabella/7370231_1?wid=1500&hei=1500&qlt=70",
            });
            DetalleCatalogo.Imagen.Add(new CatalogoProductoViewModel.Images()
            {

                UrlImage = "https://falabella.scene7.com/is/image/Falabella/7370231_1?wid=1500&hei=1500&qlt=70",
            });
            DetalleCatalogo.Imagen.Add(new CatalogoProductoViewModel.Images()
            {

                UrlImage = "https://falabella.scene7.com/is/image/Falabella/7370231_1?wid=1500&hei=1500&qlt=70",
            });
            DetalleCatalogo.Imagen.Add(new CatalogoProductoViewModel.Images()
            {

                UrlImage = "https://falabella.scene7.com/is/image/Falabella/7370231_1?wid=1500&hei=1500&qlt=70",
            });
            return View(DetalleCatalogo);
        }
        public ActionResult DetalleCarrito()
        {
            var detallecar = new List<DetalleCarritoViewModel>();
            detallecar = new List<DetalleCarritoViewModel>();

            detallecar.Add(new DetalleCarritoViewModel()
            {
            });
            DetalleCarritoViewModel model = new DetalleCarritoViewModel()
            {

            };
            model.ListaDetalleCarrito = new List<DetalleCarritoViewModel>() { };
            model.ListaDetalleCarrito.Add(new DetalleCarritoViewModel()

            {
                DetalleID = 1,
                Cantidad = "2",
                NombreProducto = "Gamer Reloj 2019",
                PrecioUnitario = Convert.ToInt32("99"),
                Total = Convert.ToInt32("99"),
                UrlImagen = "https://falabella.scene7.com/is/image/Falabella/7370231_1?wid=1500&hei=1500&qlt=70",

            });

            return View(model);
        }
        public ActionResult Procesopago()

        {
            return View();

        }

        public ActionResult Resumen()

        {
            return View();

        }
        public ActionResult Usuario()

        {

            var ListaUsuarios = new List<UsuariosViewModel>();
            ListaUsuarios.Add(new UsuariosViewModel()
            {
                //desc = "",

            });
            UsuariosViewModel model = new UsuariosViewModel()
            {
            };
            model.ListaUsuarios = new List<UsuariosViewModel>() { };

            model.ListaUsuarios.Add(new UsuariosViewModel()
            {
                UsuarioID = 1,
                Nombre = "",
                Apellidos = "",
                Ciudad = "",
                CodigoPostal = "",
                Direccion = "",
                Email = "",
                Telefono = "",
                Contraseña = "",
                ConfimarContraseña = "",
            });
            return View(model);

        }
        public ActionResult RecuperarUsuario()

        {

            return View();

        }
        public ActionResult pruebas()

        {
            return View();

        }
    }
}