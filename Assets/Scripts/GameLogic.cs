using UnityEngine;
using System.Linq;

public class GameLogic : MonoBehaviour
{
    public GameObject BaseTile;
    public GameObject Player;
    public GameObject MiniMapCam;

    void Awake()
    {
        CreateMap(9, new Vector3(0, 10, 0));
    }

    void Update()
    {

    }

    void LateUpdate()
    {
        MiniMapCam.transform.position = new Vector3(Player.transform.position.x, MiniMapCam.transform.position.y, Player.transform.position.z);
        MiniMapCam.transform.eulerAngles = new Vector3(MiniMapCam.transform.eulerAngles.x, Player.GetComponentInChildren<Camera>().transform.eulerAngles.y, MiniMapCam.transform.eulerAngles.z);
    }

    public void CreateMap(int tileCount, Vector3 startPosition)
    {
       // BaseTile = (GameObject)Instantiate(BaseTile, startPosition, Quaternion.Euler(0, 0, 0));
        Player = (GameObject)Instantiate(Player, startPosition, Quaternion.Euler(0, 0, 0));
    }
}