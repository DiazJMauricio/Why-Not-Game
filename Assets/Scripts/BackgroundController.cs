using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour {
    public float speed;
    MeshRenderer msh;
    Material mat;


    void Start () {
        msh = GetComponent<MeshRenderer>();
        mat = msh.material;
    }

	void Update () {
        Vector2 offset = mat.mainTextureOffset;
        offset.x += speed * -0.01f * Time.deltaTime;
        mat.mainTextureOffset = offset;
	}
}
