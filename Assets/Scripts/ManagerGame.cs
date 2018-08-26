using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerGame : MonoBehaviour {


    public static bool inPausa;
    public static bool onGame;
    public static bool gameOver;
    private UIManager uiManager;

    public delegate void OnLevelChange();
    public event OnLevelChange LevelIntro;
    public event OnLevelChange LevelStart;
    public event OnLevelChange LevelWin;
    public event OnLevelChange LevelLose;
    public event OnLevelChange LevelOver;

    private void Awake() {
        inPausa = false;
        gameOver = false;
        onGame = false;
        Time.timeScale = 1;

        uiManager = FindObjectOfType<UIManager>();
    }
    private void Start()
    {
        LevelIntro();
        Invoke("StartGame", 5f);
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
    {
        LevelStart();
        onGame = true;
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
    {
        if (!gameOver)
        {
            LevelLose();
            gameOver = true;
            Time.timeScale = 0;
            inPausa = true;
        }
    }
    public void WinGame() {
        LevelWin();
        onGame = false;
        Invoke("OverLevel", 5);
    }

    public void OverLevel() {
        LevelOver();
    }
}
