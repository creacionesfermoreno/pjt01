using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
	public class IngresoAjustesCajaData
	{
		
        public List<IngresoAjustesCajaDTO> ListarDetalleAjustesIngresoCaja(IngresoAjustesCajaDTO oitem)
		{
			 List<IngresoAjustesCajaDTO> lista = new List<IngresoAjustesCajaDTO>();
           
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarDetalleAjustesIngresoCaja", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oitem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oitem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoAbrirCaja", System.Data.SqlDbType.Int)).Value = oitem.CodigoAbrirCaja;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new IngresoAjustesCajaDTO()
                                {
                                    CodigoIAc = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoIAc")]),
                                    CodigoAbrirCaja = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoAbrirCaja")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    Cantidad = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("Cantidad")]),
                                    Fecha = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("Fecha")]),
                                    DescFechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("Fecha")]).ToString("dd/MM/yyyy HH:mm:ss tt"),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
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
		
		public void Registrar(IngresoAjustesCajaDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarIngresoAjustesCaja", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoIAc", System.Data.SqlDbType.Int)).Value = item.CodigoIAc;
                    cmd.Parameters.Add(new SqlParameter("@CodigoAbrirCaja", System.Data.SqlDbType.Int)).Value = item.CodigoAbrirCaja;
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar)).Value = item.Descripcion;
                    cmd.Parameters.Add(new SqlParameter("@Cantidad", System.Data.SqlDbType.Decimal)).Value = item.Cantidad;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar,100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;

                    cmd.ExecuteNonQuery();
                }
            }
		}
        
	}
}
