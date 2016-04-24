using System;
using UnityEngine;

public static class GrabTile
{
    //MARS CONSTANTS
    private const short MARS_IMAGES_MAX_X = 63;
    private const short MARS_IMAGES_MAX_Y = 31;
    private const string MARS_IMAGES_URL = "https://api.nasa.gov/mars-wmts/catalog/Mars_MGS_MOLA_DEM_mosaic_global_463m_8/1.0.0//default/default028mm/5/0/0.png";
    //private const short MARS_EQUATOR_RADIUS = 3396;
    private const short MARS_PRIME_MERIDIAN_RADIUS = 3376;
    private const float MARS_EQUATOR_CIRCUMFERENCE = 21344000;
    private const short MARS_PEAK = 24000;
    private const int MARS_TILE_HEIGHT = 333000;
    private const int MARS_TILE_WIDTH = 331000;
    private const int MARS_NUM_PIXELS = 134217728;
    private const short MARS_ROW_NUM_PIXELS = 16384;
    private const short MARS_COLUMN_NUM_PIXELS = 8192;

    //VESTA CONSTANTS
    private const short VESTA_IMAGES_MAX_X = 31;
    private const short VESTA_IMAGES_MAX_Y = 15;
    private const string VESTA_IMAGES_URL = "https://api.nasa.gov/vesta-wmts/catalog/Vesta_Dawn_HAMO_DTM_DLR_Global_48ppd8/1.0.0//default/default028mm/4/0/0.png";
    private const short VESTA_PRIME_MERIDIAN_RADIUS = 223;
    private const short VESTA_PEAK = 22000;
    private const int VESTA_TILE_HEIGHT = 11000;
    private const int VESTA_TILE_WIDTH = 14000;

    /// <param name="currentX">Your current tile's x coordinate.</param>
    /// <param name="currentY">Your current tile's y coordinate.</param>
    /// <param name="D">The direction of the tile you want to load in.</param>
    /// <param name="dimensions">the x, y, and z coordinates the Texture2D in meters.</param>
    /// <returns>A Heightmap in the form of a Texture2D.</returns>
    public static Texture2D MarsGetTile(short currentX, short currentY, Direction? D, out Vector3 dimensions, float scale = 0.001f, float scaleHeight = 0.035f)
    {
        dimensions = new Vector3(MARS_TILE_HEIGHT * scale, MARS_PEAK * scaleHeight,
            findTileWidth(211 * ((currentY > 15) ? currentY - 16 : 15 - currentY), MARS_PRIME_MERIDIAN_RADIUS) * MARS_TILE_WIDTH * scale);
        string url;

        switch (D)
        {
            case Direction.Up:
                url = MARS_IMAGES_URL.Replace("/5/0/0.png",
                                     string.Format("/5/{0}/{1}.png", (currentY == 0) ? MARS_IMAGES_MAX_Y : currentY - 1, currentX));
                break;
            case Direction.Right:
                url = MARS_IMAGES_URL.Replace("/5/0/0.png",
                                     string.Format("/5/{0}/{1}.png", currentY, (currentX == MARS_IMAGES_MAX_X) ? 0 : currentX + 1));
                break;
            case Direction.Down:
                url = MARS_IMAGES_URL.Replace("/5/0/0.png",
                                     string.Format("/5/{0}/{1}.png", (currentY == MARS_IMAGES_MAX_Y) ? 0 : currentY + 1, currentX));
                break;
            case Direction.Left:
                url = MARS_IMAGES_URL.Replace("/5/0/0.png",
                                     string.Format("/5/{0}/{1}.png", currentY, (currentX == 0) ? MARS_IMAGES_MAX_X : currentX - 1));
                break;
            default:
                url = MARS_IMAGES_URL;
                break;
        }

        WWW image = new WWW(url);
        while (!image.isDone) ;

        return image.texture;
    }

