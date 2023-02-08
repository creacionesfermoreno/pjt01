using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using E_DataModel.CentroEntrenamiento;
using E_DataModel.Common;

namespace E_DataLayer.CentroEntrenamiento
{
    public class CentroEntrenamiento_EditorPaginaWebDetalleData
    {

        public List<CentroEntrenamiento_EditorPaginaWebDetalleDTO> ecommerce_uspListarEdicionPaginaWebDetalle(CentroEntrenamiento_EditorPaginaWebDetalleDTO oFiltro)
        {
            List<CentroEntrenamiento_EditorPaginaWebDetalleDTO> lista = new List<CentroEntrenamiento_EditorPaginaWebDetalleDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspListarEdicionPaginaWebDetalle", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.VarChar,50)).Value = oFiltro.Tipo;
                    
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new CentroEntrenamiento_EditorPaginaWebDetalleDTO()
                                {
                                    Codigo = oIDataReader[oIDataReader.GetOrdinal("Codigo")].ToString(),
                                    Tipo = oIDataReader[oIDataReader.GetOrdinal("Tipo")].ToString(),
                                    Titulo = oIDataReader[oIDataReader.GetOrdinal("Titulo")].ToString(),
                                    SubTitulo = oIDataReader[oIDataReader.GetOrdinal("SubTitulo")].ToString(),
                                    LinkPago = oIDataReader[oIDataReader.GetOrdinal("PagoLink")].ToString(),
                                    UrlUmagen = oIDataReader[oIDataReader.GetOrdinal("UrlUmagen")].ToString()                                  
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }
        
        public CentroEntrenamiento_EditorPaginaWebDetalleDTO ecommerce_uspBuscarEdicionPaginaWebDetalle(CentroEntrenamiento_EditorPaginaWebDetalleDTO request)
        {
            CentroEntrenamiento_EditorPaginaWebDetalleDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspBuscarEdicionPaginaWebDetalle", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = request.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = request.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.VarChar, 200)).Value = request.Codigo;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleRow))
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new CentroEntrenamiento_EditorPaginaWebDetalleDTO();
                                itemDTO.Tipo = oIDataReader[oIDataReader.GetOrdinal("Tipo")].ToString();
                                itemDTO.Titulo = oIDataReader[oIDataReader.GetOrdinal("Titulo")].ToString();
                                itemDTO.SubTitulo = oIDataReader[oIDataReader.GetOrdinal("SubTitulo")].ToString();
                                itemDTO.UrlUmagen = oIDataReader[oIDataReader.GetOrdinal("UrlUmagen")].ToString();                              
                            }
                        }
                    }
                }
            }

            return itemDTO;
        }

        public string ecommerce_uspRegistrarEdicionPaginaWebDetalle(CentroEntrenamiento_EditorPaginaWebDetalleDTO item)
        {
            string resultado = string.Empty;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspRegistrarEdicionPaginaWebDetalle", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;                   
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.VarChar, 200)).Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.VarChar, 50)).Value = item.Tipo;
                    cmd.Parameters.Add(new SqlParameter("@Titulo", System.Data.SqlDbType.VarChar, 100)).Value = item.Titulo;
                    cmd.Parameters.Add(new SqlParameter("@SubTitulo", System.Data.SqlDbType.VarChar, 100)).Value = item.SubTitulo;
                    cmd.Parameters.Add(new SqlParameter("@PagoLink", System.Data.SqlDbType.VarChar, 200)).Value = item.LinkPago;
                    cmd.Parameters.Add(new SqlParameter("@UrlUmagen", System.Data.SqlDbType.VarChar, 200)).Value = item.UrlUmagen;
                    
                    cmd.ExecuteNonQuery();
                    resultado = cmd.Parameters["@Codigo"].Value.ToString();
                }
            }
            return resultado;
        }

        public void ecommerce_uspActualizarEdicionPaginaWebDetalle(CentroEntrenamiento_EditorPaginaWebDetalleDTO item)
        {
            string resultado = string.Empty;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspActualizarEdicionPaginaWebDetalle", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;                   
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.VarChar, 200)).Value = item.Codigo;

                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.VarChar, 50)).Value = item.Tipo;
                    cmd.Parameters.Add(new SqlParameter("@Titulo", System.Data.SqlDbType.VarChar, 100)).Value = item.Titulo;
                    cmd.Parameters.Add(new SqlParameter("@SubTitulo", System.Data.SqlDbType.VarChar, 100)).Value = item.SubTitulo;
                    cmd.Parameters.Add(new SqlParameter("@PagoLink", System.Data.SqlDbType.VarChar, 200)).Value = item.LinkPago;
                    cmd.Parameters.Add(new SqlParameter("@UrlUmagen", System.Data.SqlDbType.VarChar, 200)).Value = item.UrlUmagen;
                    
                    cmd.ExecuteNonQuery();                    
                }
            }            
        }

        public void ecommerce_uspEliminarEdicionPaginaWebDetalle(CentroEntrenamiento_EditorPaginaWebDetalleDTO item)
        {
            string resultado = string.Empty;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspEliminarEdicionPaginaWebDetalle", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.VarChar, 200)).Value = item.Codigo;
                 
                    cmd.ExecuteNonQuery();
                }

            }           
        }


    }
}
