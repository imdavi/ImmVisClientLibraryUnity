using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationBehaviour : MonoBehaviour
{

    public float RotationMultiplier = 1f;

    public void RotateX(float speed)
    {
        transform.Rotate(speed * RotationMultiplier, 0f, 0f, Space.Self);
    }

    public void RotateY(float speed)
    {
        transform.Rotate(0f, speed * RotationMultiplier, 0f, Space.Self);
    }

    public void RotateZ(float speed)
    {
        transform.Rotate(0f, 0f, speed * RotationMultiplier, Space.Self);
    }

    public void ResetRotation()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }
}
