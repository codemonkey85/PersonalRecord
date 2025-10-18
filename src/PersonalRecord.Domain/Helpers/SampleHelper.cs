using PersonalRecord.Domain.Models.Entities;
using PersonalRecord.Infrastructure.Constants;

namespace PersonalRecord.Domain.Helpers
{
    internal class SampleHelper
    {
        internal static List<Movement> GetSampleMovements()
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
                    },
                    new() {
                        MovementID = SampleConstants.SampleBacksquatId,
                        MovName = SampleConstants.SAMPLE_BACKSQUAT_NAME
                    }
                };
            return movements;
        }

        internal static List<MovementRecord> GetSampleMovementRecords(Movement? clean, Movement? deadlift)
        {
            var movementRecords = new List<MovementRecord>();
            if (clean is not null && deadlift is not null)
            {
                movementRecords.AddRange(
                    new MovementRecord
                    {
                        MovementRecordID = SampleConstants.SampleCleanPrId,
                        Movement = clean,
                        MvrDate = SampleConstants.SamplePrsDate,
                        MvrMovementID_FK = clean.MovementID,
                        MvrNotes = SampleConstants.SampleNote,
                        MvrReps = 1,
                        MvrWeight = SampleConstants.SAMPLE_CLEAN_WEIGHT
                    },
                    new MovementRecord
                    {
                        MovementRecordID = SampleConstants.SampleCleanSecondPrId,
                        Movement = clean,
                        MvrDate = SampleConstants.SamplePrsSecondDate,
                        MvrMovementID_FK = clean.MovementID,
                        MvrReps = 1,
                        MvrWeight = SampleConstants.SAMPLE_CLEAN_SECOND_WEIGHT
                    },
                    new MovementRecord
                    {
                        MovementRecordID = SampleConstants.SampleDeadliftPrId,
                        Movement = deadlift,
                        MvrDate = SampleConstants.SamplePrsDate,
                        MvrMovementID_FK = deadlift.MovementID,
                        MvrNotes = SampleConstants.SampleNote,
                        MvrReps = 1,
                        MvrWeight = SampleConstants.SAMPLE_DEADLIFT_WEIGHT
                    }
                );
            }

            return movementRecords;
        }
    }
}
