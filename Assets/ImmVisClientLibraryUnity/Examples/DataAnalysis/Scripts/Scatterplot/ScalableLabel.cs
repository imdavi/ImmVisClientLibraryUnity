using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalableLabel : ScalableBehaviour
{
    [SerializeField]
    private RectTransform panelRectTransform;

    [SerializeField]
    private RectTransform labelsContainerRectTransform;

    [SerializeField]
    private float PositionScaleFactor = 0.867f;

    private Vector3 initialPosition = Vector3.one;


    private Vector3 CurrentPosition
    {
        get
        {
            return gameObject.transform.localPosition;
        }
        set
        {
            gameObject.transform.localPosition = value;
        }
    }

    private Vector2 initialPanelSize = Vector2.zero;

    private Vector2 CurrentPanelSize
    {
        get { return panelRectTransform.sizeDelta; }
        set { panelRectTransform.sizeDelta = value; }
    }

    private Vector2 CurrentLabelsContainerSize
    {
        get { return labelsContainerRectTransform.sizeDelta; }
        set { labelsContainerRectTransform.sizeDelta = value; }
    }

    void Start()
    {
        initialPanelSize = panelRectTransform.sizeDelta;
        initialPosition = CurrentPosition;
    }

    protected override void PerformXScaleUpdate(float newScale)
    {
        float labelsWidthProportion = CurrentLabelsContainerSize.x / CurrentPanelSize.x;
        CurrentPanelSize = new Vector2(initialPanelSize.x * newScale, CurrentPanelSize.y);
        panelRectTransform.ForceUpdateRectTransforms();
        CurrentLabelsContainerSize = new Vector2(CurrentPanelSize.x * labelsWidthProportion, CurrentLabelsContainerSize.y);
        labelsContainerRectTransform.ForceUpdateRectTransforms();
    }

    protected override void PerformYScaleUpdate(float newScale)
    {
        // CurrentPanelSize = new Vector2(CurrentPanelSize.x, initialPanelSize.y * newScale);
        // panelRectTransform.ForceUpdateRectTransforms();
    }

    protected override void PerformZScaleUpdate(float newScale) { /* We don't need to update Z axis from a canvas. */   }

    public void UpdateXPositionScale(float newScale)
    {
        CurrentPosition = new Vector3(initialPosition.x * GetPositionEffectiveScale(newScale), CurrentPosition.y, CurrentPosition.z);
    }

    public void UpdateYPositionScale(float newScale)
    {
        CurrentPosition = new Vector3(CurrentPosition.x, initialPosition.y * GetPositionEffectiveScale(newScale), CurrentPosition.z);
    }

    public void UpdateZPositionScale(float newScale)
    {
        CurrentPosition = new Vector3(CurrentPosition.x, CurrentPosition.y, initialPosition.z * GetPositionEffectiveScale(newScale));
    }

    private float GetPositionEffectiveScale(float scale)
    {
        var effectiveScale = scale;

        if (effectiveScale > 1)
        {
            effectiveScale *= PositionScaleFactor;
        }

        return effectiveScale;
    }
}
