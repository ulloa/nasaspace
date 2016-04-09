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

    private Vector3 realSize;
    public Vector3 RealSize
    {
        get
        {
            if (realSize != null)
            {
                Mesh planeMesh = GetComponent<MeshFilter>().mesh;
                Bounds bounds = planeMesh.bounds;
                // size in pixels
                float boundsX = transform.localScale.x * bounds.size.x;
                float boundsY = transform.localScale.y * bounds.size.y;
                float boundsZ = transform.localScale.z * bounds.size.z;
                realSize = new Vector3(boundsX, boundsY, boundsZ);
            }

            return realSize;
        }
    }
}
