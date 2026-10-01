using System;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [Header("Board")]
    [SerializeField] private int rows = 8;
    [SerializeField] private int columns = 8;
    [SerializeField] private float blockSpacing = 1.5f;

    [Header("Block")]
    [SerializeField] private Block blockPrefab;


    // Change to 2 Dimentional array
    private Block[,] board;
    // private List<Block> blocks = new List<Block>();

    void Start()
    {
        GenerateBoard();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // GenerateBoard();

            RemoveBlock(UnityEngine.Random.Range(0, rows), UnityEngine.Random.Range(0, columns));
        }
    }

    private void GenerateBoard()
    {
        ClearBoard();
        board = new Block[rows, columns];
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                CreateBlock(row, column);
            }
        }
    }

    public Block GetLowestBlockByColor(Block.BlockColor color)
    {
        Block lowestBlock = null;
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                Block block = board[row, column];
                if (block == null) continue;
                if (block.TheBlockColor != color) continue;
                if (lowestBlock == null)
                {
                    lowestBlock = block;
                    continue;
                }

                if (block.transform.position.z < lowestBlock.transform.position.z)
                {
                    lowestBlock = block;
                }
            }
        }

        return lowestBlock;
    }



    private void CreateBlock(int row, int column)
    {
        float x = (column - (columns - 1) / 2f) * blockSpacing;
        float z = (row - (rows - 1) / 2f) * blockSpacing;
        Vector3 position = new Vector3(x, 0f, z);
        Block block = Instantiate(blockPrefab, position, Quaternion.identity, transform);
        block.SetRandomBlockColor();
        board[row, column] = block;
    }

    private void ClearBoard()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                RemoveBlock(row, column);
            }
        }
    }

    private void RemoveBlock(int row, int column)
    {
        if (board == null) return;
        if (row < 0 || row >= rows) return;
        if (column < 0 || column >= columns) return;
        Block block = board[row, column];
        if (block == null) return;
        board[row, column] = null;
        block.AnimationDestroy(() =>
        {
            CollapseColumn(column);
        });
    }

    private void CollapseColumn(int column)
    {
        int writeRow = 0;
        Block[] movingBlocks = new Block[rows];
        int[] targetRows = new int[rows];

        int movingCount = 0;

        for (int readRow = 0; readRow < rows; readRow++)
        {
            Block block = board[readRow, column];
            if (block == null) continue;
            if (writeRow != readRow)
            {
                movingBlocks[movingCount] = block;
                targetRows[movingCount] = writeRow;
                movingCount++;
            }
            writeRow++;
        }

        if (movingCount == 0) return;

        int completedMoves = 0;

        for (int i = 0; i < movingCount; i++)
        {
            Block block = movingBlocks[i];
            int targetRow = targetRows[i];

            Vector3 targetPosition = GetBlockPosition(targetRow, column);

            block.AnimationMovePosition(targetPosition, () =>
            {
                completedMoves++;
                if (completedMoves != movingCount) return;
                UpdateColumn(column);

            });
        }
    }

    private void UpdateColumn(int column)
    {
        Block[] newColumn = new Block[rows];

        int writeRow = 0;

        for (int readRow = 0; readRow < rows; readRow++)
        {
            Block block = board[readRow, column];

            if (block == null)
                continue;

            newColumn[writeRow] = block;
            writeRow++;
        }

        for (int row = 0; row < rows; row++)
        {
            board[row, column] = newColumn[row];
        }
    }
    private Vector3 GetBlockPosition(int row, int column)
    {
        float x = (column - (columns - 1) / 2f) * blockSpacing;
        float z = (row - (rows - 1) / 2f) * blockSpacing;

        return new Vector3(x, 0f, z);
    }

    private void MoveBlockPosition(Block block, int newRow, int newColumn)
    {
        float x = (newColumn - (columns - 1) / 2f) * blockSpacing;
        float z = (newRow - (rows - 1) / 2f) * blockSpacing;

        block.transform.position = new Vector3(x, 0f, z);
    }
}
