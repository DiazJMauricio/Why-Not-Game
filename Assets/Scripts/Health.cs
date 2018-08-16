using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public bool invulnerable;
    public int MaxHealth;
    private int actualHealth;
    
    public delegate void OnHealthChange();
    public event OnHealthChange EventOnHit;
    public event OnHealthChange EventOnDead;
    public event OnHealthChange EventOnRecovery;
    
	void Awake () {
        actualHealth = MaxHealth;
	}

    public void ModHealth(int cant) {
        if (!invulnerable) {

            actualHealth += cant;
            if (cant < 0 && EventOnHit != null) {
                // Resibir Hit.
                EventOnHit();
            }
            if (cant > 0 && EventOnRecovery != null)
            {
                // Resibir Vida.
                EventOnRecovery();
            }
            if (actualHealth > MaxHealth) actualHealth = MaxHealth;
            if (IsDead() && EventOnDead != null) {
                EventOnDead();
            }
        }
    }

    public bool IsDead() {
        return actualHealth <= 0;
    }
    public int GetActualHealth() {
        return actualHealth;
    }
}
