using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalableLabelsPanels : ScalableBehaviour
{
    [SerializeField]
    private ScalableLabel bottomLabelsPanel;

    [SerializeField]
    private ScalableLabel rightLabelsPanel;

    [SerializeField]
    private ScalableLabel backLabelsPanel;

    protected override void PerformXScaleUpdate(float newScale)
    {
        bottomLabelsPanel.UpdateXScale(newScale);
        rightLabelsPanel.UpdateYScale(newScale);
        backLabelsPanel.UpdateYScale(newScale);

        bottomLabelsPanel.UpdateXPositionScale(newScale);
        rightLabelsPanel.UpdateXPositionScale(newScale);
        backLabelsPanel.UpdateXPositionScale(newScale);
    }

    protected override void PerformYScaleUpdate(float newScale)
    {
        backLabelsPanel.UpdateXScale(newScale);

        bottomLabelsPanel.UpdateYPositionScale(newScale);
        rightLabelsPanel.UpdateYPositionScale(newScale);
        backLabelsPanel.UpdateYPositionScale(newScale);
    }

    protected override void PerformZScaleUpdate(float newScale)
    {
        bottomLabelsPanel.UpdateYScale(newScale);
        rightLabelsPanel.UpdateXScale(newScale);

        bottomLabelsPanel.UpdateZPositionScale(newScale);
        rightLabelsPanel.UpdateZPositionScale(newScale);
        backLabelsPanel.UpdateZPositionScale(newScale);
    }
}
