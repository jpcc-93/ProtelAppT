using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using ProtelAppT.Data;
using Microsoft.EntityFrameworkCore;
using Document = QuestPDF.Fluent.Document;
using NPOI.XSSF.UserModel;



namespace ProtelAppT.Service 
{

    public class ReporteService
    {
        private readonly ProtelDbContext _dbContext;

        public ReporteService(ProtelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<byte[]> GenerarReporteWordAsync()
        {
            using var ms = new MemoryStream();

            using (var wordDoc = WordprocessingDocument.Create(ms, WordprocessingDocumentType.Document, true))
            {
                var mainPart = wordDoc.AddMainDocumentPart();
                mainPart.Document = new DocumentFormat.OpenXml.Wordprocessing.Document(new Body());

                var body = mainPart.Document.Body;


                body.AppendChild(new Paragraph(
                    new Run(
                        new Text("Reporte de Factibilidades")
                    )
                ));


                var factibilidades = await _dbContext.FACTIBILIDAD
                    .Include(f => f.Cliente)
                    .Include(f => f.EstadoFactibilidad)
                    .ToListAsync();

                var table = new Table();


                var props = new TableProperties(new TableBorders(
                    new TopBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
                    new BottomBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
                    new LeftBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
                    new RightBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
                    new InsideHorizontalBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
                    new InsideVerticalBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 }
                ));
                table.AppendChild(props);

                var headerRow = new TableRow();
                headerRow.Append(
                    CreateCell("ID"),
                    CreateCell("Cliente"),
                    CreateCell("Proyecto"),
                    CreateCell("Descripción"),
                    CreateCell("Ubicación"),
                    CreateCell("Fecha De Creacion"),
                    CreateCell("Estado")
                );
                table.Append(headerRow);

                foreach (var f in factibilidades)
                {
                    var row = new TableRow();
                    row.Append(
                        CreateCell(f.IdFactibilidad.ToString()),
                        CreateCell(f.Cliente?.Nombre ?? "N/A"),
                        CreateCell(f.NombreProyecto ?? "N/A"),
                        CreateCell(f.Descripcion ?? "N/A"),
                        CreateCell(f.Ubicacion ?? "N/A"),
                        CreateCell(f.FechaSolicitud.ToString("dd/MM/yyyy")),
                        CreateCell(f.EstadoFactibilidad?.Nombre ?? "N/A")
                    );
                    table.Append(row);
                }

                body.Append(table);
            }

            return ms.ToArray();
        }

        private TableCell CreateCell(string text)
        {
            return new TableCell(new Paragraph(new Run(new Text(text ?? ""))));
        }






        public async Task<byte[]> GenerarReportePdfAsync()
        {
            try
            {
                var factibilidades = await _dbContext.FACTIBILIDAD
                    .Include(f => f.Cliente)
                    .Include(f => f.EstadoFactibilidad)
                    .ToListAsync();

                if (!factibilidades.Any())
                    throw new Exception("No hay datos de factibilidades.");

                var pdf = Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Margin(30);
                        page.Size(PageSizes.A4);

                        page.Header().Text("Reporte de Factibilidades").FontSize(20).Bold().AlignCenter();

                        page.Content().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(1); // ID
                                columns.RelativeColumn(2); // Cliente
                                columns.RelativeColumn(2); // Proyecto
                                columns.RelativeColumn(2); // Descripción
                                columns.RelativeColumn(2); // Ubicación
                                columns.RelativeColumn(2); // Fecha
                                columns.RelativeColumn(2); // Estado
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("ID").Bold();
                                header.Cell().Text("Cliente").Bold();
                                header.Cell().Text("Proyecto").Bold();
                                header.Cell().Text("Descripción").Bold();
                                header.Cell().Text("Ubicación").Bold();
                                header.Cell().Text("Fecha").Bold();
                                header.Cell().Text("Estado").Bold();
                            });

                            foreach (var f in factibilidades)
                            {
                                table.Cell().Text(f.IdFactibilidad.ToString());
                                table.Cell().Text(f.Cliente?.Nombre ?? "N/A");
                                table.Cell().Text(f.NombreProyecto ?? "N/A");
                                table.Cell().Text(f.Descripcion ?? "N/A");
                                table.Cell().Text(f.Ubicacion ?? "N/A");
                                table.Cell().Text(f.FechaSolicitud.ToString("dd/MM/yyyy"));
                                table.Cell().Text(f.EstadoFactibilidad?.Nombre ?? "N/A");
                            }
                        });

                        page.Footer().AlignCenter().Text(x =>
                        {
                            x.Span("ProtelAppT - Página ").FontSize(10);
                            x.CurrentPageNumber().FontSize(10);
                        });
                    });
                });

                return pdf.GeneratePdf();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR PDF: " + ex.Message);
                throw;
            }
        }







        public async Task<byte[]> GenerarReporteExcelAsync()
        {
            var factibilidades = await _dbContext.FACTIBILIDAD
                .Include(f => f.Cliente)
                .Include(f => f.EstadoFactibilidad)
                .Select(f => new
                {
                    f.IdFactibilidad,
                    Cliente = f.Cliente.Nombre,
                    proyecto = f.NombreProyecto,
                    descripcion = f.Descripcion,
                    ubicacion = f.Ubicacion,
                    Inicio = f.FechaSolicitud,
                    Estado = f.EstadoFactibilidad.Nombre
                })
                .ToListAsync();

            var workbook = new XSSFWorkbook();
            var sheet = workbook.CreateSheet("Factibilidad");

            var headerRow = sheet.CreateRow(0);
            headerRow.CreateCell(0).SetCellValue("Id Factibilidad");
            headerRow.CreateCell(1).SetCellValue("Cliente");
            headerRow.CreateCell(2).SetCellValue("Proyecto");
            headerRow.CreateCell(3).SetCellValue("Descripción");
            headerRow.CreateCell(4).SetCellValue("Ubicación");
            headerRow.CreateCell(5).SetCellValue("Fecha Solicitud");
            headerRow.CreateCell(6).SetCellValue("Estado");

            for (int i = 0; i < factibilidades.Count; i++)
            {
                var f = factibilidades[i];
                var row = sheet.CreateRow(i + 1);
                row.CreateCell(0).SetCellValue(f.IdFactibilidad);
                row.CreateCell(1).SetCellValue(f.Cliente);
                row.CreateCell(2).SetCellValue(f.proyecto);
                row.CreateCell(3).SetCellValue(f.descripcion);
                row.CreateCell(4).SetCellValue(f.ubicacion);
                row.CreateCell(5).SetCellValue(f.Inicio.ToString("dd/MM/yyyy"));
                row.CreateCell(6).SetCellValue(f.Estado);
            }

            using var stream = new MemoryStream();
            workbook.Write(stream);
            return stream.ToArray();
        }
    }
}