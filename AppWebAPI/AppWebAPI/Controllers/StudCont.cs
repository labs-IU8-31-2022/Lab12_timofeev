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

public class StudCont
{
    private const string Url = "http://192.168.7.77:5128/api/students/";

    public HttpClient GetClient()
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        return client;
    }

    public async Task<IEnumerable<Student>?> GetAll()
    {
        var client = GetClient();
        var result = await client.GetStringAsync(Url);

        return JsonConvert.DeserializeObject<IEnumerable<Student>>(result);
    }

    public async Task<Student?> Get(int id)
    {
        var client = GetClient();
        var response = await client.GetAsync(Url + id);
        if (response.StatusCode != HttpStatusCode.OK || response.Content.ToString().IsNullOrEmpty())
        {
            return null;
        }

        return JsonConvert.DeserializeObject<Student>(await response.Content.ReadAsStringAsync());
    }

    public async Task<Student?> Add(Student student)
    {
        var client = GetClient();
        var response = await client.PostAsync(Url,
            new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json"));
        if (response.StatusCode != HttpStatusCode.OK)
        {
            return null;
        }

        return JsonConvert.DeserializeObject<Student>(await response.Content.ReadAsStringAsync());
    }

    public async Task<Student?> Update(Student student)
    {
        var client = GetClient();
        var response = await client.PutAsync(Url,
            new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json"));
        if (response.StatusCode != HttpStatusCode.OK)
        {
            return null;
        }

        return JsonConvert.DeserializeObject<Student>(await response.Content.ReadAsStringAsync());
    }

    public async Task<Student?> Delete(int id)
    {
        var client = GetClient();
        var response = await client.DeleteAsync(Url + id);
        if (response.StatusCode != HttpStatusCode.OK)
        {
            return null;
        }
        
        return JsonConvert.DeserializeObject<Student>(await response.Content.ReadAsStringAsync());
    }
}