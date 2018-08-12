using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public bool invulnerable;
    public int MaxHealth;

    private int actualHealth;
    
	void Awake () {
        actualHealth = MaxHealth;
	}

    public void ModHealth(int cant) {
        if (!invulnerable) {

            actualHealth += cant;
            if (cant < 0) {
                // Resibir Hit.
            }
            if (cant > 0)
            {
                // Resibir Vida.
            }
            if (actualHealth > MaxHealth) actualHealth = MaxHealth;

        }
    }

    public bool IsDead() {
        return actualHealth <= 0;
    }

}
