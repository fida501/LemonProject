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

    private List<Block> blocks = new List<Block>();

    void Start()
    {
        GenerateBoard();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // GenerateBoard();
        }
    }

    private void GenerateBoard()
    {
        ClearBoard();
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
        foreach (Block block in blocks)
        {
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

        return lowestBlock;
    }



    private void CreateBlock(int row, int column)
    {
        float x = (column - (columns - 1) / 2f) * blockSpacing;
        float z = (row - (rows - 1) / 2f) * blockSpacing;
        Vector3 position = new Vector3(x, 0f, z);
        Block block = Instantiate(blockPrefab, position, Quaternion.identity, transform);
        block.SetRandomBlockColor();
        blocks.Add(block);
    }

    private void ClearBoard()
    {
        blocks.Clear();
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

}
