using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicio : MonoBehaviour {

    public void NuevaPartida() {
        SceneManager.LoadScene("pruebas nueva version");
    }

    public void Continuar() {
        SceneManager.LoadScene("pruebas nueva version");
    }

    public void Opciones() {

    }
    public void Arcade() {

    }
    public void Salir() {
        Application.Quit();
    }
}
