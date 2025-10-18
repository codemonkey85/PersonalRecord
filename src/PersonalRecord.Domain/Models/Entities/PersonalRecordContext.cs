namespace PersonalRecord.Domain.Models.Entities
{
    using Microsoft.EntityFrameworkCore;
    using PersonalRecord.Infrastructure.Constants;

    public class PersonalRecordContext : DbContext
    {
        public PersonalRecordContext(DbContextOptions<PersonalRecordContext> options)
            : base(options)
        {
        }

        public DbSet<Movement> MovementItems { get; set; }

        public DbSet<MovementRecord> MovementRecordItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSeeding((context, _) =>
                {
                    var any = context.Set<Movement>().Any();
                    if (!any)
                    {
                        context.Set<Movement>().AddRange(GetSampleMovements());
                        context.SaveChanges();
                    }
                })
                .UseAsyncSeeding(async (context, _, cancellationToken) =>
                {
                    var any = await context.Set<Movement>().AnyAsync(cancellationToken);
                    if (!any)
                    {
                        await context.Set<Movement>().AddRangeAsync(GetSampleMovements(), cancellationToken);
                        await context.SaveChangesAsync(cancellationToken);
                    }
                });
        }

        private IEnumerable<Movement> GetSampleMovements()
        {
            var movements = new List<Movement>
                {
                    new() {
                        MovementID = SampleConstants.SampleCleanId,
                        MovName = SampleConstants.SAMPLE_CLEAN_NAME
                    },
                    new() {
                        MovementID = SampleConstants.SampleDeadliftId,
                        MovName = SampleConstants.SAMPLE_DEADLIFT_NAME
                    }
                };
            return movements;
        }
    }
}