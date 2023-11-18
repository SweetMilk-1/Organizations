using Organizations.Enums;
using Organizations.Model;
using System.Text.RegularExpressions;

namespace Organizations.Services
{
    internal class UserInterface : IUserInterface
    {
        public void Clear()
        {
            Console.Clear();
        }

        public void PrintOrganizationList(IEnumerable<Organization> organizations)
        {
            foreach (var item in organizations)
            {
                PrintOrganization(item);
            }
        }

        private string TryGetString(string text)
        {
            string input;
            do
            {
                Console.WriteLine(text);
                input = Console.ReadLine();
            }
            while (string.IsNullOrEmpty(input));
            return input;
        }

        private string TryGetNumber(string text)
        {
            string input;
            Regex regex = new Regex(@"(\+\d{1,2}\s?)?(\(\d{3}\)\s?)?\d{3}[-]?\d{2,3}[-]?\d{2}[-]?\d{2}");
            do
            {
                Console.WriteLine(text);
                input = Console.ReadLine();
            }
            while (!regex.IsMatch(input));
            return input;
        }

        public int TryGetInt(string? text = null)
        {
            string input;
            int res;
            Regex regex = new Regex(@"(\+\d{1,2}\s?)?(\(\d{3}\)\s?)?\d{3}[-]?\d{2,3}[-]?\d{2}[-]?\d{2}");
            do
            {
                if (text != null)
                    Console.WriteLine(text);
                input = Console.ReadLine();
            }
            while (!int.TryParse(input, out res));
            return res;
        }

        private string TryGetEmail(string text)
        {
            string input;
            string pattern = @"\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,}\b";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            do
            {
                Console.WriteLine(text);
                input = Console.ReadLine();
            }
            while (!regex.IsMatch(input));
            return input;
        }

        public Organization ReadOrganization()
        {
            return new Organization
            {
                Name = TryGetString("Введите имя:"),
                Email = TryGetEmail("Введите Email:"),
                ReseptionPhone = TryGetNumber("Введите номер приемной (+X (XXX) XXX-XXXX или X-XXX-XXX-XX-XX):"),
                HumanResourcesDepartmentPhone = TryGetNumber("Введите номер отдела кадров (+X (XXX) XXX-XXXX или X-XXX-XXX-XX-XX):"),
                AccountingPhone = TryGetNumber("Введите номер бухгалтерии (+X (XXX) XXX-XXXX или X-XXX-XXX-XX-XX):")
            };
        }

        public KeyValuePair<OrganizationField, string> ChooseOrganizationFieldsAndValue()
        {
            while (true)
            {
                switch (TryGetInt("0 - Название\n1 - Email\n2 - Телефон приемной\n3 - Телефон отдела кадров\n4 - Телефон бухгалтерии"))
                {
                    case 0:
                        return new KeyValuePair<OrganizationField, string>(OrganizationField.Name, TryGetString("Введите значение:"));
                    case 1:
                        return new KeyValuePair<OrganizationField, string>(OrganizationField.Email, TryGetEmail("Введите значение:"));
                    case 2:
                        return new KeyValuePair<OrganizationField, string>(OrganizationField.ReseptionPhone, TryGetNumber("Введите значение:"));
                    case 3:
                        return new KeyValuePair<OrganizationField, string>(OrganizationField.HumanResourcesDepartmentPhone, TryGetNumber("Введите значение:"));
                    case 4:
                        return new KeyValuePair<OrganizationField, string>(OrganizationField.AccountingPhone, TryGetNumber("Введите значение:"));
                }
            }
        }
        
        public void PrintOrganization(Organization organization, string? text = null)
        {
            if (text != null)
                Console.WriteLine(text);
            Console.WriteLine($"Id: {organization.Id}");
            Console.WriteLine($"Название: {organization.Name}");
            Console.WriteLine($"Email: {organization.Email}");
            Console.WriteLine($"Телефон приемной: {organization.ReseptionPhone}");
            Console.WriteLine($"Телефон отдела кадров: {organization.HumanResourcesDepartmentPhone}");
            Console.WriteLine($"Телефон бухгалтерии: {organization.AccountingPhone}");
            Console.WriteLine();
        }

        public OrganizationMenuItem ChooseOrganizationMenuItem()
        {
            while (true)
            {
                switch (TryGetInt("0 - Выход\n1 - Изменить\n2 - Удалить"))
                {
                    case 0:
                        return OrganizationMenuItem.Exit;

                    case 1:
                        return OrganizationMenuItem.Update;

                    case 2:
                        return OrganizationMenuItem.Delete;
                }
            }
        }

        public ListOrganizationsMenuItem ChooseListOrganizationsMenuItem()
        {
            while (true)
            {
                switch (TryGetInt("0 - Выход\n1 - Выбрать организацию\n2 - Добавить организацию\n3 - Добавить фильтр\n4 - Удалить фильтр\n"))
                {
                    case 0:
                        return ListOrganizationsMenuItem.Exit;

                    case 1:
                        return ListOrganizationsMenuItem.ChooseOrganization;

                    case 2:
                        return ListOrganizationsMenuItem.Create;

                    case 3:
                        return ListOrganizationsMenuItem.AddFilter;

                    case 4:
                        return ListOrganizationsMenuItem.DeleteFilter;
                }
            }
        }

        public void PrintOrganizationFieldsAndValue(KeyValuePair<OrganizationField, string> value, string? text = null)
        {
            if (text != null)
                Console.WriteLine(text);
            string field = null;
            switch (value.Key)
            {
                case OrganizationField.Name:
                    field = "Название";
                    break;
                case OrganizationField.Email:
                    field = "Email";
                    break;
                case OrganizationField.AccountingPhone:
                    field = "Телефон бухгалтерии";
                    break;
                case OrganizationField.ReseptionPhone:
                    field = "Телефон приемной";
                    break;
                case OrganizationField.HumanResourcesDepartmentPhone:
                    field = "Телефон отдела кадров";
                    break;
            }
            Console.WriteLine($"{field}: {value.Value}");
            Console.WriteLine();
        }
    }
}
