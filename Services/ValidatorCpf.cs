using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace pbl1.Services
{
    [AttributeUsage(AttributeTargets.Property |
    AttributeTargets.Field, AllowMultiple = false)]
    sealed public class ValidatorCpf : ValidationAttribute
    {
        private string[] numberReapeted  = new string[10] {"11111111111","22222222222","33333333333","44444444444","55555555555","66666666666","77777777777","88888888888","99999999999","00000000000"};
        public override bool IsValid(object? value)
        {
            string cpf = value.ToString();

            if (CpfIsValid(cpf) && !numberReapeted.Contains(cpf))
            {
                return true;
            }
            return false;
            
        }
        public bool CpfIsValid(string cpf)
        {
            int[] multiplier1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplier2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digt;
            int sum;
            int remainder;

            cpf = cpf.Trim();

            if (cpf.Length != 11)
            {
                return false;
            }

            tempCpf = cpf.Substring(0, 9);
            sum = 0;

            for (int i = 0; i < 9; i++)
            {
                sum += int.Parse(tempCpf[i].ToString()) * multiplier1[i];
            }

            remainder = sum % 11;
            if (remainder < 2)
            {
                remainder = 0;
            }
            else
            {
                remainder = 11 - remainder;
            }

            digt = remainder.ToString();

            tempCpf = tempCpf + digt;

            sum = 0;
            for (int i = 0; i < 10; i++)
            {
                sum += int.Parse(tempCpf[i].ToString()) * multiplier2[i];
            }

            remainder = sum % 11;
            if (remainder < 2)
            {
                remainder = 0;
            }
            else
            {
                remainder = 11 - remainder;
            }

            digt = digt + remainder.ToString();


            return cpf.EndsWith(digt);
        }
    }
}