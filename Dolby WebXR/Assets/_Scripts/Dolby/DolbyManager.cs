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
    private static extern void Init(string nameStr);
    [DllImport("__Internal")]
    private static extern void Create(string nameStr);
    [DllImport("__Internal")]
    private static extern void Join(string nameStr);
    [DllImport("__Internal")]
    private static extern void Leave();

    [SerializeField] private GameObject panelObject;
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

    public void Initialize()
    {
        if (userNameInput.text != "")
        {
            Init(userNameInput.text);

            panelObject.SetActive(true);
        }
    }

    public void CreateConference()
    {
        if (conferenceNameInput.text != "")
        {
            Create(conferenceNameInput.text);

            createButton.gameObject.SetActive(false);
            joinButton.gameObject.SetActive(false);
            leaveButton.gameObject.SetActive(true);

            EnvironmentSwitcher.Singleton.ToggleGroup(EnvironmentSwitcher.EnviromentGroupName.Host);
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

            EnvironmentSwitcher.Singleton.ToggleGroup(EnvironmentSwitcher.EnviromentGroupName.Listen);
        }
    }

    public void LeaveConference()
    {
        Leave();

        leaveButton.gameObject.SetActive(false);
        createButton.gameObject.SetActive(true);
        joinButton.gameObject.SetActive(true);

        EnvironmentSwitcher.Singleton.ToggleGroup(EnvironmentSwitcher.EnviromentGroupName.None);
    }

    // Testing
    /*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Initialize();
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            CreateConference();
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            JoinConference();
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            LeaveConference();
        }
    }
    */
}
