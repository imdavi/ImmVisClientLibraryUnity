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

        if (result == 0)
        {
            var dimensions = await immvisManager.Client.GetDatasetDimensions();

            foreach (var dimension in dimensions)
            {
                Debug.Log($"{dimension.Name} - {dimension.Type}");

                var features = await immvisManager.Client.GetDimensionDescriptiveStatistics(dimension.Name);

                foreach (var feature in features)
                {
                    Debug.Log($"{feature.Name}: {feature.Value}");
                }
            }

            var correlationMatrix = await immvisManager.Client.GetCorrelationMatrix();

            for (int i = 0; i < correlationMatrix.Count; i++)
            {
                var dataRow = correlationMatrix[i];
                
                Debug.Log("Correlation Matrix:");

                for (int j = 0; j < dataRow.Values.Count; j++)
                {
                    Debug.Log($"[{i}][{j}] {dataRow.Values[j]}");    
                }
            }

            var d1 = "sepal_length";
            var d2 = "sepal_width";
            var correlation = await immvisManager.Client.GetCorrelationBetweenTwoDimensions(d1, d2);

            Debug.Log($"Correlation between {d1} and {d2}: {correlation}");

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
