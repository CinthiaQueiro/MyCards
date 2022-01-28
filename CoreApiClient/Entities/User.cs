using System.ComponentModel.DataAnnotations;

namespace CoreApiClient.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string name { get; set; }

        public string email { get; set; }

    }
}