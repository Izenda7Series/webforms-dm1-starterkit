﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WebformsIntegratedBE_Standalone.IzendaBoundary
{
    public class WebAPIService
    {
        #region Variables
        private readonly string _basedUri;
        private static WebAPIService _instance;
        #endregion

        #region CTOR
        private WebAPIService(string basedUri)
        {
            _basedUri = basedUri;
        }
        #endregion

        #region Methods
        public static WebAPIService Instance
        {
            get { return _instance ?? (_instance = new WebAPIService(System.Configuration.ConfigurationManager.AppSettings["IzendaApiUrl"])); }
        }

        public async Task<T> GetAsync<T>(string action, string authToken = null, Dictionary<string, object> parameters = null)
        {
            using (var httpClient = GetHttpClient(authToken))
            {
                var url = BuildActionUri(action, parameters);

                var httpResponse = await httpClient.GetAsync(url);
                try
                {
                    httpResponse.EnsureSuccessStatusCode();
                }
                catch (Exception ex)
                {
                    throw new WebApiException(url, httpResponse.StatusCode, ex);
                }

                string responeJson = await httpResponse.Content.ReadAsStringAsync();
                if (responeJson != "null")
                {
                    return JsonConvert.DeserializeObject<T>(responeJson);

                }
                return default(T);
            }

        }

        public async Task PostAsync<T>(string action, T data, string authToken = null)
        {
            using (var httpClient = GetHttpClient(authToken))
            {
                var url = BuildActionUri(action);
                var httpResponse = await httpClient.PostAsJsonAsync(url, data);
                try
                {
                    httpResponse.EnsureSuccessStatusCode();
                }
                catch (Exception ex)
                {
                    throw new WebApiException(url, httpResponse.StatusCode, ex);
                }
            }
        }

        public async Task<bool> PostReturnBooleanAsync<T>(string action, T data, string authToken = null)
        {
            using (var httpClient = GetHttpClient(authToken))
            {
                var url = BuildActionUri(action);
                var httpResponse = await httpClient.PostAsJsonAsync(url, data);

                try
                {
                    httpResponse.EnsureSuccessStatusCode();
                }
                catch (Exception ex)
                {
                    throw new WebApiException(url, httpResponse.StatusCode, ex);
                }

                var responseJson = await httpResponse.Content.ReadAsStringAsync();
                var result = (bool)JObject.Parse(responseJson)["success"];

                return result;
            }
        }

        public async Task<TResult> PostReturnValueAsync<TResult, T>(string action, T data, string authToken = null)
        {
            using (var httpClient = GetHttpClient(authToken))
            {
                var url = BuildActionUri(action);
                var httpResponse = await httpClient.PostAsJsonAsync(url, data);
                try
                {
                    httpResponse.EnsureSuccessStatusCode();
                }
                catch (Exception ex)
                {
                    throw new WebApiException(url, httpResponse.StatusCode, ex);
                }

                string responseJson = await httpResponse.Content.ReadAsStringAsync();
                if (responseJson != "null")
                    return JsonConvert.DeserializeObject<TResult>(responseJson);
                return default(TResult);
            }
        }

        public async Task DeleteAsync(string action, string authToken = null)
        {
            using (var httpClient = GetHttpClient(authToken))
            {
                var url = BuildActionUri(action);
                var httpResponse = await httpClient.DeleteAsync(url);
                try
                {
                    httpResponse.EnsureSuccessStatusCode();
                }
                catch (Exception ex)
                {
                    throw new WebApiException(url, httpResponse.StatusCode, ex);
                }
            }
        }

        public async Task<TResult> DeleteReturnValueAsync<TResult>(string action, string authToken = null)
        {
            using (var httpClient = GetHttpClient(authToken))
            {
                var url = BuildActionUri(action);
                var httpResponse = await httpClient.DeleteAsync(url);
                try
                {
                    httpResponse.EnsureSuccessStatusCode();
                }
                catch (Exception ex)
                {
                    throw new WebApiException(url, httpResponse.StatusCode, ex);
                }

                var responseJson = await httpResponse.Content.ReadAsStringAsync();
                return responseJson != "null" ? JsonConvert.DeserializeObject<TResult>(responseJson) : default(TResult);
            }
        }

        private HttpClient GetHttpClient(string authToken = null)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrWhiteSpace(authToken))
            {
                client.DefaultRequestHeaders.Add("access_token", authToken);
            }

            return client;
        }

        private string BuildActionUri(string action, Dictionary<string, object> parameters = null)
        {
            var url = _basedUri + action;
            if (parameters != null)
                url = AddUrlParams(url, parameters);

            return url;
        }

        private static string AddUrlParams(string url, Dictionary<string, object> parameters)
        {
            var stringBuilder = new StringBuilder(url);
            var hasFirstParam = url.Contains("?");

            foreach (var parameter in parameters)
            {
                var format = hasFirstParam ? "&{0}={1}" : "?{0}={1}";
                stringBuilder.AppendFormat(format, Uri.EscapeDataString(parameter.Key),
                    Uri.EscapeDataString(parameter.Value.ToString()));
                hasFirstParam = true;
            }

            return stringBuilder.ToString();
        } 
        #endregion
    }

    public class WebApiException: Exception
    {
        #region Properties
        public string RequestedUrl { get; private set; }

        public HttpStatusCode StatusCode { get; private set; }
        #endregion

        #region CTOR
        public WebApiException(string requestedUrl, System.Net.HttpStatusCode statusCode, Exception innerException)
            : base("Error occured when calling WebApi", innerException)
        {
            RequestedUrl = requestedUrl;
            StatusCode = statusCode;
        } 
        #endregion
    }
}
