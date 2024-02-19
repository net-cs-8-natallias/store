using System.Net;
using System.Text;
using Basket.Host.Services.Interfaces;
using ExceptionHandler;
using IdentityModel.Client;
using Newtonsoft.Json;

namespace Basket.Host.Services;

public class HttpClientService: IHttpClientService
{
       private readonly IHttpClientFactory _clientFactory;
    private readonly ILogger<HttpClientService> _logger;

    public HttpClientService(IHttpClientFactory clientFactory,
        ILogger<HttpClientService> logger)
    {
        _clientFactory = clientFactory;
        _logger = logger;
    }
    public async Task<TResponse> SendAsync<TResponse, TRequest>(string url, HttpMethod method, TRequest? content)
    {
        var client = _clientFactory.CreateClient();
        client.SetBearerToken(await GetClientCredentialsTokenAsync());

        var httpMessage = new HttpRequestMessage();
        httpMessage.RequestUri = new Uri(url);
        httpMessage.Method = method;

        if (content != null)
        {
            httpMessage.Content =
                new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
        }
           
        var result = await client.SendAsync(httpMessage);
        _logger.LogInformation($"*{GetType().Name}* status code: {result.StatusCode}");
        if (result.IsSuccessStatusCode)
        {
            var resultContent = await result.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<TResponse>(resultContent);
            _logger.LogInformation($"*{GetType().Name}* response: {response}");

            return response;
        } 
        if (result.StatusCode == HttpStatusCode.BadRequest)
        {
            var response = await result.Content.ReadAsStringAsync();
            _logger.LogInformation($"*{GetType().Name}* response: {response}");
            throw new IllegalArgumentException(response);
        }
        if (result.StatusCode == HttpStatusCode.NotFound)
        {
            var response = await result.Content.ReadAsStringAsync();
            _logger.LogInformation($"*{GetType().Name}* response: {response}");
            throw new NotFoundException(response);
        }

        return default !;
    }
    
    private async Task<string> GetClientCredentialsTokenAsync()
    {
        var client = _clientFactory.CreateClient();
        var tokenResponse = await client
            .RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = await GetDiscoveryDocumentAsync(),
                ClientId = "BasketClient",
                ClientSecret = "secret",
                Scope = "order catalog"
            });
        _logger.LogInformation($"*{GetType().Name}* response token: {tokenResponse}");
        if (tokenResponse.IsError)
        {
            _logger.LogError($"*{GetType().Name}* request failed with the following error: {tokenResponse.Error}");
            throw new Exception(
                $"RequestClientCredentialsTokenAsync failed with the following error: {tokenResponse.Error}", tokenResponse.Exception);
        }
    
        return tokenResponse.AccessToken;
    }
    
    private async Task<string>? GetDiscoveryDocumentAsync()
    {
        var client = _clientFactory.CreateClient();
        var discoveryDocument = await client
            .GetDiscoveryDocumentAsync("http://localhost:7001");
        if (discoveryDocument.IsError)
        {
            _logger.LogError($"*{GetType().Name}* discovery document: {discoveryDocument.Error}");
            return string.Empty;
        }
        _logger.LogInformation($"*{GetType().Name}* discovery document: {discoveryDocument.TokenEndpoint}");
        return discoveryDocument.TokenEndpoint;
    }
}