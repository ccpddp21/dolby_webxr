using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSwitcher : MonoBehaviour
{
    public static EnvironmentSwitcher Singleton;

    public enum EnviromentGroupName
    {
        None = 0,
        Host = 1,
        Listen = 2
    }

    [Serializable]
    public struct EnvironmentContainer
    {
        public EnviromentGroupName Name;
        public List<GameObject> Children;
    }

    [SerializeField] private List<EnvironmentContainer> containers = new List<EnvironmentContainer>();

    private void Awake()
    {
        Singleton = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        ToggleGroup(EnviromentGroupName.None);
    }

    public void ToggleGroup(EnviromentGroupName target)
    {
        foreach (EnvironmentContainer container in containers)
        {
            foreach (GameObject child in container.Children)
            {
                child.SetActive(target == container.Name);
            }
        }
    }
}
