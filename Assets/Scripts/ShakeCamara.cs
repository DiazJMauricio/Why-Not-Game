using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamara : MonoBehaviour {

    Vector3 starPosition;

    private void Start() {
        starPosition = transform.position;
    }

    public void _ShakeCamara(float duracion) {
        StartCoroutine(_Shake(duracion));
    }

    IEnumerator _Shake(float time) {
        for (float i = 0; i <= time; i += Time.fixedDeltaTime) {
            Vector3 randomPosition = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), -10);
            transform.position = randomPosition;
            
            if (i == time) {
                transform.position = starPosition;
            }
            yield return null;
        }        
        yield return null;
    }
}
