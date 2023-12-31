﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiJokes63.Data.Migrations
{
    public partial class Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JokeId",
                table: "Jokes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JokeId",
                table: "Jokes");
        }
    }
}
