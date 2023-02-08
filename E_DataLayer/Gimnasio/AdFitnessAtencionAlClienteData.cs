
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
	public class AdFitnessAtencionAlClienteData
	{
		
        public List<AdFitnessAtencionAlClienteDTO> uspListarAdFitnessAtencionAlCliente_Paginacion(AdFitnessAtencionAlClienteDTO oItem, Paging paging)
        {
            List<AdFitnessAtencionAlClienteDTO> lista = new List<AdFitnessAtencionAlClienteDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarAdFitnessAtencionAlCliente_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FechaDesde", System.Data.SqlDbType.Int)).Value = oItem.FechaDesde;
                    cmd.Parameters.Add(new SqlParameter("@FechaHasta", System.Data.SqlDbType.Int)).Value = oItem.FechaHasta;
                    cmd.Parameters.Add(new SqlParameter("@EstadoAtendido", System.Data.SqlDbType.VarChar)).Value = oItem.EstadoAtendido;
                    
                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;


                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new AdFitnessAtencionAlClienteDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    RazonSocial = oIDataReader[oIDataReader.GetOrdinal("RazonSocial")].ToString(),
                                    SubDominio = oIDataReader[oIDataReader.GetOrdinal("SubDominio")].ToString(),
                                    Estado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Mensaje = oIDataReader[oIDataReader.GetOrdinal("Mensaje")].ToString(),
                                    EstadoAtendido = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("EstadoAtendido")]),
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


        public AdFitnessAtencionAlClienteDTO uspListarAdFitnessAtencionAlCliente_NumeroRegistros(AdFitnessAtencionAlClienteDTO oItem)
        {
            AdFitnessAtencionAlClienteDTO itemDTO = null;
           
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarAdFitnessAtencionAlCliente_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FechaDesde", System.Data.SqlDbType.Int)).Value = oItem.FechaDesde;
                    cmd.Parameters.Add(new SqlParameter("@FechaHasta", System.Data.SqlDbType.Int)).Value = oItem.FechaHasta;
                    cmd.Parameters.Add(new SqlParameter("@EstadoAtendido", System.Data.SqlDbType.VarChar)).Value = oItem.EstadoAtendido;

                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new AdFitnessAtencionAlClienteDTO()
                                {
                                    CantTotal = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadRegistros")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;            
        }

        public void Registrar(AdFitnessAtencionAlClienteDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarAdFitnessAtencionAlCliente", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = item.Codigo;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Int)).Value = item.Estado;
                    cmd.Parameters.Add(new SqlParameter("@Nombres", System.Data.SqlDbType.VarChar,100)).Value = item.Nombres;

                    cmd.Parameters.Add(new SqlParameter("@Telefono", System.Data.SqlDbType.VarChar, 20)).Value = item.Telefono;
                    cmd.Parameters.Add(new SqlParameter("@Mensaje", System.Data.SqlDbType.VarChar)).Value = item.Mensaje;
                    cmd.Parameters.Add(new SqlParameter("@EstadoAtendido", System.Data.SqlDbType.Int)).Value = item.EstadoAtendido;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar,100)).Value = item.UsuarioCreacion;
                    
                    cmd.ExecuteNonQuery();
                }
            }
		}

        public void uspActualizarEstadoAdFitness_AtencionCliente(AdFitnessAtencionAlClienteDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarEstadoAdFitness_AtencionCliente", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = item.Codigo;
                    cmd.Parameters.Add(new SqlParameter("@EstadoAtendido", System.Data.SqlDbType.Int)).Value = item.EstadoAtendido;

                    cmd.ExecuteNonQuery();
                }
            }
		}

        public AdFitnessAtencionAlClienteDTO uspValidarPagosClientes_AdFitness(int CodigoUnidadNegocio, int CodigoSede)
        {
            AdFitnessAtencionAlClienteDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspValidarPagosClientes_AdFitness", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = CodigoSede;

                    cmd.Parameters.AddWithValue("@Existe", 0).Direction = System.Data.ParameterDirection.Output;
                    
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new AdFitnessAtencionAlClienteDTO();

                                itemDTO.CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]);
                                itemDTO.CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]);
                                itemDTO.Existe = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Existe")]);
                                itemDTO.FechaPagoTexto = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaPago")]).ToString("dd/MM/yyyy");
                                itemDTO.FechaVencimientoPagoTexto = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaVencimientoPago")]).ToString("dd/MM/yyyy");
                                itemDTO.FechaVencimientoDemoTexto = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaVencimientoDemo")]).ToString("dd/MM/yyyy");
                                itemDTO.TipoMoneda = oIDataReader[oIDataReader.GetOrdinal("TipoMoneda")].ToString();
                                itemDTO.MontoMensualidad = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MontoMensualidad")]);
                                itemDTO.EntidadBancaria = oIDataReader[oIDataReader.GetOrdinal("EntidadBancaria")].ToString();
                                itemDTO.NroCuenta = oIDataReader[oIDataReader.GetOrdinal("NroCuenta")].ToString();
                                itemDTO.ResponsableCuenta = oIDataReader[oIDataReader.GetOrdinal("ResponsableCuenta")].ToString();
                                itemDTO.RazonSocial = oIDataReader[oIDataReader.GetOrdinal("RazonSocial")].ToString();
                                itemDTO.CelularEnviarVoucher = oIDataReader[oIDataReader.GetOrdinal("CelularEnviarVoucher")].ToString();
                                itemDTO.CCI = oIDataReader[oIDataReader.GetOrdinal("CCI")].ToString();
                                itemDTO.Estado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]);
                                itemDTO.PlanEmpresa = oIDataReader[oIDataReader.GetOrdinal("DesPlan")].ToString(); 
                                itemDTO.EstadoEmpresa = oIDataReader[oIDataReader.GetOrdinal("DesEstadoGym")].ToString();
                                itemDTO.EstadoFinPrueba = oIDataReader[oIDataReader.GetOrdinal("EstadoFinPrueba")].ToString();

                            }
                        }
                    }
                }
            }
            return itemDTO;
          
        }

        public int uspValidarIngresoUsuarios_Saludo_AdFitness(int CodigoUnidadNegocio, int CodigoSede, string User)
        {
            int? campoRetorno = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspValidarIngresoUsuarios_Saludo_AdFitness", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar,100)).Value = CodigoSede;

                    cmd.Parameters.AddWithValue("@Existe", campoRetorno).Direction = System.Data.ParameterDirection.Output;


                    cmd.ExecuteNonQuery();
                    campoRetorno = Convert.ToInt32(cmd.Parameters["@Existe"].Value);
                }
            }
            return Convert.ToInt32(campoRetorno);            
        }




    }
}
