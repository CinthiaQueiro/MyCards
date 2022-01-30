using System.Collections.Generic;
using System.Threading.Tasks;
using CoreApiClient.Entities;

namespace CoreApiClient.Interfaces
{
    public interface IDeckCardClient : IApiClientBase<DeckCard>
    {
        Task<Message<List<DeckCard>>> GetDeckCardsUser(int idUser);
    }
}
