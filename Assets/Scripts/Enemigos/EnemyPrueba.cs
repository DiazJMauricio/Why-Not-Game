using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPrueba : Attack
{
    private void Awake()
    {
        StartCoroutine(_attack());
    }

    

    IEnumerator _attack() {
        yield return new WaitForSeconds(4.3f);


        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.2f);
            //Debug.Log(Time.time + " => "+ i +"seg");
            Disparar(transform, 0);
        }
    }
}
