using UnityEngine;
using System.Linq;

public class GameLogic : MonoBehaviour
{
    public Terrain BaseTile;
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
        Player = (GameObject)Instantiate(Player, new Vector3(0, BaseTile.SampleHeight(new Vector3(0, 0, 0)), 0), Quaternion.Euler(0, 0, 0));
    }
}