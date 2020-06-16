namespace api.Models
{
    /// <summary>
    /// A business contact belonging to a user
    /// </summary>
    public class BusinessContactRecord
    {
        public BusinessContactRecord()
        {
            Address = new BusinessContactAddress();
        }

        public int UserId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public BusinessContactAddress Address { get; set; }
    }
}
