using Doczy.Core.Entities.Common;
using Doczy.Core.Entities.Identities;

namespace Doczy.Core.Entities
{
    public class Education:BaseSectionEntity
    {
        public DoctorAppUser? Doctor { get; set; }
        public Guid DoctorId { get; set; }
        public Univercity? Univercity { get; set; }
        public Guid UnivercityId { get; set; }
        public UnivercityDegree? UnivercityDegree { get; set; }
        public Guid UnivercityDegreeId { get; set; }
        public FieldOfStudy? FieldOfStudy { get; set; }
        public Guid FieldOfStudyId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
