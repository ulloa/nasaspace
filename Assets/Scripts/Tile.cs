using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour
{
    public Color CurColor;
    void Start()
    {
        GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(Random.value, Random.value, Random.value));
        CurColor = GetComponent<MeshRenderer>().material.GetColor("_Color");
    }

    void Update()
    {
        if (GetComponent<MeshRenderer>().material.GetColor("_Color") != CurColor)
            GetComponent<MeshRenderer>().material.SetColor("_Color", CurColor);
    }
}
