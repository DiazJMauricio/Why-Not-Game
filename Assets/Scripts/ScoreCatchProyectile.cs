using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCatchProyectile : MonoBehaviour {

    public int scoreByFrame;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyBullet") {
            ManagerScore.proyectileScore += scoreByFrame;
        }
    }
}
