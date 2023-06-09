using CFACADESTRUC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace CONSULTARINFOALMACEN
{
    public class CEncabezadoGrid
    {
        public static void fEncabezadoGrid(CEstructura ep, DataGridView Grid, string sTitulo, ref Int32 nRenglon)
        {
            Grid.Columns.Clear();
            Grid.Rows.Clear();
            DataGridViewCellStyle cell = new DataGridViewCellStyle();
            cell.Alignment = DataGridViewContentAlignment.TopCenter;
            Grid.RowHeadersVisible = false;

            Grid.Columns.Add("No", "No");
            Grid.Columns["No"].Width = 30;
            Grid.Columns["No"].SortMode = DataGridViewColumnSortMode.NotSortable;
            Grid.Columns["No"].ReadOnly = true;

            Grid.Columns.Add("FOLIO_MOVIMIENTO", "FOLIO_MOVIMIENTO");
            Grid.Columns["FOLIO_MOVIMIENTO"].Width = 100;
            Grid.Columns["FOLIO_MOVIMIENTO"].SortMode = DataGridViewColumnSortMode.NotSortable;
            Grid.Columns["FOLIO_MOVIMIENTO"].ReadOnly = true;

            Grid.Columns.Add("CATEGORIA", "CATEGORIA");
            Grid.Columns["CATEGORIA"].Width = 100;
            Grid.Columns["CATEGORIA"].SortMode = DataGridViewColumnSortMode.NotSortable;
            Grid.Columns["CATEGORIA"].ReadOnly = true;

            Grid.Columns.Add("FOLIO_CODIGO", "FOLIO_CODIGO");
            Grid.Columns["FOLIO_CODIGO"].Width = 100;
            Grid.Columns["FOLIO_CODIGO"].SortMode = DataGridViewColumnSortMode.NotSortable;
            Grid.Columns["FOLIO_CODIGO"].ReadOnly = true;

            Grid.Columns.Add("DESCRIPCION_ARTICULO", "DESCRIPCION_ARTICULO");
            Grid.Columns["DESCRIPCION_ARTICULO"].Width = 200;
            Grid.Columns["DESCRIPCION_ARTICULO"].SortMode = DataGridViewColumnSortMode.NotSortable;
            Grid.Columns["DESCRIPCION_ARTICULO"].ReadOnly = true;

            Grid.Columns.Add("CANTIDAD_ACTUAL", "CANTIDAD_ACTUAL");
            Grid.Columns["CANTIDAD_ACTUAL"].Width = 100;
            Grid.Columns["CANTIDAD_ACTUAL"].SortMode = DataGridViewColumnSortMode.NotSortable;
            Grid.Columns["CANTIDAD_ACTUAL"].ReadOnly = true;

            Grid.Columns.Add("STOCK", "STOCK");
            Grid.Columns["STOCK"].Width = 100;
            Grid.Columns["STOCK"].SortMode = DataGridViewColumnSortMode.NotSortable;
            Grid.Columns["STOCK"].ReadOnly = true;

            Grid.Columns.Add("DESCRIPCION_PRESENTACION", "DESCRIPCION_PRESENTACION");
            Grid.Columns["DESCRIPCION_PRESENTACION"].Width = 200;
            Grid.Columns["DESCRIPCION_PRESENTACION"].SortMode = DataGridViewColumnSortMode.NotSortable;
            Grid.Columns["DESCRIPCION_PRESENTACION"].ReadOnly = true;

            Grid.Columns.Add("NOMBRE_PROVEEDOR", "NOMBRE_PROVEEDOR");
            Grid.Columns["NOMBRE_PROVEEDOR"].Width = 200;
            Grid.Columns["NOMBRE_PROVEEDOR"].SortMode = DataGridViewColumnSortMode.NotSortable;
            Grid.Columns["NOMBRE_PROVEEDOR"].ReadOnly = true;

            Grid.Columns.Add("REQUISICION", "REQUISICION");
            Grid.Columns["REQUISICION"].Width = 200;
            Grid.Columns["REQUISICION"].SortMode = DataGridViewColumnSortMode.NotSortable;
            Grid.Columns["REQUISICION"].ReadOnly = true;

            Grid.Columns.Add("FECHA_CADUCIDAD", "FECHA_CADUCIDAD");
            Grid.Columns["FECHA_CADUCIDAD"].Width = 200;
            Grid.Columns["FECHA_CADUCIDAD"].SortMode = DataGridViewColumnSortMode.NotSortable;
            Grid.Columns["FECHA_CADUCIDAD"].ReadOnly = true;

            Grid.Columns.Add("LOTE_CADUCIDAD", "LOTE_CADUCIDAD");
            Grid.Columns["LOTE_CADUCIDAD"].Width = 200;
            Grid.Columns["LOTE_CADUCIDAD"].SortMode = DataGridViewColumnSortMode.NotSortable;
            Grid.Columns["LOTE_CADUCIDAD"].ReadOnly = true;

            Grid.Columns.Add("MARCA_CADUCIDAD", "MARCA_CADUCIDAD");
            Grid.Columns["MARCA_CADUCIDAD"].Width = 200;
            Grid.Columns["MARCA_CADUCIDAD"].SortMode = DataGridViewColumnSortMode.NotSortable;
            Grid.Columns["MARCA_CADUCIDAD"].ReadOnly = true;

            Grid.Columns.Add("OBSERVACION", "OBSERVACION");
            Grid.Columns["OBSERVACION"].Width = 200;
            Grid.Columns["OBSERVACION"].SortMode = DataGridViewColumnSortMode.NotSortable;
            Grid.Columns["OBSERVACION"].ReadOnly = true;

            Grid.Columns.Add("EMPLEADO", "EMPLEADO");
            Grid.Columns["EMPLEADO"].Width = 200;
            Grid.Columns["EMPLEADO"].SortMode = DataGridViewColumnSortMode.NotSortable;
            Grid.Columns["EMPLEADO"].ReadOnly = true;

            Grid.Columns.Add("FECHA_MODIFICACION", "FECHA_MODIFICACION");
            Grid.Columns["FECHA_MODIFICACION"].Width = 200;
            Grid.Columns["FECHA_MODIFICACION"].SortMode = DataGridViewColumnSortMode.NotSortable;
            Grid.Columns["FECHA_MODIFICACION"].ReadOnly = true;

            Grid.Rows.Add();

            Grid.Rows[nRenglon].Cells["No"].Value = "No.";

            Grid.Rows[nRenglon].Cells["FOLIO_MOVIMIENTO"].Value = "FOLIO MOVIMIENTO";
            Grid.Rows[nRenglon].Cells["FOLIO_MOVIMIENTO"].Style = cell;
            Grid.Rows[nRenglon].Cells["CATEGORIA"].Value = "CATEGORIA";
            Grid.Rows[nRenglon].Cells["CATEGORIA"].Style = cell;
            Grid.Rows[nRenglon].Cells["FOLIO_CODIGO"].Value = "FOLIO CODIGO";
            Grid.Rows[nRenglon].Cells["FOLIO_CODIGO"].Style = cell;
            Grid.Rows[nRenglon].Cells["DESCRIPCION_ARTICULO"].Value = "DESCRIPCION ARTICULO";
            Grid.Rows[nRenglon].Cells["DESCRIPCION_ARTICULO"].Style = cell;
            Grid.Rows[nRenglon].Cells["CANTIDAD_ACTUAL"].Value = "CANTIDAD ACTUAL";
            Grid.Rows[nRenglon].Cells["CANTIDAD_ACTUAL"].Style = cell;
            Grid.Rows[nRenglon].Cells["STOCK"].Value = "STOCK";
            Grid.Rows[nRenglon].Cells["STOCK"].Style = cell;

            Grid.Rows[nRenglon].Cells["DESCRIPCION_PRESENTACION"].Value = "DESCRIPCION PRESENTACION";
            Grid.Rows[nRenglon].Cells["DESCRIPCION_PRESENTACION"].Style = cell;
            Grid.Rows[nRenglon].Cells["NOMBRE_PROVEEDOR"].Value = "NOMBRE PROVEEDOR";
            Grid.Rows[nRenglon].Cells["NOMBRE_PROVEEDOR"].Style = cell;
            Grid.Rows[nRenglon].Cells["REQUISICION"].Value = "REQUISICION";
            Grid.Rows[nRenglon].Cells["REQUISICION"].Style = cell;
            Grid.Rows[nRenglon].Cells["FECHA_CADUCIDAD"].Value = "FECHA CADUCIDAD";
            Grid.Rows[nRenglon].Cells["FECHA_CADUCIDAD"].Style = cell;
            Grid.Rows[nRenglon].Cells["LOTE_CADUCIDAD"].Value = "LOTE CADUCIDAD";
            Grid.Rows[nRenglon].Cells["LOTE_CADUCIDAD"].Style = cell;
            Grid.Rows[nRenglon].Cells["MARCA_CADUCIDAD"].Value = "MARCA CADUCIDAD";
            Grid.Rows[nRenglon].Cells["MARCA_CADUCIDAD"].Style = cell;
            Grid.Rows[nRenglon].Cells["OBSERVACION"].Value = "OBSERVACION";
            Grid.Rows[nRenglon].Cells["OBSERVACION"].Style = cell;
            Grid.Rows[nRenglon].Cells["EMPLEADO"].Value = "EMPLEADO";
            Grid.Rows[nRenglon].Cells["EMPLEADO"].Style = cell;
            Grid.Rows[nRenglon].Cells["FECHA_MODIFICACION"].Value = "FECHA_MODIFICACION";
            Grid.Rows[nRenglon].Cells["FECHA_MODIFICACION"].Style = cell;

            Grid.Rows[nRenglon].DefaultCellStyle.BackColor = SystemColors.ControlDarkDark;
            Grid.AllowUserToResizeColumns = true;
            Grid.AllowUserToOrderColumns = false;
            Grid.ColumnHeadersVisible = false;
            Grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            Grid.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            nRenglon++;
        }
    }
}
