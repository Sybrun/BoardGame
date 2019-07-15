using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject rollDiceButton;
    [SerializeField] private GameObject player_1UI;
    [SerializeField] private GameObject player_2UI;
    [SerializeField] private GameObject winScreen;

    [SerializeField] private Text winnerText;
    [SerializeField] private Text diceValueText;

    /// <summary>
    /// Zet de rolDiceButton actief of inactief op basis van de bool die wordt meegegeven 
    /// bij het aanroepen van deze functie.
    /// </summary>
    public void ButtonShouldBeInteractable(bool state)
    {
        rollDiceButton.GetComponent<Button>().interactable = state;
    }

    /// <summary>
    /// Zet de correcte UI actief op basis van de int die wordt meegegeven bij het aanroepen 
    /// van deze functie.
    /// </summary>
    public void EnableCorrectPlayerText(int player)
    {
        switch (player)
        {
            case 1:
                player_1UI.SetActive(true);
                player_2UI.SetActive(false);
                break;
            case -1:
                player_1UI.SetActive(false);
                player_2UI.SetActive(true);
                break;
        }
    }

    /// <summary>
    /// Behandelt de dobbelsteenworp UI; Geeft aan hoeveel ogen zijn gegooid.
    /// </summary>
    public void UpdateDiceValueText(int value)
    {
        diceValueText.text = value.ToString();
    }

    /// <summary>
    /// Reset de dobbelsteenworp UI; Wanneer de volgende speler aan de beurt is.
    /// </summary>
    public void RestoreDiceValueText()
    {
        diceValueText.text = "";
    }

    /// <summary>
    /// Behandelt het 'winScreen' paneel UI; Wanneer de finish is aangeraakt.
    /// </summary>
    public void ShowWinScreen(string winner)
    {
        //Zet het 'winScreen' paneel op actief;
        winScreen.SetActive(true);
    }

    /// <summary>
    /// Toont de correcte winnaar text.
    /// </summary>
    public void UpdateWinnerText(string winner)
    {
        if(winner == "Player_1")
        {
            winnerText.color = Color.red;
            winnerText.text = "SPELER 1";
        }
        else if(winner == "Player_2")
        {
            winnerText.color = Color.blue;
            winnerText.text = "SPELER 2";
        }
        //Mocht er een ogeldige winnaar zijn, zal er een bericht in de Console verschijnen.
        else
        {
            winnerText.text = "ERROR";
            Debug.Log("ERROR: Inappropriate winner at UIController.cs/UpdateWinnerText");
        }
    }

    /// <summary>
    /// Haalt irrelevante UI weg; stoomt de UI klaar voor het 'winScreen' paneel.
    /// </summary>
    public void DisableIrrelevantUI()
    {
        player_1UI.SetActive(false);
        player_2UI.SetActive(false);
        rollDiceButton.SetActive(false);
    }
}
