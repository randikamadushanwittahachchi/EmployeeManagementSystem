using BaseLibrary.Responses;
using ClientLibrary.Helper.Constracts;
using ClientLibrary.Helper.Implementations;
using ClientLibrary.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ClientLibrary.Services.Implementations;

public class GenericServic<T> : IGenericServiceInterface<T> where T : class
{

    // Injected HttpClientFactory

    private readonly IGetHttpClient _getHttpClient;
    public GenericServic(IGetHttpClient client)
    {
        _getHttpClient = client;
    }


    // CRUD Operations
    public async Task<List<T>> GetAll(string baseUrl)
    {
        var httpClient = await _getHttpClient.GetPrivateHttpClientAsync();
        var reponse = await httpClient!.GetFromJsonAsync<List<T>>(baseUrl);

        return reponse!;
    }

    public async Task<T> GetById(string baseUrl)
    {
        var httpClient = await _getHttpClient.GetPrivateHttpClientAsync();
        var reponse = await httpClient!.GetFromJsonAsync<T>(baseUrl);
        return reponse!;

    }

    public async Task<GeneralResponse> Create(T model, string baseUrl)
    {
        var httpClient =await _getHttpClient.GetPrivateHttpClientAsync();
        var reponse = await httpClient!.PostAsJsonAsync(baseUrl, model);
        var result = await reponse.Content.ReadFromJsonAsync<GeneralResponse>();
        return result!;
    }

    public async Task<GeneralResponse> Update(T model, string baseUrl)
    {
        var httpClient = await _getHttpClient.GetPrivateHttpClientAsync();
        var response = await httpClient!.PutAsJsonAsync(baseUrl, model);
        var result = await response.Content.ReadFromJsonAsync<GeneralResponse>();
        return result!;
    }

    public async Task<GeneralResponse> DeleteById(string baseUrl)
    {
        var htttpClient = await _getHttpClient.GetPrivateHttpClientAsync();
        var response = await htttpClient!.DeleteFromJsonAsync<GeneralResponse>(baseUrl);
        return response!;
    }

}
