using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using E_DataModel;
using E_DataModel.Corporativo;
using E_DataModel.Common;

namespace E_DataLayer.Corporativo
{
    public class EmpresaData
    {

        public List<EmpresaDTO> appsfit_uspAspNetUsersCentroFit_Listar(EmpresaDTO oFiltro)
        {
            List<EmpresaDTO> lista = new List<EmpresaDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("appsfit_uspAspNetUsersCentroFit_Listar", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;                    
                    cmd.Parameters.Add(new SqlParameter("@DefaultKeyUser", System.Data.SqlDbType.VarChar,50)).Value = oFiltro.DefaultKeyUser;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new EmpresaDTO()
                                {       
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    DefaultKeyEmpresa = oIDataReader[oIDataReader.GetOrdinal("DefaultKeyEmpresa")].ToString(),
                                    IdUser = oIDataReader[oIDataReader.GetOrdinal("IdUser")].ToString(),
                                    NombreComercialEmpresa = oIDataReader[oIDataReader.GetOrdinal("NombreComercial")].ToString(),
                                    LogoTipo = oIDataReader[oIDataReader.GetOrdinal("Logo")].ToString(),
                                    Ubigeo = oIDataReader[oIDataReader.GetOrdinal("Ubigeo")].ToString(),
                                    TiendaAplicacion = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("TiendaAplicacion")]),
                                    AplicacionDisponible = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("AplicacionDisponible")]),
                                    RutinasAplicacion = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("RutinasAplicacion")]),
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public List<EmpresaDTO> ecommerce_uspListarEmpresas_Paginacion(Paging paging)
        {
            List<EmpresaDTO> lista = new List<EmpresaDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspListarEmpresas_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    //cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoUnidadNegocio;
                    //cmd.Parameters.Add(new SqlParameter("@AsesorVenta", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.Vendedor;
                    //cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oClientesDTO.CodigoSede;
                    //cmd.Parameters.Add(new SqlParameter("@NombreCliente", System.Data.SqlDbType.VarChar)).Value = oClientesDTO.NombreCompleto;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new EmpresaDTO()
                                {                                   
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    NombreDuenio = oIDataReader[oIDataReader.GetOrdinal("NombreDuenio")].ToString(),
                                    ApellidosDuenio = oIDataReader[oIDataReader.GetOrdinal("ApellidosDuenio")].ToString(),
                                    CorreoDuenio = oIDataReader[oIDataReader.GetOrdinal("CorreoDuenio")].ToString(),
                                    CodigoPais = oIDataReader[oIDataReader.GetOrdinal("CodigoPais")].ToString(),
                                    CelularDuenio = oIDataReader[oIDataReader.GetOrdinal("CelularDuenio")].ToString(),
                                    TipoDocumentoEmpresa = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoDocumentoEmpresa")]),
                                    DesTipoDocumentoEmpresa = oIDataReader[oIDataReader.GetOrdinal("DesTipoDocumentoEmpresa")].ToString(),
                                    NroDocumentoEmpresa = oIDataReader[oIDataReader.GetOrdinal("NroDocumentoEmpresa")].ToString(),
                                    RazonSocialEmpresa = oIDataReader[oIDataReader.GetOrdinal("RazonSocialEmpresa")].ToString(),
                                    NombreComercialEmpresa = oIDataReader[oIDataReader.GetOrdinal("NombreComercialEmpresa")].ToString(),
                                    TelefonoEmpresa = oIDataReader[oIDataReader.GetOrdinal("TelefonoEmpresa")].ToString(),
                                    FechaAniversarioEmpresa = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaAniversarioEmpresa")]),
                                    CorreoEmpresa = oIDataReader[oIDataReader.GetOrdinal("CorreoEmpresa")].ToString(),
                                    SubDominio = oIDataReader[oIDataReader.GetOrdinal("SubDominio")].ToString(),                                 
                                    DesEstado = oIDataReader[oIDataReader.GetOrdinal("DesEstado")].ToString(),
                                    LogoTipo = oIDataReader[oIDataReader.GetOrdinal("LogoTipo")].ToString(),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")])

                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public EmpresaDTO ecommerce_uspBuscarEmpresas(EmpresaDTO oFiltro)
        {
            EmpresaDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspBuscarEmpresas", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new EmpresaDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    NombreDuenio = oIDataReader[oIDataReader.GetOrdinal("NombreDuenio")].ToString(),
                                    ApellidosDuenio = oIDataReader[oIDataReader.GetOrdinal("ApellidosDuenio")].ToString(),
                                    CorreoDuenio = oIDataReader[oIDataReader.GetOrdinal("CorreoDuenio")].ToString(),
                                    CodigoPais = oIDataReader[oIDataReader.GetOrdinal("CodigoPais")].ToString(),
                                    CelularDuenio = oIDataReader[oIDataReader.GetOrdinal("CelularDuenio")].ToString(),
                                    TipoDocumentoEmpresa = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoDocumentoEmpresa")]),
                                    NroDocumentoEmpresa = oIDataReader[oIDataReader.GetOrdinal("NroDocumentoEmpresa")].ToString(),
                                    RazonSocialEmpresa = oIDataReader[oIDataReader.GetOrdinal("RazonSocialEmpresa")].ToString(),
                                    NombreComercialEmpresa = oIDataReader[oIDataReader.GetOrdinal("NombreComercialEmpresa")].ToString(),
                                    DireccionEmpresa = oIDataReader[oIDataReader.GetOrdinal("Direccion")].ToString(),
                                    TelefonoEmpresa = oIDataReader[oIDataReader.GetOrdinal("TelefonoEmpresa")].ToString(),
                                    FechaAniversarioEmpresa = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaAniversarioEmpresa")]),
                                    CorreoEmpresa = oIDataReader[oIDataReader.GetOrdinal("CorreoEmpresa")].ToString(),
                                    SubDominio = oIDataReader[oIDataReader.GetOrdinal("SubDominio")].ToString(),
                                    LogoTipo = oIDataReader[oIDataReader.GetOrdinal("LogoTipo")].ToString(),
                                    Estado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    IdEmpresa = oIDataReader[oIDataReader.GetOrdinal("IdEmpresa")].ToString()
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }

        public List<EmpresaDTO> ecommerce_uspObtenerEmpresaPorDominio(EmpresaDTO oFiltro)
        {
            List<EmpresaDTO> lista = new List<EmpresaDTO>();
          
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspObtenerEmpresaPorDominio", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@SubDominio", System.Data.SqlDbType.VarChar)).Value = oFiltro.SubDominio;
                    
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new EmpresaDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    LogoTipo = oIDataReader[oIDataReader.GetOrdinal("LogoTipo")].ToString(),
                                    SubDominio = oIDataReader[oIDataReader.GetOrdinal("SubDominio")].ToString(),
                                    NombreComercialEmpresa = oIDataReader[oIDataReader.GetOrdinal("NombreComercialEmpresa")].ToString(),
                                    ColorEmpresa = oIDataReader[oIDataReader.GetOrdinal("ColorEmpresa")].ToString()

                                    //DesEstado = oIDataReader[oIDataReader.GetOrdinal("DesEstado")].ToString()
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public List<EmpresaDTO> ecommerce_uspObtenerEmpresaPorDominio_AppFitness(EmpresaDTO oFiltro)
        {
            List<EmpresaDTO> lista = new List<EmpresaDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspObtenerEmpresaPorDominio_AppFitness", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@DefaultKeyUser", System.Data.SqlDbType.VarChar)).Value = oFiltro.DefaultKeyUser;
                    cmd.Parameters.Add(new SqlParameter("@SubDominio", System.Data.SqlDbType.VarChar)).Value = oFiltro.SubDominio;
                    cmd.Parameters.AddWithValue("@Validador", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new EmpresaDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    LogoTipo = oIDataReader[oIDataReader.GetOrdinal("Logo")].ToString(),
                                    //ColorEmpresa = oIDataReader[oIDataReader.GetOrdinal("ColorEmpresa")].ToString(),
                                    SubDominio = oIDataReader[oIDataReader.GetOrdinal("SubDominio")].ToString(),
                                    NombreComercialEmpresa = oIDataReader[oIDataReader.GetOrdinal("NombreComercialEmpresa")].ToString(),
                                    DefaultKeyEmpresa = oIDataReader[oIDataReader.GetOrdinal("DefaultKeyEmpresa")].ToString(),
                                    Ubigeo = oIDataReader[oIDataReader.GetOrdinal("Ubigeo")].ToString(),
                                    TieneFacturacionElectronica = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("TieneFacturacionElectronica")]),
                                    TiendaAplicacion = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("TiendaAplicacion")]),
                                    AplicacionDisponible = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("AplicacionDisponible")]),
                                    RutinasAplicacion = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("RutinasAplicacion")]),

                                    //DesEstado = oIDataReader[oIDataReader.GetOrdinal("DesEstado")].ToString()
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public int appsfit_uspAspNetUsersCentroFit_AgregarFavorito(EmpresaDTO item)
        {
            int Validador = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("appsfit_uspAspNetUsersCentroFit_AgregarFavorito", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;                   
                    cmd.Parameters.Add(new SqlParameter("@DefaultKeyUser", System.Data.SqlDbType.VarChar, 100)).Value = item.DefaultKeyUser;
                    cmd.Parameters.Add(new SqlParameter("@DefaultKeyEmpresa", System.Data.SqlDbType.VarChar, 100)).Value = item.DefaultKeyEmpresa;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Bit)).Value = item.Estado == 0 ? false: true;
                    cmd.Parameters.Add(new SqlParameter("@Validador", System.Data.SqlDbType.Int)).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    Validador = Convert.ToInt32(cmd.Parameters["@Validador"].Value);
                }

            }
            return Validador;
        }

        public string RegistrarEmpresa(EmpresaDTO item)
        {
            string ID = "";
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspRegistrarEmpresa", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@NombreDuenio", System.Data.SqlDbType.VarChar, 100)).Value = item.NombreDuenio;
                    cmd.Parameters.Add(new SqlParameter("@ApellidosDuenio", System.Data.SqlDbType.VarChar, 100)).Value = item.ApellidosDuenio;
                    if (!string.IsNullOrEmpty(item.CorreoDuenio))
                    {
                        cmd.Parameters.Add(new SqlParameter("@CorreoDuenio", System.Data.SqlDbType.VarChar, 100)).Value = item.CorreoDuenio;
                    }
                    if (!string.IsNullOrEmpty(item.CodigoPais))
                    {
                        cmd.Parameters.Add(new SqlParameter("@CodigoPais", System.Data.SqlDbType.VarChar,100)).Value = item.CodigoPais;
                    }
                    if (!string.IsNullOrEmpty(item.CelularDuenio))
                    {
                        cmd.Parameters.Add(new SqlParameter("@CelularDuenio", System.Data.SqlDbType.VarChar, 50)).Value = item.CelularDuenio;
                    }
                    
                    cmd.Parameters.Add(new SqlParameter("@TipoDocumentoEmpresa", System.Data.SqlDbType.Int)).Value = item.TipoDocumentoEmpresa;
                    
                    if (!string.IsNullOrEmpty(item.NroDocumentoEmpresa))
                    {
                        cmd.Parameters.Add(new SqlParameter("@NroDocumentoEmpresa", System.Data.SqlDbType.VarChar,50)).Value = item.NroDocumentoEmpresa;
                    }
                    if (!string.IsNullOrEmpty(item.RazonSocialEmpresa))
                    {
                        cmd.Parameters.Add(new SqlParameter("@RazonSocialEmpresa", System.Data.SqlDbType.VarChar,100)).Value = item.RazonSocialEmpresa;
                    }
                    if (!string.IsNullOrEmpty(item.DireccionEmpresa))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Direccion", System.Data.SqlDbType.VarChar, 200)).Value = item.DireccionEmpresa;
                    }
                    if (!string.IsNullOrEmpty(item.NombreComercialEmpresa))
                    {
                        cmd.Parameters.Add(new SqlParameter("@NombreComercialEmpresa", System.Data.SqlDbType.VarChar, 100)).Value = item.NombreComercialEmpresa;
                    }
                    if (!string.IsNullOrEmpty(item.TelefonoEmpresa))
                    {
                        cmd.Parameters.Add(new SqlParameter("@TelefonoEmpresa", System.Data.SqlDbType.VarChar, 100)).Value = item.TelefonoEmpresa;
                    }                   
                    cmd.Parameters.Add(new SqlParameter("@FechaAniversarioEmpresa", System.Data.SqlDbType.DateTime)).Value = item.FechaAniversarioEmpresa;
                    if (!string.IsNullOrEmpty(item.CorreoEmpresa))
                    {
                        cmd.Parameters.Add(new SqlParameter("@CorreoEmpresa", System.Data.SqlDbType.VarChar, 100)).Value = item.CorreoEmpresa;
                    }
                    if (!string.IsNullOrEmpty(item.SubDominio))
                    {
                        cmd.Parameters.Add(new SqlParameter("@SubDominio", System.Data.SqlDbType.VarChar, 100)).Value = item.SubDominio;
                    }
                    cmd.Parameters.Add(new SqlParameter("@LogoTipo", System.Data.SqlDbType.VarChar, 250)).Value = item.LogoTipo;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Int)).Value = item.Estado;
                    if (!string.IsNullOrEmpty(item.UsuarioCreacion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    }

                    if (!string.IsNullOrEmpty(item.IdEmpresa))
                    {
                        cmd.Parameters.Add(new SqlParameter("@IdEmpresa", System.Data.SqlDbType.VarChar, 100)).Value = item.IdEmpresa;
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@IdEmpresa", System.Data.SqlDbType.VarChar, 100)).Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(new SqlParameter("@ColorEmpresa", System.Data.SqlDbType.VarChar, 250)).Value = item.ColorEmpresa;
                    cmd.ExecuteNonQuery();
                    ID = cmd.Parameters["@IdEmpresa"].Value.ToString();
                }

            }
            return ID;
        }

        public void ActualizarEmpresa(EmpresaDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspActualizarEmpresa", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@NombreDuenio", System.Data.SqlDbType.VarChar, 100)).Value = item.NombreDuenio;
                    cmd.Parameters.Add(new SqlParameter("@ApellidosDuenio", System.Data.SqlDbType.VarChar, 100)).Value = item.ApellidosDuenio;
                    if (!string.IsNullOrEmpty(item.CorreoDuenio))
                    {
                        cmd.Parameters.Add(new SqlParameter("@CorreoDuenio", System.Data.SqlDbType.VarChar, 100)).Value = item.CorreoDuenio;
                    }
                    if (!string.IsNullOrEmpty(item.CodigoPais))
                    {
                        cmd.Parameters.Add(new SqlParameter("@CodigoPais", System.Data.SqlDbType.VarChar, 100)).Value = item.CodigoPais;
                    }
                    if (!string.IsNullOrEmpty(item.CelularDuenio))
                    {
                        cmd.Parameters.Add(new SqlParameter("@CelularDuenio", System.Data.SqlDbType.VarChar, 50)).Value = item.CelularDuenio;
                    }

                    cmd.Parameters.Add(new SqlParameter("@TipoDocumentoEmpresa", System.Data.SqlDbType.Int)).Value = item.TipoDocumentoEmpresa;

                    if (!string.IsNullOrEmpty(item.NroDocumentoEmpresa))
                    {
                        cmd.Parameters.Add(new SqlParameter("@NroDocumentoEmpresa", System.Data.SqlDbType.VarChar, 50)).Value = item.NroDocumentoEmpresa;
                    }
                    if (!string.IsNullOrEmpty(item.RazonSocialEmpresa))
                    {
                        cmd.Parameters.Add(new SqlParameter("@RazonSocialEmpresa", System.Data.SqlDbType.VarChar, 100)).Value = item.RazonSocialEmpresa;
                    }
                    if (!string.IsNullOrEmpty(item.DireccionEmpresa))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Direccion", System.Data.SqlDbType.VarChar, 200)).Value = item.DireccionEmpresa;
                    }
                    if (!string.IsNullOrEmpty(item.NombreComercialEmpresa))
                    {
                        cmd.Parameters.Add(new SqlParameter("@NombreComercialEmpresa", System.Data.SqlDbType.VarChar, 100)).Value = item.NombreComercialEmpresa;
                    }
                    if (!string.IsNullOrEmpty(item.TelefonoEmpresa))
                    {
                        cmd.Parameters.Add(new SqlParameter("@TelefonoEmpresa", System.Data.SqlDbType.VarChar, 100)).Value = item.TelefonoEmpresa;
                    }
                    cmd.Parameters.Add(new SqlParameter("@FechaAniversarioEmpresa", System.Data.SqlDbType.DateTime)).Value = item.FechaAniversarioEmpresa;
                    if (!string.IsNullOrEmpty(item.CorreoEmpresa))
                    {
                        cmd.Parameters.Add(new SqlParameter("@CorreoEmpresa", System.Data.SqlDbType.VarChar, 100)).Value = item.CorreoEmpresa;
                    }
                    if (!string.IsNullOrEmpty(item.SubDominio))
                    {
                        cmd.Parameters.Add(new SqlParameter("@SubDominio", System.Data.SqlDbType.VarChar, 100)).Value = item.SubDominio;
                    }
                  
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Int)).Value = item.Estado;
                    if (!string.IsNullOrEmpty(item.UsuarioCreacion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    }
                    if (!string.IsNullOrEmpty(item.LogoTipo))
                    {
                        cmd.Parameters.Add(new SqlParameter("@LogoTipo", System.Data.SqlDbType.VarChar, 250)).Value = item.LogoTipo;
                    }
                    if (!string.IsNullOrEmpty(item.ColorEmpresa))
                    {
                        cmd.Parameters.Add(new SqlParameter("@ColorEmpresa", System.Data.SqlDbType.VarChar, 250)).Value = item.ColorEmpresa;
                    }

                    cmd.ExecuteNonQuery();
                    //ID = cmd.Parameters["@po_CodigoProducto"].Value.ToString();
                }

            }
        }


    }
}
