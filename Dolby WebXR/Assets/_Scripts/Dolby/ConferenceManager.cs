using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using UnityEngine;

public class ConferenceManager : MonoBehaviour
{
    //Test
    public string token = "";

    [Serializable]
    public struct ConferenceDetails
    {
        public string conferenceId;
        public string conferenceAlias;
        public string conferencePincode;
        public bool isProtected;
        public string ownerToken;
        public object usersToken;
    }

    [SerializeField] private ConferenceDetails activeConferenceDetails;
    public ConferenceDetails ActiveConferenceDetails
    {
        get { return activeConferenceDetails; }
    }

    private async void CreateConference()
    {
        HttpClient client = new HttpClient();
        HttpRequestMessage request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://api.voxeet.com/v2/conferences/create"),
            Headers =
            {
                { "Accept", "application/json" },
                { "Authorization", String.Format("Bearer {0}", DolbyManager.Singleton.BearerAccessToken) }, // DolbyManager.Singleton.BearerAccessToken) },
            },
            Content = new StringContent("{\"parameters\":{\"dolbyVoice\":true,\"liveRecording\":true},\"participants\":{\"<externalId>\":{\"permissions\":[\"JOIN\",\"SEND_AUDIO\"]}},\"ownerExternalId\":\"test\",\"alias\":\"testConference\"}")
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

            activeConferenceDetails = JsonUtility.FromJson<ConferenceDetails>(body);
        }
    }

    private async void JoinConference()
    {
        HttpClient client = new HttpClient();
        HttpRequestMessage request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(String.Format("https://session.voxeet.com/v1/conferences/{0}/join", activeConferenceDetails.conferenceId)),
            Headers =
            {
                { "Accept", "application/json" },
                { "Authorization", String.Format("Bearer {0}", DolbyManager.Singleton.ClientAccessToken) },
            },
            Content = new StringContent("{\"user\": {\"type\":\"user\"}}")
            {
                Headers =
                {
                    ContentType = new MediaTypeHeaderValue("application/json")
                }
            }
        };
        using (var response = await client.SendAsync(request))
        {
            //response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            Debug.Log(body);

            //activeConferenceDetails = JsonUtility.FromJson<ConferenceDetails>(body);
        }
    }
}