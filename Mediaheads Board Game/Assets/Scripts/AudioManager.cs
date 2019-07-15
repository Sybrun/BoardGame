using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    void Awake()
    {
        MakeSingleton();
    }

    /// <summary>
    /// Aanmaken van de AudioManager singleton, zodat er continu maar één AudioManager is.
    /// </summary>
    void MakeSingleton()
    {
        //Is er al een AudioManager instance?
        if (instance != null)
        {
            //vernietig deze instance;
            Destroy(gameObject);
        }
        //Is er nog géén AudioManager instance?
        else
        {
            //Maak deze instance de AudioManager instance.
            instance = this;
            //DontDestroyOnLoad() zorgt ervoor dat de AudioManager blijft leven na scene wisseling.
            DontDestroyOnLoad(gameObject);
        }
    }
}
