using System.Text;
using System.Text.RegularExpressions;

namespace NAF.Domain.Base
{
    public static class Util
    {
        private const string SenhaCaracteresValidos = LetrasMaiusculas + LetrasMinusculas + Numeros + Especiais;
        private const string LetrasMaiusculas = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string LetrasMinusculas = "abcdefghijklmnopqrstuvwxyz";
        private const string Numeros = "1234567890";
        private const string Especiais = "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~";

        public static string DeixaNumeros(string texto)
        {
            return string.IsNullOrWhiteSpace(texto) ? string.Empty : string.Join("", Regex.Split(texto, @"[^\d]"));
        }

        public static bool ValidaDocumento(string documento)
        {
            documento = DeixaNumeros(documento);

            return documento.Length == 11 ? ValidaCpf(documento) : ValidaCnpj(documento);
        }

        public static bool ValidaCnpj(string cnpj)
        {
            var multiplicador1 = new[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            int soma;
            int resto;
            string digito;
            string tempCnpj;

            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;
            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (var i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (var i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto;
            return cnpj.EndsWith(digito);
        }

        public static bool ValidaCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            cpf = DeixaNumeros(cpf);

            if (cpf.Length != 11)
                return false;

            if (cpf.Distinct().Count() == 1)
                return false;

            var multiplicador1 = new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            var tempCpf = cpf.Substring(0, 9);
            var soma = 0;

            for (var i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            var resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            var digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (var i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto;
            return cpf.EndsWith(digito);
        }

        public static bool ValidaSenha(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha))
                throw new InvalidDataException("Senha deve conter letras e números e pelo menos 8 caracteres");

            var temLetra = false;
            var temNumero = false;
            var temUpper = false;
            var temLower = false;
            var temEspecial = false;

            foreach (var c in senha)
            {
                if (char.IsLetter(c))
                    temLetra = true;
                if (char.IsNumber(c))
                    temNumero = true;
                if (char.IsUpper(c))
                    temUpper = true;
                if (char.IsLower(c))
                    temLower = true;
                if (Especiais.Contains(c))
                    temEspecial = true;
            }

            if (!(temNumero && temLetra && temUpper && temLower && temEspecial) || senha.Length < 8)
                throw new InvalidDataException("Senha deve conter letras maiusculas e minusculas, números, caracteres especiais e pelo menos 8 caracteres");

            if (senha.Contains(" "))
                throw new InvalidDataException("A senha não pode conter espaços");

            return true;
        }

        public static string CriarSenha()
        {
            var tamanho = 16;
            var valormaximo = SenhaCaracteresValidos.Length - 1;

            var random = new Random(DateTime.Now.Millisecond);
            var senhaBuilder = new StringBuilder(tamanho);

            for (var i = 0; i < tamanho; i++)
                senhaBuilder.Append(SenhaCaracteresValidos[random.Next(0, valormaximo)]);

            var senha = senhaBuilder.ToString();

            if (!senha.Any(char.IsDigit))
            {
                var posNum = random.Next(0, Numeros.Length - 1);
                var posSenha = random.Next(0, senha.Length - 1);
                senhaBuilder[posSenha] = Numeros[posNum];
            }
            if (!senha.Any(c => char.IsLetter(c) && char.IsUpper(c)))
            {
                var posCaracter = random.Next(0, LetrasMaiusculas.Length - 1);
                var posSenha = random.Next(0, senha.Length - 1);
                senhaBuilder[posSenha] = LetrasMaiusculas[posCaracter];
            }
            if (!senha.Any(c => char.IsLetter(c) && char.IsLower(c)))
            {
                var posCaracter = random.Next(0, LetrasMinusculas.Length - 1);
                var posSenha = random.Next(0, senha.Length - 1);
                senhaBuilder[posSenha] = LetrasMinusculas[posCaracter];
            }
            if (!senha.Any(c => Especiais.Contains(c)))
            {
                var posCaracter = random.Next(0, Especiais.Length - 1);
                var posSenha = random.Next(0, senha.Length - 1);
                senhaBuilder[posSenha] = Especiais[posCaracter];
            }

            return senhaBuilder.ToString();
        }
    }
}
