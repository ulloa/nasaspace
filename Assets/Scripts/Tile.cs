using UnityEngine;
using System.Linq;
using UnityEditor;

public class Tile : MonoBehaviour
{
    Vector3 Dimensions { get; set; }
    Vector2 TileLocation { get; set; }

    public void SetHeightMap(Texture2D heightMap, Vector3 dimensions, Vector2 tileLocation)
    {
        Dimensions = dimensions;
        TileLocation = new Vector2(Mathf.Min(Mathf.Max(0, tileLocation.x), 63), Mathf.Min(Mathf.Max(0, tileLocation.y), 31));
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
    public void OnBecameVisible()
    {
        if (!EditorApplication.isPaused)
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
                    var tempDimension = new Vector3();
                    PlusX.GetComponent<Tile>().SetHeightMap(GrabTile.MarsGetTile((short)TileLocation.x, (short)TileLocation.y, Direction.Up, out tempDimension), tempDimension, new Vector2(TileLocation.x, TileLocation.y - 1));
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
                    var tempDimension = new Vector3();
                    PlusZ.GetComponent<Tile>().SetHeightMap(GrabTile.MarsGetTile((short)TileLocation.x, (short)TileLocation.y, Direction.Right, out tempDimension), tempDimension, new Vector2(TileLocation.x + 1, TileLocation.y));
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
                    var tempDimension = new Vector3();
                    NegativeX.GetComponent<Tile>().SetHeightMap(GrabTile.MarsGetTile((short)TileLocation.x, (short)TileLocation.y, Direction.Right, out tempDimension), tempDimension, new Vector2(TileLocation.x, TileLocation.y + 1));
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
                    var tempDimension = new Vector3();
                    NegativeZ.GetComponent<Tile>().SetHeightMap(GrabTile.MarsGetTile((short)TileLocation.x, (short)TileLocation.y, Direction.Right, out tempDimension), tempDimension, new Vector2(TileLocation.x - 1, TileLocation.y));
                }
            }
        }
    }
}
