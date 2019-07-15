using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    private AudioSource audioSource;

    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject settingsPanel;

    private void Start()
    {
        GetHandles();
        //Zet de waarde van de volumeSlider gelijk aan de AudioSource volume.
        volumeSlider.value = audioSource.volume;
    }

    /// <summary>
    /// GetHandles haalt alle componenten op die gerefereerd worden in dit script.
    /// </summary>
    void GetHandles()
    {
        audioSource = GameObject.FindWithTag("AudioManager").GetComponent<AudioSource>();
    }

    /// <summary>
    /// Past de volume aan, op basis van de waarde van de volumeSlider.
    /// </summary>
    public void UpdateVolume()
    {
        audioSource.volume = volumeSlider.value;
    }

    /// <summary>
    /// Bepaalt of settingsPanel actief of inactief wordt gezet.
    /// </summary>
    public void SettingsPanelState(bool state)
    {
        settingsPanel.SetActive(state);
    }

    /// <summary>
    /// Bepaalt of menuPanel actief of inactief wordt gezet.
    /// </summary>
    public void MenuPanelState(bool state)
    {
        menuPanel.SetActive(state);
    }
}
