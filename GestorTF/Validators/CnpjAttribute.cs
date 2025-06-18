using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace GestorTF.Validators
{
    public class CnpjAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null) return true; // [Required] deve ser usado separadamente

            var cnpj = value.ToString()?.Replace(".", "").Replace("-", "").Replace("/", "");

            if (string.IsNullOrWhiteSpace(cnpj) || cnpj.Length != 14 || !Regex.IsMatch(cnpj, @"^\d{14}$"))
                return false;

            return ValidarCnpj(cnpj);
        }

        private bool ValidarCnpj(string cnpj)
        {
            int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCnpj = cnpj[..12];
            int soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            int resto = (soma % 11);
            int digito1 = (resto < 2) ? 0 : 11 - resto;

            tempCnpj += digito1;
            soma = 0;

            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            int digito2 = (resto < 2) ? 0 : 11 - resto;

            return cnpj.EndsWith($"{digito1}{digito2}");
        }
    }
}