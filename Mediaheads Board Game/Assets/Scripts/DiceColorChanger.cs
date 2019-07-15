using UnityEngine;

public class DiceColorChanger : MonoBehaviour
{
    [SerializeField] private Color redDice;
    [SerializeField] private Color blueDice;

    private Material m_Dice;

    [SerializeField] private GameObject[] dices;

    /// <summary>
    /// Behandelt de kleur van de dobbelstenen.
    /// </summary>
    public void SwitchDiceColor(int playerTurn)
    {
        //Eerst wordt er gekeken naar wie momenteel aan de beurt is. (1 = Speler 1, -1 = Speler 2)
        switch (playerTurn)
        {
            case 1:
                //Voor elke dobbelsteen in de GameObject[] wordt de volgende code uitgevoerd.
                foreach (GameObject dice in dices)
                {
                    //Ophalen van de material.
                    m_Dice = dice.GetComponent<Renderer>().material;
                    //Material kleur aanpassen op Speler 1 kleur (rood).
                    m_Dice.color = redDice;
                }
                break;
            case -1:
                //Voor elke dobbelsteen in de GameObject[] wordt de volgende code uitgevoerd.
                foreach (GameObject dice in dices)
                {
                    //Ophalen van de material.
                    m_Dice = dice.GetComponent<Renderer>().material;
                    //Material kleur aanpassen op Speler 2 kleur (blauw).
                    m_Dice.color = blueDice;
                }
                break;
        }
    }

}
