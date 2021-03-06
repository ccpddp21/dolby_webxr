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

    [SerializeField] private EnvironmentSwitcher envSwitcher;

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
            createButton.gameObject.SetActive(false);
            joinButton.gameObject.SetActive(false);
            leaveButton.gameObject.SetActive(true);

            envSwitcher.ToggleGroup(EnvironmentSwitcher.EnviromentGroupName.Host);

            Create(conferenceNameInput.text);
        }
    }

    public void JoinConference()
    {
        if (conferenceNameInput.text != "")
        {
            createButton.gameObject.SetActive(false);
            joinButton.gameObject.SetActive(false);
            leaveButton.gameObject.SetActive(true);

            envSwitcher.ToggleGroup(EnvironmentSwitcher.EnviromentGroupName.Listen);

            Join(conferenceNameInput.text);
        }
    }

    public void LeaveConference()
    {
        leaveButton.gameObject.SetActive(false);
        createButton.gameObject.SetActive(true);
        joinButton.gameObject.SetActive(true);

        envSwitcher.ToggleGroup(EnvironmentSwitcher.EnviromentGroupName.None);

        Leave();
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
            ExecuteEnvironmentSwitch(1);
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            ExecuteEnvironmentSwitch(2);
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            ExecuteEnvironmentSwitch(0);
        }
    }
    */
}
