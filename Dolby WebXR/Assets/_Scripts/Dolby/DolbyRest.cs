using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using UnityEngine;

public class DolbyRest : MonoBehaviour
{

    [Serializable]
    protected struct BearerAuth
    {
        public string token_type;
        public string access_token;
        public int expires_in;
    }

    [Serializable]
    protected struct ClientAuth
    {
        public string token_type;
        public string access_token;
        public string refresh_token;
        public int expires_in;
    }

    [Serializable]
    protected struct ClientVerify
    {
        public string jwt_user_token;
        public string user_id;
        public string wsUri;
    }

    public static DolbyRest Singleton;

    [SerializeField] private BearerAuth bearerAuth;
    public string BearerAccessToken
    {
        get { return bearerAuth.access_token; }
    }

    [SerializeField] private ClientAuth clientAuth;
    public string ClientAccessToken
    {
        get { return clientAuth.access_token; }
    }

    [SerializeField] private ClientVerify clientVerify;

    private void Awake()
    {
        Singleton = this;
    }

    public async void GetBearerToken()
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://api.voxeet.com/v1/auth/token"),
            Headers =
            {
                { "Accept", "application/json" },
                { "Cache-Control", "no-cache" },
                { "Authorization", "Basic ZFFRTVUyazJOamh3RFdET1F6T3VYUT09OktIdWw5NUZLT0RwOUNiVjlKNHNtVlZ5TmlJbHl6cERST2FuLWFKOWJvMHc9" },
            },
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
            }),
        };
        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();

            bearerAuth = JsonUtility.FromJson<BearerAuth>(body);
        }
    }

    public async void GetSessionToken()
    {
        HttpClient client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://session.voxeet.com/v1/oauth2/token"),
            Headers =
            {
                { "Accept", "application/json" },
                { "Cache-Control", "no-cache" },
                { "Authorization", "Basic ZFFRTVUyazJOamh3RFdET1F6T3VYUT09OktIdWw5NUZLT0RwOUNiVjlKNHNtVlZ5TmlJbHl6cERST2FuLWFKOWJvMHc9" },
            },
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
            }),
        };
        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();

            clientAuth = JsonUtility.FromJson<ClientAuth>(body);
        }

        // Once your app is done using the HttpClient object call dispose to 
        // free up system resources (the underlying socket and memory used for the object)
        client.Dispose();

        VerifyClient();
    }

    private async void VerifyClient()
    {
        HttpClient client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://session.voxeet.com/v2/users/identify"),
            Headers =
            {
                { "Accept", "application/json,text/plain,*/*" },
                { "Cache-Control", "no-cache" },
                { "Authorization", String.Format("Bearer {0}", DolbyRest.Singleton.ClientAccessToken) },
            },
            Content = new StringContent("{\"name\":\"test user\",\"sdkVersion\":\"3.3.0\"}")
            {
                Headers =
                {
                    ContentType = new MediaTypeHeaderValue("application/json")
                }
            }
        };

        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();

            clientVerify = JsonUtility.FromJson<ClientVerify>(body);
        }

        // Once your app is done using the HttpClient object call dispose to 
        // free up system resources (the underlying socket and memory used for the object)
        client.Dispose();

        ConfigClient();
    }

    private async void ConfigClient()
    {
        HttpClient client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://telemetry.voxeet.com/api/v1/metrics/configuration"),
            Headers =
            {
                { "Accept", "application/json,text/plain,*/*" },
                { "Cache-Control", "no-cache" },
                { "Authorization", String.Format("Bearer {0}", DolbyRest.Singleton.ClientAccessToken) },
            },
        };

        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            Debug.Log(body);

            //clientVerify = JsonUtility.FromJson<ClientVerify>(body);
        }

        // Once your app is done using the HttpClient object call dispose to 
        // free up system resources (the underlying socket and memory used for the object)
        client.Dispose();

        //PutSessionTimestamp();
    }

    private async void PutSessionTimestamp()
    {
        long milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        string param = "{\"version\":\"1.0\",\"userId\":\"" + clientVerify.user_id + "\",\"timestamp\":" + milliseconds + ",\"metrics\":{\"name\":\"" + SystemInfo.deviceName + "\",\"os\":\"" + SystemInfo.operatingSystem + "\",\"timeZone\":\"UTC - 05:00\",\"sdkPlatform\":\"browser\",\"sdkVersion\":\"3.3.0\",\"uxkitVersion\":\"\",\"hardware\":{\"cpu\":\"\",\"ram\":6,\"screen\":\"1832x1920\"}}}";
        HttpClient client = new HttpClient();
        Debug.Log(param);

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Put,
            RequestUri = new Uri(String.Format("https://telemetry.voxeet.com/api/v1/metrics/device/users/{0}/timestamp/{1}", clientVerify.user_id, milliseconds)),
            Headers =
            {
                { "Accept", "application/json,text/plain,*/*" },
                { "Cache-Control", "no-cache" },
                { "Authorization", String.Format("Bearer {0}", DolbyRest.Singleton.ClientAccessToken) },
            },
            //Content = new StringContent("{\"version\":\"1.0\",\"userId\":\"" + clientVerify.user_id + "\",\"timestamp\":" + milliseconds + ",\"}")
            //{
            //    Headers =
            //    {
            //        ContentType = new MediaTypeHeaderValue("application/json")
            //    }
            //},
        };

        using (var response = await client.SendAsync(request))
        {
            //response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            Debug.Log(body);

            //clientVerify = JsonUtility.FromJson<ClientVerify>(body);
        }

        // Once your app is done using the HttpClient object call dispose to 
        // free up system resources (the underlying socket and memory used for the object)
        client.Dispose();
    }
}
