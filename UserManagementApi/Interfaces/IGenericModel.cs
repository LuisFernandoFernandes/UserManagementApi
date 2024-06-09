namespace UserManagementApi.Interfaces
{
    public interface IGenericModel
    {
        public int Id { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAlteracao { get; set; }
    }
}
