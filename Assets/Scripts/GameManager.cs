using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static bool pause;
    public static bool lvRun;

    public Nivel nivel;

    static int faseActual;
    static List<int> controlDeInstanciaDeNivel = new List<int>();
    UIManager uIManager;
    private bool lvEnd;

    private void Awake() {
        uIManager = FindObjectOfType<UIManager>();
        StartLevel();
        CotroladorDeFase();
    }
    
	void Update () {
        if (Input.GetKeyDown(KeyCode.F)) {
            RestarEscene();
        }


        if (Input.GetButtonDown("pause")) {
            pause = !pause;
            if (pause) {
                Pause();
            }
            else {
                Resume();
            }
        }
    }

    public void StartGame() {//
        lvRun = true;
        Timing.RunCoroutine(_StarFase(faseActual));
        faseActual++;
        Timing.RunCoroutine(_StarFase(faseActual));
    }
    public void EndGame() {
        GameObject player = FindObjectOfType<PlayerMoveManager>().gameObject;
        Timing.RunCoroutine(_DespedirPlayer(player));
        uIManager.MostrarWinPanel();
        lvRun = false;
    }

    public void Pause() {//
        uIManager.MostrarPausaPanel();
        Time.timeScale = 0;
        pause = true;
    }
    public void Resume() {//
        uIManager.MostrarPausaPanel();
        Time.timeScale = 1;
        pause = false;
    }
    public void GameOver() {//
        if (!lvEnd) { 
            uIManager.MostrarGameOver();
            lvRun = false;
            lvEnd = true;
            Time.timeScale = 0;
            pause = true;
        }
    }
    public void RestarEscene() {//
        SceneManager.LoadScene("main");
    }

    public void VolverAlMenu()//
    {
        SceneManager.LoadScene("MenuInicio");
    }

    private void IniciarPlayer() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        //Timing.RunCoroutine(_IntroducirPlayer(player));
        StartCoroutine(_IntroducirPlayer(player));
    }
    public void StartLevel() {
        Time.timeScale = 1;
        faseActual = 0;
        lvEnd = false;
        pause = false;
        lvRun = false;
        IniciarPlayer();
    }
    public void InstanciarEnemy(Enemy enemy, int nDeInstancia, float xInver = 1, float yInver = 1) {//
        
        GameObject Enemy = Instantiate(enemy).gameObject;
        Enemy.GetComponent<Enemy>().inversion.x = xInver;
        Enemy.GetComponent<Enemy>().inversion.y = yInver;
        Enemy.GetComponent<Enemy>().numeroDeInstanciaDelNivel = nDeInstancia;
    }
    public void CotroladorDeFase() {//
        for (int i = 0; i < nivel.fasesDelNivel.Count; i++) {
            controlDeInstanciaDeNivel.Add(0);
        }
    }
    public void InformarDefuncion(int FaseaDeLaDefuncion) {//

        if (faseActual < nivel.fasesDelNivel.Count) {
            controlDeInstanciaDeNivel[FaseaDeLaDefuncion]++;

            if (controlDeInstanciaDeNivel[FaseaDeLaDefuncion] == nivel.fasesDelNivel[FaseaDeLaDefuncion].instanciaDeFase.Count)
            {
                faseActual++;
                if (faseActual <= nivel.fasesDelNivel.Count-1) {
                    Timing.RunCoroutine(_StarFase(faseActual));
                }
            }
        }
        else {
            Debug.Log("You win");
            EndGame();
        }
    }

    
   
    IEnumerator<float> _StarFase(int fase) {
        float segundo = 0;
        
        while (segundo < 3) {
            float segundoDeInstancia = 1;
            if (!pause)
            {
                for (int instancia = 0; instancia < nivel.fasesDelNivel[fase].instanciaDeFase.Count; instancia++)
                {
                    segundoDeInstancia += nivel.fasesDelNivel[fase].instanciaDeFase[instancia].waitForSecons;
                
                    if (Mathf.Round(segundo * 100) / 100 == Mathf.Round(segundoDeInstancia * 100) / 100) {
/*
                        float inverX = nivel.fasesDelNivel[fase].instanciaDeFase[instancia].Xinvert;
                        float inverY = nivel.fasesDelNivel[fase].instanciaDeFase[instancia].Yinvert;
                        Enemy bullet = nivel.fasesDelNivel[fase].instanciaDeFase[instancia].enemy;

                        InstanciarEnemy(bullet, fase, inverX, inverY);*/
                    }
                }
            
                segundo += Time.fixedDeltaTime;
            }
            yield return Timing.WaitForOneFrame;
           
        }
    }
    IEnumerator _IntroducirPlayer(GameObject player)
    //IEnumerator<float> _IntroducirPlayer(GameObject player)
    {
        Vector3 centerPos = new Vector3(0, 0, 0);
        while (player.transform.position != centerPos)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, centerPos, 3 * Time.deltaTime);
            //yield return Timing.WaitForOneFrame;
            yield return null;
        }
        uIManager.MostrarCartelLv();
        //yield return Timing.WaitForSeconds(1f);
        yield return new WaitForSeconds(1f);
        StartGame();
        
    }

    IEnumerator<float> _DespedirPlayer(GameObject player)
    {
        yield return Timing.WaitForSeconds(1f);
        Vector3 centerPos = new Vector3(10, 0, 0);
        for (float i = 0; i < 15; i += Time.fixedDeltaTime)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, centerPos, 5 * Time.deltaTime);
            yield return Timing.WaitForOneFrame;
        }
        VolverAlMenu();
    }
}
