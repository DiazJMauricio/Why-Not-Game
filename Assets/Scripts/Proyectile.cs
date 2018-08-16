using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  (!)
public class Proyectile : MonoBehaviour {

    public int damage = 1;
    public List<string> targets;              //  <-  Lista de Tags de los Objetivos.
    public List<string> crashs;

    //  onCollision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Health>() != null) {
            CheckForDamage(collision.gameObject);
            CheckForCrash(collision.gameObject);
        }
    }

    //  Busca si el objeto en la collision esta en la lista de objetivo e si es asi impone el daño.
    void CheckForDamage(GameObject impact)
    {
        if (targets.Contains(impact.tag))
        {
            //  inserta el daño
            impact.GetComponent<Health>().ModHealth(damage);
        }
    }
    void CheckForCrash(GameObject impact) {
        if (crashs.Contains(impact.tag))
        {
            GetComponent<Health>().ModHealth(-1);
        }
    }
}
