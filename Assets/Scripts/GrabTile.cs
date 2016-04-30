using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;

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

    //VESTA CONSTANTS
    private const short VESTA_IMAGES_MAX_X = 127;
    private const short VESTA_IMAGES_MAX_Y = 63;
    private const string VESTA_IMAGES_URL = "https://api.nasa.gov/vesta-wmts/catalog/global_LAMO/1.0.0//default/default028mm/6/0/0.png";
    private const short VESTA_PRIME_MERIDIAN_RADIUS = 223;
    private const float VESTA_EQUATOR_CIRCUMFERENCE = 1774000;
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

    public static Texture2D GetMarsSquare(Vector2 picCoordinants, int squareLength, out Vector3 dimensions, float horizontalScale = 0.01f, float verticalScale = 0.085f)
    {
        float tileSize = (MARS_EQUATOR_CIRCUMFERENCE * squareLength * horizontalScale) / (MARS_IMAGES_MAX_X + 1);
        dimensions = new Vector3(tileSize, MARS_PEAK * verticalScale, tileSize);
        squareLength = squareLength - 1;
        Texture2D marsChunk;

        int upperXBound;
        int lowerXBound;
        int upperYBound;
        int lowerYBound;

        if (picCoordinants.y > MARS_IMAGES_MAX_Y)
            picCoordinants.y = MARS_IMAGES_MAX_Y;

        if (picCoordinants.x > MARS_IMAGES_MAX_X)
            picCoordinants.x = MARS_IMAGES_MAX_X;

        if (picCoordinants.x == MARS_IMAGES_MAX_X)
        {
            lowerXBound = MARS_IMAGES_MAX_X - squareLength;
            upperXBound = MARS_IMAGES_MAX_X;
        }
        else
        {
            lowerXBound = (int)picCoordinants.x;
            upperXBound = (int)(picCoordinants.x + squareLength);
        }

        if (picCoordinants.y == MARS_IMAGES_MAX_Y)
        {
            lowerYBound = (int)(MARS_IMAGES_MAX_Y - squareLength);
            upperYBound = MARS_IMAGES_MAX_Y;
        }
        else
        {
            upperYBound = (int)(picCoordinants.y + squareLength);
            lowerYBound = (int)(picCoordinants.y);
        }

        Color[] pixels = null;
        int width = 0;
        int height = 0;

        for (int y = upperYBound; y >= lowerYBound; y--) //Iterate through each image row.
        {
            for (int x = lowerXBound; x <= upperXBound; x++) //Iterate through each image column.
            {
                var image = new WWW(MARS_IMAGES_URL.Replace("/5/0/0.png", string.Format("/5/{0}/{1}.png", y, x)));
                while (!image.isDone) ;

                marsChunk = image.texture;

                if (pixels == null)
                {
                    height = marsChunk.height;
                    width = marsChunk.width;
                    pixels = new Color[((squareLength + 1) * marsChunk.width) * (marsChunk.height * (squareLength + 1))];
                }

                for (short j = 0; j < 256; j++) //Iterate through each pixel row.
                {
                    for (short i = 0; i < 256; i++) //Iterate through each pixel column.
                        pixels[(y - lowerYBound) * ((squareLength + 1) * marsChunk.height) * marsChunk.height
                            + j * ((squareLength + 1) * marsChunk.width) + (x - lowerXBound) * marsChunk.height + i] = marsChunk.GetPixel(i, (marsChunk.height - 1) - j);
                }
            }
        }

        Texture2D mars = new Texture2D(width * (squareLength + 1), height * (squareLength + 1));
        mars.SetPixels(pixels);

        //byte[] bytes = mars.EncodeToPNG();
        //File.WriteAllBytes(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Mars_SavedScreen.png", bytes);

        return mars;
    }

    public static Texture2D GetVestaSquare(Vector2 picCoordinants, out Vector3 dimensions, float horizontalScale = 0.001f, float verticalScale = 0.035f)
    {
        float tileSize = VESTA_EQUATOR_CIRCUMFERENCE * horizontalScale / (VESTA_IMAGES_MAX_X + 1);
        dimensions = new Vector3(tileSize, VESTA_PEAK * verticalScale, tileSize);
        WWW image = new WWW("");
        short upperXBound;
        short lowerXBound;
        short upperYBound;
        short lowerYBound;

        if (picCoordinants.y > VESTA_IMAGES_MAX_Y)
            picCoordinants.y = VESTA_IMAGES_MAX_Y;

        if (picCoordinants.x > VESTA_IMAGES_MAX_X)
            picCoordinants.x = VESTA_IMAGES_MAX_X;

        if (picCoordinants.x == VESTA_IMAGES_MAX_X)
        {
            lowerXBound = VESTA_IMAGES_MAX_X - 1;
            upperXBound = VESTA_IMAGES_MAX_X;
        }
        else
        {
            lowerXBound = (short)(picCoordinants.x);
            upperXBound = (short)(picCoordinants.x + 1);
        }

        if (picCoordinants.y == VESTA_IMAGES_MAX_Y)
        {
            lowerYBound = VESTA_IMAGES_MAX_Y - 1;
            upperYBound = VESTA_IMAGES_MAX_Y;
        }
        else
        {
            upperYBound = (short)(picCoordinants.y + 1);
            lowerYBound = (short)(picCoordinants.y);
        }

        var images = new List<Texture2D>();

        for (short y = upperYBound; y >= lowerYBound; y--) //Iterate through each image row.
        {
            for (short x = lowerXBound; x <= upperXBound; x++) //Iterate through each image column.
            {
                image = new WWW(VESTA_IMAGES_URL.Replace("/6/0/0.png", string.Format("/6/{0}/{1}.png", y, x)));
                while (!image.isDone) ;

                images.Add(image.texture);
            }
        }

        Texture2D vesta = new Texture2D(image.texture.width * (int)Mathf.Sqrt(images.Count), image.texture.height * (int)Mathf.Sqrt(images.Count));
        vesta.PackTextures(images.ToArray(), 0);

        byte[] bytes = vesta.EncodeToPNG();
        File.WriteAllBytes(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Vesta_SavedScreen.png", bytes);

        return vesta;
    }

    public static Texture2D GetMarsHeightMap()
    {
        return new Texture2D(2, 2);
    }
}

public enum Direction
{ Up, Down, Left, Right };

public enum SceneToLoad { Mars, Vesta }