
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
	public class RedesSocialesData
	{
		
		public RedesSocialesDTO BuscarPorCodigoRedesSociales(RedesSocialesDTO oitem)
		{
			RedesSocialesDTO itemDTO = null;
           
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarRedesSocialesPorCodigo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oitem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oitem.CodigoSede;
                 
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                itemDTO = new RedesSocialesDTO()
                                {
                                    CodigoSede = Convert.ToInt32(reader[reader.GetOrdinal("CodigoSede")]),
                                    UrlYoutube = reader[reader.GetOrdinal("UrlYoutube")].ToString(),
                                    UrlFacebook = reader[reader.GetOrdinal("UrlFacebook")].ToString(),
                                    UrlTwitter = reader[reader.GetOrdinal("UrlTwitter")].ToString(),
                                    UrlInstagram = reader[reader.GetOrdinal("UrlInstagram")].ToString()
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;            
		}
	
		public void Registrar(RedesSocialesDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarRedesSociales", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@UrlYoutube", System.Data.SqlDbType.VarChar,200)).Value = item.UrlYoutube;
                    cmd.Parameters.Add(new SqlParameter("@UrlFacebook", System.Data.SqlDbType.VarChar,200)).Value = item.UrlFacebook;
                    cmd.Parameters.Add(new SqlParameter("@UrlTwitter", System.Data.SqlDbType.VarChar,200)).Value = item.UrlTwitter;
                    cmd.Parameters.Add(new SqlParameter("@UrlInstagram", System.Data.SqlDbType.VarChar,200)).Value = item.UrlInstagram;

                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    
                    cmd.ExecuteNonQuery();
                }
            }
		}
		
		public void Actualizar(RedesSocialesDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarRedesSociales", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@UrlYoutube", System.Data.SqlDbType.VarChar, 200)).Value = item.UrlYoutube;
                    cmd.Parameters.Add(new SqlParameter("@UrlFacebook", System.Data.SqlDbType.VarChar, 200)).Value = item.UrlFacebook;
                    cmd.Parameters.Add(new SqlParameter("@UrlTwitter", System.Data.SqlDbType.VarChar, 200)).Value = item.UrlTwitter;
                    cmd.Parameters.Add(new SqlParameter("@UrlInstagram", System.Data.SqlDbType.VarChar, 200)).Value = item.UrlInstagram;

                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;

                    cmd.ExecuteNonQuery();
                }
            }            
		}
        
	}
}
