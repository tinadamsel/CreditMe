using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    public partial class AddRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql("INSERT INTO CommonDropDown VALUES ('Female',1,0,GETDATE());INSERT INTO CommonDropDown VALUES ('Male',1,0,GETDATE());");

			migrationBuilder.Sql("INSERT INTO AspNetRoles VALUES (NewId(),'SuperAdmin','SuperAdmin',NEWID());INSERT INTO AspNetRoles VALUES (NewId(),'Admin','Admin',NEWID());");
		}
    }
}
