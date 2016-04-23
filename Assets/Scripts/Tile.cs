using UnityEngine;
using System.Linq;
using UnityEditor;

public class Tile : MonoBehaviour
{
    Vector3 Dimensions { get; set; }

    public void SetHeightMap(Texture2D heightMap, Vector3 dimensions)
    {
        Dimensions = dimensions;
        var terrain = GetComponent<Terrain>();
        terrain.terrainData.size = Dimensions;
        new TextureConverter().ApplyHeightmap(GetComponent<Terrain>(), heightMap);
    }

    private Vector3 center;
    public Vector3 Center
    {
        get
        {
            if (center == Vector3.zero)
            {
                center = transform.position + new Vector3(Dimensions.x / 2, Dimensions.y, -Dimensions.z / 2);
            }

            return center;
        }
    }

    public GameObject PlusX;
    public GameObject NegativeX;
    public GameObject PlusZ;
    public GameObject NegativeZ;
    public void OnBecameVisible()
    {
        if (!EditorApplication.isPaused && false)
        {
            Collider collider = null;
            if (PlusX == null)
            {
                collider = Physics.OverlapBox(Center + new Vector3(Dimensions.x, 0, 0), new Vector3(0.1f, 0.1f, 0.1f)).FirstOrDefault();
                if (collider != null)
                    PlusX = collider.gameObject;
                else
                {
                    PlusX = (GameObject)Instantiate(gameObject, new Vector3(transform.position.x + Dimensions.x, transform.position.y, transform.position.z), Quaternion.Euler(0, 0, 0));
                    PlusX.GetComponent<Tile>().SetHeightMap(null, new Vector3(500, 100, 500));
                }
            }

            if (PlusZ == null)
            {
                collider = Physics.OverlapBox(Center + new Vector3(0, 0, +Dimensions.z), new Vector3(0.1f, 0.1f, 0.1f)).FirstOrDefault();
                if (collider != null)
                    PlusZ = collider.gameObject;
                else
                {
                    PlusZ = (GameObject)Instantiate(gameObject, new Vector3(transform.position.x, transform.position.y, transform.position.z + Dimensions.z), Quaternion.Euler(0, 0, 0));
                    PlusZ.GetComponent<Tile>().SetHeightMap(null, new Vector3(500, 100, 500));
                }
            }

            if (NegativeX == null)
            {
                collider = Physics.OverlapBox(Center + new Vector3(-Dimensions.x, 0, 0), new Vector3(0.1f, 0.1f, 0.1f)).FirstOrDefault();
                if (collider != null)
                    NegativeX = collider.gameObject;
                else
                {
                    NegativeX = (GameObject)Instantiate(gameObject, new Vector3(transform.position.x - Dimensions.x, transform.position.y, transform.position.z), Quaternion.Euler(0, 0, 0));
                    NegativeX.GetComponent<Tile>().SetHeightMap(null, new Vector3(500, 100, 500));
                }
            }

            if (NegativeZ == null)
            {
                collider = Physics.OverlapBox(Center + new Vector3(0, 0, -Dimensions.z), new Vector3(0.1f, 0.1f, 0.1f)).FirstOrDefault();
                if (collider != null)
                    NegativeZ = collider.gameObject;
                else
                {
                    NegativeZ = (GameObject)Instantiate(gameObject, new Vector3(transform.position.x, transform.position.y, transform.position.z - Dimensions.z), Quaternion.Euler(0, 0, 0));
                    NegativeZ.GetComponent<Tile>().SetHeightMap(null, new Vector3(500, 100, 500));
                }
            }
        }
    }
}
