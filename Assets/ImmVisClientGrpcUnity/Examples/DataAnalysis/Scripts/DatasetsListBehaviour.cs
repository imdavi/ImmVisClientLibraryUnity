using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatasetsListBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonPrefab;

    [SerializeField]
    private GameObject listContentGameObject;

    private List<string> listContent;
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

        var datasetButtonBehaviour = button.GetComponent<DatasetButtonBehaviour>();

        if (datasetButtonBehaviour != null)
        {
            datasetButtonBehaviour.SetButtonDataset(datasetPath);
            datasetButtonBehaviour.OnDatasetButtonClick += HandleDatasetButtonClick;
        }
    }

    private void HandleDatasetButtonClick(string clickedDataset)
    {
        Debug.Log($"Clicked on {clickedDataset}");
    }
}
