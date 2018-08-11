using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicio : MonoBehaviour {

    public void NuevaPartida() {
        SceneManager.LoadScene("main");
    }

    public void Continuar() {
        SceneManager.LoadScene("main");
    }

    public void Opciones() {

    }
    public void Arcade() {

    }
    public void Salir() {
        Application.Quit();
    }
}
