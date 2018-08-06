using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class GameManager : MonoBehaviour {

    public Nivel nivel;
    int faseActual = 0;
    public List<int> controlDeInstanciaDeNivel = new List<int>();

	// Use this for initialization
	void Start () {
        Timing.RunCoroutine(_StarFase(faseActual));
        faseActual++;
        Timing.RunCoroutine(_StarFase(faseActual));
        CotroladorDeFase();

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.F)) {
            Timing.RunCoroutine(_StarFase(1));
        }
    }
    IEnumerator<float> _StarFase(int fase) {
        float segundo = 0;
        
        while (segundo < 3) {
            float segundoDeInstancia = 1;
            for (int instancia = 0; instancia < nivel.fasesDelNivel[fase].instanciaDeFase.Count; instancia++)
            {
                segundoDeInstancia += nivel.fasesDelNivel[fase].instanciaDeFase[instancia].waitForSecons;
                
                if (Mathf.Round(segundo * 100) / 100 == Mathf.Round(segundoDeInstancia * 100) / 100) {

                    float inverX = nivel.fasesDelNivel[fase].instanciaDeFase[instancia].Xinvert;
                    float inverY = nivel.fasesDelNivel[fase].instanciaDeFase[instancia].Yinvert;
                    Enemy bullet = nivel.fasesDelNivel[fase].instanciaDeFase[instancia].enemy;

                    InstanciarEnemy(bullet, fase, inverX, inverY);
                }
            }
            segundo += Time.fixedDeltaTime;
            yield return Timing.WaitForOneFrame;
        }
    }

    public void InstanciarEnemy(Enemy enemy, int nDeInstancia, float xInver = 1, float yInver = 1) {
        
        GameObject Enemy = Instantiate(enemy).gameObject;
        Enemy.GetComponent<Enemy>().inversion.x = xInver;
        Enemy.GetComponent<Enemy>().inversion.y = yInver;
        Enemy.GetComponent<Enemy>().numeroDeInstanciaDelNivel = nDeInstancia;
    }
    public void CotroladorDeFase() {
        for (int i = 0; i < nivel.fasesDelNivel.Count; i++) {
            controlDeInstanciaDeNivel.Add(0);
        }
    }
    public void InformarDefuncion(int numeroDeInstaciaDeLaDefuncion) {

        if (faseActual < nivel.fasesDelNivel.Count) {
            controlDeInstanciaDeNivel[numeroDeInstaciaDeLaDefuncion]++;

            if (controlDeInstanciaDeNivel[numeroDeInstaciaDeLaDefuncion] == nivel.fasesDelNivel[numeroDeInstaciaDeLaDefuncion].instanciaDeFase.Count)
            {
                faseActual++;
                if (faseActual <= nivel.fasesDelNivel.Count-1)
                {
                    Timing.RunCoroutine(_StarFase(faseActual));
                }
            }
        }
    }
}
