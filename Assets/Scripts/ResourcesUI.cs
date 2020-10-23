using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ResourcesUI : MonoBehaviour
{
    [SerializeField] private Transform resourceTemplate;

    private ResourceTypeListSO resourceTypeList;
    private Dictionary<ResourceTypeSO, Transform> resourceTypeTransformDictionary; 
    private void Awake() {
        resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
        resourceTemplate.gameObject.SetActive(false);
        resourceTypeTransformDictionary = new Dictionary<ResourceTypeSO, Transform>();
        int index = 0;
        foreach (ResourceTypeSO resourceType in resourceTypeList.list) {
            Transform resourceTransform = Instantiate(resourceTemplate, transform);
            resourceTransform.gameObject.SetActive(true);
            resourceTypeTransformDictionary[resourceType] = resourceTransform;

            float offsetAmount = -145f;
            resourceTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);
            resourceTransform.GetChild(0).GetComponent<Image>().sprite = resourceType.sprite;
            index++;
        }
    }

    private void Start () {
        UpdateResourceAmount();
    }

    private void UpdateResourceAmount() {
        foreach (ResourceTypeSO resourceType in resourceTypeTransformDictionary.Keys) {
            Transform resourceTransform = resourceTypeTransformDictionary[resourceType];

            int resourceAmount = ResourceManager.Instance.GetResourceAmount(resourceType);
            resourceTransform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText(resourceAmount.ToString());
        }
    }
}
