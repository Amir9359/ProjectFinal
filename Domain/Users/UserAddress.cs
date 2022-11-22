using Domain.Attributes;

namespace Domain.Users
{
    [Audtable]
    public class UserAddress
    {
        public int Id { get; set; }
        public string State { get;  private set; }
        public string City { get;  private set; }
        public string ZipCode { get;  private set; }
        public string PostalAddres { get;  private set; }
        public string UserId { get;  private set; }
        public string ReciverName { get;  private set; }

        public UserAddress(string state, string city, string zipCode,
            string postalAddres, string userId, string reciverName)
        {
            State = state;
            City = city;
            ZipCode = zipCode;
            PostalAddres = postalAddres;
            UserId = userId;
            ReciverName = reciverName;
        }

        public UserAddress()
        {
            
        }
    }
}