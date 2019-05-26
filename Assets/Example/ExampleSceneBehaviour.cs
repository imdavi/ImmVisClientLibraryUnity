using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleSceneBehaviour : MonoBehaviour
{
    public ImmVisManager immvisManager;

    // Start is called before the first frame update
    async void Start()
    {
        var result = await immvisManager.Client.OpenDatasetFromFile("./example_datasets/IRIS.csv");

        if(result == 0) {
            var dimensions = await immvisManager.Client.GetDatasetDimensions();

            foreach (var dimension in dimensions)
            {
                Debug.Log($"{dimension.Name} - {dimension.Type}");
            }

            var data = await immvisManager.Client.GetDatasetValues();

            foreach (var row in data)
            {
                Debug.Log($"{row.Values}");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
