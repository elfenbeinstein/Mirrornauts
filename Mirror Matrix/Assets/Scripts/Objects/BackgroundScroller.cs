using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// background scroller during game
/// </summary>

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] float scrollSpeed = 0.5f;

    MeshRenderer mr;
    Material mat;

    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        mat = mr.material;
    }

    void Update()
    {
        mat.mainTextureOffset = new Vector2(0, mat.mainTextureOffset.y + scrollSpeed * Time.deltaTime);
    }
}