    public static Texture2D VestaGetTile(short currentX, short currentY, Direction? D, out Vector3 dimensions, float scale = 0.01f)
    {
        dimensions = new Vector3(VESTA_TILE_HEIGHT * scale, VESTA_PEAK * scale,
            findTileWidth(211 * ((currentY > 15) ? currentY - 16 : 15 - currentY), VESTA_PRIME_MERIDIAN_RADIUS) * VESTA_TILE_WIDTH * scale);
        string url;

        switch (D)
        {
            case Direction.Up:
                url = VESTA_IMAGES_URL.Replace("/4/0/0.png",
                                     string.Format("/4/{0}/{1}.png", (currentY == 0) ? VESTA_IMAGES_MAX_Y : currentY - 1, currentX));
                break;
            case Direction.Right:
                url = VESTA_IMAGES_URL.Replace("/4/0/0.png",
                                     string.Format("/4/{0}/{1}.png", currentY, (currentX == VESTA_IMAGES_MAX_X) ? 0 : currentX + 1));
                break;
            case Direction.Down:
                url = VESTA_IMAGES_URL.Replace("/4/0/0.png",
                                     string.Format("/4/{0}/{1}.png", (currentY == VESTA_IMAGES_MAX_Y) ? 0 : currentY + 1, currentX));
                break;
            case Direction.Left:
                url = VESTA_IMAGES_URL.Replace("/4/0/0.png",
                                     string.Format("/4/{0}/{1}.png", currentY, (currentX == 0) ? VESTA_IMAGES_MAX_X : currentX - 1));
                break;
            default:
                url = VESTA_IMAGES_URL;
                break;
        }

        WWW image = new WWW(url);
        while (!image.isDone) ;

        return image.texture;
    }

    private static float findTileWidth(int z, short terresBodyPMRadius)
    {
        //return (2 * Mathf.PI * Mathf.Sqrt(Mathf.Pow(MARS_EQUATOR_RADIUS, 2) - Mathf.Pow(MARS_EQUATOR_RADIUS, 2) * Mathf.Pow(z, 2) 
        //                                                / Mathf.Pow(MARS_PRIME_MERIDIAN_RADIUS, 2)) / MARS_EQUATOR_CIRCUMFERENCE);
        return Mathf.Sqrt(1 - Mathf.Pow(z / terresBodyPMRadius, 2));
    }

    public static Texture2D GetMarsHeightMap(out Vector3 dimensions, float horizontalScale = 0.001f, float verticalScale = 0.035f)
    {
        dimensions = new Vector3(MARS_EQUATOR_CIRCUMFERENCE * horizontalScale, MARS_PEAK * verticalScale,
                                                            MARS_EQUATOR_CIRCUMFERENCE * horizontalScale);
        Color[] pixels = new Color[MARS_NUM_PIXELS];
        WWW image;
        Texture2D marsChunk;

        for (int y = 0; y <= MARS_IMAGES_MAX_Y; y++) //Iterate through each image row.
        {
            for (int x = 0; x <= MARS_IMAGES_MAX_X; x++) //Iterate through each image column.
            {
                image = new WWW(MARS_IMAGES_URL.Replace("/5/0/0.png", string.Format("/5/{0}/{1}.png", y, x)));
                while (!image.isDone) ;
                marsChunk = image.texture;

                for (int j = 0; j < 256; j++) //Iterate through each pixel row.
                {
                    for (int i = 0; i < 256; i++) //Iterate through each pixel column.
                        pixels[y * MARS_ROW_NUM_PIXELS * 256 + j * MARS_ROW_NUM_PIXELS + x * 256 + i] = marsChunk.GetPixel(i, j);
                }
            }
        }

        Texture2D mars = new Texture2D(MARS_ROW_NUM_PIXELS, MARS_COLUMN_NUM_PIXELS);
        mars.SetPixels(pixels);

        return mars;
    }
}

public enum Direction
{ Up, Down, Left, Right };