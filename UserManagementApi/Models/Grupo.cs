namespace UserManagementApi.Models
{
    public class Grupo : GenericModel
    {
        public string Descricao { get; set; } = string.Empty;
        public bool Administrador { get; set; }
    }
}
