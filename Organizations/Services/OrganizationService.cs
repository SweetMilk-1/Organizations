using Organizations.Enums;
using Organizations.Helpers;
using Organizations.Model;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Organizations.Services
{
    internal class OrganizationService : IOrganizationService
    {
        readonly XmlHelper _xmlHelper;
        
        public OrganizationService(XmlHelper xmlHelper)
        {
            _xmlHelper = xmlHelper;
        }
        public int Create(Organization organization)
        {
            var list = GetAll().ToList();
            list.Add(organization);

            organization.Id = (list.MaxBy(x => x.Id)?.Id ?? 0) + 1;

            _xmlHelper.Serialize(list);

            return organization.Id;
        }

        public void Delete(int id)
        {
            List<Organization> list = new List<Organization>(GetAll().Where(item => item.Id != id));
            _xmlHelper.Serialize(list);
        }

        public IEnumerable<Organization> GetAll(KeyValuePair<OrganizationField, string>? filer = null)
        {
            var list = _xmlHelper.Deserialize() as IEnumerable<Organization> ?? new List<Organization>();
            if (filer.HasValue)
            {
                switch (filer.Value.Key)
                {
                    case OrganizationField.Name:
                        list = list.Where(item => item.Name.Contains(filer.Value.Value));
                        break;
                    case OrganizationField.Email:
                        list = list.Where(item => item.Email.Contains(filer.Value.Value));
                        break;
                    case OrganizationField.AccountingPhone:
                        list = list.Where(item => item.AccountingPhone.Contains(filer.Value.Value));
                        break;
                    case OrganizationField.HumanResourcesDepartmentPhone:
                        list = list.Where(item => item.HumanResourcesDepartmentPhone.Contains(filer.Value.Value));
                        break;
                    case OrganizationField.ReseptionPhone:
                        list = list.Where(item => item.ReseptionPhone.Contains(filer.Value.Value));
                        break;
                }
            }
            return list;
        }

        public Organization? GetById(int id)
        {
            return GetAll().FirstOrDefault(item => item.Id == id);
        }

        public void Update(int id, Organization organization)
        {
            var list = GetAll().Where(item => item.Id != id).ToList();
            organization.Id = id;
            list.Add(organization);
            _xmlHelper.Serialize(list);
        }
    }
}
