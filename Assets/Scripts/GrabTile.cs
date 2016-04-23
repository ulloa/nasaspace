using UnityEngine;

public class GrabTile
{
    private const short MAX_X = 63;
    private const short MAX_Y = 31;
    private const string marsImagesURL = "https://api.nasa.gov/mars-wmts/catalog/Mars_MGS_MOLA_DEM_mosaic_global_463m_8/1.0.0//default/default028mm/5/0/0.png";
    private const int equatorRadius = 3396;
    private const int r3 = 3376;
    private const double equatorCircumference = 2 * Mathf.PI * equatorRadius;
    private const int MarsPeak = 24000;
    private const int MarsTileHeight = 331000;
    //private Dictionary<string, Vector3> tileDimentsions = new Dictionary<string, Vector3>() { { "", new Vector3() }, };
    //private static string[,] tilePaths = 
    //    new string[4, 8] { { "https://api.nasa.gov/mars-wmts/catalog/Mars_MGS_MOLA_DEM_mosaic_global_463m_8/1.0.0//default/default028mm/2/0/0.png",
    //                         "https://api.nasa.gov/mars-wmts/catalog/Mars_MGS_MOLA_DEM_mosaic_global_463m_8/1.0.0//default/default028mm/2/0/1.png",
    //                         "https://api.nasa.gov/mars-wmts/catalog/Mars_MGS_MOLA_DEM_mosaic_global_463m_8/1.0.0//default/default028mm/2/0/2.png",
    //                         "https://api.nasa.gov/mars-wmts/catalog/Mars_MGS_MOLA_DEM_mosaic_global_463m_8/1.0.0//default/default028mm/2/0/3.png",
    //                         "https://api.nasa.gov/mars-wmts/catalog/Mars_MGS_MOLA_DEM_mosaic_global_463m_8/1.0.0//default/default028mm/2/0/4.png",
    //                         "https://api.nasa.gov/mars-wmts/catalog/Mars_MGS_MOLA_DEM_mosaic_global_463m_8/1.0.0//default/default028mm/2/0/5.png",
    //                         "https://api.nasa.gov/mars-wmts/catalog/Mars_MGS_MOLA_DEM_mosaic_global_463m_8/1.0.0//default/default028mm/2/0/6.png",
    //                         "https://api.nasa.gov/mars-wmts/catalog/Mars_MGS_MOLA_DEM_mosaic_global_463m_8/1.0.0//default/default028mm/2/0/7.png"
    //                       },
    //                       { "https://api.nasa.gov/mars-wmts/catalog/Mars_MGS_MOLA_DEM_mosaic_global_463m_8/1.0.0//default/default028mm/2/1/0.png",
    //                         "https://api.nasa.gov/mars-wmts/catalog/Mars_MGS_MOLA_DEM_mosaic_global_463m_8/1.0.0//default/default028mm/2/1/1.png",
    //                         "https://api.nasa.gov/mars-wmts/catalog/Mars_MGS_MOLA_DEM_mosaic_global_463m_8/1.0.0//default/default028mm/2/1/2.png",
    //                         "https://api.nasa.gov/mars-wmts/catalog/Mars_MGS_MOLA_DEM_mosaic_global_463m_8/1.0.0//default/default028mm/2/1/3.png",
    //                         "https://api.nasa.gov/mars-wmts/catalog/Mars_MGS_MOLA_DEM_mosaic_global_463m_8/1.0.0//default/default028mm/2/1/4.png",
    //                         "https://api.nasa.gov/mars-wmts/catalog/Mars_MGS_MOLA_DEM_mosaic_global_463m_8/1.0.0//default/default028mm/2/1/5.png",
    //                         "https://api.nasa.gov/mars-wmts/catalog/Mars_MGS_MOLA_DEM_mosaic_global_463m_8/1.0.0//default/default028mm/2/1/6.png",
    //                         "https://api.nasa.gov/mars-wmts/catalog/Mars_MGS_MOLA_DEM_mosaic_global_463m_8/1.0.0//default/default028mm/2/1/7.png"
    //                       },
    //                       { "https://api.nasa.gov/mars-wmts/catalog/Mars_MGS_MOLA_DEM_mosaic_global_463m_8/1.0.0//default/default028mm/2/2/0.png",
    //                         "https://api.nasa.gov/mars-wmts/catalog/Mars_MGS_MOLA_DEM_mosaic_global_463m_8/1.0.0//default/default028mm/2/2/1.png",
    //                         "https://api.nasa.gov/mars-wmts/catalog/Mars_MGS_MOLA_DEM_mosaic_global_463m_8/1.0.0//default/default028mm/2/2/2.png",
    //                         "https://api.nasa.gov/mars-wmts/catalog/Mars_MGS_MOLA_DEM_mosaic_global_463m_8/1.0.0//default/default028mm/2/2/3.png",
    //                         "https://api.nasa.gov/mars-wmts/catalog/Mars_MGS_MOLA_DEM_mosaic_global_463m_8/1.0.0//default/default028mm/2/2/4.png",
    //                         "https://api.nasa.gov/mars-wmts/catalog/Mars_MGS_MOLA_DEM_mosaic_global_463m_8/1.0.0//default/default028mm/2/2/5.png",
    //                         "https://api.nasa.gov/mars-wmts/catalog/Mars_MGS_MOLA_DEM_mosaic_global_463m_8/1.0.0//default/default028mm/2/2/6.png",
    //                         "https://api.nasa.gov/mars-wmts/catalog/Mars_MGS_MOLA_DEM_mosaic_global_463m_8/1.0.0//default/default028mm/2/2/7.png"
    //                       },
    //                       { "https://api.nasa.gov/mars-wmts/catalog/Mars_MGS_MOLA_DEM_mosaic_global_463m_8/1.0.0//default/default028mm/2/3/0.png",
    //                         "https://api.nasa.gov/mars-wmts/catalog/Mars_MGS_MOLA_DEM_mosaic_global_463m_8/1.0.0//default/default028mm/2/3/0.png",
    //                         "https://api.nasa.gov/mars-wmts/catalog/Mars_MGS_MOLA_DEM_mosaic_global_463m_8/1.0.0//default/default028mm/2/3/0.png",
    //                         "https://api.nasa.gov/mars-wmts/catalog/Mars_MGS_MOLA_DEM_mosaic_global_463m_8/1.0.0//default/default028mm/2/3/0.png",
    //                         "https://api.nasa.gov/mars-wmts/catalog/Mars_MGS_MOLA_DEM_mosaic_global_463m_8/1.0.0//default/default028mm/2/3/0.png",
    //                         "https://api.nasa.gov/mars-wmts/catalog/Mars_MGS_MOLA_DEM_mosaic_global_463m_8/1.0.0//default/default028mm/2/3/0.png",
    //                         "https://api.nasa.gov/mars-wmts/catalog/Mars_MGS_MOLA_DEM_mosaic_global_463m_8/1.0.0//default/default028mm/2/3/6.png",
    //                         "https://api.nasa.gov/mars-wmts/catalog/Mars_MGS_MOLA_DEM_mosaic_global_463m_8/1.0.0//default/default028mm/2/3/7.png"
    //                       } };

