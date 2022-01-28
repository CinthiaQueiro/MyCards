using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreApiClient.Entities;

namespace CoreApiClient.Interfaces
{
    public interface IApiClientBase<TEntity> where TEntity : class
    {
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        Task<Message<ICollection<TEntity>>> GetAllAsync();

        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="UriFormatException"></exception>
        Task<Message<TEntity>> GetAsync(long id);

        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="UriFormatException"></exception>
        Task<Message<TEntity>> DeleteAsync(long id);

        /// <summary>
        /// Common method for making POST calls
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="UriFormatException"></exception>
        Task<Message<TEntity>> PostAsync(TEntity content);

        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="HttpRequestException"></exception>
        Task<Message<TEntity>> UpdateAsync(TEntity content);


        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="UriFormatException"></exception>
        Uri CreateRequestUri(string relativePath, string queryString = "");

        void AddHeaders();

        /// <summary>
        /// Common method for making GET Message<TEntity2> calls
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="UriFormatException"></exception>
        Task<Message<TEntity2>> GetMessageAsync<TEntity2>(string endpoint);
        Task<Message<TEntity2>> PostMessageAsync<TEntity2>(string endpoint, object content);
    }
}
