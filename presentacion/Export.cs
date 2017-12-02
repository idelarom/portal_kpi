using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web;

namespace presentacion
{
    /// <summary>
    /// Metodos que exportan DataTables o DataSet Tipados
    /// </summary>
    public class Export
    {
        //string Title, XLColor HeaderBackgroundColor, XLColor HeaderForeColor, int HeaderFont,
        //                      bool DateRange, string FromDate, XLColor DateRangeBackgroundColor,
        //                      XLColor DateRangeForeColor, int DateRangeFont, List<System.Data.DataTable> gv, XLColor ColumnBackgroundColor,
        //                      XLColor ColumnForeColor, string[] SheetName, int NumberSheet, string FileName, HttpResponse response
        /// <summary>
        /// Exporta Contenido de uno o mas DataTable a Excel
        /// </summary>
        public string toExcel(string Title, XLColor HeaderBackgroundColor, XLColor HeaderForeColor, int HeaderFont,
                              bool DateRange, string FromDate, XLColor DateRangeBackgroundColor,
                              XLColor DateRangeForeColor, int DateRangeFont, List<System.Data.DataTable> gv, XLColor ColumnBackgroundColor,
                              XLColor ColumnForeColor, string[] SheetName, int NumberSheet, string FileName, HttpResponse response)
        {
            try
            {
                //Creamos nuevo Arhcivo
                var wb = new XLWorkbook();
                // agregamos libros a archivo
                for (int sheets_num = 0; sheets_num < NumberSheet; sheets_num++)
                {
                    System.Data.DataTable table = gv[sheets_num];
                    if (table.Rows.Count != 0)
                    {
                        var ws = wb.Worksheets.Add(SheetName[sheets_num]);
                        //Titulo
                        ws.Cell("A1").Value = Title;
                        ws.Cell("A2").Value = "Creado: " + FromDate;
                        //columnas
                        string[] cols = new string[table.Columns.Count];
                        for (int c = 0; c < table.Columns.Count; c++)
                        {
                            var a = table.Columns[c].ToString();
                            cols[c] = table.Columns[c].ToString().Replace('_', ' ');
                        }

                        char StartCharCols = 'A';
                        int StartIndexCols = 3;

                        #region CreatingColumnHeaders

                        for (int i = 1; i <= cols.Length; i++)
                        {
                            if (i == cols.Length)
                            {
                                string DataCell = StartCharCols.ToString() + StartIndexCols.ToString();
                                ws.Cell(DataCell).Value = cols[i - 1];
                                ws.Cell(DataCell).WorksheetColumn().Width = cols[i - 1].ToString().Length + 10;
                                ws.Cell(DataCell).Style.Font.Bold = true;
                                ws.Cell(DataCell).Style.Fill.BackgroundColor = ColumnBackgroundColor;
                                ws.Cell(DataCell).Style.Font.FontColor = ColumnForeColor;
                            }
                            else
                            {
                                string DataCell = StartCharCols.ToString() + StartIndexCols.ToString();
                                ws.Cell(DataCell).Value = cols[i - 1];
                                ws.Cell(DataCell).WorksheetColumn().Width = cols[i - 1].ToString().Length + 10;
                                ws.Cell(DataCell).Style.Font.Bold = true;
                                ws.Cell(DataCell).Style.Fill.BackgroundColor = ColumnBackgroundColor;
                                ws.Cell(DataCell).Style.Font.FontColor = ColumnForeColor;
                                StartCharCols++;
                            }
                        }

                        #endregion CreatingColumnHeaders

                        string Range = "A1:" + StartCharCols.ToString() + "1";
                        ws.Range(Range).Merge();
                        ws.Range(Range).Style.Font.FontSize = HeaderFont;
                        ws.Range(Range).Style.Font.Bold = true;
                        ws.Range(Range).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                        ws.Range(Range).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        if (HeaderBackgroundColor != null && HeaderForeColor != null)
                        {
                            ws.Range(Range).Style.Fill.BackgroundColor = HeaderBackgroundColor;
                            ws.Range(Range).Style.Font.FontColor = HeaderForeColor;
                        }

                        //estilo
                        Range = "A2:" + StartCharCols.ToString() + "2";
                        ws.Range(Range).Merge();
                        ws.Range(Range).Style.Font.FontSize = 10;
                        ws.Range(Range).Style.Font.Bold = true;
                        ws.Range(Range).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Bottom);
                        ws.Range(Range).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                        Range = "A3:" + StartCharCols.ToString() + "3";
                        ws.Range(Range).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        ws.Range(Range).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        ws.Range(Range).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        ws.Range(Range).Style.Border.BottomBorder = XLBorderStyleValues.Thin;

                        char StartCharData = 'A';
                        int StartIndexData = 4;

                        char StartCharDataCol = char.MinValue;
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            for (int j = 0; j < table.Columns.Count; j++)
                            {
                                string DataCell = StartCharData.ToString() + StartIndexData;
                                var a = table.Rows[i][j].ToString();
                                a = a.Replace("&nbsp;", " ");
                                a = a.Replace("&amp;", "&");
                                //verificamos que sea entero
                                int val = 0;
                                DateTime dt = DateTime.Now;
                                if (int.TryParse(a, out val))
                                {
                                    ws.Cell(DataCell).Value = val;
                                }
                                else if (DateTime.TryParse(a, out dt))
                                {
                                    ws.Cell(DataCell).Value = dt.ToShortDateString();
                                }
                                ws.Cell(DataCell).SetValue(a);
                                StartCharData++;
                            }
                            StartCharData = 'A';
                            StartIndexData++;
                        }

                        char LastChar = Convert.ToChar(StartCharData + table.Columns.Count - 1);
                        int TotalRows = table.Rows.Count + 3;
                        Range = "A4:" + LastChar + TotalRows;
                        ws.Range(Range).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        ws.Range(Range).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        ws.Range(Range).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        ws.Range(Range).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    }
                }
                //Mandamos response para guardar
                HttpResponse httpResponse = response;
                httpResponse.Clear();
                httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                httpResponse.AddHeader("content-disposition", "attachment;filename=" + FileName);

