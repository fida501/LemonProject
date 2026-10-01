using System;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [Header("Board")]
    [SerializeField] private int rows = 8;
    [SerializeField] private int columns = 8;
    [SerializeField] private float blockSpacing = 1.5f;

    [Header("Block")]
    [SerializeField] private Block blockPrefab;

    void Start()
    {
        GenerateBoard();
    }

    private void GenerateBoard()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                CreateBlock(row, column);
            }
        }
    }

    private void CreateBlock(int row, int column)
    {
        Vector3 position = new Vector3(column * blockSpacing, 0f, row * blockSpacing);
        Block block = Instantiate(blockPrefab, position, Quaternion.identity, transform);
        block.SetRandomBlockColor();
    }

}
