using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NAF.Infra.Data.Migrations
{
    public partial class EntitiesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Area",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DtAlteracao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Codigo = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DtInclusao = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Area", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PerguntaFrequente",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Pergunta = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Resposta = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DtAlteracao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Codigo = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DtInclusao = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerguntaFrequente", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Identificador = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TelefoneCelular = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DocumentoFederal = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TipoPerfil = table.Column<int>(type: "int", nullable: false),
                    Situacao = table.Column<int>(type: "int", nullable: false),
                    IdentityUserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Codigo = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DtInclusao = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_AspNetUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Servico",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descricao = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DtAlteracao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CodigoArea = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    AreaId = table.Column<long>(type: "bigint", nullable: true),
                    Codigo = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DtInclusao = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Servico_Area_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Area",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Chamado",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CodigoOperador = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    Titulo = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descricao = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Situacao = table.Column<int>(type: "int", nullable: false),
                    DtAlteracao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CodigoUsuario = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UsuarioId = table.Column<long>(type: "bigint", nullable: true),
                    CodigoServico = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ServicoId = table.Column<long>(type: "bigint", nullable: true),
                    Codigo = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DtInclusao = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chamado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chamado_Servico_ServicoId",
                        column: x => x.ServicoId,
                        principalTable: "Servico",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Chamado_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ChamadoComentario",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Mensagem = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DtAlteracao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CodigoUsuario = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UsuarioId = table.Column<long>(type: "bigint", nullable: true),
                    CodigoChamado = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ChamadoId = table.Column<long>(type: "bigint", nullable: true),
                    Codigo = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DtInclusao = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChamadoComentario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChamadoComentario_Chamado_ChamadoId",
                        column: x => x.ChamadoId,
                        principalTable: "Chamado",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChamadoComentario_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ChamadoDocumento",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NomeArquivo = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Arquivo = table.Column<byte[]>(type: "longblob", nullable: true),
                    CodigoUsuario = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UsuarioId = table.Column<long>(type: "bigint", nullable: true),
                    CodigoChamado = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ChamadoId = table.Column<long>(type: "bigint", nullable: true),
                    Codigo = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DtInclusao = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChamadoDocumento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChamadoDocumento_Chamado_ChamadoId",
                        column: x => x.ChamadoId,
                        principalTable: "Chamado",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChamadoDocumento_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ChamadoHistorico",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TipoAlteracao = table.Column<int>(type: "int", nullable: false),
                    CampoAlterado = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ValorAntigo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ValorNovo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CodigoUsuario = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UsuarioId = table.Column<long>(type: "bigint", nullable: true),
                    CodigoChamado = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ChamadoId = table.Column<long>(type: "bigint", nullable: true),
                    Codigo = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DtInclusao = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChamadoHistorico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChamadoHistorico_Chamado_ChamadoId",
                        column: x => x.ChamadoId,
                        principalTable: "Chamado",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChamadoHistorico_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Chamado_ServicoId",
                table: "Chamado",
                column: "ServicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Chamado_UsuarioId",
                table: "Chamado",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ChamadoComentario_ChamadoId",
                table: "ChamadoComentario",
                column: "ChamadoId");

            migrationBuilder.CreateIndex(
                name: "IX_ChamadoComentario_UsuarioId",
                table: "ChamadoComentario",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ChamadoDocumento_ChamadoId",
                table: "ChamadoDocumento",
                column: "ChamadoId");

            migrationBuilder.CreateIndex(
                name: "IX_ChamadoDocumento_UsuarioId",
                table: "ChamadoDocumento",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ChamadoHistorico_ChamadoId",
                table: "ChamadoHistorico",
                column: "ChamadoId");

            migrationBuilder.CreateIndex(
                name: "IX_ChamadoHistorico_UsuarioId",
                table: "ChamadoHistorico",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_PerguntaFrequente_DtAlteracao",
                table: "PerguntaFrequente",
                column: "DtAlteracao");

            migrationBuilder.CreateIndex(
                name: "IX_Servico_AreaId",
                table: "Servico",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Email",
                table: "Usuario",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_IdentityUserId",
                table: "Usuario",
                column: "IdentityUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChamadoComentario");

            migrationBuilder.DropTable(
                name: "ChamadoDocumento");

            migrationBuilder.DropTable(
                name: "ChamadoHistorico");

            migrationBuilder.DropTable(
                name: "PerguntaFrequente");

            migrationBuilder.DropTable(
                name: "Chamado");

            migrationBuilder.DropTable(
                name: "Servico");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Area");
        }
    }
}
