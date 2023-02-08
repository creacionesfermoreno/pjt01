using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
	public class ComprasData
	{
        public List<ComprasDTO> ListarControlIngresosPorND(ComprasDTO oComprasDTO)
        {
            List<ComprasDTO> lista = new List<ComprasDTO>();
            
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListar_Socios_Inasistencias_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oComprasDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oComprasDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oComprasDTO.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oComprasDTO.CodigoSede;
                    
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ComprasDTO()
                                {
                                    CodigoIngreso = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoIngreso")]),
                                    NroDocumento = oIDataReader[oIDataReader.GetOrdinal("NroDocumento")].ToString(),
                                    FechaIngreso = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaIngreso")]),
                                    CodigoTipoDocumento = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoTipoDocumento")]),
                                    Percepcion = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Percepcion")]),
                                    TotalNeto = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("TotalNeto")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString()
                                });
                            }
                        }

                    }
                }
            }
            
            return lista;
        }
       
		public int Registrar(ComprasDTO item)
		{
            int? codigo = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarControlIngreso", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CodigoIngreso", codigo).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@CodigoTipoDocumento", System.Data.SqlDbType.Int)).Value = item.CodigoTipoDocumento;
                    cmd.Parameters.Add(new SqlParameter("@NroDocumento", System.Data.SqlDbType.VarChar,50)).Value = item.NroDocumento;
                    cmd.Parameters.Add(new SqlParameter("@FechaIngreso", System.Data.SqlDbType.DateTime)).Value = item.FechaIngreso;
                    cmd.Parameters.Add(new SqlParameter("@Percepcion", System.Data.SqlDbType.Decimal)).Value = item.Percepcion;
                    cmd.Parameters.Add(new SqlParameter("@TotalNeto", System.Data.SqlDbType.Decimal)).Value = item.TotalNeto;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoProveedor", System.Data.SqlDbType.Int)).Value = item.CodigoProveedor;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;

                    cmd.ExecuteNonQuery();
                    codigo = Convert.ToInt32(cmd.Parameters["@CodigoIngreso"].Value);
                }
            }

            return Convert.ToInt32(codigo);
		}
        

		public void Eliminar(ComprasDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEliminarControlIngreso", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoIngreso", System.Data.SqlDbType.Int)).Value = item.CodigoIngreso;
                    
                    cmd.ExecuteNonQuery();
                }
            }
		}
        
	}
}
