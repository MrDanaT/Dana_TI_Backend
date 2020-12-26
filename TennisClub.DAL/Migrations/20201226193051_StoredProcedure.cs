using Microsoft.EntityFrameworkCore.Migrations;

namespace TennisClub.DAL.Migrations
{
    public partial class StoredProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE SoftDeleteMember
	                    @pId int = 1
                        AS
                        BEGIN
	                        SET NOCOUNT ON;
	                    UPDATE [dbo].[tblMembers]
	                    SET [Deleted] = 1
	                    WHERE [Id] = @pId
                        END";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
