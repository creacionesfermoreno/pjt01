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
    public class CentroEntrenamiento_AspNetUsersData
    {

        public CentroEntrenamiento_AspNetUsersDTO CentroEntrenamiento_uspBuscarAspNetUsers_imprimirticket_DefaultKey(CentroEntrenamiento_AspNetUsersDTO request)
        {
            CentroEntrenamiento_AspNetUsersDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspBuscarAspNetUsers_imprimirticket_DefaultKey", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@DefaultKey", System.Data.SqlDbType.VarChar, 100)).Value = request.DefaultKeyUser;
                   
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new CentroEntrenamiento_AspNetUsersDTO()
                                {

                                    FullName = oIDataReader[oIDataReader.GetOrdinal("FullName")].ToString(),
                                    UserName = oIDataReader[oIDataReader.GetOrdinal("UserName")].ToString(),
                                    Email = oIDataReader[oIDataReader.GetOrdinal("Email")].ToString(),
                                    PhoneNumber = oIDataReader[oIDataReader.GetOrdinal("PhoneNumber")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]),

                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }

    }
}
