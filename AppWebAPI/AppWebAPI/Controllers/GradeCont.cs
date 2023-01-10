using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Xamarin.Android.Net;
using Newtonsoft.Json;

namespace AppWebAPI;

public class GradeCont
{
    private const string Url = "http://192.168.7.77:5128/api/grades/";
    
    private HttpClient GetClient()
    {
        var client = new HttpClient(new AndroidClientHandler());
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        return client;
    }

    public async Task<IEnumerable<Grade>?> GetAll()
    {
        var client = GetClient();
        var result = await client.GetStringAsync(Url);

        return JsonConvert.DeserializeObject<IEnumerable<Grade>>(result);
    }

    public async Task<Grade?> Get(int id)
    {
        var client = GetClient();
        var response = await client.GetAsync(Url + id);
        if (response.StatusCode != HttpStatusCode.OK || response.Content.ToString().IsNullOrEmpty())
        {
            return null;
        }

        return JsonConvert.DeserializeObject<Grade>(await response.Content.ReadAsStringAsync());
    }

    public async Task<Grade?> Add(Grade grade)
    {
        var client = GetClient();
        var response = await client.PostAsync(Url,
            new StringContent(JsonConvert.SerializeObject(grade), Encoding.UTF8, "application/json"));
        if (response.StatusCode != HttpStatusCode.OK)
        {
            return null;
        }

        return JsonConvert.DeserializeObject<Grade>(await response.Content.ReadAsStringAsync());
    }

    public async Task<Grade?> Update(Grade grade)
    {
        var client = GetClient();
        var response = await client.PutAsync(Url,
            new StringContent(JsonConvert.SerializeObject(grade), Encoding.UTF8, "application/json"));
        if (response.StatusCode != HttpStatusCode.OK)
        {
            return null;
        }

        return JsonConvert.DeserializeObject<Grade>(await response.Content.ReadAsStringAsync());
    }

    public async Task<Grade?> Delete(int id)
    {
        var client = GetClient();
        var response = await client.DeleteAsync(Url + id);
        if (response.StatusCode != HttpStatusCode.OK)
        {
            return null;
        }
        
        return JsonConvert.DeserializeObject<Grade>(await response.Content.ReadAsStringAsync());
    }
}