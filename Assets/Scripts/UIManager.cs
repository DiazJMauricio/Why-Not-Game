using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Image[] vidasImage;
    public Image barraEnergiaEmpty;
    public Image barraEnergia;
    float barraEnergiaWidthfull;
    int vidasActuales;

    public GameObject CartelLv;
    public GameObject pausaPanel;
    public GameObject optionPanel;
    public GameObject gameOver;
    public GameObject winPanel;

    ManagerGame managerGame;

    void Awake()
    {
        managerGame = FindObjectOfType<ManagerGame>();

        barraEnergiaEmpty.color = Style.GetColor(Style.barraEnergiaEmpty);
        barraEnergia.color = Style.GetColor(Style.barraEnergiaFull);
        barraEnergiaWidthfull = barraEnergiaEmpty.rectTransform.sizeDelta.x;

        ActualizarVidas();
        ActualizarEnergia();

        CartelLv.SetActive(true);
        pausaPanel.SetActive(false);
        gameOver.SetActive(false);
        winPanel.SetActive(false);

        managerGame.LevelStart += MostrarCartelLv;
        managerGame.LevelWin += MostrarWinPanel;
        managerGame.LevelLose += MostrarGameOver;
    }
    public void ShowOptions() {
        optionPanel.SetActive(true);
    }

    public void MostrarCartelLv() {
        CartelLv.SetActive(!CartelLv.activeInHierarchy);
    }
    public void MostrarPausaPanel() {
        pausaPanel.SetActive(!pausaPanel.activeInHierarchy);
        if (!pausaPanel.activeInHierarchy && optionPanel.activeInHierarchy) {
            optionPanel.SetActive(false);
        }
    }
    public void MostrarGameOver()
    {
        gameOver.SetActive(!gameOver.activeInHierarchy);
    }
    public void MostrarWinPanel()
    {
        winPanel.SetActive(!winPanel.activeInHierarchy);
    }

    public void ActualizarVidas(int vidas = 0) {        
        foreach (Image vida in vidasImage) {
            vida.color = Style.GetColor(Style.vidaEmpty);
        }
        for (int i = 0; i < vidas; i++) {
            vidasImage[i].color = Style.GetColor(Style.vidaFull);
        }
    }

    public void ActualizarEnergia(int energia = 0) {
        RectTransform rectTransform = barraEnergia.rectTransform;
        float width = (barraEnergiaWidthfull / 15) * energia;
        rectTransform.sizeDelta = new Vector2(width, rectTransform.sizeDelta.y);
    }
}
