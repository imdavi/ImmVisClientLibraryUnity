using System;
using System.Collections;
using System.Collections.Generic;
using Google.Protobuf.Collections;
using TMPro;
using UnityEngine;

public class ScatterplotBehaviour : MonoBehaviour
{
    public Mesh DataPointMesh;
    public Material DataPointMaterial;
    public Vector3 OriginPoint = new Vector3(-0.5f, -0.5f, -0.5f);
    private const float POINT_MIN_SIZE = 0.05f;
    private const float POINT_MIN_SCALE = 1f;
    private const float POINT_MAX_SCALE = 10f;
    private float PointSize = POINT_MIN_SIZE;
    public float ColorMultiplier = 0.5f;

    [SerializeField]
    private LabelsBehaviour labelsBehaviour;

    [SerializeField]
    private GridsBehaviour gridsBehaviour;

    private uint[] args = new uint[5] { 0, 0, 0, 0, 0 };

    private ComputeBuffer positionsBuffer;
    private ComputeBuffer argsBuffer;
    private ComputeBuffer colorsBuffer;
    private ComputeBuffer sizesBuffer;

    private const int FLOAT_STRIDE = 4;

    private const int VECTOR4_BUFFER_STRIDE = FLOAT_STRIDE * 4;

    private const int VECTOR3_BUFFER_STRIDE = FLOAT_STRIDE * 3;

    private const string POSITIONS_BUFFER_NAME = "positionsBuffer";

    private const string SIZES_BUFFER_NAME = "sizesBuffer";

    private const string COLORS_BUFFER_NAME = "colorsBuffer";

    private MaterialPropertyBlock block;
    private const string MATRIX_PROPERTY_NAME = "_TransformMatrix";
    private const string POINT_MINIMUM_SIZE_PROPERTY_NAME = "_PointMinimumSize";
    private const string FILTER_Y = "_FilterY";
    private const int MaxAmountOfDimensions = 5;

    void Start()
    {
        UpdateMaterialProperties();

        argsBuffer = new ComputeBuffer(1, args.Length * sizeof(uint), ComputeBufferType.IndirectArguments);
    }

    internal void PlotData(NormalisedDataset data)
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
                        color.w = 1.0f;
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

        PlotData(positions, colors, sizes);
        labelsBehaviour.UpdateLabels(data.ColumnsNames, data.ColumnsLabels);
        gridsBehaviour.SetupGrids(data.ColumnsLabels);
    }

    public void UpdateMaterialProperties()
    {
        if (block == null)
        {
            block = new MaterialPropertyBlock();
        }

        block.SetMatrix(MATRIX_PROPERTY_NAME, transform.localToWorldMatrix * Matrix4x4.Translate(OriginPoint));
        block.SetFloat(POINT_MINIMUM_SIZE_PROPERTY_NAME, PointSize);
    }

    void Update()
    {
        UpdateMaterialProperties();

        if (DataPointMesh != null && DataPointMaterial != null && argsBuffer != null)
        {
            Graphics.DrawMeshInstancedIndirect(
                DataPointMesh,
                0,
                DataPointMaterial,
                new Bounds(Vector3.zero, new Vector3(100.0f, 100.0f, 100.0f)),
                argsBuffer,
                0,
                block
            );
        }
    }

    internal void ResetScatterplot()
    {
        OnDisable();
    }

    void OnDisable()
    {
        if (positionsBuffer != null) positionsBuffer.Release();
        positionsBuffer = null;

        if (colorsBuffer != null) colorsBuffer.Release();
        colorsBuffer = null;

        if (sizesBuffer != null) sizesBuffer.Release();
        sizesBuffer = null;

        if (argsBuffer != null) argsBuffer.Release();
        argsBuffer = null;
    }

    private void PlotData(Vector3[] positions, Vector4[] colors, float[] sizes)
    {
        if (positions.Length != colors.Length || positions.Length != sizes.Length)
        {
            Debug.Log("Make sure that positions and colors have the same length before plotting.");
            return;
        }

        var pointsCount = positions.Length;

        if (positionsBuffer != null) positionsBuffer.Release();
        if (colorsBuffer != null) colorsBuffer.Release();
        if (sizesBuffer != null) sizesBuffer.Release();

        positionsBuffer = new ComputeBuffer(pointsCount, VECTOR3_BUFFER_STRIDE);
        colorsBuffer = new ComputeBuffer(pointsCount, VECTOR4_BUFFER_STRIDE);
        sizesBuffer = new ComputeBuffer(pointsCount, FLOAT_STRIDE);

        positionsBuffer.SetData(positions);
        colorsBuffer.SetData(colors);
        sizesBuffer.SetData(sizes);

        DataPointMaterial.SetBuffer(POSITIONS_BUFFER_NAME, positionsBuffer);
        DataPointMaterial.SetBuffer(COLORS_BUFFER_NAME, colorsBuffer);
        DataPointMaterial.SetBuffer(SIZES_BUFFER_NAME, sizesBuffer);

        uint numIndices = (DataPointMesh != null) ? (uint)DataPointMesh.GetIndexCount(0) : 0;
        args[0] = numIndices;
        args[1] = (uint)pointsCount;
        argsBuffer.SetData(args);
    }

    public void UpdatePointSizeScale(float newScale)
    {
        PointSize = Mathf.Clamp(newScale, POINT_MIN_SCALE, POINT_MAX_SCALE) * POINT_MIN_SIZE;
    }
}
