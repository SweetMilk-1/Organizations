using Microsoft.Win32.SafeHandles;
using Organizations.Enums;
using Organizations.Helpers;
using Organizations.Model;
using Organizations.Services;

namespace Organizations
{
    internal class Program
    {
        static void Main(string[] args)
        {
            XmlHelper xmlHelper = new XmlHelper(typeof(List<Organization>), "MyData.xml");
            IUserInterface userInterface = new UserInterface();
            IOrganizationService organizationService = new OrganizationService(xmlHelper);

            Organization choosenOrganization = null;
            KeyValuePair<OrganizationField, string>? filter = null;

            while (true)
            {
                userInterface.Clear();
                if (choosenOrganization != null)
                {
                    userInterface.PrintOrganization(choosenOrganization, "ОРГАНИЗАЦИЯ");
                    var organizationMenuItem = userInterface.ChooseOrganizationMenuItem();
                    switch (organizationMenuItem)
                    {
                        case OrganizationMenuItem.Exit:
                            choosenOrganization = null;
                            break;
                        case OrganizationMenuItem.Delete:
                            DeleteOrganization(organizationService, choosenOrganization);
                            choosenOrganization = null;
                            break;
                        case OrganizationMenuItem.Update:
                            UpdateOrganization(userInterface, organizationService, choosenOrganization);
                            break;
                    }
                }
                else
                {
                    userInterface.PrintOrganizationList(organizationService.GetAll(filter));
                    if (filter.HasValue)
                    {
                        userInterface.PrintOrganizationFieldsAndValue(filter.Value, "ФИЛЬТР");
                    }
                    var listOrganizationsMenuItem = userInterface.ChooseListOrganizationsMenuItem();
                    switch (listOrganizationsMenuItem)
                    {
                        case ListOrganizationsMenuItem.Exit:
                            return;
                        case ListOrganizationsMenuItem.Create:
                            choosenOrganization = CreateOrganization(userInterface, organizationService);
                            break;
                        case ListOrganizationsMenuItem.AddFilter:
                            filter = userInterface.ChooseOrganizationFieldsAndValue();
                            break;
                        case ListOrganizationsMenuItem.DeleteFilter:
                            filter = null;
                            break;
                        case ListOrganizationsMenuItem.ChooseOrganization:
                            choosenOrganization = ChooseOrganization(userInterface, organizationService);
                            break;
                    }
                }
            }
        }

        private static Organization? ChooseOrganization(IUserInterface userInterface, IOrganizationService organizationService)
        {
            Organization choosenOrganization = null;
            do
            {
                int id = userInterface.TryGetInt("Введите Id организации (0 - выйти)");
                if (id == 0)
                    break;
                choosenOrganization = organizationService.GetById(id);
            } while (choosenOrganization == null);
            return choosenOrganization;
        }

        private static Organization CreateOrganization(IUserInterface userInterface, IOrganizationService organizationService)
        {
            userInterface.Clear();
            var choosenOrganization = userInterface.ReadOrganization();
            organizationService.Create(choosenOrganization);
            return choosenOrganization;
        }

        private static void DeleteOrganization(IOrganizationService organizationService, Organization choosenOrganization)
        {
            organizationService.Delete(choosenOrganization.Id);
        }

        private static void UpdateOrganization(IUserInterface userInterface, IOrganizationService organizationService, Organization choosenOrganization)
        {
            var patch = userInterface.ChooseOrganizationFieldsAndValue();
            switch (patch.Key)
            {
                case OrganizationField.Name:
                    choosenOrganization.Name = patch.Value;
                    break;
                case OrganizationField.Email:
                    choosenOrganization.Email = patch.Value;
                    break;
                case OrganizationField.ReseptionPhone:
                    choosenOrganization.ReseptionPhone = patch.Value;
                    break;
                case OrganizationField.HumanResourcesDepartmentPhone:
                    choosenOrganization.HumanResourcesDepartmentPhone = patch.Value;
                    break;
                case OrganizationField.AccountingPhone:
                    choosenOrganization.AccountingPhone = patch.Value;
                    break;
            }
            organizationService.Update(choosenOrganization.Id, choosenOrganization);
        }
    }
}