using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalRecord.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddSampleDataIfEmpty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO MovementItems (MovementID, MovName)
                SELECT '80a917f2-299b-422d-a5fe-3a2842f21e29', 'Clean (Sample)'
                UNION ALL
                SELECT '5e1808aa-6ed8-4dac-9bd2-4317d4f27376', 'Backsquat (Sample)'
                UNION ALL
                SELECT '8613aa4d-154a-4423-8ffa-63cfcaa063b2', 'Deadlift (Sample)'
                WHERE NOT EXISTS (SELECT 1 FROM MovementItems);

                INSERT INTO MovementRecordItems (MovementRecordID, MvrWeight, MvrReps, MvrDate, MvrNotes, MvrMovementID_FK)
                SELECT '7700db04-f8a1-4fc4-a54e-49cac742178d', 80, 1, '2025-01-01', 'Sample', '80a917f2-299b-422d-a5fe-3a2842f21e29'
                UNION ALL
                SELECT 'ecb6eed0-7b63-4b14-ba50-d9dc7d43dc27', 130, 1, '2025-01-01', 'Sample', '8613aa4d-154a-4423-8ffa-63cfcaa063b2'
                WHERE NOT EXISTS (SELECT 1 FROM MovementRecordItems);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DELETE FROM MovementRecordItems 
                WHERE MovementRecordID IN (
                    '7700db04-f8a1-4fc4-a54e-49cac742178d', 
                    'ecb6eed0-7b63-4b14-ba50-d9dc7d43dc27');

                DELETE FROM MovementItems 
                WHERE MovementID IN (
                    '80a917f2-299b-422d-a5fe-3a2842f21e29', 
                    '5e1808aa-6ed8-4dac-9bd2-4317d4f27376', 
                    '8613aa4d-154a-4423-8ffa-63cfcaa063b2');
            ");
        }
    }
}
