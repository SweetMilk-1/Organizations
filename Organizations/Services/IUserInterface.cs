
using Organizations.Enums;
using Organizations.Model;
using System.Dynamic;

namespace Organizations.Services
{
    internal interface IUserInterface
    {
        void Clear();
        Organization ReadOrganization();
        void PrintOrganization(Organization organization, string? text = null);
        void PrintOrganizationList(IEnumerable<Organization> organizations);
        int TryGetInt(string? text);
        OrganizationMenuItem ChooseOrganizationMenuItem();
        ListOrganizationsMenuItem ChooseListOrganizationsMenuItem();
        KeyValuePair<OrganizationField, string> ChooseOrganizationFieldsAndValue();
        void PrintOrganizationFieldsAndValue(KeyValuePair<OrganizationField, string> value, string? text = null);
    }
}
