using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
	public class CajaAperturaCierreData
	{
        public CajaAperturaCierreDTO uspInformacionGeneralAbrirCaja(CajaAperturaCierreDTO oCajaAperturaCierreDTO)
        {
            CajaAperturaCierreDTO itemDTO = new CajaAperturaCierreDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspInformacionGeneralAbrirCaja", conn))
                {
                   // cmd.CommandTimeout = 100;

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oCajaAperturaCierreDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oCajaAperturaCierreDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Vendedores", System.Data.SqlDbType.VarChar,100)).Value = oCajaAperturaCierreDTO.UsuarioCreacion;
                    
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new CajaAperturaCierreDTO()
                                {                                   
                                    Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoCaja")]),
                                    Estado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    TipoApertura = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoApertura")]),
                                    desEstado = oIDataReader[oIDataReader.GetOrdinal("DescEstado")].ToString(),
                                    DescEstadoTipoApertura = oIDataReader[oIDataReader.GetOrdinal("DescEstadoTipoApertura")].ToString(),
                                    desFechaAbrirCaja = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaAbrirCaja")]).ToString("dd/MM/yyyy HH:mm:ss tt"),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    MontoApertura = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoApertura")]),
                                    //TotalMenbresias = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteMembresias")]),
                                    TotalMenbresias_efectivo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteMembresias_Efectivo")]),
                                    //TotalMenbresias_debito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteMembresias_Debito")]),
                                    //TotalMenbresias_credito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteMembresias_Credito")]),
                                    //TotalMenbresias_deposito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteMembresias_Deposito")]),
                                    //TotalMenbresias_web = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteMembresias_Web")]),

                                    //TotalProductos = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteProductos")]),
                                    TotalProductos_efectivo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteProductos_Efectivo")]),
                                    //TotalProductos_debito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteProductos_Debito")]),
                                    //TotalProductos_credito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteProductos_Credito")]),
                                    //TotalProductos_deposito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteProductos_Deposito")]),
                                    //TotalProductos_web = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteProductos_Web")]),

                                    //TotalLibres = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteLibres")]),
                                    TotalLibres_efectivo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteLibres_Efectivo")]),
                                    //TotalLibres_debito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteLibres_Debito")]),
                                    //TotalLibres_credito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteLibres_Credito")]),
                                    //TotalLibres_deposito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteLibres_Deposito")]),
                                    //TotalLibres_web = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteLibres_Web")]),
                                    
                                    //TotalSuplementos = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteSuplementos")]),
                                    TotalSuplementos_efectivo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteSuplementos_Efectivo")]),
                                    //TotalSuplemento_debito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteSuplementos_Debito")]),
                                    //TotalSuplementos_credito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteSuplementos_Credito")]),
                                    //TotalSuplementos_deposito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteSuplementos_Deposito")]),
                                    //TotalSuplementos_web = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteSuplementos_Web")]),
                                    
                                    //TotalRopa = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteRopa")]),
                                    TotalRopa_efectivo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteRopa_Efectivo")]),
                                    //TotalRopa_debito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteRopa_Debito")]),
                                    //TotalRopa_credito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteRopa_Credito")]),
                                    //TotalRopa_deposito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteRopa_Deposito")]),
                                    //TotalRopa_web = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteRopa_Web")]),
                                    
                                    //TotalAccesorios = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteAccesorio")]),
                                    TotalAccesorios_efectivo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteAccesorio_Efectivo")]),
                                    //TotalAccesorios_debito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteAccesorio_Debito")]),
                                    //TotalAccesorios_credito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteAccesorio_Credito")]),
                                    //TotalAccesorios_deposito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteAccesorio_Deposito")]),
                                    //TotalAccesorios_web = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteAccesorio_Web")]),
                                    
                                    TotalEgresos = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteEgresos")]),
                                    //SumaTotal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("SumaTotalVenta")]),
                                    DineroAjusteCaja = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoAjusteCaja")]),
                                    //Total = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("DineroCaja")]),
                                    DineroDejadoCajaChica = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("DineroDejadoCajaChica")])
                                };
                            }
                        }
                    }
                }
            }

            return itemDTO;
            
        }


        public CajaAperturaCierreDTO uspInformacionGeneralAbrirCaja_otrasformaspago(CajaAperturaCierreDTO oCajaAperturaCierreDTO)
        {
            CajaAperturaCierreDTO itemDTO = new CajaAperturaCierreDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspInformacionGeneralAbrirCaja_otrasformaspago", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oCajaAperturaCierreDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oCajaAperturaCierreDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Vendedores", System.Data.SqlDbType.VarChar, 100)).Value = oCajaAperturaCierreDTO.UsuarioCreacion;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new CajaAperturaCierreDTO()
                                {
                                 
                                    TotalMenbresias_debito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteMembresias_Debito")]),
                                    TotalMenbresias_credito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteMembresias_Credito")]),
                                    TotalMenbresias_deposito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteMembresias_Deposito")]),
                                    TotalMenbresias_web = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteMembresias_Web")]),
                                                                       
                                    TotalProductos_debito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteProductos_Debito")]),
                                    TotalProductos_credito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteProductos_Credito")]),
                                    TotalProductos_deposito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteProductos_Deposito")]),
                                    TotalProductos_web = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteProductos_Web")]),
                                   
                                    TotalLibres_debito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteLibres_Debito")]),
                                    TotalLibres_credito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteLibres_Credito")]),
                                    TotalLibres_deposito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteLibres_Deposito")]),
                                    TotalLibres_web = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteLibres_Web")]),
                                    
                                    TotalSuplemento_debito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteSuplementos_Debito")]),
                                    TotalSuplementos_credito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteSuplementos_Credito")]),
                                    TotalSuplementos_deposito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteSuplementos_Deposito")]),
                                    TotalSuplementos_web = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteSuplementos_Web")]),
                                    
                                    TotalRopa_debito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteRopa_Debito")]),
                                    TotalRopa_credito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteRopa_Credito")]),
                                    TotalRopa_deposito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteRopa_Deposito")]),
                                    TotalRopa_web = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteRopa_Web")]),
                                    
                                    TotalAccesorios_debito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteAccesorio_Debito")]),
                                    TotalAccesorios_credito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteAccesorio_Credito")]),
                                    TotalAccesorios_deposito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteAccesorio_Deposito")]),
                                    TotalAccesorios_web = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("ImporteAccesorio_Web")])
                                  
                                };
                            }
                        }
                    }
                }
            }

            return itemDTO;

        }



        public CajaAperturaCierreDTO uspBuscarAperturaCaja(CajaAperturaCierreDTO oCajaAperturaCierre)
		{
            CajaAperturaCierreDTO itemDTO = new CajaAperturaCierreDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarAperturaCaja", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oCajaAperturaCierre.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@Usuario", System.Data.SqlDbType.VarChar,100)).Value = oCajaAperturaCierre.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oCajaAperturaCierre.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPerfil", System.Data.SqlDbType.Int)).Value = oCajaAperturaCierre.CodigPerfil;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new CajaAperturaCierreDTO()
                                {
                                    Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    MontoApertura = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoApertura")]),
                                    desFecha = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaEdicion")]).ToString("yyyy/MM/dd hh:mm:ss tt")
                                };
                            }
                        }
                    }
                }
            }

            return itemDTO;            
		}
        
        public List<CajaAperturaCierreDTO> uspListarAperturaCaja_Paginacion(CajaAperturaCierreDTO oCajaAperturaCierreDTO, Paging paging)
        {
            List<CajaAperturaCierreDTO> lista = new List<CajaAperturaCierreDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarAperturaCaja_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oCajaAperturaCierreDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oCajaAperturaCierreDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUsuario", System.Data.SqlDbType.VarChar,100)).Value = oCajaAperturaCierreDTO.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oCajaAperturaCierreDTO.FechaCreacion;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oCajaAperturaCierreDTO.Fecha;


                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new CajaAperturaCierreDTO()
                                {
                                    Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    desFechaAbrirCaja = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaAbrirCaja")]).ToString("dd/MM/yyyy HH:mm:ss tt"),
                                    desFechaCerrarCaja = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCierreCaja")]).ToString("dd/MM/yyyy HH:mm:ss tt"),
                                    MontoApertura = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoApertura")]),

                                    TotalMenbresias = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalMenbresias")]),
                                    TotalMenbresias_efectivo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalMenbresias_Efectivo")]),
                                    TotalMenbresias_debito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalMenbresias_Debito")]),
                                    TotalMenbresias_credito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalMenbresias_Credito")]),
                                    TotalMenbresias_deposito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalMenbresias_Deposito")]),
                                    TotalMenbresias_web = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalMenbresias_Web")]),

                                    TotalProductos = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalProductos")]),
                                    TotalProductos_efectivo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalProductos_Efectivo")]),
                                    TotalProductos_debito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalProductos_Debito")]),
                                    TotalProductos_credito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalProductos_Credito")]),
                                    TotalProductos_deposito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalProductos_Deposito")]),
                                    TotalProductos_web = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalProductos_Web")]),

                                    TotalLibres = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalLibres")]),
                                    TotalLibres_efectivo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalLibres_Efectivo")]),
                                    TotalLibres_debito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalLibres_Debito")]),
                                    TotalLibres_credito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalLibres_Credito")]),
                                    TotalLibres_deposito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalLibres_Deposito")]),
                                    TotalLibres_web = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalLibres_Web")]),

                                    TotalSuplementos = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalSuplementos")]),
                                    TotalSuplementos_efectivo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalSuplementos_Efectivo")]),
                                    TotalSuplemento_debito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalSuplementos_Debito")]),
                                    TotalSuplementos_credito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalSuplementos_Credito")]),
                                    TotalSuplementos_deposito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalSuplementos_Deposito")]),
                                    TotalSuplementos_web = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalSuplementos_Web")]),

                                    TotalRopa = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalRopa")]),
                                    TotalRopa_efectivo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalRopa_Efectivo")]),
                                    TotalRopa_debito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalRopa_Debito")]),
                                    TotalRopa_credito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalRopa_Credito")]),
                                    TotalRopa_deposito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalRopa_Deposito")]),
                                    TotalRopa_web = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalRopa_Web")]),

                                    TotalAccesorios = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalAccesorios")]),
                                    TotalAccesorios_efectivo = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalAccesorios_efectivo")]),
                                    TotalAccesorios_debito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalAccesorios_debito")]),
                                    TotalAccesorios_credito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalAccesorios_credito")]),
                                    TotalAccesorios_deposito = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalAccesorios_deposito")]),
                                    TotalAccesorios_web = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalAccesorios_web")]),

                                    TotalEgresos = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalEgresos")]),
                                    SumaTotal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("SumaTotal")]),
                                    TotalDeudas = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalDeudas")]),
                                    Total = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Total")]),
                                    DineroDejadoCajaChica = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("DineroDejadoCajaChica")]),
                                    DineroRetirado = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("DineroRetirado")]),
                                    DineroAjusteCaja = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("DineroAjusteCaja")]),

                                    MontoCierre = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoCierre")]),
                                    desEstado = oIDataReader[oIDataReader.GetOrdinal("DescEstado")].ToString(),

                                    Estado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("NombreCompleto")].ToString(),
                                    Observacion = oIDataReader[oIDataReader.GetOrdinal("Observacion")].ToString(),
                                    TipoApertura = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoApertura")]),
                                    DescEstadoTipoApertura = oIDataReader[oIDataReader.GetOrdinal("DescEstadoTipoApertura")].ToString()
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public CajaAperturaCierreDTO uspListarAperturaCaja_NumeroRegistros(CajaAperturaCierreDTO oCajaAperturaCierre)
        {
            CajaAperturaCierreDTO itemDTO = new CajaAperturaCierreDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarAperturaCaja_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oCajaAperturaCierre.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oCajaAperturaCierre.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUsuario", System.Data.SqlDbType.VarChar,100)).Value = oCajaAperturaCierre.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oCajaAperturaCierre.FechaCreacion;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oCajaAperturaCierre.Fecha;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new CajaAperturaCierreDTO()
                                {
                                    CantidadTotalFilas = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadRegistros")])
                                };
                            }
                        }
                    }
                }
            }

            return itemDTO;
        }

        public void uspRegistrarAbrirCaja(CajaAperturaCierreDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarAbrirCaja", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = item.Codigo;
                    cmd.Parameters.Add(new SqlParameter("@MontoApertura", System.Data.SqlDbType.Decimal)).Value = item.MontoApertura;
                    cmd.Parameters.Add(new SqlParameter("@MontoCierre", System.Data.SqlDbType.Decimal)).Value = item.MontoCierre;

                    cmd.Parameters.Add(new SqlParameter("@Faltante", System.Data.SqlDbType.Decimal)).Value = item.Faltante;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Int)).Value = item.Estado;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar,100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@TotalMenbresias", System.Data.SqlDbType.Decimal)).Value = item.TotalMenbresias;
                    cmd.Parameters.Add(new SqlParameter("@TotalProductos", System.Data.SqlDbType.Decimal)).Value = item.TotalProductos;
                    cmd.Parameters.Add(new SqlParameter("@TotalLibres", System.Data.SqlDbType.Decimal)).Value = item.TotalLibres;
                    cmd.Parameters.Add(new SqlParameter("@TotalRopa", System.Data.SqlDbType.Decimal)).Value = item.TotalRopa;
                    cmd.Parameters.Add(new SqlParameter("@TotalSuplementos", System.Data.SqlDbType.Decimal)).Value = item.TotalSuplementos;
                    cmd.Parameters.Add(new SqlParameter("@TotalAccesorios", System.Data.SqlDbType.Decimal)).Value = item.TotalAccesorios;

                    cmd.Parameters.Add(new SqlParameter("@TotalEgresos", System.Data.SqlDbType.Decimal)).Value = item.TotalEgresos;
                    cmd.Parameters.Add(new SqlParameter("@SumaTotal", System.Data.SqlDbType.Decimal)).Value = item.SumaTotal;
                    cmd.Parameters.Add(new SqlParameter("@TotalDeudas", System.Data.SqlDbType.Decimal)).Value = item.TotalDeudas;
                    cmd.Parameters.Add(new SqlParameter("@Total", System.Data.SqlDbType.Decimal)).Value = item.Total;

                    cmd.Parameters.Add(new SqlParameter("@DineroDejadoCajaChica", System.Data.SqlDbType.Decimal)).Value = item.DineroDejadoCajaChica;
                    cmd.Parameters.Add(new SqlParameter("@DineroRetirado", System.Data.SqlDbType.Decimal)).Value = item.DineroRetirado;
                    cmd.Parameters.Add(new SqlParameter("@DineroAjusteCaja", System.Data.SqlDbType.Decimal)).Value = item.DineroAjusteCaja;
                

                    cmd.Parameters.Add(new SqlParameter("@Observacion", System.Data.SqlDbType.VarChar,200)).Value = item.Observacion;
                    cmd.Parameters.Add(new SqlParameter("@TipoApertura", System.Data.SqlDbType.Int)).Value = item.TipoApertura;
                    cmd.ExecuteNonQuery();
                }
            }
            
		}

        public void UpdateAbrirCaja(CajaAperturaCierreDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspUpdateAbrirCaja", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = item.Codigo;
                    cmd.Parameters.Add(new SqlParameter("@MontoApertura", System.Data.SqlDbType.Decimal)).Value = item.MontoApertura;
                    cmd.Parameters.Add(new SqlParameter("@MontoCierre", System.Data.SqlDbType.Decimal)).Value = item.MontoCierre;
                    cmd.Parameters.Add(new SqlParameter("@Faltante", System.Data.SqlDbType.Decimal)).Value = item.Faltante;

                    cmd.Parameters.Add(new SqlParameter("@TotalMenbresias", System.Data.SqlDbType.Decimal)).Value = item.TotalMenbresias;
                    cmd.Parameters.Add(new SqlParameter("@TotalMenbresias_efectivo", System.Data.SqlDbType.Decimal)).Value = item.TotalMenbresias_efectivo;
                    cmd.Parameters.Add(new SqlParameter("@TotalMenbresias_debito", System.Data.SqlDbType.Decimal)).Value = item.TotalMenbresias_debito;
                    cmd.Parameters.Add(new SqlParameter("@TotalMenbresias_credito", System.Data.SqlDbType.Decimal)).Value = item.TotalMenbresias_credito;
                    cmd.Parameters.Add(new SqlParameter("@TotalMenbresias_deposito", System.Data.SqlDbType.Decimal)).Value = item.TotalMenbresias_deposito;
                    cmd.Parameters.Add(new SqlParameter("@TotalMenbresias_web", System.Data.SqlDbType.Decimal)).Value = item.TotalMenbresias_web;

                    cmd.Parameters.Add(new SqlParameter("@TotalProductos", System.Data.SqlDbType.Decimal)).Value = item.TotalProductos;
                    cmd.Parameters.Add(new SqlParameter("@TotalProductos_efectivo", System.Data.SqlDbType.Decimal)).Value = item.TotalProductos_efectivo;
                    cmd.Parameters.Add(new SqlParameter("@TotalProductos_debito", System.Data.SqlDbType.Decimal)).Value = item.TotalProductos_debito;
                    cmd.Parameters.Add(new SqlParameter("@TotalProductos_credito", System.Data.SqlDbType.Decimal)).Value = item.TotalProductos_credito;
                    cmd.Parameters.Add(new SqlParameter("@TotalProductos_deposito", System.Data.SqlDbType.Decimal)).Value = item.TotalProductos_deposito;
                    cmd.Parameters.Add(new SqlParameter("@TotalProductos_web", System.Data.SqlDbType.Decimal)).Value = item.TotalProductos_web;

                    cmd.Parameters.Add(new SqlParameter("@TotalLibres", System.Data.SqlDbType.Decimal)).Value = item.TotalLibres;
                    cmd.Parameters.Add(new SqlParameter("@TotalLibres_efectivo", System.Data.SqlDbType.Decimal)).Value = item.TotalLibres_efectivo;
                    cmd.Parameters.Add(new SqlParameter("@TotalLibres_debito", System.Data.SqlDbType.Decimal)).Value = item.TotalLibres_debito;
                    cmd.Parameters.Add(new SqlParameter("@TotalLibres_credito", System.Data.SqlDbType.Decimal)).Value = item.TotalLibres_credito;
                    cmd.Parameters.Add(new SqlParameter("@TotalLibres_deposito", System.Data.SqlDbType.Decimal)).Value = item.TotalLibres_deposito;
                    cmd.Parameters.Add(new SqlParameter("@TotalLibres_web", System.Data.SqlDbType.Decimal)).Value = item.TotalLibres_web;

                    cmd.Parameters.Add(new SqlParameter("@TotalRopa", System.Data.SqlDbType.Decimal)).Value = item.TotalRopa;
                    cmd.Parameters.Add(new SqlParameter("@TotalRopa_efectivo", System.Data.SqlDbType.Decimal)).Value = item.TotalRopa_efectivo;
                    cmd.Parameters.Add(new SqlParameter("@TotalRopa_debito", System.Data.SqlDbType.Decimal)).Value = item.TotalRopa_debito;
                    cmd.Parameters.Add(new SqlParameter("@TotalRopa_credito", System.Data.SqlDbType.Decimal)).Value = item.TotalRopa_credito;
                    cmd.Parameters.Add(new SqlParameter("@TotalRopa_deposito", System.Data.SqlDbType.Decimal)).Value = item.TotalRopa_deposito;
                    cmd.Parameters.Add(new SqlParameter("@TotalRopa_web", System.Data.SqlDbType.Decimal)).Value = item.TotalRopa_web;

                    cmd.Parameters.Add(new SqlParameter("@TotalSuplementos", System.Data.SqlDbType.Decimal)).Value = item.TotalSuplementos;
                    cmd.Parameters.Add(new SqlParameter("@TotalSuplementos_efectivo", System.Data.SqlDbType.Decimal)).Value = item.TotalSuplementos_efectivo;
                    cmd.Parameters.Add(new SqlParameter("@TotalSuplementos_debito", System.Data.SqlDbType.Decimal)).Value = item.TotalSuplemento_debito;
                    cmd.Parameters.Add(new SqlParameter("@TotalSuplementos_credito", System.Data.SqlDbType.Decimal)).Value = item.TotalSuplementos_credito;
                    cmd.Parameters.Add(new SqlParameter("@TotalSuplementos_deposito", System.Data.SqlDbType.Decimal)).Value = item.TotalSuplementos_deposito;
                    cmd.Parameters.Add(new SqlParameter("@TotalSuplementos_web", System.Data.SqlDbType.Decimal)).Value = item.TotalSuplementos_web;

                    cmd.Parameters.Add(new SqlParameter("@TotalAccesorios", System.Data.SqlDbType.Decimal)).Value = item.TotalAccesorios;
                    cmd.Parameters.Add(new SqlParameter("@TotalAccesorios_efectivo", System.Data.SqlDbType.Decimal)).Value = item.TotalAccesorios_efectivo;
                    cmd.Parameters.Add(new SqlParameter("@TotalAccesorios_debito", System.Data.SqlDbType.Decimal)).Value = item.TotalAccesorios_debito;
                    cmd.Parameters.Add(new SqlParameter("@TotalAccesorios_credito", System.Data.SqlDbType.Decimal)).Value = item.TotalAccesorios_credito;
                    cmd.Parameters.Add(new SqlParameter("@TotalAccesorios_deposito", System.Data.SqlDbType.Decimal)).Value = item.TotalAccesorios_deposito;
                    cmd.Parameters.Add(new SqlParameter("@TotalAccesorios_web", System.Data.SqlDbType.Decimal)).Value = item.TotalAccesorios_web;

                    cmd.Parameters.Add(new SqlParameter("@TotalEgresos", System.Data.SqlDbType.Decimal)).Value = item.TotalEgresos;
                    cmd.Parameters.Add(new SqlParameter("@SumaTotal", System.Data.SqlDbType.Decimal)).Value = item.SumaTotal;
                    cmd.Parameters.Add(new SqlParameter("@TotalDeudas", System.Data.SqlDbType.Decimal)).Value = item.TotalDeudas;
                    cmd.Parameters.Add(new SqlParameter("@Total", System.Data.SqlDbType.Decimal)).Value = item.Total;

                    cmd.Parameters.Add(new SqlParameter("@DineroDejadoCajaChica", System.Data.SqlDbType.Decimal)).Value = item.DineroDejadoCajaChica;
                    cmd.Parameters.Add(new SqlParameter("@DineroRetirado", System.Data.SqlDbType.Decimal)).Value = item.DineroRetirado;
                    cmd.Parameters.Add(new SqlParameter("@DineroAjusteCaja", System.Data.SqlDbType.Decimal)).Value = item.DineroAjusteCaja;
                   
                    cmd.ExecuteNonQuery();
                }
            }
            
		}
   
	}
}
