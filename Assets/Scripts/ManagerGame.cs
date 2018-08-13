using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerGame : MonoBehaviour {


    public static bool inPausa;
    private UIManager uiManager;


    private void Awake() {
        uiManager = FindObjectOfType<UIManager>();
    }

	// Update is called once per frame
	void Update ()
    {
        // mover a Manager Input.
        if (Input.GetButtonDown("pause"))
        {
            inPausa = !inPausa;

            if (inPausa) Pause();
            else Resume();
        }
    }

    public void StartGame()
    {/*
        lvRun = true;
        Timing.RunCoroutine(_StarFase(faseActual));
        faseActual++;
        Timing.RunCoroutine(_StarFase(faseActual));*/

    }

    public void Pause()
    {
        uiManager.MostrarPausaPanel();
        inPausa = true;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        uiManager.MostrarPausaPanel();
        inPausa = false;
        Time.timeScale = 1;
    }

    public void GameOver()
    {/*
        if (!lvEnd)
        {
            uIManager.MostrarGameOver();
            lvRun = false;
            lvEnd = true;
            Time.timeScale = 0;
            pause = true;
        }*/
    }
    public void WinGame() {

    }
}
