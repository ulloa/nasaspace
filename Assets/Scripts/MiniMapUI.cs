using UnityEngine;
using System.Collections;

public class MiniMapUI : MonoBehaviour
{

    public RenderTexture miniMapTexture;
    public Material miniMapMaterial;

    private float offset;

    void Start()
    {
        offset = 10;
    }

    void OnGUI()
    {
        if (Event.current.type == EventType.Repaint)
        {
            Graphics.DrawTexture(new Rect(0, 0, 256, 256), miniMapTexture, miniMapMaterial);
        }
    }
}
