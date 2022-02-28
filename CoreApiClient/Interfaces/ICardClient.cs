using System.Collections.Generic;
using System.Threading.Tasks;
using CoreApiClient.Entities;

namespace CoreApiClient.Interfaces
{
    public interface ICardClient : IApiClientBase<Card>
    {

        Task<Message<User>> Get(string email);
        //Task<Message<User>> Post(string name, string email);
        Task<Message<List<Card>>> GetCards(int idDeckCard);
    }
}
