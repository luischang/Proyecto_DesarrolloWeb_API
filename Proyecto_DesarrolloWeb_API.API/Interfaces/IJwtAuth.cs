namespace Proyecto_DesarrolloWeb_API.API.Interfaces
{
    public interface IJwtAuth
    {
        string Authentication(string username, string password);
    }
}
