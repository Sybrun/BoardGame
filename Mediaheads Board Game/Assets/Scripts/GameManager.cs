using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private WaypointManager waypointManager;
    private UIController uIController;
    private DiceController diceController;
    private PlayerMovement playerMovement;
    private CameraSwitcher cameraSwitcher;
    private DiceColorChanger diceColorChanger;

    [SerializeField] private Dice[] dices;

    public int PlayerTurn { get; private set; } = 1;

    void Start()
    {
        GetHandles();
    }

    /// <summary>
    /// GetHandles haalt alle scripts op die gerefereerd worden in dit script.
    /// </summary>
    void GetHandles()
    {
        uIController = GameObject.FindWithTag("GameManager").GetComponent<UIController>();
        diceController = GameObject.FindWithTag("GameManager").GetComponent<DiceController>();
        waypointManager = GameObject.FindWithTag("GameManager").GetComponent<WaypointManager>();
        cameraSwitcher = GameObject.FindWithTag("CameraController").GetComponent<CameraSwitcher>();
        diceColorChanger = GameObject.FindWithTag("GameManager").GetComponent<DiceColorChanger>();
    }

    /// <summary>
    /// Deze functie wacht met uitvoeren van de code totdat de movePlayer event is aangeroepen.
    /// </summary>
    public void MovePlayerListener()
    {
        //Eerst wordt er gekeken naar welke speler momenteel aan de beurt is.
        if (PlayerTurn == 1)
        {
            //Haalt het PlayerMovement script op van de speler.
            playerMovement = GameObject.FindWithTag("Player_1").GetComponent<PlayerMovement>();
            //Zet een bool op 'true', resulterend in activiteit in het PlayerMovement script.
            playerMovement.ShouldMove();
        }
        else if (PlayerTurn == -1)
        {
            //Haalt het PlayerMovement script op van de speler.
            playerMovement = GameObject.FindWithTag("Player_2").GetComponent<PlayerMovement>();
            //Zet een bool op 'true', resulterend in activiteit in het PlayerMovement script.
            playerMovement.ShouldMove();
        }
    }

    /// <summary>
    /// Wordt aangeroepen op het eind van een speler's beurt en stoomt de code klaar voor de volgende speler.
    /// </summary>
    public void NextTurn()
    {
        //Zorgt ervoor dat de volgende speler aan de beurt is (1 = Speler 1, -1 = Speler 2)
        PlayerTurn *= -1;
        //Toont de correcte UI, die aangeeft welke speler aan de beurt is.
        uIController.EnableCorrectPlayerText(PlayerTurn);
        //Herstelt de tekst die de waarde van de worp weergeeft.
        uIController.RestoreDiceValueText();
        //Past de kleur van de dobbelstenen aan.
        diceColorChanger.SwitchDiceColor(PlayerTurn);
        //RestoreValues wordt in x aantal seconden aangeroepen, zodat er een kleine pauze is.
        Invoke("RestoreValues", 0.5f);
    }

    /// <summary>
    /// Deze functie verwijderd alle geschiedenis van de dobbelsteen- en andere waarden die relevant waren 
    /// voor de vorige worp.
    /// </summary>
    void RestoreValues()
    {
        //Behandelt de geschiedenis van het DiceController script.
        diceController.ResetDiceValues();
        //Herstelt de interactiviteit van de rollDiceButton.
        uIController.ButtonShouldBeInteractable(true);
        //Behandelt de geschiedenis van elk Dice script in de dices[].
        foreach (Dice dice in dices)
        {
            dice.calledOnce = false;
        }
    }

    /// <summary>
    /// Wordt aangeroepen wanneer het level is voltooid door een speler.
    /// </summary>
    public void LevelCompleted(string winner)
    {
        //Toont de juiste winnaar
        uIController.UpdateWinnerText(winner);
        //Toont het 'winScreen' paneel.
        uIController.ShowWinScreen(winner);
        //Zet alle UI die niet relevant is voor het 'winScreen' paneel op inactief.
        uIController.DisableIrrelevantUI();
        //Zet de tweede camera op actief.
        cameraSwitcher.SwitchCamera();
    }
}
