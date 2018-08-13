using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScene : MonoBehaviour {

    public string siguienteEscene;
    
    public void SiguienteEscena() {
        SceneManager.LoadScene(siguienteEscene);
    }
    public void RestarScena() {
        Scene escenaActual = SceneManager.GetActiveScene();
        SceneManager.LoadScene(escenaActual.name);
    }

    public void VolverAlMenu() {
        SceneManager.LoadScene("MenuInicio");
    }
}
