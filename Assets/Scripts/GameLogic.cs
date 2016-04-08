using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour
{
    public GameObject BaseTile;
    public GameObject Player;
    public float PlayerRadius { get; set; }
    private GameObject currentTile { get; set; }
    private GameObject nextTile { get; set; }

    void Start()
    {
        CreateMap(9, new Vector3(0, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        CheckBoundary(Player.transform.position);
    }

    public void CheckBoundary(Vector3 curPos)
    {
        Collider[] hitColliders = Physics.OverlapSphere(curPos, PlayerRadius, 8);

        foreach (var collider in hitColliders)
        {
            if (collider.GetComponent<Tile>() != null)
            {
                nextTile = collider.gameObject;
                break;
            }
        }
    }

    public void CreateMap(int tileCount, Vector3 startPosition)
    {
        currentTile = (GameObject)Instantiate(BaseTile, startPosition, Quaternion.Euler(0, 0, 0));
        Instantiate(Player, startPosition, Quaternion.Euler(0, 0, 0));
        Instantiate(BaseTile, new Vector3(currentTile.transform.localScale.x, 0, currentTile.transform.localScale.z), Quaternion.Euler(0, 0, 0));
        Instantiate(BaseTile, new Vector3(currentTile.transform.localScale.x, 0, 0), Quaternion.Euler(0, 0, 0));
        Instantiate(BaseTile, new Vector3(-currentTile.transform.localScale.x, 0, 0), Quaternion.Euler(0, 0, 0));
        Instantiate(BaseTile, new Vector3(0, 0, currentTile.transform.localScale.z), Quaternion.Euler(0, 0, 0));
        Instantiate(BaseTile, new Vector3(0, 0, -currentTile.transform.localScale.z), Quaternion.Euler(0, 0, 0));
        Instantiate(BaseTile, new Vector3(-currentTile.transform.localScale.x, 0, currentTile.transform.localScale.z), Quaternion.Euler(0, 0, 0));
        Instantiate(BaseTile, new Vector3(currentTile.transform.localScale.x, 0, -currentTile.transform.localScale.z), Quaternion.Euler(0, 0, 0));
        Instantiate(BaseTile, new Vector3(-currentTile.transform.localScale.x, 0, -currentTile.transform.localScale.z), Quaternion.Euler(0, 0, 0));
    }
}