using System.Collections.Generic;
using UnityEngine;

public class CardGridGenerator : MonoBehaviour
{
    [Header("Grid Settings")]
    [Min(1)]
    public int cardsPerRow = 5;

    public float columnSpacing = 1.5f;
    public float rowSpacing = 2.0f;

    [Header("Plane Position")]
    public float gridZ = 0f;

    [Header("Vertical Offset")]
    [Tooltip("Moves the entire grid down (negative = up, positive = down)")]
    public float verticalOffset = 0f;

    [Header("Card Count")]
    [Min(0)]
    public int cardCount = 0;

    [Header("Debug")]
    public List<Transform> points = new List<Transform>();

    private void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        ClearExistingPoints();
        points.Clear();

        if (cardCount <= 0)
            return;

        int rows = Mathf.CeilToInt((float)cardCount / cardsPerRow);

        // Start Y from the top row and move downward
        float startY = ((rows - 1) * rowSpacing) / 2f - verticalOffset;

        for (int row = 0; row < rows; row++)
        {
            int cardsInThisRow = Mathf.Min(
                cardsPerRow,
                cardCount - row * cardsPerRow
            );

            // Center THIS row based on how many cards it has
            float rowWidth = (cardsInThisRow - 1) * columnSpacing;
            float startX = -rowWidth / 2f;

            for (int col = 0; col < cardsInThisRow; col++)
            {
                int index = row * cardsPerRow + col;

                GameObject point = new GameObject($"CardPoint_{index}");
                point.transform.SetParent(transform);

                float x = startX + col * columnSpacing;
                float y = startY - row * rowSpacing;

                point.transform.localPosition = new Vector3(x, y, gridZ);
                points.Add(point.transform);
            }
        }
    }

    private void ClearExistingPoints()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
