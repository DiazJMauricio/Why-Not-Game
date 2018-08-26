using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicio : MonoBehaviour {
    public GameObject panelOpciones;

    public void NuevaPartida() {
        SceneManager.LoadScene("pruebas nueva version");
    }

    public void Continuar() {
        SceneManager.LoadScene("pruebas nueva version");
    }

    public void Opciones() {
        panelOpciones.SetActive(true);
    }
    public void Arcade() {

    }
    public void Salir() {
        Application.Quit();
    }
}
