using Organizations.Enums;
using Organizations.Model;

namespace Organizations.Services
{
    /// <summary>
    /// Паттерн Репозиторий
    /// </summary>
    internal interface IOrganizationService
    {
        IEnumerable<Organization> GetAll(KeyValuePair<OrganizationField, string>? filter = null);
        Organization? GetById(int id);
        int Create(Organization organization);
        void Update(int id, Organization organization);
        void Delete(int id);
    }
}
