using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] float scrollSpeed = 0.5f;

    MeshRenderer mr;
    Material mat;
    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        mat = mr.material;
    }

    // Update is called once per frame
    void Update()
    {
        mat.mainTextureOffset = new Vector2(0, mat.mainTextureOffset.y + scrollSpeed * Time.deltaTime);
    }
}
