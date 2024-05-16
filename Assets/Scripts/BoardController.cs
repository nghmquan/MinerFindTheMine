using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoardController : Singleton<BoardController>
{
    [SerializeField] private Transform tilePrefab;
    [SerializeField] private Transform tileHolder;

    private List<Tile> tiles = new();

    private int width;
    private int height;
    private int numMines;

    private readonly float tileSize = 0.5f;

    private bool _isFirstClick = true;

    public bool IsFirstClick { get => _isFirstClick; set => _isFirstClick = value; }
    

    void SetBoardData()
    {
        BoardData _boardData = GameManager.Instance.GetBoardData;
        width = _boardData.width;
        height = _boardData.height;
        numMines = _boardData.numMines;
    }

    //Game Operation
    public void Initialized()
    {
        SetBoardData();
        CreateGameBoard();
        ResetRandomMines();
    }

    public void ResetBoardIfFirstClickBomb()
    {
        DestroyBoardTiles();
        CreateGameBoard();
        ResetRandomMines();
        IsFirstClick = true;
    }

    void DestroyBoardTiles()
    {
        foreach (var tile in tiles)
        {
            Destroy(tile);
        }
        tiles.Clear();
    }

    public void CreateGameBoard()
    {

        // Create the array of tiles.
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                // Position the tile in the correct place (centred).
                Transform tileTransform = Instantiate(tilePrefab);
                tileTransform.parent = tileHolder;
                float xIndex = col - ((width - 1) / 2.0f);
                float yIndex = row - ((height - 1) / 2.0f);
                tileTransform.localPosition = new Vector2(xIndex * tileSize, yIndex * tileSize);
                // Keep a reference to the tile for setting up the game.
                Tile tile = tileTransform.GetComponent<Tile>();
                tiles.Add(tile);
            }
        }
    }



    private void ResetRandomMines()
    {
        // Randomly shuffle the tile positions to get indices for mine positions.
        int[] minePositions = Enumerable.Range(0, tiles.Count).OrderBy(x => Random.Range(0.0f, 1.0f)).ToArray();

        // Set mines at the first numMines positions.
        for (int i = 0; i < numMines; i++)
        {
            int pos = minePositions[i];
            tiles[pos].isMine = true;
        }

        // Update all the tiles to hold the correct number of mines.
        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i].mineCount = GetMinesNearbyNumber(i);
        }
    }

    // Given a location work out how many mines are surrounding it.
    private int GetMinesNearbyNumber(int location)
    {
        int count = 0;
        foreach (int pos in Helper.GetNeighbours(location,width,height))
        {
            if (tiles[pos].isMine)
            {
                count++;
            }
        }
        return count;
    }

    public void ClickNeighbours(Tile tile)
    {
        int location = tiles.IndexOf(tile);
        foreach (int pos in Helper.GetNeighbours(location,width,height))
        {
            tiles[pos].ClickedTile();
        }
    }

    public void LoseGame()
    {
        // Disable clicks on all mines.
        foreach (Tile tile in tiles)
        {
            tile.ShowGameOverState();
        }
        Debug.Log("Lose!");
        GameManager.Instance.ShowLoseGame();
    }

    public void WinGame()
    {
        // If there are numMines left active then we're done.
        int count = 0;
        foreach (Tile tile in tiles)
        {
            if (tile.active)
            {
                count++;
            }
        }
        if (count == numMines)
        {
            // Flag and disable everything, we're done.
            Debug.Log("Winner!");
            foreach (Tile tile in tiles)
            {
                tile.active = false;
                tile.SetFlaggedIfMine();
            }
            GameManager.Instance.ShowWinGame();
        }
    }

    // Click on all surrounding tiles if mines are all flagged.
    public void ExpandIfFlagged(Tile tile)
    {
        int location = tiles.IndexOf(tile);
        // Get the number of flags.
        int flag_count = 0;
        foreach (int pos in Helper.GetNeighbours(location, width, height))
        {
            if (tiles[pos].flagged)
            {
                flag_count++;
            }
        }
        // If we have the right number click surrounding tiles.
        if (flag_count == tile.mineCount)
        {
            // Clicking a flag does nothing so this is safe.
            ClickNeighbours(tile);
        }
    }
}
