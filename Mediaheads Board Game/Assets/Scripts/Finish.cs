using UnityEngine;

public class Finish : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        GetHandles();
    }

    /// <summary>
    /// GetHandles haalt alle scripts op die gerefereerd worden in dit script.
    /// </summary>
    void GetHandles()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    /// <summary>
    /// Wanneer de trigger in aanraking komt met Speler- 1 of 2, wordt de code uitgevoerd.
    /// </summary>
    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player_1" || col.tag == "Player_2")
        {
            //De finish is aangeraakt door een speler; deze speler wordt doorgegeven als winnaar.
            gameManager.LevelCompleted(col.tag);
        }
    }
}
