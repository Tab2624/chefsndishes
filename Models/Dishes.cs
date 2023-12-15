#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace chefsndishes;
public class Dish
{

    [Key]
    public int DishId { get; set; }

    [Required]
    public string DishName { get; set; }

    [Required]
    [Range(1, 5, ErrorMessage = "The number must be between 1 and 5.")]
    public int Tastiness { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "The number must be greater than 0.")]
    public int Calories { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    // Foreign key
    [ForeignKey("ChefId")]
    public int ChefId { get; set; }

    public Chef? Creator {get; set;}

    // // Navigation property for the one-to-many relationship
    // [NotMapped]
    // public Chef Chef { get; set; }
}