using System.Collections;
using System.Collections.Generic;
using Google.Protobuf.Collections;
using UnityEngine;

public class GridsBehaviour : MonoBehaviour
{
    [SerializeField]
    private UIGridRenderer gridRendererBottom;

    [SerializeField]
    private UIGridRenderer gridRendererLeft;

    [SerializeField]
    private UIGridRenderer gridRendererBack;


    public void SetupGrids(RepeatedField<ColumnsLabels> columnsLabels)
    {
        if (columnsLabels.Count >= 3)
        {
            var xColumsnAmount = columnsLabels[0].Labels.Count - 1;
            var yColumsnAmount = columnsLabels[1].Labels.Count - 1;
            var zColumsnAmount = columnsLabels[2].Labels.Count - 1;


            gridRendererBottom.gridSize = new Vector2Int(xColumsnAmount, zColumsnAmount);
            gridRendererBack.gridSize = new Vector2Int(xColumsnAmount, yColumsnAmount);
            gridRendererLeft.gridSize = new Vector2Int(zColumsnAmount, yColumsnAmount);

            gridRendererBottom.SetAllDirty();
            gridRendererBack.SetAllDirty();
            gridRendererLeft.SetAllDirty();
        }
    }
}
