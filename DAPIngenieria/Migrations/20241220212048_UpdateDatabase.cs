using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAPIngenieria.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicio_Cliente_IdCliente",
                table: "Servicio");

            migrationBuilder.DropForeignKey(
                name: "FK_Servicio_OrdenTrabajo_IdOrden",
                table: "Servicio");

            migrationBuilder.DropForeignKey(
                name: "FK_Servicio_TipoServicios_IdTipoServicio",
                table: "Servicio");

            migrationBuilder.AddColumn<string>(
                name: "RazonSocial",
                table: "OrdenTrabajo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicio_Cliente_IdCliente",
                table: "Servicio",
                column: "IdCliente",
                principalTable: "Cliente",
                principalColumn: "IdCliente",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Servicio_OrdenTrabajo_IdOrden",
                table: "Servicio",
                column: "IdOrden",
                principalTable: "OrdenTrabajo",
                principalColumn: "IdOrden",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Servicio_TipoServicios_IdTipoServicio",
                table: "Servicio",
                column: "IdTipoServicio",
                principalTable: "TipoServicios",
                principalColumn: "IdTipoServicio",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Servicio_Cliente",
            //    table: "Servicio");

            migrationBuilder.DropForeignKey(
                name: "FK_Servicio_OrdenTrabajo_IdOrden",
                table: "Servicio");

            migrationBuilder.DropForeignKey(
                name: "FK_Servicio_TipoServicios_IdTipoServicio",
                table: "Servicio");

            migrationBuilder.DropColumn(
                name: "RazonSocial",
                table: "OrdenTrabajo");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicio_Cliente_IdCliente",
                table: "Servicio",
                column: "IdCliente",
                principalTable: "Cliente",
                principalColumn: "IdCliente",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Servicio_OrdenTrabajo_IdOrden",
                table: "Servicio",
                column: "IdOrden",
                principalTable: "OrdenTrabajo",
                principalColumn: "IdOrden",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Servicio_TipoServicios_IdTipoServicio",
                table: "Servicio",
                column: "IdTipoServicio",
                principalTable: "TipoServicios",
                principalColumn: "IdTipoServicio",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
