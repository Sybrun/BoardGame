using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject winScreenCamera;

    /// <summary>
    /// Wisselt de Main Camera met de 'winScreenCamera'; maakt het win scherm ietwat interessanter.
    /// </summary>
    public void SwitchCamera()
    {
        mainCamera.SetActive(false);
        winScreenCamera.SetActive(true);
    }
}
