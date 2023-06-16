using CFACADECONN;
using CFACADESTRUC;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CONSULTARINFOALMACEN
{
    public class CMostrarInformacionGrid
    {
        public static void fMostrarInformacionGrid(CEstructura ep, 
                                                   DataGridView Grid, 
                                                   string sTitulo, 
                                                   Int32 nCategoria, 
                                                   Int32 nFolio, 
                                                   string sDescripcionArticulo, 
                                                   ref Int32 nRenglon, 
                                                   Int32 nOpcion,
                                                   Int32 nConsultaFechas,
                                                   string sFechaInicial, 
                                                   string sFechaFinal,
                                                   Int32 nDescontinuado,
                                                   string sLote)
        {
            string sError = "", sConsulta, sFecha, sFechaFormateada;
            bool bContinuar = false;

            Grid.Rows.Add();

            DataGridViewCellStyle cell = new DataGridViewCellStyle();
            cell.Alignment = DataGridViewContentAlignment.TopCenter;

            Grid.Rows[nRenglon].Cells["No"].Value = nRenglon.ToString();
            Grid.Rows[nRenglon].Cells["No"].Style = cell;

            Grid.Rows[nRenglon].Cells["CATEGORIA"].Value = nCategoria.ToString();
            Grid.Rows[nRenglon].Cells["CATEGORIA"].Style = cell;

            if(nFolio != 0)
            {
                Grid.Rows[nRenglon].Cells["FOLIO_CODIGO"].Value = nFolio.ToString("D4");
                Grid.Rows[nRenglon].Cells["FOLIO_CODIGO"].Style = cell;

                Grid.Rows[nRenglon].Cells["DESCRIPCION_ARTICULO"].Value = sDescripcionArticulo.ToString();
                Grid.Rows[nRenglon].Cells["DESCRIPCION_ARTICULO"].Style = cell;
            }

            try
            {
                NpgsqlConnection conn = new NpgsqlConnection();
                if (CConeccion.conexionPostgre(ep, ref conn, ref sError))
                {
                    sConsulta = String.Format(" SELECT folio_articulo, numero_categoria, nombre_categoria, nombre_articulo, cantidad, stock, des_presentacion, nombre_proveedor, requisicion, fecha_caducidad, lote_caducidad, " +
                                              "        marca_caducidad, observacion, des_nombreusuario, folio_movimiento, fecha_modificacion " +
                                              " FROM fConsultar_informacion_almacen({0}::SMALLINT, {1}::INTEGER, {2}::INTEGER, {3}::SMALLINT, '{4}'::DATE, '{5}'::DATE, {6}::SMALLINT, '{7}')"
                                              , nOpcion, nCategoria, nFolio, nConsultaFechas, sFechaInicial, sFechaFinal, nDescontinuado, sLote);
                    NpgsqlCommand com = new NpgsqlCommand(sConsulta, conn);
                    NpgsqlDataReader reader;

                    //MessageBox.Show(sConsulta);

                    reader = com.ExecuteReader();

                    while (reader.Read())
                    {
                        Grid.Rows.Add();

                        Grid.Rows[nRenglon].Cells["CANTIDAD_ACTUAL"].Value = Convert.ToInt32(reader["cantidad"].ToString().Trim());
                        Grid.Rows[nRenglon].Cells["CANTIDAD_ACTUAL"].Style = cell;

                        Grid.Rows[nRenglon].Cells["STOCK"].Value = Convert.ToInt32(reader["stock"].ToString().Trim());
                        Grid.Rows[nRenglon].Cells["STOCK"].Style = cell;

                        Grid.Rows[nRenglon].Cells["FOLIO_MOVIMIENTO"].Value = Convert.ToInt32(reader["folio_movimiento"].ToString().Trim());
                        Grid.Rows[nRenglon].Cells["FOLIO_MOVIMIENTO"].Style = cell;

                        Grid.Rows[nRenglon].Cells["CATEGORIA"].Value = Convert.ToInt32(reader["numero_categoria"].ToString().Trim());
                        Grid.Rows[nRenglon].Cells["CATEGORIA"].Style = cell;

                        nFolio = Convert.ToInt32(reader["folio_articulo"].ToString().Trim());
                        Grid.Rows[nRenglon].Cells["FOLIO_CODIGO"].Value = nFolio.ToString("D4");
                        Grid.Rows[nRenglon].Cells["FOLIO_CODIGO"].Style = cell;

                        Grid.Rows[nRenglon].Cells["DESCRIPCION_ARTICULO"].Value = reader["nombre_categoria"].ToString().Trim();
                        Grid.Rows[nRenglon].Cells["DESCRIPCION_ARTICULO"].Style = cell;

                        Grid.Rows[nRenglon].Cells["DESCRIPCION_PRESENTACION"].Value = reader["nombre_articulo"].ToString().Trim();
                        Grid.Rows[nRenglon].Cells["DESCRIPCION_PRESENTACION"].Style = cell;

                        Grid.Rows[nRenglon].Cells["NOMBRE_PROVEEDOR"].Value = reader["nombre_proveedor"].ToString().Trim();
                        Grid.Rows[nRenglon].Cells["NOMBRE_PROVEEDOR"].Style = cell;

                        Grid.Rows[nRenglon].Cells["REQUISICION"].Value = reader["requisicion"].ToString().Trim();
                        Grid.Rows[nRenglon].Cells["REQUISICION"].Style = cell;

                        sFecha = reader["fecha_caducidad"].ToString();
                        sFechaFormateada = String.Format("{0}-{1}-{2}", sFecha.Substring(0, 2), sFecha.Substring(3, 2), sFecha.Substring(6, 4));

                        if (sFechaFormateada == "01-01-1900")
                        {
                            sFechaFormateada = "";
                        }
                        Grid.Rows[nRenglon].Cells["FECHA_CADUCIDAD"].Value = sFechaFormateada.ToString().Trim();
                        Grid.Rows[nRenglon].Cells["FECHA_CADUCIDAD"].Style = cell;

                        Grid.Rows[nRenglon].Cells["LOTE_CADUCIDAD"].Value = reader["lote_caducidad"].ToString().Trim();
                        Grid.Rows[nRenglon].Cells["LOTE_CADUCIDAD"].Style = cell;

                        Grid.Rows[nRenglon].Cells["MARCA_CADUCIDAD"].Value = reader["marca_caducidad"].ToString().Trim();
                        Grid.Rows[nRenglon].Cells["MARCA_CADUCIDAD"].Style = cell;

                        Grid.Rows[nRenglon].Cells["OBSERVACION"].Value = reader["observacion"].ToString().Trim();
                        Grid.Rows[nRenglon].Cells["OBSERVACION"].Style = cell;

                        Grid.Rows[nRenglon].Cells["EMPLEADO"].Value = reader["des_nombreusuario"].ToString().Trim();
                        Grid.Rows[nRenglon].Cells["EMPLEADO"].Style = cell;

                        sFecha = reader["fecha_modificacion"].ToString();
                        sFechaFormateada = String.Format("{0}-{1}-{2}", sFecha.Substring(0, 2), sFecha.Substring(3, 2), sFecha.Substring(6, 4));
                        Grid.Rows[nRenglon].Cells["FECHA_MODIFICACION"].Value = sFechaFormateada.ToString().Trim();
                        Grid.Rows[nRenglon].Cells["FECHA_MODIFICACION"].Style = cell;

                        nRenglon++;
                        bContinuar = true;
                    }
                }

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }

                if (!bContinuar)
                {
                    Grid.Rows.Clear();
                    Grid.Columns.Clear();

                    MessageBox.Show("No existe información para mostrar. Verifique!!!", sTitulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se presento un problema al mostar la información..." + ex.Message.ToString().Trim(), sTitulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            fRenglonesNo(Grid);
        }

        public static void fRenglonesNo(DataGridView Grid)
        {
            DataGridViewCellStyle cell = new DataGridViewCellStyle();
            cell.Alignment = DataGridViewContentAlignment.TopCenter;

            for (var i = 1; i < Grid.Rows.Count; i++)
            {
                Grid.Rows[i].Cells["No"].Value = i.ToString();
                Grid.Rows[i].Cells["No"].Style = cell;

                if (Grid.Rows[i].Cells["CANTIDAD_ACTUAL"].Value == null)
                {
                    Grid.Rows.RemoveAt(i);
                }
            }
        }
    }
}
