using UnityEngine;
using System.Linq;
using UnityEditor;
using System.Collections;

public class Tile : MonoBehaviour
{
    Vector3 Dimensions { get; set; }
    Vector2 TileLocation { get; set; }

    bool Generate { get; set; }

    public void SetHeightMap(Texture2D heightMap, Vector3 dimensions)
    {
        Dimensions = dimensions;
        var terrain = GetComponent<Terrain>();
        terrain.terrainData.size = Dimensions;
        TextureConverterV2.ApplyHeightmap(terrain, heightMap);
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
    public void deadfunction()
    {
        if (!EditorApplication.isPaused && (PlusX == null || PlusZ == null || NegativeX == null || NegativeZ == null) && Generate)
        {
            StartCoroutine(TileGeneration());
        }
    }

    private IEnumerator TileGeneration()
    {
        yield return null;
        Collider collider = null;
        if (PlusX == null)
        {
            collider = Physics.OverlapBox(Center + new Vector3(Dimensions.x, 0, 0), new Vector3(0.1f, 0.1f, 0.1f)).FirstOrDefault();
            if (collider != null)
                PlusX = collider.gameObject;
            else
            {
                PlusX = (GameObject)Instantiate(gameObject, new Vector3(transform.position.x + Dimensions.x, 0, transform.position.z), Quaternion.Euler(0, 0, 0));
            }
        }

        if (PlusZ == null)
        {
            collider = Physics.OverlapBox(Center + new Vector3(0, 0, +Dimensions.z), new Vector3(0.1f, 0.1f, 0.1f)).FirstOrDefault();
            if (collider != null)
                PlusZ = collider.gameObject;
            else
            {
                PlusZ = (GameObject)Instantiate(gameObject, new Vector3(transform.position.x, 0, transform.position.z + Dimensions.z), Quaternion.Euler(0, 0, 0));
            }
        }

        if (NegativeX == null)
        {
            collider = Physics.OverlapBox(Center + new Vector3(-Dimensions.x, 0, 0), new Vector3(0.1f, 0.1f, 0.1f)).FirstOrDefault();
            if (collider != null)
                NegativeX = collider.gameObject;
            else
            {
                NegativeX = (GameObject)Instantiate(gameObject, new Vector3(transform.position.x - Dimensions.x, 0, transform.position.z), Quaternion.Euler(0, 0, 0));
            }
        }

        if (NegativeZ == null)
        {
            collider = Physics.OverlapBox(Center + new Vector3(0, 0, -Dimensions.z), new Vector3(0.1f, 0.1f, 0.1f)).FirstOrDefault();
            if (collider != null)
                NegativeZ = collider.gameObject;
            else
            {
                NegativeZ = (GameObject)Instantiate(gameObject, new Vector3(transform.position.x, 0, transform.position.z - Dimensions.z), Quaternion.Euler(0, 0, 0));
            }
        }

        yield return null;
    }
}
