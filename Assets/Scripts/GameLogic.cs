using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    public GameObject BaseTile;
    public GameObject Player;
    public GameObject MiniMapCam;
    public GameObject PausePanel;
    public bool IsPause;
    public Texture2D MarsMap;
    public Texture2D VestaMap;

    void Awake()
    {
        CreateMap(9);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            UnPause();
        }
    }

    void LateUpdate()
    {
        if (!IsPause)
        {
            MiniMapCam.transform.position = new Vector3(Player.transform.position.x, MiniMapCam.transform.position.y, Player.transform.position.z);
            MiniMapCam.transform.eulerAngles = new Vector3(MiniMapCam.transform.eulerAngles.x, Player.GetComponentInChildren<Camera>().transform.eulerAngles.y, MiniMapCam.transform.eulerAngles.z);
        }
    }

    public void CreateMap(int tileCount)
    {
        //var dimension = new Vector3();
        //BaseTile.GetComponent<Tile>().SetHeightMap(GrabTile.GetMarsHeightMap(out dimension), dimension);
        PausePanel.GetComponent<RawImage>().texture = MarsMap;
        Player = (GameObject)Instantiate(Player, new Vector3(0, BaseTile.GetComponent<Terrain>().SampleHeight(new Vector3(0, 0, 0)), 0), Quaternion.Euler(0, 0, 0));
    }

    public void ExitGame()
    {

    }

    public void UnPause()
    {
        IsPause = !IsPause;
        MiniMapCam.GetComponent<MiniMapUI>().IsPause = IsPause;

        if (IsPause)
        {
            Time.timeScale = 0;
            PausePanel.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1;
            PausePanel.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    Vector3[] MarsPositions = new Vector3[] { new Vector3(0,0,10), new Vector3(10,0,0) };    

    public void TeleportPlayerToLocation(int positionNumber)
    {
        Player.transform.position = new Vector3(MarsPositions[positionNumber].x,
            BaseTile.GetComponent<Terrain>().SampleHeight(MarsPositions[positionNumber]), MarsPositions[positionNumber].z);
        UnPause();
    }
}