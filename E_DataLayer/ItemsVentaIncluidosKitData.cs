using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using E_DataModel;
using E_DataModel.Common;

namespace E_DataLayer
{
    public class ItemsVentaIncluidosKitData
    {
        public void ecommerce_uspRegistrarItemsVentaIncluidosKit(ItemsVentaIncluidosKitDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("ecommerce_uspRegistrarItemsVentaIncluidosKit", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoItemIncluidosKitVenta", System.Data.SqlDbType.Int)).Value = item.CodigoItemIncluidosKitVenta;
                    cmd.Parameters.Add(new SqlParameter("@CodigoItemVentaSuperior", System.Data.SqlDbType.Int)).Value = item.CodigoItemVentaSuperior;
                    cmd.Parameters.Add(new SqlParameter("@CodigoItemVenta", System.Data.SqlDbType.Int)).Value = item.CodigoItemVenta;
                    cmd.Parameters.Add(new SqlParameter("@Referencia", System.Data.SqlDbType.VarChar, 100)).Value = item.Referencia;
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar, 100)).Value = item.Descripcion;
                    cmd.Parameters.Add(new SqlParameter("@Cantidad", System.Data.SqlDbType.Decimal)).Value = item.Cantidad;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Int)).Value = item.Estado;

                    if (!string.IsNullOrEmpty(item.UsuarioCreacion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    }
                    cmd.ExecuteNonQuery();
                }

            }
        }
    }
}
