using UnityEngine;

public class DiceSide : MonoBehaviour
{
    private bool onGround;

    [SerializeField] private int sideValue;
    public int SideValue { get { return sideValue; } }

    /// <summary>
    /// Wanneer de trigger van het GameObject in aanraking komt met de grond, zal OnGround 'true' worden.
    /// </summary>
    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Ground")
        {
            onGround = true;
        }
    }

    /// <summary>
    /// Wanneer de trigger van het GameObject de aanraking met de grond verliest, wordt OnGround 'false'.
    /// </summary>
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Ground")
        {
            onGround = false;
        }
    }

    /// <summary>
    /// Deze functie wordt aangeroepen door het Dice Script om te controleren of de dobbelsteen geland is.
    /// </summary>
    public bool OnGround()
    {
        //De waarde van OnGround wordt teruggegeven na het aanroepen van de functie.
        return onGround;
    }
}
