using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatterplotPlotter : MonoBehaviour
{
    public Mesh DataPointMesh;
    public Material DataPointMaterial;
    public Vector3 OriginPoint = new Vector3(-0.5f, -0.5f, -0.5f);
    private const float POINT_MIN_SIZE = 0.05f;
    private const float POINT_MIN_SCALE = 1f;
    private const float POINT_MAX_SCALE = 10f;
    private float PointSize = POINT_MIN_SIZE;

    [Range(0f, 1f)]
    public float PointsAlpha = 1f;

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
    private const string POINTS_ALPHA_PROPERTY_NAME = "_PointsAlpha";

    void Start()
    {
        UpdateMaterialProperties();

        argsBuffer = new ComputeBuffer(1, args.Length * sizeof(uint), ComputeBufferType.IndirectArguments);
    }

    public void UpdateMaterialProperties()
    {
        if (block == null)
        {
            block = new MaterialPropertyBlock();
        }

        block.SetMatrix(MATRIX_PROPERTY_NAME, transform.localToWorldMatrix * Matrix4x4.Translate(OriginPoint));
        block.SetFloat(POINT_MINIMUM_SIZE_PROPERTY_NAME, PointSize);
        block.SetFloat(POINTS_ALPHA_PROPERTY_NAME, PointsAlpha);
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

    public void Plot(Vector3[] positions, Vector4[] colors, float[] sizes)
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
