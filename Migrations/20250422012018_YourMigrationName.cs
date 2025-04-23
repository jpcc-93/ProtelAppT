using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProtelAppT.Migrations
{
    /// <inheritdoc />
    public partial class YourMigrationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AUTORIZACION",
                columns: table => new
                {
                    ID_AUTORIZACION = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOMBRE_AUTORIZACION = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DESCRIPCION_AUTORIZACION = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AUTORIZACION", x => x.ID_AUTORIZACION);
                });

            migrationBuilder.CreateTable(
                name: "ESTADOCLIENTE",
                columns: table => new
                {
                    ID_ESTADO_CLIENTE = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOMBRE = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ESTADOCLIENTE", x => x.ID_ESTADO_CLIENTE);
                });

            migrationBuilder.CreateTable(
                name: "ESTADOFACTIBILIDAD",
                columns: table => new
                {
                    ID_ESTADO_FACTIBILIDAD = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOMBRE = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ESTADOFACTIBILIDAD", x => x.ID_ESTADO_FACTIBILIDAD);
                });

            migrationBuilder.CreateTable(
                name: "ROL",
                columns: table => new
                {
                    ID_ROL = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOMBRE = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROL", x => x.ID_ROL);
                });

            migrationBuilder.CreateTable(
                name: "CLIENTE",
                columns: table => new
                {
                    ID_CLIENTE = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOMBRE = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DIRECCION = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TELEFONO = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EMAIL = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FECHA_CREACION = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FECHA_ACTUALIZACION = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ID_ESTADO_CLIENTE = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLIENTE", x => x.ID_CLIENTE);
                    table.ForeignKey(
                        name: "FK_CLIENTE_ESTADOCLIENTE_ID_ESTADO_CLIENTE",
                        column: x => x.ID_ESTADO_CLIENTE,
                        principalTable: "ESTADOCLIENTE",
                        principalColumn: "ID_ESTADO_CLIENTE",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ASIGNACIONROLAUTORIZACION",
                columns: table => new
                {
                    ID_ASIGNACION = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_ROL = table.Column<int>(type: "int", nullable: false),
                    ID_AUTORIZACION = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASIGNACIONROLAUTORIZACION", x => x.ID_ASIGNACION);
                    table.ForeignKey(
                        name: "FK_ASIGNACIONROLAUTORIZACION_AUTORIZACION_ID_AUTORIZACION",
                        column: x => x.ID_AUTORIZACION,
                        principalTable: "AUTORIZACION",
                        principalColumn: "ID_AUTORIZACION",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ASIGNACIONROLAUTORIZACION_ROL_ID_ROL",
                        column: x => x.ID_ROL,
                        principalTable: "ROL",
                        principalColumn: "ID_ROL",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USUARIO",
                columns: table => new
                {
                    CORREO = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NOMBRE = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PASSWORD_HASH = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ID_ROL = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO", x => x.CORREO);
                    table.ForeignKey(
                        name: "FK_USUARIO_ROL_ID_ROL",
                        column: x => x.ID_ROL,
                        principalTable: "ROL",
                        principalColumn: "ID_ROL",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FACTIBILIDAD",
                columns: table => new
                {
                    ID_FACTIBILIDAD = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_CLIENTE = table.Column<int>(type: "int", nullable: false),
                    NOMBRE_PROYECTO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DESCRIPCION = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UBICACION = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FECHA_SOLICITUD = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FECHA_RESPUESTA = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ID_ESTADO_FACTIBILIDAD = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FACTIBILIDAD", x => x.ID_FACTIBILIDAD);
                    table.ForeignKey(
                        name: "FK_FACTIBILIDAD_CLIENTE_ID_CLIENTE",
                        column: x => x.ID_CLIENTE,
                        principalTable: "CLIENTE",
                        principalColumn: "ID_CLIENTE",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FACTIBILIDAD_ESTADOFACTIBILIDAD_ID_ESTADO_FACTIBILIDAD",
                        column: x => x.ID_ESTADO_FACTIBILIDAD,
                        principalTable: "ESTADOFACTIBILIDAD",
                        principalColumn: "ID_ESTADO_FACTIBILIDAD",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ASIGNACIONROLAUTORIZACION_ID_AUTORIZACION",
                table: "ASIGNACIONROLAUTORIZACION",
                column: "ID_AUTORIZACION");

            migrationBuilder.CreateIndex(
                name: "IX_ASIGNACIONROLAUTORIZACION_ID_ROL",
                table: "ASIGNACIONROLAUTORIZACION",
                column: "ID_ROL");

            migrationBuilder.CreateIndex(
                name: "IX_CLIENTE_ID_ESTADO_CLIENTE",
                table: "CLIENTE",
                column: "ID_ESTADO_CLIENTE");

            migrationBuilder.CreateIndex(
                name: "IX_FACTIBILIDAD_ID_CLIENTE",
                table: "FACTIBILIDAD",
                column: "ID_CLIENTE");

            migrationBuilder.CreateIndex(
                name: "IX_FACTIBILIDAD_ID_ESTADO_FACTIBILIDAD",
                table: "FACTIBILIDAD",
                column: "ID_ESTADO_FACTIBILIDAD");

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_ID_ROL",
                table: "USUARIO",
                column: "ID_ROL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ASIGNACIONROLAUTORIZACION");

            migrationBuilder.DropTable(
                name: "FACTIBILIDAD");

            migrationBuilder.DropTable(
                name: "USUARIO");

            migrationBuilder.DropTable(
                name: "AUTORIZACION");

            migrationBuilder.DropTable(
                name: "CLIENTE");

            migrationBuilder.DropTable(
                name: "ESTADOFACTIBILIDAD");

            migrationBuilder.DropTable(
                name: "ROL");

            migrationBuilder.DropTable(
                name: "ESTADOCLIENTE");
        }
    }
}
