using System.Collections;
using System.Collections.Generic;
using Google.Protobuf.Collections;
using UnityEngine;

public class LabelsBehaviour : MonoBehaviour
{

    [SerializeField]
    private LabelsPanelBehaviour bottomLabelsPanel;

    [SerializeField]
    private LabelsPanelBehaviour rightLabelsPanel;

    [SerializeField]
    private LabelsPanelBehaviour backLabelsPanel;

    public void UpdateLabels(RepeatedField<string> columnsNames, RepeatedField<ColumnsLabels> columnsLabels)
    {
        for (int i = 0; i < Mathf.Clamp(columnsNames.Count, 1, 3); i++)
        {
            var name = columnsNames[i];
            var labels = columnsLabels[i].Labels;

            switch (i)
            {
                case 0:
                    bottomLabelsPanel.SetupPanel(name, labels);
                    break;
                case 1:
                    backLabelsPanel.SetupPanel(name, labels);
                    break;
                case 2:
                    rightLabelsPanel.SetupPanel(name, labels);
                    break;
                default:
                    Debug.Log($"Column: {name} ({string.Join(",", labels)})");
                    break;
            }

        }
    }
}
