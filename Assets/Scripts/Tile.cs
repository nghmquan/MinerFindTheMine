using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class Tile : Singleton<Tile>
{
    [Header("Tile Sprites")]
    [SerializeField] private Sprite unclickedTile;
    [SerializeField] private Sprite flaggedTile;
    [SerializeField] private List<Sprite> clickedTiles;
    [SerializeField] private Sprite mineTile;
    [SerializeField] private Sprite mineWrongTile;
    [SerializeField] private Sprite mineHitTile;
   

    private SpriteRenderer spriteRenderer;
    public bool flagged = false;
    public bool active = true;
    public bool isMine = false;
    public int mineCount = 0;


    void Awake()
    {
        // This should always exist due to the RequireComponent helper.
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseOver()
    {
        // If it hasn't already been pressed.
        if (active)
        {
            if (Input.GetMouseButtonDown(0))
            {
                // If left click reveal the tile contents.
                ClickedTile();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                // If right click toggle flag on/off.
                flagged = !flagged;
                if (flagged)
                {
                    spriteRenderer.sprite = flaggedTile;
                }
                else
                {
                    spriteRenderer.sprite = unclickedTile;
                }
            }
        }
        else
        {
            // If you're pressing both mouse buttons.
            if (Input.GetMouseButton(0) && Input.GetMouseButton(1))
            {
                // Check for valid expansion.
                BoardController.Instance.ExpandIfFlagged(this);
            }
        }
    }

    public void ClickedTile()
    {
        // Don't allow left clicks on flags.
        if (active & !flagged)
        {
            // Ensure it can no longer be pressed again.
            active = false;
            if (isMine)
            {
                if (BoardController.Instance.IsFirstClick)
                {
                    Debug.Log("Hit Mines First Click");
                    BoardController.Instance.ResetBoardIfFirstClickBomb();
                    return;
                }
                // Game over :(
                spriteRenderer.sprite = mineHitTile;
                BoardController.Instance.LoseGame();
            }
            else
            {
                // It was a safe click, set the correct sprite.

                BoardController.Instance.IsFirstClick = false;
                spriteRenderer.sprite = clickedTiles[mineCount];
                if (mineCount == 0)
                {
                    // Register that the click should expand out to the neighbours.
                    BoardController.Instance.ClickNeighbours(this);
                }
                // Whenever we successfully make a change check for game over.
                BoardController.Instance.WinGame();
            }
        }
    }

    // If this tile should be shown at game over, do so.
    public void ShowGameOverState()
    {
        if (active)
        {
            active = false;
            if (isMine & !flagged)
            {
                // If mine and not flagged show mine.
                spriteRenderer.sprite = mineTile;
            }
            else if (flagged & !isMine)
            {
                // If flagged incorrectly show crossthrough mine
                spriteRenderer.sprite = mineWrongTile;
            }
        }
    }

    // Helper function to flag remaning mines on game completion.
    public void SetFlaggedIfMine()
    {
        if (isMine)
        {
            flagged = true;
            spriteRenderer.sprite = flaggedTile;
        }
    }

}