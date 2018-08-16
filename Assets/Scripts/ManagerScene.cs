using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScene : MonoBehaviour {

    public string siguienteEscena;
    private void Awake()
    {
        ManagerGame mg = FindObjectOfType<ManagerGame>();
        mg.LevelOver += SiguienteEscena;
    }

    public void SiguienteEscena() {
        SceneManager.LoadScene(siguienteEscena);
    }
    public void ReiniciarScena() {
        Scene escenaActual = SceneManager.GetActiveScene();
        SceneManager.LoadScene(escenaActual.name);
    }

    public void VolverAlMenu() {
        SceneManager.LoadScene("MenuInicio");
    }
}
