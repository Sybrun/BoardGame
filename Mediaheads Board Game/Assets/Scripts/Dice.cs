using UnityEngine;

public class Dice : MonoBehaviour
{
    private DiceSide diceSide;
    private Rigidbody rigidBody;
    private DiceController diceController;

    [SerializeField] private DiceSide[] diceSides;
    public bool calledOnce;

    void Start()
    {
        GetHandles();
    }

    /// <summary>
    /// Hier wordt bijgehouden of de dobbelsteen stil ligt en of de waarde van de worp nog berekend moet worden.
    /// </summary>
    void Update()
    {
        if(rigidBody.IsSleeping() && diceController.ShouldCalculateDiceValue && !calledOnce)
        {
            //Als de dobbelsteen stil ligt wordt de waarde opgehaald.
            SideValueCheck();
            calledOnce = true;
        }
    }

    /// <summary>
    /// GetHandles haalt alle scripts op die gerefereerd worden in dit script.
    /// </summary>
    void GetHandles()
    {
        diceController = GameObject.FindWithTag("GameManager").GetComponent<DiceController>();
        rigidBody = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Elk DiceSide script in de diceSide[] wordt gecontroleerd of de desbetreffende zijde op de grond ligt.
    /// Als een zijde op de grond ligt (een dobbelsteen geland is), wordt de nodige informatie doorgegeven
    /// </summary>
    void SideValueCheck()
    {
        foreach (DiceSide side in diceSides)
        {
            if (side.OnGround())
            {
                //Doorgeven aan het DiceController script dat er een dobbelsteen is geland.
                diceController.landedDice += 1;
                //Waarde van de stilliggende dobbelsteen doorgeven aan de totaalwaarde van de worp.
                diceController.AddDiceValue(side.SideValue);

                Debug.Log(side.SideValue + " has been rolled");
            }
        }
    }
}
