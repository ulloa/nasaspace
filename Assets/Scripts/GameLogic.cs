using UnityEngine;
using System.Linq;

public class GameLogic : MonoBehaviour
{
    public GameObject BaseTile;
    public GameObject Player;
    private float PlayerRadius = 10f;
    public GameObject CurrentTile;
    public GameObject NextTile;
    public GameObject OldTile;

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
        CheckBoundary();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
        Gizmos.DrawWireSphere(Player.transform.position, PlayerRadius);
    }

    public void CheckBoundary()
    {
        Collider[] hitColliders = Physics.OverlapSphere(Player.transform.position, PlayerRadius);

        var collider = hitColliders.FirstOrDefault(x => x.GetComponent<Tile>() != null && x.gameObject != CurrentTile);
        if (collider != null)
        {
            NextTile = collider.gameObject;
        }

        hitColliders = Physics.OverlapSphere(Player.transform.position, PlayerRadius);
        collider = hitColliders.FirstOrDefault(x => x.gameObject == CurrentTile);
        if (collider == null)
        {
            OldTile = CurrentTile;
            CurrentTile = NextTile;
            NextTile = null;
        }
    }

    public void CreateMap(int tileCount, Vector3 startPosition)
    {
        var clone = CurrentTile = (GameObject)Instantiate(BaseTile, startPosition, Quaternion.Euler(0, 0, 0));
        clone.name = "tile" + 1;
        Player = (GameObject)Instantiate(Player, startPosition, Quaternion.Euler(0, 0, 0));
        clone = (GameObject)Instantiate(BaseTile, new Vector3(CurrentTile.transform.localScale.x, 0, CurrentTile.transform.localScale.z), Quaternion.Euler(0, 0, 0));
        clone.name = "tile" + 2;
        clone = (GameObject)Instantiate(BaseTile, new Vector3(CurrentTile.transform.localScale.x, 0, 0), Quaternion.Euler(0, 0, 0));
        clone.name = "tile" + 3;
        clone = (GameObject)Instantiate(BaseTile, new Vector3(-CurrentTile.transform.localScale.x, 0, 0), Quaternion.Euler(0, 0, 0));
        clone.name = "tile" + 4;
        clone = (GameObject)Instantiate(BaseTile, new Vector3(0, 0, CurrentTile.transform.localScale.z), Quaternion.Euler(0, 0, 0));
        clone.name = "tile" + 5;
        clone = (GameObject)Instantiate(BaseTile, new Vector3(0, 0, -CurrentTile.transform.localScale.z), Quaternion.Euler(0, 0, 0));
        clone.name = "tile" + 6;
        clone = (GameObject)Instantiate(BaseTile, new Vector3(-CurrentTile.transform.localScale.x, 0, CurrentTile.transform.localScale.z), Quaternion.Euler(0, 0, 0));
        clone.name = "tile" + 7;
        clone = (GameObject)Instantiate(BaseTile, new Vector3(CurrentTile.transform.localScale.x, 0, -CurrentTile.transform.localScale.z), Quaternion.Euler(0, 0, 0));
        clone.name = "tile" + 8;
        clone = (GameObject)Instantiate(BaseTile, new Vector3(-CurrentTile.transform.localScale.x, 0, -CurrentTile.transform.localScale.z), Quaternion.Euler(0, 0, 0));
        clone.name = "tile" + 9;
    }
}