using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuAvailableDatasets : BaseScreen
{
    [SerializeField]
    private GameObject buttonPrefab;

    [SerializeField]
    private GameObject listContentGameObject;

    [SerializeField]
    private MainMenuManager mainMenuManager;

    protected override void OnShow(object data = null)
    {
        if (data != null && data is AvailableDatasetsList)
        {
            ClearDatasetsList();
            LoadAvailableDatasetList((AvailableDatasetsList)data);
        }
    }

    private void ClearDatasetsList()
    {
        foreach (Transform child in listContentGameObject.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void LoadAvailableDatasetList(AvailableDatasetsList availableDatasetsList)
    {
        foreach (var datasetPath in availableDatasetsList.DatasetsPaths)
        {
            CreateDatasetButton(datasetPath);
        }
    }

    private void CreateDatasetButton(string datasetPath)
    {
        GameObject button = Instantiate(buttonPrefab) as GameObject;
        button.name = $"DatasetButton({datasetPath})";
        button.SetActive(true);

        button.transform.SetParent(listContentGameObject.transform);
        button.transform.localScale = Vector3.one;

        var datasetButtonBehaviour = button.GetComponent<DatasetButtonBehaviour>();

        if (datasetButtonBehaviour != null)
        {
            datasetButtonBehaviour.SetButtonDataset(datasetPath);
            if (mainMenuManager != null)
            {
                datasetButtonBehaviour.OnDatasetButtonClick += mainMenuManager.SelectedDataset;
            }
        }
    }
}
