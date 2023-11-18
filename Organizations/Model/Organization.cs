namespace Organizations.Model
{
    [Serializable]
    public class Organization
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ReseptionPhone { get; set; }
        public string HumanResourcesDepartmentPhone { get; set; }
        public string AccountingPhone { get; set; }
    }
}

