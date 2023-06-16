using CFACADECONN;
using CFACADEFUN;
using CFACADESTRUC;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UCCOMBOBOX;
using System.Diagnostics;

using iText;
using iText.IO.Image;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Kernel.Font;
using iText.IO.Font;
using iText.Kernel.Pdf.Canvas;
using System.Data.Odbc;
using iText.Kernel.Pdf.Annot.DA;
using iText.IO.Font.Constants;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout.Properties;
using iText.Kernel.Colors;
using iText.Forms;
using iText.Kernel.Pdf.Navigation;
using iText.Kernel.Utils;

namespace CONSULTARINFOALMACEN
{
    public partial class frmConsultarInformacion : CControl
    {
        CEstructura ep;
        string sTitulo = "CONSULTAR INFORMACIÓN ALMACEN";
        Int32 nRenglon;
        //PdfFont font = PdfFontFactory.CreateFont("c:/windows/fonts/Arial.ttf", PdfEncodings.IDENTITY_H);

        public frmConsultarInformacion(CEstructura ed)
        {
            InitializeComponent();
            ep = ed;
        }

        private void frmConsultarInformacion_Load(object sender, EventArgs e)
        {
            HabilitarTeclaEscape = true;
            HabilitarTeclasSalir = true;

            AgregarControl(rdbCodigo, null, "", false);
            AgregarControl(rdbCategoria, null, "", false);
            AgregarControl(rdbLote, null, "", false);
            AgregarControl(rdbActivos, null, "", false);
            AgregarControl(rdbInactivos, null, "", false);
            AgregarControl(cmbCodigos, null, "", false);
            AgregarControl(cmbCategorias, null, "", false);
            AgregarControl(chkRangoFechas, null, "", false);
            AgregarControl(dtpInicio, null, "", false);
            AgregarControl(dtpFin, null, "", false);
            AgregarControl(txtLote, null, "", false);

            AgregarControl(btnConsultar, null, "", false);
            AgregarControl(Grid, null, "", true);

            // Botones
            AgregarControl(btnPdf, null, "", false);
            AgregarControl(btnLimpiar, null, "", false);
            AgregarControl(btnSalir, null, "", false);

            fInicializa();
        }

        public void fInicializa()
        {
            fLimpiarInformacion(gbGral);

            cmbCodigos.DataSource = null;
            cmbCategorias.DataSource = null;

            string sPresentacion = "SELECT descripcion, identificador FROM cmb_mostrar_categoria_articulos(0::SMALLINT)";
            cmbComboBox.fLlenarComboBoxPostgres(ep, sTitulo, sPresentacion, 3, cmbCodigos, 1);

            sPresentacion = "SELECT descripcion, identificador FROM cmb_mostrar_categoria_articulos(2::SMALLINT)";
            cmbComboBox.fLlenarComboBoxPostgres(ep, sTitulo, sPresentacion, 3, cmbCategorias, 1);

            rdbCodigo.Select();

            rdbCodigo.Checked = true;
            rdbActivos.Checked = true;
            cmbCodigos.Enabled = true;
            cmbCategorias.Enabled = false;

            dtpInicio.Enabled = false;
            dtpFin.Enabled = false;

            chkRangoFechas.Checked = false;
        }

