using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform tilePrefab;
    [SerializeField] private Transform gameHolder;

    private readonly List<Tile> tiles = new();

    private int width;
    private int height;
    private int numMines;
    private readonly float tileSize = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        CreateGameBoard(18,14,40);//Easy
        //CreateGameBoard(16,16,40);//Medium
        //CreateGameBoard(30,16,99);//Hard
        ResetGameState();
    }

    public void CreateGameBoard(int width, int height, int numMines)
    {
        //Save the game parameters we're using.
        this.width = width;
        this.height = height;
        this.numMines = numMines;

        //Create the array of tiles.
        for(int row = 0; row < height; row++)//row boardgame
        {
            for(int col = 0; col < width; col++)//column boardgame
            {
                //Position the tile in the correct place (centred)
                Transform tileTransform = Instantiate(tilePrefab);
                tileTransform.parent = gameHolder;
                float xIndex = col - ((width - 1) / 2.0f);
                float yIndex = row - ((height - 1) / 2.0f);
                tileTransform.localPosition = new Vector2(xIndex  * tileSize, yIndex * tileSize);
                //Keep a reference to the tile for setting up the game.
                Tile tile = tileTransform.GetComponent<Tile>();
                tiles.Add(tile);
                tile.gameManager = this;
            }
        }
    }

    public void ResetGameState()
    {
        //Randomly shuffle the tile positions to get indices for mine positions.
        int[] minePosition = Enumerable.Range(0, tiles.Count).OrderBy(x => Random.Range(0.0f, 1.0f)).ToArray();
        
        //Set mines at the first numMines positions.
        for(int i = 0; i < numMines; i++)
        {
            int pos = minePosition[i];
            tiles[pos].isMine = true;
        }

        //Update all the tiles to hold the corret number of mines.
        for(int i = 0; i < tiles.Count; i++)
        {
            tiles[i].mineCount = HowManyMines(i);
        }
    }

    //Given a location work out how many mines are surrounding it.
    private int HowManyMines(int location) {
        int count = 0;
        foreach(int pos in GetNeighbours(location))
        {
            if (tiles[pos].isMine)
            {
                count++;
            }
        }
        return count;
    }

    //Given a position, return the positions of all neighbours
    private List<int> GetNeighbours(int pos)
    {
        List<int> neighbours = new();
        int row = pos / width;
        int col = pos % width;
        // (0,0) is bottom left.
        if(row < (height - 1))
        {
            neighbours.Add(pos + width);//North position
            if(col > 0)
            {
                neighbours.Add(pos + width - 1);//Nort-West position
            }
            if(col < (width - 1))
            {
                neighbours.Add(pos + width + 1);//North-East position
            }
        }
        if(col > 0)
        {
            neighbours.Add(pos - 1);//West
        }
        if(col < (width - 1))
        {
            neighbours.Add(pos + 1);//East
        }
        if(row > 0)
        {
            neighbours.Add(pos - width);//South
            if(col > 0)
            {
                neighbours.Add(pos - width - 1);//South-West
            }
            if(col < (width - 1))
            {
                neighbours.Add(pos - width + 1);//South-East
            }
        }
        return neighbours;
    }
    
    public void ClickNeighbours(Tile tile)
    {
        int location = tiles.IndexOf(tile);
        foreach(int pos in GetNeighbours(location))
        {
            tiles[pos].ClickedTile();
        }
    }

    public void GameOver()
    {
        //Disable clicks on all time.
        foreach(Tile tile in tiles)
        {
            tile.ShowGameOverState();
        }
    }

    public void CheckGameOver()
    {
        //If there are numMines left active then we're done.
        int count = 0;
        foreach(Tile tile in tiles)
        {
            if (tile.active)
            {
                count++;
            }
        }
        if(count == numMines)
        {
            //Flag and disable everything, we're done
            Debug.Log("Winner!");
            foreach(Tile tile in tiles)
            {
                tile.active = false;
                tile.SetFlaggedIfMine();
            }
        }
    }

    //Click on all surrounding tiles if mines all flagged.
    public void ExpandIfFlagged(Tile tile) 
    {
        int location = tiles.IndexOf(tile);
        //Get the number of flags.
        int flagCount = 0;
        foreach(int pos in GetNeighbours(location))
        {
            if (tiles[pos].flagged)
            {
                flagCount++;
            }
        }
        //If we have the right number click surrounding tiles.
        if(flagCount == tile.mineCount)
        {
            //Clicking a flag does nothing so this is safe.
            ClickNeighbours(tile);
        }
    }
}
