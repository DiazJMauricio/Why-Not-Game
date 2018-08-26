using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    EntityMove move;
    public GameObject bulletShoot;

    protected void Disparar(Transform pos, float rotacion = 0)
    {
        move = GetComponent<EntityMove>();
        if (!ManagerGame.inPausa)
        {
            rotacion = _presisarRotacion(rotacion);

            GameObject objeto = Instantiate(bulletShoot).gameObject;
            objeto.transform.position = pos.position;
            objeto.transform.rotation = Quaternion.Euler(0, 0, rotacion);
        }
    }
    /// funciones para Disparar
    float _presisarRotacion(float r)
    {
        if (move.inversion.y < 0) r += (180 - r) * 2;
        if (move.inversion.x < 0) r += (180 - r) * 2 + 180;
        return r;
    }
}
