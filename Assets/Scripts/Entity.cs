using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Todas las IEntity son instancias de la Fase de un nivel.
public class Entity : MonoBehaviour {
    public Health miHealth                      { get; set; }
    public MoveWithPattern MoveWithP            { get; set; }
    public int NumeroDeLaFasePerteneciente      { get; set; }
    private ManagerLevel LevelManager           { get; set; }


    protected virtual void Awake ()
    {
        MoveWithP = GetComponent<MoveWithPattern>();
        miHealth = GetComponent<Health>();
        LevelManager = FindObjectOfType<ManagerLevel>();
        NumeroDeLaFasePerteneciente = 0;
	}

    protected virtual void Update ()
    {
        //  El objeto sale de la escena.
        if (MoveWithP.FueraDeCuadro())  KillThisObject();

        //  El objeto fue Eliminado por el player.
        else if (miHealth.IsDead())    DeadByHealth();
    }


    protected virtual void DeadByHealth()
    {
        // Animacion de muerte.
        //[TEMPORAL]
        KillThisObject();
    }

    protected virtual void KillThisObject() {
        LevelManager.InformarDefuncion(NumeroDeLaFasePerteneciente);
        Destroy(gameObject);
    }
}
