using System.ComponentModel.DataAnnotations;

namespace WalkingAPI.Models.DTO;

public class AddRegionRequestDto
{
    [Required]
    public string Code { get; set; }
    [Required]
    public string Name { get; set; }
    public string? RegionImageUrl { get; set; }
}