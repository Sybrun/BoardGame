using UnityEngine;
using UnityEngine.Events;

public class DiceController : MonoBehaviour
{
    private UIController uIController;
    [SerializeField] private GameObject[] dice;

    [SerializeField] private float minRollForce = 50;
    [SerializeField] private float maxRollForce = 100;
    [SerializeField] private float eventExecutionDelay = 1f;

    public bool ShouldCalculateDiceValue { get; private set; }
    [HideInInspector] public bool calledOnce;

    public int DiceTotalValue { get; private set; }
    [HideInInspector] public int landedDice;

    public UnityEvent movePlayer;

    void Start()
    {
        GetHandles();
    }

    /// <summary>
    /// Hier wordt bijgehouden of alle dobbelstenen zijn geland en actie ondernomen wanneer dat het geval is.
    /// </summary>
    void Update()
    {
        if (landedDice >= dice.Length && !calledOnce)
        {
            Debug.Log("Total: " + DiceTotalValue);

            //Alle dobbelstenen zijn geland en de waarden van de dobbelstenen zijn al opgeteld.
            ShouldCalculateDiceValue = false;
            //Extra visual feedback voor de speler; weergeeft in de UI wat de speler heeft gegooid.
            uIController.UpdateDiceValueText(DiceTotalValue);
            //De ExecuteEvent wordt na x aantal seconden aangeroepen.
            Invoke("ExecuteEvent", eventExecutionDelay);
            //calledOnce wordt op 'true' gezet zodat alle functionaliteit maar één keer wordt aangeroepen.
            calledOnce = true;
        }
    }

    /// <summary>
    /// GetHandles haalt alle scripts op die gerefereerd worden in dit script.
    /// </summary>
    void GetHandles()
    {
        uIController = GameObject.FindWithTag("GameManager").GetComponent<UIController>();
    }

    public void RollDice()
    {
        MoveDice();
        //Hier geven we met een bool aan dat de waarde van de dobbelstenen berekend moet worden.
        ShouldCalculateDiceValue = true;
    }

    /// <summary>
    /// Alle dobbelstenen uit de GameObject[] dice worden in een willekeurige richting gegooid.
    /// </summary>
    void MoveDice()
    {
        foreach(GameObject d in dice)
        {
            Rigidbody rigidBody = d.GetComponent<Rigidbody>();
            //Geeft rotatie aan de dobbelstenen
            rigidBody.AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));
            //Geeft kracht aan de dobbelstenen, waar minRollForce en maxRollForce de hoogte van de dobbelstenen bepalen.
            rigidBody.AddForce(Random.Range(-20, 20), Random.Range(minRollForce, maxRollForce), Random.Range(-20, 20));
        }
    }

    /// <summary>
    /// Wordt aangeroepen als een dobbelsteen landt en geeft zijn waarde door aan de totaalwaarde van de worp.
    /// </summary>
    public void AddDiceValue(int amount)
    {
        DiceTotalValue += amount;
    }

    /// <summary>
    /// verwijdert de geschiedenis van de dobbelsteenwaarden.
    /// </summary>
    public void ResetDiceValues()
    {
        landedDice = 0;
        DiceTotalValue = 0;
        calledOnce = false;
    }

    /// <summary>
    /// Roept de movePlayer event aan.
    /// </summary>
    void ExecuteEvent()
    {
        movePlayer.Invoke();
    }
}
