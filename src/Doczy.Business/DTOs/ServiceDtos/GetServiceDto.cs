using Doczy.Core.Entities.Identities;
using Doczy.Core.Entities;
using Doczy.Business.DTOs.ServiceTypeDtos;

namespace Doczy.Business.DTOs.ServiceDtos
{
    public class GetServiceDto
    {
        public string? Name { get; set; }
        public byte Duration { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public GetServiceTypeDto? ServiceType { get; set; }
    }
}