    /// <param name="currentX">Your current tile's x coordinate.</param>
    /// <param name="currentY">Your current tile's y coordinate.</param>
    /// <param name="D">The direction of the tile you want to load in.</param>
    /// <param name="dimensions">the x, y, and z coordinates the Texture2D in meters.</param>
    /// <returns>A Heightmap in the form of a Texture2D.</returns>
    public static Texture2D MarsGetTile(short currentX, short currentY, Direction? D, out Vector3 dimensions, float scale = 0.01f)
    {
        dimensions = new Vector3(MarsTileHeight * scale, MarsPeak * scale, 
                    ((int)findTileWidth(211 * ((currentY > 15)? currentY - 16 : 15 - currentY)) * MarsTileHeight) * scale);
        string url;

        switch (D)
        {
            case Direction.Up:
                url = marsImagesURL.Replace("/5/0/0.png", 
                                     string.Format("/5/{0}/{1}.png", (currentY == 0) ? MAX_Y : currentY - 1, currentX));
                break;
            case Direction.Right:
                url = marsImagesURL.Replace("/5/0/0.png",
                                     string.Format("/5/{0}/{1}.png", currentY, (currentX == MAX_X) ? 0 : currentX + 1));
                break;
            case Direction.Down:
                url = marsImagesURL.Replace("/5/0/0.png",
                                     string.Format("/5/{0}/{1}.png", (currentY == MAX_Y)? 0 : currentY + 1, currentX));
                break;
            case Direction.Left:
                url = marsImagesURL.Replace("/5/0/0.png",
                                     string.Format("/5/{0}/{1}.png", currentY, (currentX == 0) ? MAX_X : currentX - 1));
                break;
            default:
                url = marsImagesURL;
                break;
        }

        Texture2D tex = new Texture2D(2, 2);
        WWW image = new WWW(url);
        while (!image.isDone) ;
        tex = image.texture;

        return tex;
    }

    private static double findTileWidth(int z)
    {
            return (2 * Mathf.PI * Mathf.Sqrt(Mathf.Pow(equatorRadius, 2) - (Mathf.Pow(equatorRadius, 2) * Mathf.Pow(z, 2) 
                                                                        / Mathf.Pow(r3, 2)))) / equatorCircumference;

    }
}

public enum Direction
{ Up, Down, Left, Right };

public enum TerrestrialBody
{ Mars, Vesta };