using System.ComponentModel.DataAnnotations;

namespace CoreApiClient.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

    }
}