using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Todas las IEntity son instancias de la Fase de un nivel.
public class EntityDefault : MonoBehaviour, IEntity {

    public Health Health                        { get; set; }
    public MoveWithPattern MoveWithP            { get; set; }
    public int NumeroDeLaFasePerteneciente      { get; set; }
    public GameManager IGameManager             { get; set; }


    protected virtual void Awake () {
        IGameManager = FindObjectOfType<GameManager>();
        NumeroDeLaFasePerteneciente = 0;
	}

    protected virtual void Update () {

        //  El objeto sale de la escena.
        if (MoveWithP.FueraDeCuadro())  KillThisObject();

        //  El objeto fue Eliminado por el player.
        else if (Health.IsDead())    DeadByHealth();
    }


    protected virtual void DeadByHealth()
    {
        // Animacion de muerte.
        //[TEMPORAL]
        KillThisObject();
    }

    private void KillThisObject() {
        IGameManager.InformarDefuncion(NumeroDeLaFasePerteneciente);
        Destroy(gameObject);
    }
}
