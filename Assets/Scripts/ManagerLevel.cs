using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerLevel : MonoBehaviour {
    
    public Nivel LevelSettins;

    static int faseActual;
    static List<int> controlDeInstanciaDeNivel;
    ManagerGame gameManager;
    bool lvWin;
    

    void Awake () {
        
        if (LevelSettins == null) Debug.LogError("Nivel no ingresado");
        controlDeInstanciaDeNivel = new List<int>();
        CotroladorDeFase();
        gameManager = GetComponent<ManagerGame>();
    }

    private void Start()
    {
        lvWin = false;
        faseActual = 0;
        gameManager.LevelStart += StartLevel;
    }

    public void StartLevel()
    {
        StartCoroutine(_StarFase(faseActual));
        faseActual++;
        StartCoroutine(_StarFase(faseActual));
    }

    public void CotroladorDeFase()
    {
        for (int i = 0; i < LevelSettins.fasesDelNivel.Count; i++)
        {
            controlDeInstanciaDeNivel.Add(0);
        }
    }

    //  Inicializa la fase siguiente si todas las entidades de una face fueron eliminadas.
    public void InformarDefuncion(int FaseaDeLaDefuncion)
    {
        if (faseActual < LevelSettins.fasesDelNivel.Count)
        {
            controlDeInstanciaDeNivel[FaseaDeLaDefuncion]++;

            if (controlDeInstanciaDeNivel[FaseaDeLaDefuncion] == LevelSettins.fasesDelNivel[FaseaDeLaDefuncion].instanciaDeFase.Count)
            {
                faseActual++;
                if (faseActual <= LevelSettins.fasesDelNivel.Count - 1)
                {
                    StartCoroutine(_StarFase(faseActual));
                }
            }
        }
        else
        {
            if (!lvWin)
            {
                Debug.Log("You win");
                lvWin = true;
                gameManager.WinGame();
            }
        }
    }

    public void InstanciarEntity(Entity entity, int nDeInstancia, Vector2 invert)
    {
        GameObject Entity = Instantiate(entity).gameObject;
        Entity.GetComponent<Entity>().entityMove.inversion = invert;
        Entity.GetComponent<Entity>().NumeroDeLaFasePerteneciente = nDeInstancia;
    }
    

    //  Inicializa una Fase
    IEnumerator _StarFase(int fase)
    {
        for (int instancia = 0; instancia < LevelSettins.fasesDelNivel[fase].instanciaDeFase.Count; instancia++)
        {

            yield return new WaitForSeconds(LevelSettins.fasesDelNivel[fase].instanciaDeFase[instancia].waitForSecons);


            float inverX = LevelSettins.fasesDelNivel[fase].instanciaDeFase[instancia].Xinvert;
            float inverY = LevelSettins.fasesDelNivel[fase].instanciaDeFase[instancia].Yinvert;

            Vector2 inversion = new Vector2(inverX, inverY);
            Entity entity = LevelSettins.fasesDelNivel[fase].instanciaDeFase[instancia].entity;

            InstanciarEntity(entity, fase, inversion);

        }
    }

        
    
}
