using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GridManager : MonoBehaviour
{

    Cube[] blocks;
    List<Cube> blocksToDelete = new List<Cube>();

    public List<Vector2Int> occupiedSpaces = new List<Vector2Int>(); // Used to check for if a position can be moved into.

    Dictionary<Cube, Vector2Int> existingCubePositions = new Dictionary<Cube, Vector2Int>();

    public List<Vector2Int> GetOccupiedSpaces() {
        return occupiedSpaces;
        }

    public void UpdateInactiveCubeList() {
        blocks = FindObjectsOfType<Cube>();
        if (blocks.Length > 0) {
            UpdateOccupiedSpaces();
            CheckForFullRow();
            }
        }

    private void UpdateOccupiedSpaces() {
        occupiedSpaces.Clear();
        existingCubePositions.Clear();

        foreach (Cube block in blocks) {
            Vector2Int blockPosition = GetBlockPosition(block);
            occupiedSpaces.Add(blockPosition);
            existingCubePositions[block] = blockPosition;
            }
        }

    private Vector2Int GetBlockPosition(Cube block) {
        Vector3Int globalPos = Vector3Int.RoundToInt(block.transform.TransformPoint(Vector3.zero));
        return (Vector2Int)globalPos;
        }

    private void CheckForFullRow() {
        int previousXCoordinate = -1;
        int currentXCoordinate = 0;
        int counter = 0;
        List<Vector2Int> sortedOccupiedSpaces = occupiedSpaces.OrderBy(x => x.y).ThenBy(x => x.x).ToList();

        for (int i = 0; i < sortedOccupiedSpaces.Count - 1; i++) {
            currentXCoordinate = sortedOccupiedSpaces[i].y;
            if (currentXCoordinate == previousXCoordinate) {
                counter += 1;
                } else {
                counter = 0;
                }
            if (counter == 9) {
                ClearRow(currentXCoordinate);
                }
            previousXCoordinate = currentXCoordinate;
            }
        }

    private void ClearRow(int rowToClear) {
        print($"Clearing row {rowToClear}");
        blocksToDelete.Clear();

        foreach (KeyValuePair<Cube, Vector2Int> keyValuePair in existingCubePositions) {
            if (keyValuePair.Value.y == rowToClear) {
                Destroy(keyValuePair.Key.gameObject); ;
                } else if (keyValuePair.Value.y > rowToClear) {
                keyValuePair.Key.transform.position += new Vector3Int(0, -1, 0);
                }
            }
        UpdateOccupiedSpaces();
        }

    }


