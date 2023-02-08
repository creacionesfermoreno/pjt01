using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using E_DataModel.Gimnasio;
using E_DataModel.Common;

namespace E_DataLayer.Gimnasio
{
    public class SociosFichaSaludMasterData
    {
        //si se usa
        public List<SociosFichaSaludMasterDTO> uspListarSociosFichaSaludMaster(SociosFichaSaludMasterDTO oItem)
        {
            List<SociosFichaSaludMasterDTO> lista = new List<SociosFichaSaludMasterDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarSociosFichaSaludMaster", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    //cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oAsistenciaDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@Siglas", System.Data.SqlDbType.VarChar,50)).Value = oItem.Siglas;
                    
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new SociosFichaSaludMasterDTO()
                                {

                                    CodigoMaster = oIDataReader[oIDataReader.GetOrdinal("CodigoMaster")].ToString(),
                                    Siglas = oIDataReader[oIDataReader.GetOrdinal("Siglas")].ToString(),
                                    SiglasTitulo = oIDataReader[oIDataReader.GetOrdinal("SiglasTitulo")].ToString(),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    valor = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("valor")]),
                                    Estado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString()
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
