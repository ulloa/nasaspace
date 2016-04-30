using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
            MiniMapCam.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 150, Player.transform.position.z);
            MiniMapCam.transform.eulerAngles = new Vector3(MiniMapCam.transform.eulerAngles.x, Player.GetComponentInChildren<Camera>().transform.eulerAngles.y, MiniMapCam.transform.eulerAngles.z);
        }
    }

    public void CreateMap(int tileCount)
    {
        var dimension = new Vector3();

        if (PublicVariables.scenetoload == SceneToLoad.Mars)
        {
            BaseTile.GetComponent<Tile>().SetHeightMap(GrabTile.GetMarsSquare(new Vector2(34, 16), 7, out dimension), dimension);
            PausePanel.GetComponent<RawImage>().texture = MarsMap;
        }
        else
        {
            BaseTile.GetComponent<Tile>().SetHeightMap(GrabTile.GetVestaSquare(new Vector2(83, 20), out dimension), dimension);
            PausePanel.GetComponent<RawImage>().texture = VestaMap;
        }

        var width = BaseTile.GetComponent<Terrain>().terrainData.heightmapWidth / 2;
        var height = BaseTile.GetComponent<Terrain>().terrainData.heightmapHeight / 2;

        Player = (GameObject)Instantiate(Player, new Vector3(width, BaseTile.GetComponent<Terrain>().SampleHeight(new Vector3(width, 0, height)), height), Quaternion.Euler(0, 0, 0));
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("Menu");
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

    Vector3[] MarsPositions = new Vector3[] { new Vector3(0, 0, 10), new Vector3(10, 0, 0) };
    Vector3[] VestaPositions = new Vector3[] { new Vector3(0, 0, 10), new Vector3(10, 0, 0) };

    public void TeleportPlayerToLocation(int positionNumber)
    {
        if (PublicVariables.scenetoload == SceneToLoad.Mars)
        {
            Player.transform.position = new Vector3(MarsPositions[positionNumber].x,
                BaseTile.GetComponent<Terrain>().SampleHeight(MarsPositions[positionNumber]), MarsPositions[positionNumber].z);
        }
        else
        {
            Player.transform.position = new Vector3(VestaPositions[positionNumber].x,
                BaseTile.GetComponent<Terrain>().SampleHeight(VestaPositions[positionNumber]), VestaPositions[positionNumber].z);
        }
        UnPause();
    }
}