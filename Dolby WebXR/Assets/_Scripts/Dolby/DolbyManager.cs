using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using UnityEngine;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine.UI;

public class DolbyManager : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void Create(string nameStr);
    [DllImport("__Internal")]
    private static extern void Join(string nameStr);
    [DllImport("__Internal")]
    private static extern void Leave();

    [SerializeField] private TMP_InputField userNameInput;
    [SerializeField] private TMP_InputField conferenceNameInput;
    [SerializeField] private Button createButton;
    [SerializeField] private Button joinButton;
    [SerializeField] private Button leaveButton;

    private void Start()
    {
        leaveButton.gameObject.SetActive(false);
        createButton.gameObject.SetActive(true);
        joinButton.gameObject.SetActive(true);
    }

    public void CreateConference()
    {
        if (conferenceNameInput.text != "")
        {
            Create(conferenceNameInput.text);

            createButton.gameObject.SetActive(false);
            joinButton.gameObject.SetActive(false);
            leaveButton.gameObject.SetActive(true);
        }
    }

    public void JoinConference()
    {
        if (conferenceNameInput.text != "")
        {
            Join(conferenceNameInput.text);

            createButton.gameObject.SetActive(false);
            joinButton.gameObject.SetActive(false);
            leaveButton.gameObject.SetActive(true);
        }
    }

    public void LeaveConference()
    {
        Leave();

        createButton.gameObject.SetActive(false);
        joinButton.gameObject.SetActive(false);
        leaveButton.gameObject.SetActive(true);
    }

}
