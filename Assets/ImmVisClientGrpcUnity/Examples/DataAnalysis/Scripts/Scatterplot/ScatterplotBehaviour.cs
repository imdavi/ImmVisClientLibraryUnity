using System;
using System.Collections;
using System.Collections.Generic;
using Google.Protobuf.Collections;
using TMPro;
using UnityEngine;

public class ScatterplotBehaviour : MonoBehaviour
{
    [SerializeField]
    private LabelsBehaviour labelsBehaviour;

    [SerializeField]
    private GridsBehaviour gridsBehaviour;

    [SerializeField]
    private ScatterplotPlotter pointsPlotter;

    [SerializeField]
    private ScatterplotPlotter centroidsPlotter;

    public float ColorMultiplier = 0.5f;

    private const int MaxAmountOfDimensions = 5;

    internal void PlotToPoints(NormalisedDataset data, bool isTranslucent = false)
    {
        Plot(pointsPlotter, data, isTranslucent);
        UpdateLabels(data.ColumnsNames, data.ColumnsLabels);
        UpdateGrids(data.ColumnsLabels);
    }

    private void UpdateGrids(RepeatedField<ColumnsLabels> columnsLabels)
    {
        gridsBehaviour.SetupGrids(columnsLabels);
    }

    private void UpdateLabels(RepeatedField<string> columnsNames, RepeatedField<ColumnsLabels> columnsLabels)
    {
        labelsBehaviour.UpdateLabels(columnsNames, columnsLabels);
    }

    private void Plot(ScatterplotPlotter plotter, NormalisedDataset data, bool isTranslucent = false)
    {
        var values = data.Rows;
        var pointsCount = values.Count;

        Vector3[] positions = new Vector3[pointsCount];

        Vector4[] colors = new Vector4[pointsCount];

        float[] sizes = new float[pointsCount];

        for (int i = 0; i < pointsCount; i++)
        {
            var point = values[i].Values;

            Vector3 position = new Vector3();
            Vector4 color = new Vector4();
            float size = 0.0f;

            for (int dimensionIndex = 0; dimensionIndex < MaxAmountOfDimensions; dimensionIndex++)
            {
                float value = 0.0f;

                if (dimensionIndex < point.Count)
                {
                    value = point[dimensionIndex];
                }

                switch (dimensionIndex)
                {
                    case 0: // X
                        position.x = value;
                        break;
                    case 1: // Y
                        position.y = value;
                        break;
                    case 2: // Z
                        position.z = value;
                        break;
                    case 3: // Color
                        var pointColor = Color.HSVToRGB((value + 1) * ColorMultiplier, 1.0f, 1.0f);

                        color.x = pointColor.r;
                        color.y = pointColor.g;
                        color.z = pointColor.b;
                        break;
                    case 4: // Size
                        size = value;
                        break;
                }
            }
            positions[i] = position;
            colors[i] = color;
            sizes[i] = size;
        }
        // if (isTranslucent)
        // {
        //     PointsAlpha = 0.5f;
        // }
        // else
        // {
        //     PointsAlpha = 1f;
        // }
        plotter.Plot(positions, colors, sizes);
    }

    internal void PlotKMeans(KMeansAnalysisResponse datasetToPlot)
    {
        Plot(pointsPlotter, datasetToPlot.LabelsMapping, true);
        Plot(centroidsPlotter, datasetToPlot.Centroids);
        UpdateLabels(datasetToPlot.LabelsMapping.ColumnsNames, datasetToPlot.LabelsMapping.ColumnsLabels);
        UpdateGrids(datasetToPlot.LabelsMapping.ColumnsLabels);
    }

    public void UpdatePointSizeScale(float newScale) 
    {
        pointsPlotter.UpdatePointSizeScale(newScale);
        centroidsPlotter.UpdatePointSizeScale(newScale);
    }

    internal void Reset()
    {
        pointsPlotter?.ResetScatterplot();
    }
}
