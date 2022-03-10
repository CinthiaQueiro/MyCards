using CoreApiClient.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GatewayApi.CoreAPI.Clients;
using Microsoft.AspNetCore.Http;
using CoreApiClient.Utility;
using CoreApiClient.Entities;

namespace CoreApiClient.Clients
{
    public abstract class ApiClientBase<TEntity> : IApiClientBase<TEntity> where TEntity : class
    {
        public HttpClient _httpClient;

        public virtual string _url { get; set; }

        public virtual string _urlAdressApi { get; set; } = "apiUrl";

        private Uri BaseEndpoint { get; set; }
        private readonly IConfiguration _configuration;
        private readonly SessionUtility _sessionUtility;

        public ApiClientBase(IConfiguration configuration, SessionUtility sessionUtility)
        {
            _configuration = configuration;
            var settings = _configuration.GetSection("MySettings")
                .AsEnumerable().ToList();
            var web = settings.Where(c => c.Key.Contains(_urlAdressApi)).FirstOrDefault();
            _sessionUtility = sessionUtility;
            BaseEndpoint = new Uri(web.Value);  //baseEndpoint ?? throw new ArgumentNullException("baseEndpoint");
            _httpClient = new HttpClient();
        }



        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        public async Task<Message<ICollection<TEntity>>> GetAllAsync()
        {
            AddHeaders();
            var reqUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                              _url));

            var response = await _httpClient.GetAsync(reqUrl, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Message<ICollection<TEntity>>>(data);
        }

        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="UriFormatException"></exception>
        public async Task<Message<TEntity>> GetAsync(long id)
        {
            var retorno = new Message<TEntity>();

            try
            {
                var reqUrl = CreateRequestUri($"{string.Format(System.Globalization.CultureInfo.InvariantCulture, _url)}/{id}");
                var response = await _httpClient.GetAsync(reqUrl, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();
                retorno = await response.Content.ReadAsAsync<Message<TEntity>>();
                return retorno;
            }
            catch (AccessViolationException ex)
            {
                retorno.IsSuccess = false;
                retorno.ReturnMessage = ex.Message;
                retorno.ReturnMessage += $"| servidor: {_url}";
                return retorno;
            }
        }

        public async Task<Message<TEntity>> DeleteAsync(long id)
        {
            var retorno = new Message<TEntity>();

            try
            {
                var reqUrl = CreateRequestUri($"{string.Format(System.Globalization.CultureInfo.InvariantCulture, _url)}/{id}");
                var response = await _httpClient.DeleteAsync(reqUrl);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                retorno = JsonConvert.DeserializeObject<Message<TEntity>>(data);
            }
            catch (Exception ex)
            {
                retorno.IsSuccess = false;
                retorno.ReturnMessage = ex.Message;
                return retorno;
            }

            return retorno;
        }

        /// <summary>
        /// Common method for making POST calls
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="UriFormatException"></exception>
        public async Task<Message<TEntity>> PostAsync(TEntity content)
        {
            var retorno = new Message<TEntity>();
            try
            {
                var reqUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture, _url));
                var response = await _httpClient.PostAsync(reqUrl, CreateHttpContent(content));
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                retorno = JsonConvert.DeserializeObject<Message<TEntity>>(data);
                retorno.ReturnMessage += " teste " + reqUrl;
            }
            catch (Exception ex)
            {
                retorno.IsSuccess = false;
                retorno.ReturnMessage = ex.Message;
                return retorno;
            }
           
            return retorno;
        }


        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="HttpRequestException"></exception>
        public async Task<Message<TEntity>> UpdateAsync(TEntity content)
        {
            var retorno = new Message<TEntity>();
            try
            {
                var reqUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture, $"{_url}/Update"));
                var response = await _httpClient.PostAsync(reqUrl, CreateHttpContent(content));
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                retorno = JsonConvert.DeserializeObject<Message<TEntity>>(data);
            }
            catch (Exception ex)
            {
                retorno.IsSuccess = false;
                retorno.ReturnMessage = ex.Message;
                return retorno;
            }
            return retorno;
        }

        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="UriFormatException"></exception>
        public Uri CreateRequestUri(string relativePath, string queryString = "")
        {
            relativePath = relativePath.Replace("//", "/");
            var endpoint = new Uri(BaseEndpoint, relativePath);
            var url = new Uri(BaseEndpoint.OriginalString).Append(relativePath);
            return url;
        }

        public HttpContent CreateHttpContent(TEntity content)
        {
            var json = JsonConvert.SerializeObject(content, MicrosoftDateFormatSettings);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        public static JsonSerializerSettings MicrosoftDateFormatSettings
        {
            get
            {
                return new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
            }
        }

        public void AddHeaders()
        {
            _httpClient.DefaultRequestHeaders.Remove("userIP");
            _httpClient.DefaultRequestHeaders.Add("userIP", "192.168.1.1");
        }

        public async Task<Message<TEntity2>> GetMessageAsync<TEntity2>(string endpoint)
        {
            var retorno = new Message<TEntity2>();

            try
            {
                AddHeaders();
                var reqUrl = CreateRequestUri($"{string.Format(System.Globalization.CultureInfo.InvariantCulture, _url)}/{endpoint}");
                var response = await _httpClient.GetAsync(reqUrl, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<Message<TEntity2>>();
            }
            catch (Exception ex)
            {
                retorno.IsSuccess = false;
                retorno.ReturnMessage = ex.Message;
                return retorno;
            }
        }

        public async Task<TEntity2> GetJsonAsync<TEntity2>(string endpoint)
        {
            try
            {
                AddHeaders();
                var reqUrl = CreateRequestUri($"{string.Format(System.Globalization.CultureInfo.InvariantCulture, _url)}/{endpoint}");
                var response = await _httpClient.GetAsync(reqUrl, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<TEntity2>();
            }
            catch (Exception e)
            {               
                return default(TEntity2);
            }
        }

        public async Task<TEntity2> PostJsonAsync<TEntity2>(string endpoint, object content)
        {
            try
            {
                AddHeaders();
                var reqUrl = CreateRequestUri($"{string.Format(System.Globalization.CultureInfo.InvariantCulture, _url)}/{endpoint}");
                var response = await _httpClient.PostAsJsonAsync(reqUrl, content);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<TEntity2>();
            }
            catch (Exception e)
            {
                return default(TEntity2);
            }
        }
        public async Task<Message<TEntity2>> PostMessageAsync<TEntity2>(string endpoint, object content)
        {
            var retorno = new Message<TEntity2>();

            try
            {
                AddHeaders();
                var reqUrl = CreateRequestUri($"{string.Format(System.Globalization.CultureInfo.InvariantCulture, _url)}/{endpoint}");
                var response = await _httpClient.PostAsJsonAsync(reqUrl, content);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<Message<TEntity2>>();
            }
            catch (Exception ex)
            {
                retorno.IsSuccess = false;
                retorno.ReturnMessage = ex.Message;
                retorno.ReturnMessage += $"\n servidor: {_url}";
                return retorno;
            }
        }
    }
}
