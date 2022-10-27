using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace pbl1.Models
{
    public class Furniture
    {
        [Key]
        public int FurnitureId { get; set; }
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 50 caracteres")]
        [Display(Name = "Nome do movel")]
        public string Name { get; set; }
        [Required(ErrorMessage = "A descrição é obrigatória")]
        [StringLength(250, MinimumLength = 20, ErrorMessage = "A descrição deve ter entre 20 e 250 caracteres")]
        [Display(Name = "Descrição")]
        public string Description { get; set; }
        [Required(ErrorMessage = "O material é obrigatório")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "O material deve ter entre 5 e 50 caracteres")]
        [Display(Name = "Material")]
        public string Material { get; set; }
        [Required(ErrorMessage = "A cor é obrigatória")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "A cor deve ter entre 5 e 50 caracteres")]
        [Display(Name = "Cor")]
        public string Color { get; set; }
        [Display(Name = "Foto")]
        [Required(ErrorMessage = "A foto é obrigatória")]
        public string Photo { get; set; }
        public Status StatusFurniture { get; set; }
        [Display(Name = "Cliente")]
        public int UserId { get; set; }
        public User? User{ get; set; }
        [Display(Name = "Funcionário")]
        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        public enum Status
        {
            Solicitado,
            Producao,
            Entregue
        }

        public Furniture()
        {
            StatusFurniture = Status.Solicitado;
        }

        public string ToBase64(string path)
        {
            using var fileStream = File.OpenRead(path);
            using MemoryStream ms = new MemoryStream();
            fileStream.CopyTo(ms);
            byte[] imageBytes = ms.ToArray();
            return Convert.ToBase64String(imageBytes);
        }

        public bool SelectedForEmployee(Employee employee)
        {
            if (StatusFurniture == Status.Solicitado)
            {
                StatusFurniture = Status.Producao;
                Employee = employee;
                return true;
            }
            return false;
        }

        public bool FinishFurniture()
        {
            if (StatusFurniture == Status.Producao)
            {
                StatusFurniture = Status.Entregue;
                Employee = null;
                return true;
            }
            return false;
        }
    }
}