namespace PersonalRecord.Infrastructure.Constants
{
    public static class SampleConstants
    {
        // Movements
        public static Guid SampleCleanId = Guid.Parse("80a917f2-299b-422d-a5fe-3a2842f21e29");
        public static Guid SampleBacksquatId = Guid.Parse("5e1808aa-6ed8-4dac-9bd2-4317d4f27376");
        public static Guid SampleDeadliftId = Guid.Parse("8613aa4d-154a-4423-8ffa-63cfcaa063b2");

        public const string SAMPLE_CLEAN_NAME = "Clean (Sample)";
        public const string SAMPLE_BACKSQUAT_NAME = "Backsquat (Sample)";
        public const string SAMPLE_DEADLIFT_NAME = "Deadlift (Sample)";

        // PRs
        public static Guid SampleCleanPrId = Guid.Parse("7700db04-f8a1-4fc4-a54e-49cac742178d");
        public static Guid SampleDeadliftPrId = Guid.Parse("ecb6eed0-7b63-4b14-ba50-d9dc7d43dc27");

        public const int SAMPLE_CLEAN_WEIGHT = 89;
        public const int SAMPLE_DEADLIFT_WEIGHT = 130;

        public static DateTime SamplePrsDate = new(2025 - 01 - 01);
    }
}