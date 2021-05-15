using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalableGameObject : ScalableBehaviour
{
    private Vector3 initialScale = Vector3.one;
    private Vector3 initialPosition = Vector3.one;

    [SerializeField]
    private bool PreventXScale = false;

    [SerializeField]
    private bool PreventYScale = false;

    [SerializeField]
    private bool PreventZScale = false;

    [SerializeField]
    [Range(0.1f, 1f)]
    public float ScaleFactor = 1.0f;

    private Vector3 CurrentScale
    {
        get { return gameObject.transform.localScale; }
        set { gameObject.transform.localScale = value; }
    }

    private Vector3 CurrentPosition
    {
        get { return gameObject.transform.localPosition; }
        set { gameObject.transform.localPosition = value; }
    }

    void Start()
    {
        initialScale = CurrentScale;
        initialPosition = CurrentPosition;
    }

    protected override void PerformXScaleUpdate(float newScale)
    {
        if (!PreventXScale)
        {
            CurrentScale = new Vector3(initialScale.x * GetEffectiveScale(newScale), CurrentScale.y, CurrentScale.z);
        }
        CurrentPosition = new Vector3(initialPosition.x * GetEffectiveScale(newScale), CurrentPosition.y, CurrentPosition.z);
    }
    protected override void PerformYScaleUpdate(float newScale)
    {
        if (!PreventYScale)
        {
            CurrentScale = new Vector3(CurrentScale.x, initialScale.y * GetEffectiveScale(newScale), CurrentScale.z);
        }
        CurrentPosition = new Vector3(CurrentPosition.x, initialPosition.y * GetEffectiveScale(newScale), CurrentPosition.z);
    }

    protected override void PerformZScaleUpdate(float newScale)
    {
        if (!PreventZScale)
        {
            CurrentScale = new Vector3(CurrentScale.x, CurrentScale.y, initialScale.z * GetEffectiveScale(newScale));
        }
        CurrentPosition = new Vector3(CurrentPosition.x, CurrentPosition.y, initialPosition.z * GetEffectiveScale(newScale));
    }

    private float GetEffectiveScale(float scale)
    {
        var effectiveScale = scale;

        if (effectiveScale > 1)
        {
            effectiveScale *= ScaleFactor;
        }
        return effectiveScale;
    }
}
