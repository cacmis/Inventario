
namespace Inventory.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = "";
        public string Name { get; set; } = "";
        public string Phone { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Active { get; set; }

        // Seiguentes dos campos son para la authenticacion,
        // para encriptar el password y despues poder hacer la validacion del password
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }   

}