using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationControl : MonoBehaviour
{
    private float CurrentXRotation
    {
        get
        {
            return gameObject.transform.rotation.eulerAngles.x;
        }
    }

    private float CurrentYRotation
    {
        get
        {
            return gameObject.transform.rotation.eulerAngles.y;
        }
    }

    private float CurrentZRotation
    {
        get
        {
            return gameObject.transform.rotation.eulerAngles.z;
        }
    }


    public void UpdateXRotation(float newRotation)
    {
        Debug.Log($"Before: {gameObject.transform.rotation.eulerAngles}");
        var rotationToSet = new Vector3(ClampRotation(180-newRotation), CurrentYRotation, CurrentZRotation);
        Debug.Log($"Rotation to set:: {rotationToSet}");
        gameObject.transform.rotation = Quaternion.Euler(rotationToSet);
        Debug.Log($"After: {gameObject.transform.rotation.eulerAngles}");
    }

    public void UpdateYRotation(float newRotation)
    {
        gameObject.transform.rotation = Quaternion.Euler(CurrentXRotation, ClampRotation(newRotation), CurrentZRotation);
    }

    public void UpdateZRotation(float newRotation)
    {
        gameObject.transform.rotation = Quaternion.Euler(CurrentXRotation, CurrentYRotation, ClampRotation(newRotation));
    }

    private float ClampRotation(float rotation)
    {
        var clamppedValue = Mathf.Clamp(rotation, 0, 360);

        if (clamppedValue == 360)
        {
            return 0;
        }

        return clamppedValue;
    }
}
