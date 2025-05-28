using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.IO;
using System.Linq;
using ProtelAppT.Data;
using Microsoft.EntityFrameworkCore;
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
            mainPart.Document = new Document(new Body());

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
} 
}