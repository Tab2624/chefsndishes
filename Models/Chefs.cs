#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace chefsndishes;

public class Chef
{
    [Key]
    public int ChefId { get; set; }

    [Required]
    public string FirstName { get; set; }
    public string LastName { get; set; }

    [Required(ErrorMessage = "Field is required")]
    [DateMustBeInPast]
    [DataType(DataType.Date)]
    public DateTime DOB { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public List<Dish> Dishes { get; set; } = new();
}

public class DateMustBeInPastAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        return DateTime.Compare(((DateTime)value), DateTime.Today) > 0 ? new ValidationResult("Date cannot be in the future") : ValidationResult.Success;
    }
}
