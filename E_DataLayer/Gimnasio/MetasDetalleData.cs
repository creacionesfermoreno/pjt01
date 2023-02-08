
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
	public class MetasDetalleData
	{
		
        public List<MetasDetalleDTO> uspListarMetasDetalle(MetasDetalleDTO oitem)
		{
			List<MetasDetalleDTO> lista = new List<MetasDetalleDTO>();
            
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarMetasDetalle", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoEntidadNegocio", System.Data.SqlDbType.Int)).Value = oitem.CodigoEntidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oitem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMeta", System.Data.SqlDbType.Int)).Value = oitem.CodigoMeta;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new MetasDetalleDTO()
                                {
                                    CodigoEntidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoEntidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoMeta = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMeta")]),
                                    Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    CodigoVendedor = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoVendedor")]),
                                    NombreCompleto = oIDataReader[oIDataReader.GetOrdinal("NombreCompleto")].ToString(),
                                    Meta = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Meta")]),
                                    MetaSemanal = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("MetaSemanal")]),
                                    CantidadMetaPlan = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadPlan")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString()                                    
                                });
                            }
                        }

                    }
                }
            }
            return lista;            
		}

        public void Registrar(MetasDetalleDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {            
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarMetasDetalle", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoEntidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoEntidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMeta", System.Data.SqlDbType.Int)).Value = item.CodigoMeta;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = item.Codigo;
                    cmd.Parameters.Add(new SqlParameter("@CodigoVendedor", System.Data.SqlDbType.Int)).Value = item.CodigoVendedor;

                    cmd.Parameters.Add(new SqlParameter("@Meta", System.Data.SqlDbType.Decimal)).Value = item.Meta;
                    cmd.Parameters.Add(new SqlParameter("@MetaSemanal", System.Data.SqlDbType.Decimal)).Value = item.MetaSemanal;
                    cmd.Parameters.Add(new SqlParameter("@CantidadPlan", System.Data.SqlDbType.Int)).Value = item.CantidadMetaPlan;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = item.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = item.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar,100)).Value = item.UsuarioCreacion;
                    
                    cmd.ExecuteNonQuery();
                }
            }          		
		}
        
		public void Actualizar(MetasDetalleDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarMetasDetalle", conn))
                {                   
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoEntidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoEntidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMeta", System.Data.SqlDbType.Int)).Value = item.CodigoMeta;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = item.Codigo;
                    cmd.Parameters.Add(new SqlParameter("@CodigoVendedor", System.Data.SqlDbType.Int)).Value = item.CodigoVendedor;

                    cmd.Parameters.Add(new SqlParameter("@Meta", System.Data.SqlDbType.Decimal)).Value = item.Meta;
                    cmd.Parameters.Add(new SqlParameter("@CantidadPlan", System.Data.SqlDbType.Int)).Value = item.CantidadMetaPlan;
                    cmd.Parameters.Add(new SqlParameter("@MetaSemanal", System.Data.SqlDbType.Decimal)).Value = item.MetaSemanal;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = item.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = item.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioEdicion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;

                    cmd.ExecuteNonQuery();
                }
            }
		}


        
	}
}
