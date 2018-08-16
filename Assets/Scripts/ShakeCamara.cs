using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamara : MonoBehaviour {

    Vector3 starPosition;

    private void Start() {
        starPosition = transform.position;
    }

    public void _ShakeCamara(float duracion, float radio) {
        StartCoroutine(_Shake(duracion,radio));
    }

    IEnumerator _Shake(float time,float radio) {
        for (float i = 0; i <= time; i += Time.fixedDeltaTime) {
            Vector3 randomPosition = new Vector3(Random.Range(-radio, radio), Random.Range(-radio, radio), -10);
            transform.position = randomPosition;
            
            if (i == time) {
                transform.position = starPosition;
            }
            yield return null;
        }        
        yield return null;
    }
}
