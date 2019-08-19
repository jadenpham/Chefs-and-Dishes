using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
namespace ChefsNdishes
{
    public class Chef
    {
        [Key]
        public int ChefId {get; set;}

        [Required]
        [MinLength(2,ErrorMessage="Chef's first name must be at least 2 characters.")]
        public string FirstName {get; set;}

        [Required]
        [MinLength(2,ErrorMessage="Chef's last name must be at least 2 characters.")]
        public string LastName {get; set;}

        [Required]
        [DataType(DataType.Date)]
        [AgeCheck]
        public DateTime DOB {get; set;}

        public DateTime CreatedAt {get; set;} = DateTime.Now;
        public DateTime UpdatedAt {get; set;} = DateTime.Now;

        public List<Dish> CreatedDishes {get; set;}
    }


    public class AgeCheck : ValidationAttribute 
    {
        protected override ValidationResult IsValid(object date, ValidationContext validationContext)
        {
            DateTime DOB = Convert.ToDateTime(date);
            DateTime today = DateTime.Now;
            var age = today.Year - DOB.Year;
            if(DOB.Date > today.AddYears(-age)) age--;
            if(age <18)
            {
                return new ValidationResult("Chef must be 18 years or older");
            } else{
                return ValidationResult.Success;
            }
        }
    }

    public class Dish
    {
        [Key]
        public int DishId {get; set; }

        public Chef Creator {get; set;}

        public int ChefId{get; set ;}

        [Required]
        [MinLength(2, ErrorMessage="Please provide a name for dish. Min 2 characters")]
        public string Name {get; set;}

        [Required]
        [Range(0, 15000, ErrorMessage="Please provide the amount of calories. 0-15000")]
        public int Calories {get; set;}

        [Required]
        [MinLength(10, ErrorMessage="Please provide a description. Min 10 characters")]
        public string Description {get; set;}

        [Required]
        [Range(1,5, ErrorMessage="Please select level of tastiness")]
        public int Tastiness {get; set;}

        public DateTime CreatedAt {get; set;} = DateTime.Now;
        public DateTime UpdatedAt {get; set;} = DateTime.Now;

    }
}