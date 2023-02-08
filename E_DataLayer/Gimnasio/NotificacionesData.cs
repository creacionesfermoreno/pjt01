using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
    public class NotificacionesData
    {

        public List<ProductoDTO> VerMasStockProductos(int CodigoUnidadNegocio, int TipoBusqueda, int CodigoCategoria, int CodSede)
        {
            List<ProductoDTO> lista = new List<ProductoDTO>();
           
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspVerMasStockProductos", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@TipoBusqueda", System.Data.SqlDbType.Int)).Value = TipoBusqueda;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCategoria", System.Data.SqlDbType.Int)).Value = CodigoCategoria;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = CodSede;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ProductoDTO()
                                {
                                    DesCategoria = oIDataReader[oIDataReader.GetOrdinal("DesCategoria")].ToString(),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    PrecioVenta = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("PrecioVenta")]),
                                    Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Cantidad")]),
                                    CantidadMinima = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadMinima")])                                 
                                });
                            }
                        }

                    }
                }
            }
            return lista;            
        }

        public List<ProductoDTO> NotificacionStockProductos(int CodigoUnidadNegocio,int CodSede)
        {
            List<ProductoDTO> lista = new List<ProductoDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspNotificacionStockProductos", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = CodSede;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ProductoDTO()
                                {
                                    DesCategoria = oIDataReader[oIDataReader.GetOrdinal("DesCategoria")].ToString(),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    Cantidad = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Cantidad")])                               
                                });
                            }
                        }

                    }
                }
            }
            return lista;            
        }

    }
}
