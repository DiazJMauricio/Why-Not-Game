using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PanelOpciones : MonoBehaviour {

    public AudioMixer audioMixer;
    public Dropdown dropdownResolution;

    Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;
        dropdownResolution.ClearOptions();

        List<string> opciones = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            opciones.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height) {
                currentResolutionIndex = i;
            }
        }

        dropdownResolution.AddOptions(opciones);
        dropdownResolution.value = currentResolutionIndex;
        dropdownResolution.RefreshShownValue();
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
    }
    public void SetMusicVolume(float volume) {
        audioMixer.SetFloat("MusicVolume", volume);

    }
    public void SetEfectVolume(float volume) {
        audioMixer.SetFloat("EfectVolume", volume);
    }

    public void SetQuality(int index) {
        QualitySettings.SetQualityLevel(index);
    }

    public void SetResolution(int index)
    {
        Resolution resolution = resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }


    public void Volver() {
        gameObject.SetActive(false);
    }

    public void Aplicar() {

    }
    public void FullScreen(bool fs) {
        Screen.fullScreen = fs;
    }
}
