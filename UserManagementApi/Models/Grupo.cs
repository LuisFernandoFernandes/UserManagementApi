namespace UserManagementApi.Models
{
    public class Grupo
    {
        public int IdGrupo { get; set; }
        public string Descricao { get; set; }
        public bool Administrador { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAlteracao { get; set; }
    }
}
