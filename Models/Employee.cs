using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using pbl1.Services;

namespace pbl1.Models
{
    public class Employee
    {
        [Key]
        public int UserId { get; set; }
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "O nome deve ter entre 5 e 50 caracteres")] 
        [Display(Name = "Nome")]
        public string Name { get; set; }
        [Required(ErrorMessage = "O email é obrigatório")]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "CPF")]
        [ValidatorCpf(ErrorMessage = "CPF inválido")]
        [StringLength(11 , MinimumLength = 11, ErrorMessage = "O CPF deve ter 11 caracteres")]
        public string Cpf { get; set; }
        [Required]
        [StringLength(11, MinimumLength = 10)]
        [Display(Name = "Telefone")]
        [RegularExpression(@"^[0-9]{10,11}$", ErrorMessage = "Telefone inválido")]
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(250)]
        [Display(Name = "Endereço")]
        public string Address { get; set; }
        [Required(ErrorMessage = "O cargo é obrigatório")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "O cargo deve ter entre 5 e 50 caracteres")]
        [Display(Name = "Cargo")]
        public string Role { get; set; }
        [Required(ErrorMessage = "O salário é obrigatório")]
        [Display(Name = "Salário")]
        public double Salary { get; set; }
        
        [Display(Name = "Status")]
        public Status StatusEmployee { get; set; }


        public enum Status
        {
            Disponivel,
            Indisponivel
        }

        public bool SelectFurniture(Furniture furniture)
        {
            if (StatusEmployee == Status.Disponivel)
            {
                StatusEmployee = Status.Indisponivel;
                furniture.SelectedForEmployee(this);
                return true;
            }
            return false;
        }

        public bool FinishFurniture(Furniture furniture)
        {
            if (StatusEmployee == Status.Indisponivel)
            {
                StatusEmployee = Status.Disponivel;
                furniture.FinishFurniture();
                return true;
            }
            return false;
        }
    }
}