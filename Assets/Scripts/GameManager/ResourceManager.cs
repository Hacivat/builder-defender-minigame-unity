using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public event EventHandler OnResourceChangedEvent;
    public static ResourceManager Instance { get; private set; }
    private Dictionary<ResourceTypeSO, int> resourceAmountDictionary;
    private ResourceTypeListSO resourceTypeList;

    private void Awake() {
        Instance = this;
        resourceAmountDictionary = new Dictionary<ResourceTypeSO, int>();

        resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
        foreach (ResourceTypeSO resourceType in resourceTypeList.list) {
            resourceAmountDictionary[resourceType] = 0;
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.T)) {
            AddResource(resourceTypeList.list[0], 1);
            TestResourceAmountDictionary();
        }

        if (Input.GetKeyDown(KeyCode.Y)) {
            AddResource(resourceTypeList.list[1], 1);
            TestResourceAmountDictionary();
        }
    }

    public void TestResourceAmountDictionary() {
        foreach (ResourceTypeSO resourceType in resourceAmountDictionary.Keys) {
            Debug.Log(resourceType.nameString + ": " + resourceAmountDictionary[resourceType]);
        }
    }

    public void AddResource(ResourceTypeSO resourceType, int amount) {
        resourceAmountDictionary[resourceType] += amount;
        OnResourceChangedEvent?.Invoke(this, EventArgs.Empty);
    }

    public int GetResourceAmount(ResourceTypeSO resourceType) {
        return resourceAmountDictionary[resourceType];
    }
}