        public static void fLimpiarInformacion(System.Windows.Forms.GroupBox gb)
        {
            foreach (System.Windows.Forms.Control c in gb.Controls)
            {
                if (c is System.Windows.Forms.TextBox || c is System.Windows.Forms.RichTextBox)
                {
                    c.Text = "";
                }
                else if (c is System.Windows.Forms.ComboBox)
                {
                    var tmp = c as System.Windows.Forms.ComboBox;
                    tmp.DataSource = null;
                    tmp.Items.Clear();
                }
                else if (c is DataGridView)
                {
                    var tmp = c as DataGridView;
                    tmp.Rows.Clear();
                    tmp.Columns.Clear();
                }
                else if (c is System.Windows.Forms.CheckBox)
                {
                    var tmp = c as System.Windows.Forms.CheckBox;
                    tmp.Checked = false;
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            fInicializa();
        }

        private void rdbCodigo_CheckedChanged(object sender, EventArgs e)
        {
            cmbCodigos.Enabled = true;
            cmbCategorias.Enabled= false;
            txtLote.Enabled = false;
        }

        private void rdbCategoria_CheckedChanged(object sender, EventArgs e)
        {
            cmbCodigos.Enabled= false;
            cmbCategorias.Enabled = true;
            txtLote.Enabled = false;
        }

        private void rdbLote_CheckedChanged(object sender, EventArgs e)
        {
            cmbCodigos.Enabled = false;
            cmbCategorias.Enabled = false;
            txtLote.Enabled = true;
        }

        private void chkRangoFechas_CheckedChanged(object sender, EventArgs e)
        {
            string sFecha;

            if (chkRangoFechas.Checked)
            {
                dtpInicio.Enabled = true;
                dtpFin.Enabled = true;
            }
            else
            {
                dtpInicio.Enabled = false;
                dtpFin.Enabled = false;

                dtpInicio.Format = DateTimePickerFormat.Custom;
                dtpInicio.CustomFormat = "dd/MM/yyyy";
                sFecha = CFuncionesGral.fConsultarFechaServerPostgres(ep, ref sError);
                dtpInicio.Text = sFecha.ToString();
                dtpFin.Text = sFecha.ToString();
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            string sDato, sDato1, sDescripcionArticulo = "", sFechaInicial = "1900-01-01", sFechaFinal = "1900-01-01", sLote = "";
            char sDelimitador = '-', sDelimitador1 = '|';
            Int32 nCategoria = 0, nFolio = 0, nOpcion = 0, nDescontinuado = 0, nConsultaFechas = 0;
            bool bConsultar = true;

            Grid.Rows.Clear();
            Grid.Columns.Clear();

            nRenglon = 0;
            CEncabezadoGrid.fEncabezadoGrid(ep, Grid, sTitulo, ref nRenglon);

            if (rdbCodigo.Checked)
            {
                sDato = cmbCodigos.Text;
                if (!String.IsNullOrEmpty(sDato))
                {
                    string[] valores = sDato.Split(sDelimitador1);
                    sDato1 = valores[0].ToString();
                    sDescripcionArticulo = valores[1].ToString();
                    string[] valor = sDato1.Split(sDelimitador);
                    nCategoria = Convert.ToInt32(valor[0].ToString());
                    nFolio = Convert.ToInt32(valor[1].ToString());
                    nOpcion = 0;
                }
                else
                {
                    MessageBox.Show("Favor de revisar la información, que se desea consultar!!!", sTitulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bConsultar = false;
                }
            }
            else if (rdbCategoria.Checked)
            {
                sDato1 = cmbCategorias.Text.ToString();
                if (!String.IsNullOrEmpty(sDato1))
                {
                    string[] valor = sDato1.Split('-');
                    nCategoria = Convert.ToInt32(valor[0].ToString());
                    nOpcion = 1;
                }
                else
                {
                    MessageBox.Show("Favor de revisar la información que se desea consultar!!!", sTitulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bConsultar = false;
                }
            }
            else if (rdbLote.Checked)
            {
                sLote = txtLote.Text.ToString();
                if (String.IsNullOrEmpty(sLote))
                {
                    MessageBox.Show("Favor de proporcionar un LOTE!!!", sTitulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bConsultar=false;
                }
                nOpcion = 2;
            }

            if (rdbActivos.Checked)
            {
                nDescontinuado = 0;
            }
            else if (rdbInactivos.Checked)
            {
                nDescontinuado = 1;
            }

            if (chkRangoFechas.Checked)
            {
                var nLongituTexto = 0;

                sDato = dtpInicio.Text.ToString();
                nLongituTexto = 0;
                nLongituTexto = sDato.Length;
                sFechaInicial = string.Format("{0}-{1}-{2}", sDato.ToString().Substring(nLongituTexto - 4, 4), sDato.ToString().Substring(3, 2), sDato.ToString().Substring(0, 2));

                sDato = dtpFin.Text.ToString();
                nLongituTexto = 0;
                nLongituTexto = sDato.Length;
                sFechaFinal = string.Format("{0}-{1}-{2}", sDato.ToString().Substring(nLongituTexto - 4, 4), sDato.ToString().Substring(3, 2), sDato.ToString().Substring(0, 2));

                nConsultaFechas = 1;
            }

            if (bConsultar && fValidarFechas())
            {
                CMostrarInformacionGrid.fMostrarInformacionGrid(ep, Grid, sTitulo, nCategoria, nFolio, sDescripcionArticulo, ref nRenglon, nOpcion, nConsultaFechas, sFechaInicial, sFechaFinal, nDescontinuado, sLote);
            }
        }

        public bool fValidarFechas()
        {
            string sFechaInicial, sFechaFinal, sDato;
            Int32 nLongituTexto = 0;
            bool valorRegresa = true;

            sDato = dtpInicio.Text.ToString();
            nLongituTexto = 0;
            nLongituTexto = sDato.Length;
            sFechaInicial = string.Format("{0}-{1}-{2}", sDato.ToString().Substring(nLongituTexto - 4, 4), sDato.ToString().Substring(3, 2), sDato.ToString().Substring(0, 2));

            sDato = dtpFin.Text.ToString();
            nLongituTexto = 0;
            nLongituTexto = sDato.Length;
            sFechaFinal = string.Format("{0}-{1}-{2}", sDato.ToString().Substring(nLongituTexto - 4, 4), sDato.ToString().Substring(3, 2), sDato.ToString().Substring(0, 2));

            if(!CFuncionesGral.fValidarFechasPostgres(ep, sFechaInicial, sFechaFinal))
            {
                MessageBox.Show("La fecha final, no puede ser menor que la fecha inicial!!!", sTitulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
                valorRegresa = false;
            }

            return valorRegresa;
        }

        private void btnPdf_Click(object sender, EventArgs e)
        {
            if (Grid.RowCount <= 0)
            {
                MessageBox.Show("No existe información para exportar!!!", sTitulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                fGenerarPDF();
            }
        }

        public void fGenerarPDF()
        {
            string sDireccion = @"C:\LESP\FORMATOS\", sDato = "";
            Int32 nContador = 0;
            bool valorRegresa = false;
            Int32 numberOfPages = 0, nTamanioFuente = 10, nTamanioFuenteGrid = 8;
            List<string> encabezado = new List<string>();
            iText.Layout.Element.Paragraph p = new Paragraph();

            if (MessageBox.Show("¿Desea generar el archivo PDF?", sTitulo, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                while (nContador < 2)
                {
                    if (Directory.Exists(sDireccion))
                    {
                        valorRegresa = true;
                        nContador = 3;
                    }
                    else
                    {
                        Directory.CreateDirectory(@sDireccion);
                    }

                    nContador++;
                }

                try
                {
                    if(valorRegresa)
                    {
                        var sRuta = @"C:\LESP\FORMATOS\Control_inventarios.pdf";

                        PdfWriter writer = new PdfWriter(sRuta);
                        PdfDocument pdfDoc = new PdfDocument(writer);
                        Document doc = new Document(pdfDoc, PageSize.A4.Rotate(), false);

                        doc.SetMargins(30, 20, 20, 20);

                        p = new Paragraph()
                            .Add("CONTROL DE ALMACEN")
                            .SetMultipliedLeading(2)
                            .SetFontSize(14)
                            .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                            .SetTextRenderingMode(PdfCanvasConstants.TextRenderingMode.FILL_STROKE)
                            .SetStrokeWidth(0.5f);
                        doc.Add(p);

                        encabezado.Add("FOLIO MOVIMIENTO");
                        encabezado.Add("DESCRIPCION");
                        encabezado.Add("CANTIDAD ACTUAL");
                        encabezado.Add("STOCK");
                        encabezado.Add("DESCRIPCION PRESENTACION");
                        encabezado.Add("REQUISICION");
                        encabezado.Add("FECHA CADUCIDAD");
                        encabezado.Add("LOTE CADUCIDAD");
                        encabezado.Add("MARCA CADUCIDAD");
                        encabezado.Add("OBSERVACION");
                        encabezado.Add("FECHA MODIFICACION");

                        Table table = new Table(11);

                        Cell cell = new Cell();
                        foreach (var dato in encabezado)
                        {
                            cell = new Cell();
                            cell.Add(new Paragraph(dato.ToString()).SetFontSize(nTamanioFuente).SetBold()).SetBackgroundColor(ColorConstants.LIGHT_GRAY);
                            table.AddCell(cell);
                        }

                        for (var i = 1; i < Grid.RowCount - 1; i++)
                        {
                            sDato = "";
                            if(Convert.ToInt32(Grid["FOLIO_MOVIMIENTO", i].Value.ToString()) != 0)
                            {
                                sDato = Grid["FOLIO_MOVIMIENTO", i].Value.ToString();
                            }
                            cell = new Cell();
                            cell.Add(new Paragraph(sDato).SetFontSize(nTamanioFuenteGrid).SetBold());
                            table.AddCell(cell);

                            sDato = Grid["CATEGORIA", i].Value.ToString() + " - " + Grid["FOLIO_CODIGO", i].Value.ToString() + " " + Grid["DESCRIPCION_ARTICULO", i].Value.ToString();
                            cell = new Cell();
                            cell.Add(new Paragraph(sDato).SetFontSize(nTamanioFuenteGrid).SetBold());
                            table.AddCell(cell);

                            sDato = Grid["CANTIDAD_ACTUAL", i].Value.ToString();
                            cell = new Cell();
                            cell.Add(new Paragraph(sDato).SetFontSize(nTamanioFuenteGrid).SetBold());
                            table.AddCell(cell);

                            sDato = Grid["STOCK", i].Value.ToString();
                            cell = new Cell();
                            cell.Add(new Paragraph(sDato).SetFontSize(nTamanioFuenteGrid).SetBold());
                            table.AddCell(cell);

                            sDato = Grid["DESCRIPCION_PRESENTACION", i].Value.ToString();
                            cell = new Cell();
                            cell.Add(new Paragraph(sDato).SetFontSize(nTamanioFuenteGrid).SetBold());
                            table.AddCell(cell);

                            sDato = Grid["REQUISICION", i].Value.ToString();
                            cell = new Cell();
                            cell.Add(new Paragraph(sDato).SetFontSize(nTamanioFuenteGrid).SetBold());
                            table.AddCell(cell);

                            sDato = "";
                            if(Grid["FECHA_CADUCIDAD", i].Value != null)
                            {
                                sDato = Grid["FECHA_CADUCIDAD", i].Value.ToString();
                            }
                            cell = new Cell();
                            cell.Add(new Paragraph(sDato).SetFontSize(nTamanioFuenteGrid).SetBold());
                            table.AddCell(cell);

                            sDato = "";
                            if (Grid["LOTE_CADUCIDAD", i].Value != null)
                            {
                                sDato = Grid["LOTE_CADUCIDAD", i].Value.ToString();
                            }
                            cell = new Cell();
                            cell.Add(new Paragraph(sDato).SetFontSize(nTamanioFuenteGrid).SetBold());
                            table.AddCell(cell);

                            sDato = "";
                            if (Grid["MARCA_CADUCIDAD", i].Value != null)
                            {
                                sDato = Grid["MARCA_CADUCIDAD", i].Value.ToString();
                            }
                            cell = new Cell();
                            cell.Add(new Paragraph(sDato).SetFontSize(nTamanioFuenteGrid).SetBold());
                            table.AddCell(cell);

                            sDato = "";
                            if (Grid["OBSERVACION", i].Value != null)
                            {
                                sDato = Grid["OBSERVACION", i].Value.ToString();
                            }
                            cell = new Cell();
                            cell.Add(new Paragraph(sDato).SetFontSize(nTamanioFuenteGrid).SetBold());
                            table.AddCell(cell);

                            sDato = Grid["FECHA_MODIFICACION", i].Value.ToString();
                            cell = new Cell();
                            cell.Add(new Paragraph(sDato).SetFontSize(nTamanioFuenteGrid).SetBold());
                            table.AddCell(cell);
                        }

                        doc.Add(table);

                        numberOfPages = pdfDoc.GetNumberOfPages();
                        for (int i = 1; i <= numberOfPages; i++)
                        {
                            doc.ShowTextAligned(new Paragraph("pagína " + i + " de " + numberOfPages),
                                    430, 40, i, TextAlignment.CENTER, VerticalAlignment.TOP, 0);
                        }

                        doc.Close();
                        Process.Start(sRuta);
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Se presento un problema al generar el PDF: " + ex.Message.ToString(), sTitulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
