using UnityEngine;
using System.Collections;
using System.Linq;

public class Tile : MonoBehaviour
{
    public Color CurColor;
    void Awake()
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
            if (realSize == Vector3.zero)
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

    public GameObject PlusX;
    public GameObject NegativeX;
    public GameObject PlusZ;
    public GameObject NegativeZ;
    public void OnWillRenderObject()
    {
        Collider collider = null;
        if (PlusX == null)
        {
            collider = Physics.OverlapBox(transform.GetComponent<Renderer>().bounds.center + new Vector3(RealSize.x, 0, 0), new Vector3(0.1f, 0.1f, 0.1f)).FirstOrDefault();
            if (collider != null)
                PlusX = collider.gameObject;
            else
                PlusX = (GameObject)Instantiate(gameObject, new Vector3(transform.position.x + RealSize.x, transform.position.y, transform.position.z), Quaternion.Euler(0, 0, 0));
        }

        if (PlusZ == null)
        {
            collider = Physics.OverlapBox(transform.GetComponent<Renderer>().bounds.center + new Vector3(0, 0, +RealSize.z), new Vector3(0.1f, 0.1f, 0.1f)).FirstOrDefault();
            if (collider != null)
                PlusZ = collider.gameObject;
            else
                PlusZ = (GameObject)Instantiate(gameObject, new Vector3(transform.position.x, transform.position.y, transform.position.z + RealSize.z), Quaternion.Euler(0, 0, 0));
        }

        if (NegativeX == null)
        {
            collider = Physics.OverlapBox(transform.GetComponent<Renderer>().bounds.center + new Vector3(-RealSize.x, 0, 0), new Vector3(0.1f, 0.1f, 0.1f)).FirstOrDefault();
            if (collider != null)
                NegativeX = collider.gameObject;
            else
                NegativeX = (GameObject)Instantiate(gameObject, new Vector3(transform.position.x - RealSize.x, transform.position.y, transform.position.z), Quaternion.Euler(0, 0, 0));
        }

        if (NegativeZ == null)
        {
            collider = Physics.OverlapBox(transform.GetComponent<Renderer>().bounds.center + new Vector3(0, 0, -RealSize.z), new Vector3(0.1f, 0.1f, 0.1f)).FirstOrDefault();
            if (collider != null)
                NegativeZ = collider.gameObject;
            else
                NegativeZ = (GameObject)Instantiate(gameObject, new Vector3(transform.position.x, transform.position.y, transform.position.z - RealSize.z), Quaternion.Euler(0, 0, 0));
        }
    }
}
