using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScalableBehaviour : MonoBehaviour
{
    private const float MIN_SCALE = 0f;

    private const float MAX_SCALE = 5f;

    public void UpdateXScale(float newScale)
    {
        PerformXScaleUpdate(ClampScale(newScale));
    }
    public void UpdateYScale(float newScale)
    {
        PerformYScaleUpdate(ClampScale(newScale));
    }
    public void UpdateZScale(float newScale)
    {
        PerformZScaleUpdate(ClampScale(newScale));
    }

    protected abstract void PerformXScaleUpdate(float newScale);

    protected abstract void PerformYScaleUpdate(float newScale);

    protected abstract void PerformZScaleUpdate(float newScale);

    private float ClampScale(float scale)
    {
        return Mathf.Clamp(scale, MIN_SCALE, MAX_SCALE);
    }
}
