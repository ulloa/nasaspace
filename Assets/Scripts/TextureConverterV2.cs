using UnityEngine;

public class TextureConverterV2
{
    public static void ApplyHeightmap(Terrain terrainParent, Texture2D heightmap)
    {
        var terrain = terrainParent.terrainData;
        var w = heightmap.width;
        var h = heightmap.height;
        var w2 = terrain.heightmapWidth;
        var heightmapData = terrain.GetHeights(0, 0, w2, w2);
        var mapColors = heightmap.GetPixels();
        var map = new Color[w2 * w2];

        if (w2 != w || h != w)
        {
            var ratioX = 1.0 / (((float)w2) / (w - 1));
            var ratioY = 1.0 / (((float)w2) / (h - 1));
            for (int y = 0; y < w2; y++)
            {
                var yy = Mathf.Floor(y * ((float)ratioY));
                var y1 = yy * w;
                var y2 = (yy + 1) * w;
                var yw = y * w2;
                for (int x = 0; x < w2; x++)
                {
                    var xx = Mathf.Floor(x * ((float)ratioX));

                    var bl = mapColors[(int)(y1 + xx)];
                    var br = mapColors[(int)(y1 + xx + 1)];
                    var tl = mapColors[(int)(y2 + xx)];
                    var tr = mapColors[(int)(y2 + xx + 1)];

                    float xLerp = x * ((float)ratioX) - xx;
                    map[yw + x] = Color.Lerp(Color.Lerp(bl, br, xLerp), Color.Lerp(tl, tr, xLerp), y * ((float)ratioY) - yy);
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
                heightmapData[y, x] = map[y * w2 + x].grayscale;
            }
        }

        terrain.SetHeights(0, 0, heightmapData);
    }
}
