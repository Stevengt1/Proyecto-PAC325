using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Proyecto_PAC325.Migrations
{
    /// <inheritdoc />
    public partial class InitialIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BitacoraEventos",
                columns: table => new
                {
                    IdEvento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    TablaDeEvento = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    TipoDeEvento = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    FechaDeEvento = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DescripcionDeEvento = table.Column<string>(type: "longtext", nullable: false),
                    StackTrace = table.Column<string>(type: "longtext", nullable: true),
                    DatosAnteriores = table.Column<string>(type: "longtext", nullable: true),
                    DatosPosteriores = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BitacoraEventos", x => x.IdEvento);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CAJAS",
                columns: table => new
                {
                    IdCaja = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    IdComercio = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "longtext", nullable: false),
                    Descripcion = table.Column<string>(type: "longtext", nullable: false),
                    TelefonoSINPE = table.Column<string>(type: "longtext", nullable: false),
                    FechaDeRegistro = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FechaDeModificacion = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAJAS", x => x.IdCaja);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "COMERCIOS",
                columns: table => new
                {
                    IdComercio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Identificacion = table.Column<string>(type: "longtext", nullable: false),
                    TipoIdentificacion = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "longtext", nullable: false),
                    TipoDeComercio = table.Column<int>(type: "int", nullable: false),
                    Telefono = table.Column<string>(type: "longtext", nullable: false),
                    CorreoElectronico = table.Column<string>(type: "longtext", nullable: false),
                    Direccion = table.Column<string>(type: "longtext", nullable: false),
                    FechaDeRegistro = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FechaDeModificacion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COMERCIOS", x => x.IdComercio);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "REPORTES_MENSUALES",
                columns: table => new
                {
                    IdReporte = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    IdComercio = table.Column<int>(type: "int", nullable: false),
                    CantidadDeCajas = table.Column<int>(type: "int", nullable: false),
                    MontoTotalRecaudado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CantidadDeSINPES = table.Column<int>(type: "int", nullable: false),
                    MontoTotalComision = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaDelReporte = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REPORTES_MENSUALES", x => x.IdReporte);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SINPE",
                columns: table => new
                {
                    IdSinpe = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    TelefonoOrigen = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    NombreOrigen = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    TelefonoDestinatario = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    NombreDestinatario = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaDeRegistro = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Descripcion = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Estado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SINPE", x => x.IdSinpe);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CONFIGURACIONES_COMERCIOS",
                columns: table => new
                {
                    IdConfiguracion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    IdComercio = table.Column<int>(type: "int", nullable: false),
                    TipoConfiguracion = table.Column<int>(type: "int", nullable: false),
                    Comision = table.Column<int>(type: "int", nullable: false),
                    FechaDeRegistro = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FechaDeModificacion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CONFIGURACIONES_COMERCIOS", x => x.IdConfiguracion);
                    table.ForeignKey(
                        name: "FK_CONFIGURACIONES_COMERCIOS_COMERCIOS_IdComercio",
                        column: x => x.IdComercio,
                        principalTable: "COMERCIOS",
                        principalColumn: "IdComercio",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "USUARIOS",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    IdComercio = table.Column<int>(type: "int", nullable: false),
                    IdNetUser = table.Column<Guid>(type: "char(36)", nullable: true),
                    Nombres = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    PrimerApellido = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    SegundoApellido = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Identificacion = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    CorreoElectronico = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    FechaDeRegistro = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FechaDeModificacion = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Estado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIOS", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK_USUARIOS_COMERCIOS_IdComercio",
                        column: x => x.IdComercio,
                        principalTable: "COMERCIOS",
                        principalColumn: "IdComercio",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CONFIGURACIONES_COMERCIOS_IdComercio",
                table: "CONFIGURACIONES_COMERCIOS",
                column: "IdComercio");

            migrationBuilder.CreateIndex(
                name: "IX_USUARIOS_IdComercio",
                table: "USUARIOS",
                column: "IdComercio");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BitacoraEventos");

            migrationBuilder.DropTable(
                name: "CAJAS");

            migrationBuilder.DropTable(
                name: "CONFIGURACIONES_COMERCIOS");

            migrationBuilder.DropTable(
                name: "REPORTES_MENSUALES");

            migrationBuilder.DropTable(
                name: "SINPE");

            migrationBuilder.DropTable(
                name: "USUARIOS");

            migrationBuilder.DropTable(
                name: "COMERCIOS");
        }
    }
}
