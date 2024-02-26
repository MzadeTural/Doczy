using Doczy.Core.Entities.Common;
using Doczy.Core.Entities.Identities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Doczy.Core.Entities
{
    public class Appointment : BaseSectionEntity
    {
        public string? PainDescription { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [DataType(DataType.Time)]
        public DateTime Time { get; set; }
        public DoctorAppUser? Doctor { get; set; }     
        public Guid DoctorId { get; set; }     
        public PatientAppUser? Patient { get; set; }      
        public Guid PatientId { get; set; }
        public Service? Service { get; set; }
        public Guid ServiceId { get; set; }
    }
}
