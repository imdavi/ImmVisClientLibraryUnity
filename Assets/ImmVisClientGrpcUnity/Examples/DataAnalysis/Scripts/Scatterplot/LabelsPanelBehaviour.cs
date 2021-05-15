using System.Collections;
using System.Collections.Generic;
using Google.Protobuf.Collections;
using TMPro;
using UnityEngine;

public class LabelsPanelBehaviour : MonoBehaviour
{
    [SerializeField]
    private RectTransform panelRectTransform;

    [SerializeField]
    private TextMeshProUGUI title;

    [SerializeField]
    private RectTransform labelsContainerRectTransform;

    [SerializeField]
    private GameObject labelsContainerGameObject;

    [SerializeField]
    private GameObject axisLabelPrefab;

    public void SetupPanel(string titleText,  RepeatedField<string> labels)
    {
        title.text = titleText;
        SetupLabels(labels);
    }

    private void SetupLabels(RepeatedField<string> labels)
    {
        RemoveAllLabels();

        float panelWidth = panelRectTransform.rect.width;

        float containerWidth = panelWidth + (panelWidth / (labels.Count - 1));
        labelsContainerRectTransform.sizeDelta = new Vector2(containerWidth, labelsContainerRectTransform.sizeDelta.y);

        foreach (var label in labels)
        {
            var labelGameObject = Instantiate(axisLabelPrefab);
            labelGameObject.transform.SetParent(labelsContainerGameObject.transform, false);
            labelGameObject.name = $"Label ({label})";

            var labelTextComponent = labelGameObject.GetComponent<TextMeshProUGUI>();

            if(labelTextComponent != null) 
            {
                labelTextComponent.text = label;
            }
        }
    }

    private void RemoveAllLabels()
    {
        foreach (Transform child in labelsContainerGameObject.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
