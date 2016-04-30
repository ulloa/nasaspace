using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TileContainer
{
    public Texture2D Texture { get; set; }
    public Vector2 Position { get; set; }
}

public class TileContainerSorter : IComparer<TileContainer>
{
    int IComparer<TileContainer>.Compare(TileContainer a, TileContainer b)
    {
        if (a.Position.y != b.Position.y)
        {
            if (a.Position.y > b.Position.y)
                return -1;
            else
                return 1;
        }
        else if(a.Position.x != b.Position.x)
        {
            if (a.Position.x > b.Position.x)
                return 1;
            else
                return -1;
        }

        return 0;
    }
}