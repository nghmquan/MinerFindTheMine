using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper
{
    public static List<int> GetNeighbours(int pos,int width, int height)
    {
        List<int> neighbours = new();
        int row = pos / width;
        int col = pos % width;
        // (0,0) is bottom left.
        if (row < (height - 1))
        {
            neighbours.Add(pos + width); // North
            if (col > 0)
            {
                neighbours.Add(pos + width - 1); // North-West
            }
            if (col < (width - 1))
            {
                neighbours.Add(pos + width + 1); // North-East
            }
        }
        if (col > 0)
        {
            neighbours.Add(pos - 1); // West
        }
        if (col < (width - 1))
        {
            neighbours.Add(pos + 1); // East
        }
        if (row > 0)
        {
            neighbours.Add(pos - width); // South
            if (col > 0)
            {
                neighbours.Add(pos - width - 1); // South-West
            }
            if (col < (width - 1))
            {
                neighbours.Add(pos - width + 1); // South-East
            }
        }
        return neighbours;
    }
}
