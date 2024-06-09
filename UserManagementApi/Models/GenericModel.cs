using UserManagementApi.Interfaces;

namespace UserManagementApi.Models
{
    public class GenericModel : IGenericModel
    {
        public int Id { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public DateTime DataAlteracao { get; set; } = DateTime.Now;

    }
}