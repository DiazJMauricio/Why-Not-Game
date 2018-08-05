using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjColor : MonoBehaviour {

    public Style.StyleColor color;
    public List<SpriteRenderer> spritesRender = new List<SpriteRenderer>();
    public List<ParticleSystem> particulas = new List<ParticleSystem>();

    void Awake() {
        SetColor();
    }

    public void SetColor() {
        foreach (ParticleSystem p in particulas) {
            ParticleSystem.MainModule mainModule = p.main;
            mainModule.startColor = Style.GetColor(color);
        }
        foreach (SpriteRenderer sr in spritesRender) {
            sr.color = Style.GetColor(color);
        }
    }
}
