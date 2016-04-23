using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public GameObject BaseTile;
    public GameObject Player;
    public GameObject MiniMapCam;

    void Awake()
    {
        CreateMap(9);
    }

    void Update()
    {

    }

    void LateUpdate()
    {
        MiniMapCam.transform.position = new Vector3(Player.transform.position.x, MiniMapCam.transform.position.y, Player.transform.position.z);
        MiniMapCam.transform.eulerAngles = new Vector3(MiniMapCam.transform.eulerAngles.x, Player.GetComponentInChildren<Camera>().transform.eulerAngles.y, MiniMapCam.transform.eulerAngles.z);
    }

    public void CreateMap(int tileCount)
    {
        BaseTile = (GameObject)Instantiate(BaseTile, new Vector3(-250, 0, -250), Quaternion.Euler(0, 0, 0));
        var dimension = new Vector3();
        BaseTile.GetComponent<Tile>().SetHeightMap(GrabTile.MarsGetTile(22,22,null, out dimension), dimension, new Vector2(0,0));
        Player = (GameObject)Instantiate(Player, new Vector3(0, BaseTile.GetComponent<Terrain>().SampleHeight(new Vector3(0, 0, 0)), 0), Quaternion.Euler(0, 0, 0));
    }
}