                // Cargamos datos de libros en archivo
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    wb.SaveAs(memoryStream);
                    memoryStream.WriteTo(httpResponse.OutputStream);
                    memoryStream.Close();
                }
                // volcamos el stream
                httpResponse.Flush();
                httpResponse.Close();
                return "";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        /// <summary>
        /// Exporta a PDF DataTable
        /// </summary>
        public string ToPdf(string NameDoc, List<System.Data.DataTable> dtable, int NumberTables, string[] TableName, HttpResponse response)
        {
            try
            {
                iTextSharp.text.Font myfont = FontFactory.GetFont("Tahoma", BaseFont.IDENTITY_H, 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
                Document document = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
                System.IO.MemoryStream mStream = new System.IO.MemoryStream();
                PdfWriter writer = PdfWriter.GetInstance(document, mStream);
                iTextSharp.text.Font titlefont = FontFactory.GetFont("Tahoma", BaseFont.IDENTITY_H, 7, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);
                iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 6);
                document.Open();
                for (int T = 0; T < NumberTables; T++)
                {
                    if (dtable[T].Rows.Count != 0)
                    {
                        document.NewPage();
                        System.Data.DataTable dt = dtable[T];
                        PdfPTable table = new PdfPTable(dt.Columns.Count);
                        PdfPRow row = null;
                        table.WidthPercentage = 90;
                        int iCol = 0;
                        string colname = "";
                        PdfPCell cell = new PdfPCell(new Phrase("Reporte"));
                        cell.Colspan = dt.Columns.Count;
                        foreach (DataColumn c in dt.Columns)
                        {
                            table.AddCell(new Phrase(c.ColumnName, titlefont));
                        }
                        foreach (DataRow r in dt.Rows)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                for (int i = 0; i < dt.Columns.Count; i++)
                                {
                                    table.AddCell(new Phrase(r[i].ToString(), font5));
                                }
                            }
                        }
                        iTextSharp.text.Font fuente = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12);//LA FUENTE DE NUESTRO TEXTO
                        Paragraph Titulo = new Paragraph(TableName[T], fuente);
                        Titulo.Alignment = Element.ALIGN_CENTER;
                        Paragraph Fecha = new Paragraph(DateTime.Now.ToString("D", CultureInfo.CreateSpecificCulture("es-MX")));
                        Fecha.Alignment = Element.ALIGN_CENTER;
                        Paragraph espacio = new Paragraph(" ");
                        // Creamos la imagen y le ajustamos el tamaño
                        iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance("http://gamamateriales.com.mx/wp-content/uploads/2015/07/Logo-GAMA-MATERIALES_web.png");
                        float percentage = 1f;
                        percentage = 150 / jpg.Width;
                        jpg.ScalePercent(percentage * 50);
                        jpg.Alignment = iTextSharp.text.Image.TEXTWRAP | iTextSharp.text.Image.ALIGN_CENTER;
                        
                        document.Add(Titulo);
                        document.Add(Fecha);
                        document.Add(espacio);
                        document.Add(table);
                    }
                }
                document.Close();
                //response para guardar
                response.ContentType = "application/octet-stream";
                response.AddHeader("Content-Disposition", "attachment; filename=" + NameDoc + ".pdf");
                response.Clear();
                response.BinaryWrite(mStream.ToArray());
                // volcamos el stream
                response.Flush();
                response.End();
                return "";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

       
    }
}