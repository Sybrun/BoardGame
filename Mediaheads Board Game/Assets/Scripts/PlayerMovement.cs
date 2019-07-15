using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private WaypointManager waypointManager;

    [SerializeField] private float moveSpeed = 1f;

    private bool shouldMove;

    void Start()
    {
        GetHandles();
    }

    /// <summary>
    /// Controleert wanneer de speler in beweging moet komen.
    /// </summary>
    void Update()
    {
        //Als dit script Speler 1's pion zit en de pion zou moeten bewegen, wordt code aangeroepen.
        if(transform.tag == "Player_1" && shouldMove)
        {
            //Verplaatst Speler 1's pion.
            MovePlayer_1();
            //Houdt de speler's progressie bij.
            waypointManager.CheckWaypointProgression(transform, 1);
        }
        //Als dit script Speler 2's pion zit en de pion zou moeten bewegen, wordt code aangeroepen.
        else if (transform.tag == "Player_2" && shouldMove)
        {
            //Verplaatst Speler 2's pion.
            MovePlayer_2();
            //Houdt de speler's progressie bij.
            waypointManager.CheckWaypointProgression(transform, -1);
        }
    }

    /// <summary>
    /// GetHandles haalt alle scripts op die gerefereerd worden in dit script.
    /// </summary>
    void GetHandles()
    {
        waypointManager = GameObject.FindWithTag("GameManager").GetComponent<WaypointManager>();
    }

    /// <summary>
    /// Verplaatst Speler 1 van zijn huidige positie naar de positie van de volgende waypoint/tegel.
    /// </summary>
    void MovePlayer_1()
    {
        transform.position = Vector3.MoveTowards(
                    transform.position,
                    waypointManager.player_1Waypoints[waypointManager.Player_1WaypointIndex].transform.position,
                    moveSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Verplaatst Speler 2 van zijn huidige positie naar de positie van de volgende waypoint/tegel.
    /// </summary>
    void MovePlayer_2()
    {
        transform.position = Vector3.MoveTowards(
                        transform.position,
                        waypointManager.player_2Waypoints[waypointManager.Player_2WaypointIndex].transform.position,
                        moveSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Zet shouldMove op 'true', zodat de speler in beweging kan komen.
    /// </summary>
    public void ShouldMove()
    {
        shouldMove = true;
    }

    /// <summary>
    /// Zet shouldMove op 'false, zodat de speler tot stilstand komt.
    /// </summary>
    public void StopPlayer()
    {
        shouldMove = false;
    }
}
