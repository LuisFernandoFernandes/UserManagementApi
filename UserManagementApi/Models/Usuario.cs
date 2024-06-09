namespace UserManagementApi.Models
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public class Usuario : GenericModel
    {
        public string Nome { get; set; } = string.Empty;
        private string senha = string.Empty;
        public string Senha {
            get { return senha; }
            set { senha = Encrypt(value); }
        }
        public string CPF { get; set; } = string.Empty;
        public int IdGrupo { get; set; }
        public Grupo Grupo { get; set; } = new Grupo();

        private string Encrypt(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }

}
