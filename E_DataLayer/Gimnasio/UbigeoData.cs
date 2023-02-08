using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
    public class UbicacionesData
    {
      
        public List<UbicacionesDTO> Listar(UbicacionesDTO oUbicacionesDTO)
        {

            List<UbicacionesDTO> lista = new List<UbicacionesDTO>();
           
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarUbigeo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Tipo", System.Data.SqlDbType.Int, 10)).Value = oUbicacionesDTO.Tipo;
                    cmd.Parameters.Add(new SqlParameter("@Ubigeo", System.Data.SqlDbType.VarChar, 100)).Value = oUbicacionesDTO.Ubicaciones;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar, 100)).Value = oUbicacionesDTO.Buscador;
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                UbicacionesDTO Ubicaciones = new UbicacionesDTO()
                                {
                                    CodigoUbicaciones = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUbigeo")]),
                                    Ubicaciones = Convert.ToString(oIDataReader[oIDataReader.GetOrdinal("Ubigeo")]),
                                    Pais = Convert.ToString(oIDataReader[oIDataReader.GetOrdinal("Pais")]),
                                    Departamento = Convert.ToString(oIDataReader[oIDataReader.GetOrdinal("Departamento")]),
                                    Provincia = Convert.ToString(oIDataReader[oIDataReader.GetOrdinal("Provincia")]),
                                    Distrito = Convert.ToString(oIDataReader[oIDataReader.GetOrdinal("Distrito")]),
                                    Tipo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Tipo")]),
                                };

                                lista.Add(Ubicaciones);
                            }
                        }
                    }
                }
            }
            return lista;
            
        }

    }
}
