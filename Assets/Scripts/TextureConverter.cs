using UnityEngine;

public class TextureConverter
{
    public void ApplyHeightmap(Terrain terrain, Texture2D heightMap)
    {
        var terrainData = terrain.terrainData;
        var w = heightMap.width;
        var h = heightMap.height;
        var w2 = terrainData.heightmapWidth;
        var heightMapData = terrainData.GetHeights(0, 0, w2, w2);
        var mapColors = heightMap.GetPixels();
        var map = new Color[w2 * w2];

        if (w2 != w || h != w)
        {
            float ratioX = 1 / w2 / (w - 1);
            float ratioY = 1 / w2 / (h - 1);
            for (int y = 0; y < w2; y++)
            {
                var yy = Mathf.Floor((float)(y * ratioY));
                var y1 = yy * w;
                var y2 = (yy + 1) * w;
                var yw = y * w2;
                for (int x = 0; x < w2; x++)
                {
                    var xx = Mathf.Floor((float)(x * ratioX));

                    var bl = mapColors[(int)(y1 + xx)];
                    var br = mapColors[(int)(y1 + xx + 1)];
                    var tl = mapColors[(int)(y2 + xx)];
                    var tr = mapColors[(int)(y2 + xx + 1)];

                    var xLerp = (x * ratioX) - xx;
                    map[yw + x] = Color.Lerp(Color.Lerp(bl, br, (float)xLerp), Color.Lerp(tl, tr, (float)xLerp), (float)(y * ratioY - yy));
                }
            }
        }
        else
        {
            // Use original if no resize is needed
            map = mapColors;
        }

        // Assign texture data to heightmap
        for (int y = 0; y < w2; y++)
        {
            for (int x = 0; x < w2; x++)
            {
                heightMapData[y, x] = map[y * w2 + x].grayscale;
            }
        }

        terrainData.SetHeights(0, 0, heightMapData);
    }
}