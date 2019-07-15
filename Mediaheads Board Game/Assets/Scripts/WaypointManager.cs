using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    private GameManager gameManager;
    private DiceController diceController;
    private PlayerMovement playerMovement;
    private UIController uIController;

    public Transform[] player_1Waypoints;
    public Transform[] player_2Waypoints;

    public int Player_1CurrentWaypoint { get; private set; }
    public int Player_2CurrentWaypoint { get; private set; }
    public int Player_1WaypointIndex { get; private set; }
    public int Player_2WaypointIndex { get; private set; }

    private int stepsTaken;
    private int destination;

    private bool reachedDestination;

    void Start()
    {
        GetHandles();
    }

    /// <summary>
    /// Controleert of de speler zijn eindbestemming heeft beriekt.
    /// </summary>
    void Update()
    {
        if (reachedDestination)
        {
            //Herstelt de waarde van de reachedDestination bool, zodat deze code maar één keer wordt aangeroept.
            reachedDestination = false;
            //De speler heeft zijn eindbestemming bereikt, dus de volgende speler is aan de beurt.
            gameManager.NextTurn();
        }
    }

    /// <summary>
    /// GetHandles haalt alle scripts op die gerefereerd worden in dit script.
    /// </summary>
    void GetHandles()
    {
        diceController = GameObject.FindWithTag("GameManager").GetComponent<DiceController>();
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        uIController = GameObject.FindWithTag("GameManager").GetComponent<UIController>();
    }

    /// <summary>
    /// Deze functie is verantwoordelijk voor het bijhouden van de speler's progressie op het bord.
    /// </summary>
    public void CheckWaypointProgression(Transform playerTransform, int playerTurn)
    {
        //Eerst wordt er gekeken naar wie momenteel aan de beurt is. (1 = Speler 1, -1 = Speler 2)
        switch (playerTurn)
        {
            case 1:
                //Als de positie van Speler 1 gelijk is aan de positie van de volgende waypoint wordt deze code uitgevoerd.
                if (playerTransform.position == player_1Waypoints[Player_1WaypointIndex].transform.position)
                {
                    //Waypoint index gaat één omhoog zodat de speler een nieuwe locatie heeft om naar te reizen.
                    Player_1WaypointIndex += 1;
                    //Genomen stappen worden bijgehouden om te controleren dat de speler niet meer stappen neemt dan de gegooide ogen.
                    stepsTaken += 1;
                    //Visual feedback voor de speler; Als een speler een stap neemt, past de UI zich hierop aan.
                    uIController.UpdateDiceValueText(diceController.DiceTotalValue - stepsTaken);
                }

                //Als de positie van Speler 1 gelijk is aan de positie van de eindbestemming wordt deze code uitgevoerd.
                if (playerTransform.position == player_1Waypoints[CalculateDestination(playerTurn)].transform.position)
                {
                    //De waypoint van Speler 1 wordt bijgewerkt, zodat de eindbestemming de volgende beurt gelijk is aan de startpositie.
                    UpdateCurrentWaypoint(stepsTaken, playerTurn);
                    //Verwijderen van de genomen stappen geschiedenis.
                    stepsTaken = 0;
                    //Aangeven dat de speler zijn eindbestemming heeft bereikt.
                    reachedDestination = true;

                    //Ophalen van Speler 1's PlayerMovement script.
                    playerMovement = GameObject.FindWithTag("Player_1").GetComponent<PlayerMovement>();
                    //De speler stoppen.
                    playerMovement.StopPlayer();
                }
                break;

            case -1:
                //Als de positie van Speler 2 gelijk is aan de positie van de volgende waypoint wordt deze code uitgevoerd.
                if (playerTransform.position == player_2Waypoints[Player_2WaypointIndex].transform.position)
                {
                    //Waypoint index gaat één omhoog zodat de speler een nieuwe locatie heeft om naar te reizen.
                    Player_2WaypointIndex += 1;
                    //Genomen stappen worden bijgehouden om te controleren dat de speler niet meer stappen neemt dan de gegooide ogen.
                    stepsTaken += 1;
                    //Visual feedback voor de speler; Als een speler een stap neemt, past de UI zich hierop aan.
                    uIController.UpdateDiceValueText(diceController.DiceTotalValue - stepsTaken);
                }

                //Als de positie van Speler 2 gelijk is aan de positie van de eindbestemming wordt deze code uitgevoerd.
                if (playerTransform.position == player_2Waypoints[CalculateDestination(playerTurn)].transform.position)
                {
                    //De waypoint van Speler 2 wordt bijgewerkt, zodat de eindbestemming de volgende beurt gelijk is aan de startpositie.
                    UpdateCurrentWaypoint(stepsTaken, playerTurn);
                    //Verwijderen van de genomen stappen geschiedenis.
                    stepsTaken = 0;
                    //Aangeven dat de speler zijn eindbestemming heeft bereikt.
                    reachedDestination = true;

                    //Ophalen van Speler 2's PlayerMovement script.
                    playerMovement = GameObject.FindWithTag("Player_2").GetComponent<PlayerMovement>();
                    //De speler stoppen.
                    playerMovement.StopPlayer();
                }
                break;
        }
    }

    /// <summary>
    /// Wanneer de waypoint bijgewerkt moet worden, wordt deze functie aangeroept.
    /// </summary>
    public void UpdateCurrentWaypoint(int currentWaypoint, int playerTurn)
    {
        //Eerst wordt er gekeken naar wie momenteel aan de beurt is. (1 = Speler 1, -1 = Speler 2)
        switch (playerTurn)
        {
            case 1:
                //De waypoint index van Speler 1 wordt bijgewerkt zodat de juiste startpositie en eindbestemming kan worden berekend.
                Player_1CurrentWaypoint += currentWaypoint;
                break;

            case -1:
                //De waypoint index van Speler 2 wordt bijgewerkt zodat de juiste startpositie en eindbestemming kan worden berekend.
                Player_2CurrentWaypoint += currentWaypoint;
                break;
        }
    }

    /// <summary>
    /// Hier wordt de eindbestemming van de speler berekend.
    /// </summary>
    int CalculateDestination(int playerTurn)
    {
        //Eerst wordt er gekeken naar wie momenteel aan de beurt is. (1 = Speler 1, -1 = Speler 2)
        switch (playerTurn)
        {
            case 1:
                //De eindbestemming is de huidige positie + het aantal gegooide ogen ( -1, want de array is 0-based)
                destination = Player_1CurrentWaypoint + diceController.DiceTotalValue - 1;

                //Als de eindbestemming index hoger is dan het aantal waypoints, wordt de eindbestemming op de laatste waypoint gezet.
                if (destination > player_1Waypoints.Length - 1)
                {
                    destination = player_1Waypoints.Length - 1;
                }
                break;
            case -1:
                //De eindbestemming is de huidige positie + het aantal gegooide ogen ( -1, want de array is 0-based)
                destination = Player_2CurrentWaypoint + diceController.DiceTotalValue - 1;

                //Als de eindbestemming index hoger is dan het aantal waypoints, wordt de eindbestemming op de laatste waypoint gezet.
                if (destination > player_2Waypoints.Length - 1)
                {
                    destination = player_2Waypoints.Length - 1;
                }
                break;
        }
        //Vervolgens wordt de waarde van de eindbestemming teruggegeven zodat de rest van de code de speler correct kan aansturen.
        return destination;
    }
}
