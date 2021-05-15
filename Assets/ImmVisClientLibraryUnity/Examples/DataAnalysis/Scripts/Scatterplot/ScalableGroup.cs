using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalableGroup : ScalableBehaviour
{
    [SerializeField]
    private List<ScalableBehaviour> Scalables;

    protected override void PerformXScaleUpdate(float newScale)
    {
        foreach (var scalable in Scalables)
        {
            scalable.UpdateXScale(newScale);
        }
    }
    protected override void PerformYScaleUpdate(float newScale)
    {
        foreach (var scalable in Scalables)
        {
            scalable.UpdateYScale(newScale);
        }
    }

    protected override void PerformZScaleUpdate(float newScale)
    {
        foreach (var scalable in Scalables)
        {
            scalable.UpdateZScale(newScale);
        }
    }
}
