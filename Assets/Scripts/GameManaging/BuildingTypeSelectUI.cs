using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectUI : MonoBehaviour
{
    [SerializeField] private Sprite arrowSprite;
    [SerializeField] private Transform buttonTemplate;   
    
    private Transform arrowButton;
    private BuildingTypeListSO buildingTypeList;
    private Dictionary<BuildingTypeSO, Transform> buildingTypeTransformDictionary;
    private Dictionary<BuildingTypeSO, Transform> buttonTransformDictionary;
    private void Awake() {  
        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
        buttonTemplate.gameObject.SetActive(false);

        buildingTypeTransformDictionary = new Dictionary<BuildingTypeSO, Transform>();
        buttonTransformDictionary = new Dictionary<BuildingTypeSO, Transform>();

        int index = 0;

        arrowButton = Instantiate(buttonTemplate, transform);
        arrowButton.gameObject.SetActive(true);

        float offsetAmount = 130f;
        arrowButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);
        arrowButton.GetChild(1).GetComponent<Image>().sprite = arrowSprite;

        Vector3 arrowScale = arrowButton.GetChild(1).GetComponent<RectTransform>().localScale;
        arrowButton.GetChild(1).GetComponent<RectTransform>().localScale = new Vector3(arrowScale.x * .5f, arrowScale.y * .5f, 0);

        arrowButton.GetComponent<Button>().onClick.AddListener(() => {
            BuildingManager.Instance.SetActiveBuildingType(null);
        });
        index++;

        foreach (BuildingTypeSO buildingType in buildingTypeList.list) {
            Transform buttonTransform = Instantiate(buttonTemplate, transform);
            buttonTransform.gameObject.SetActive(true);
            buildingTypeTransformDictionary[buildingType] = buttonTransform;

            offsetAmount = 130f;
            buttonTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);
            buttonTransform.GetChild(1).GetComponent<Image>().sprite = buildingType.sprite;

            buttonTransform.GetComponent<Button>().onClick.AddListener(() => {
                BuildingManager.Instance.SetActiveBuildingType(buildingType);
            });

            buttonTransformDictionary[buildingType] = buttonTransform;
            
            index++;
        }
    }

    private void Start () {
        UpdateActiveBuildingTypeButton();
        BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingTypeSelectUIListenTo_OnActiveBuildingTypeChanged;
    }

    private void BuildingTypeSelectUIListenTo_OnActiveBuildingTypeChanged(object sender, System.EventArgs e) {
        UpdateActiveBuildingTypeButton();
    }

    private void UpdateActiveBuildingTypeButton() {
        arrowButton.Find("Selected").gameObject.SetActive(false);
        foreach (BuildingTypeSO buildingType in buildingTypeTransformDictionary.Keys) {
            Transform buttonTransform = buttonTransformDictionary[buildingType];
            buttonTransform.Find("Selected").gameObject.SetActive(false);
        }

        BuildingTypeSO activeBuildingType = BuildingManager.Instance.GetActiveBuildingType();
        if (activeBuildingType == null) 
            arrowButton.Find("Selected").gameObject.SetActive(true);
        else 
            buttonTransformDictionary[activeBuildingType].Find("Selected").gameObject.SetActive(true);
    }
}
