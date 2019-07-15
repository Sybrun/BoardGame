using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    [SerializeField] private int cameraRotationSpeed;

    void Update()
    {
        RotateCamera();
    }

    /// <summary>
    /// Roteert de camera; zorgt voor afwisseling.
    /// </summary>
    void RotateCamera()
    {
        transform.Rotate(0, cameraRotationSpeed * Time.deltaTime, 0);
    }
}
