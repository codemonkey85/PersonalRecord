namespace PersonalRecord.Domain.Models.Entities
{
    using Microsoft.EntityFrameworkCore;
    using PersonalRecord.Domain.Helpers;
    using PersonalRecord.Infrastructure;
    using PersonalRecord.Infrastructure.Constants;
    using PersonalRecord.Services.Interfaces;

    public class PersonalRecordContext : DbContext
    {
        private readonly ISettingsService _settingsService;

        public PersonalRecordContext(
            DbContextOptions<PersonalRecordContext> options,
            ISettingsService settingsService)
            : base(options)
        {
            _settingsService = settingsService;
        }

        public DbSet<Movement> MovementItems { get; set; }

        public DbSet<MovementRecord> MovementRecordItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var setting = _settingsService.GetSettings();
            optionsBuilder
                .UseSeeding((context, _) =>
                {
                    var any = context.Set<Movement>().Any();
                    if (!any && !setting.InitialDataAdded)
                    {
                        var movements = SampleHelper.GetSampleMovements();
                        context.Set<Movement>().AddRange(movements);
                        context.SaveChanges();

                        var clean = context.Set<Movement>().FirstOrDefault(m => m.MovementID == SampleConstants.SampleCleanId);
                        var deadlift = context.Set<Movement>().FirstOrDefault(m => m.MovementID == SampleConstants.SampleDeadliftId);
                        var movementRecords = SampleHelper.GetSampleMovementRecords(clean, deadlift);
                        context.Set<MovementRecord>().AddRange(movementRecords);
                        context.SaveChanges();

                        SetInitialDataAddedTrue(setting);
                    }
                })
                .UseAsyncSeeding(async (context, _, cancellationToken) =>
                {
                    var any = await context.Set<Movement>().AnyAsync(cancellationToken);
                    if (!any && !setting.InitialDataAdded)
                    {
                        var movements = SampleHelper.GetSampleMovements();
                        await context.Set<Movement>().AddRangeAsync(movements, cancellationToken);
                        await context.SaveChangesAsync(cancellationToken);

                        var clean = await context.Set<Movement>().FirstOrDefaultAsync(m => m.MovementID == SampleConstants.SampleCleanId, cancellationToken);
                        var deadlift = await context.Set<Movement>().FirstOrDefaultAsync(m => m.MovementID == SampleConstants.SampleDeadliftId, cancellationToken);
                        var movementRecords = SampleHelper.GetSampleMovementRecords(clean, deadlift);
                        await context.Set<MovementRecord>().AddRangeAsync(movementRecords, cancellationToken);
                        await context.SaveChangesAsync(cancellationToken);

                        SetInitialDataAddedTrue(setting);
                    }
                });
        }

        private void SetInitialDataAddedTrue(Setting setting)
        {
            setting.InitialDataAdded = true;
            _settingsService.UpdateSettings(setting);
        }
    }
}