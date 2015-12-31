using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace Dogs.Migrations.MyDB
{
    public partial class Initial1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Dog",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "UserId", table: "Dog");
        }
    }
}
