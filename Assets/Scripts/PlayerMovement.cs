using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class PlayerMovement : MonoBehaviour {

    TetraminoLoader tetraminoLoader;
    GridManager gridManager;
    BlockParent blockParent;

    List<Vector2Int> childrenPositions = new List<Vector2Int>();

    bool playerControlEnabled = true;
    bool touchingGround = false;

    public bool ControlsEnabled() {
        return playerControlEnabled;
        }

    void Start() {
        tetraminoLoader = FindObjectOfType<TetraminoLoader>();
        gridManager = FindObjectOfType<GridManager>();
        blockParent = this.GetComponent<BlockParent>();

        StartCoroutine(ProcessConstantVerticalDescent());
        StartCoroutine(ProcessHorizontalMovement());
        StartCoroutine(ProcessRotationalMovement());
        }

    IEnumerator ProcessConstantVerticalDescent() {
        while (playerControlEnabled) {
            yield return StartCoroutine(WaitBeforeVerticalMovement());
            CheckForVerticalCollision();
            if (!touchingGround) {
                this.transform.position += new Vector3Int(0, -1, 0);
                }
            }
        }

    IEnumerator WaitBeforeVerticalMovement() {
        float verticalInput = Input.GetAxis("Vertical");
        if (verticalInput >= 0) {
            yield return new WaitForSeconds(1);
            } else {
            yield return new WaitForSeconds(0.25f);
            }
        }

    private void CheckForVerticalCollision() {
        childrenPositions.Clear();
        GetPositionsOfChildren(childrenPositions);
        CheckIfCanMoveDown(childrenPositions);
        }

    private void GetPositionsOfChildren(List<Vector2Int> childrenPositions) {
        GameObject[] children = blockParent.GetChildren();
        foreach (GameObject child in children) {
            childrenPositions.Add(new Vector2Int((int)child.transform.position.x, (int)child.transform.position.y));
            }
        }

    private void CheckIfCanMoveDown(List<Vector2Int> childrenPositions) {
        foreach (Vector2Int childPosition in childrenPositions) {
            Vector2Int nextPosition = childPosition + Vector2Int.down;
            if (gridManager.GetOccupiedSpaces().Contains(nextPosition) || childPosition.y == 0) {
                touchingGround = true;
                StartCoroutine(WaitAndSpawnBlock());
                }
            }
        }

    IEnumerator WaitAndSpawnBlock() {

        yield return new WaitForSeconds(1);
        if (playerControlEnabled) {
            playerControlEnabled = false;
            gridManager.UpdateInactiveCubeList();
            tetraminoLoader.SpawnBlock();
            }
        }

    bool canMoveRight = true;
    bool canMoveLeft = true;

    IEnumerator ProcessHorizontalMovement() {
        while (playerControlEnabled) {
            yield return StartCoroutine(WaitBeforeHorizontalMovement());
            CheckForHorizontalCollision();
            if (playerControlEnabled) {
                float horizontalInput = Input.GetAxis("Horizontal");
                if (canMoveRight && horizontalInput > 0) {
                    this.transform.position += new Vector3Int(1, 0, 0);
                    } else if (canMoveLeft && horizontalInput < 0) {
                    this.transform.position += new Vector3Int(-1, 0, 0);
                    }
                }
            }
        }

    IEnumerator WaitBeforeHorizontalMovement() {
        yield return new WaitForSeconds(0.2f);
        }

    private void CheckForHorizontalCollision() {
        childrenPositions.Clear();
        GetPositionsOfChildren(childrenPositions);
        CheckIfCanMoveHorizontal(childrenPositions);
        }

    private void CheckIfCanMoveHorizontal(List<Vector2Int> childrenPositions) {
        foreach (Vector2Int childPosition in childrenPositions) {
            CheckLeftMovement(childPosition);
            CheckRightMovement(childPosition);
            if (!canMoveLeft || !canMoveRight) {
                break;
                }
            }
        }

    private void CheckLeftMovement(Vector2Int childPosition) {
        Vector2Int nextPositionLeft = childPosition + Vector2Int.left;
        if (gridManager.GetOccupiedSpaces().Contains(nextPositionLeft) || childPosition.x <= 0) {
            canMoveLeft = false;
            } else {
            canMoveLeft = true;
            }
        }

    private void CheckRightMovement(Vector2Int childPosition) {
        Vector2Int nextPositionRight = childPosition + Vector2Int.right;
        if (gridManager.GetOccupiedSpaces().Contains(nextPositionRight) || childPosition.x >= 9) {
            canMoveRight = false;
            } else {
            canMoveRight = true;
            }
        }

    bool canRotateCounterClockwise = true;
    bool canRotateClockwise = true;
    int rotationState = 1;

    IEnumerator ProcessRotationalMovement() {
        while (playerControlEnabled) {
            yield return StartCoroutine(WaitBeforeRotationalMovement());
            CheckForRotationalCollision();
            }
        }

    IEnumerator WaitBeforeRotationalMovement() {
        yield return new WaitForSeconds(0.1f);
        if (Input.GetKey(KeyCode.Q)) {
            if (rotationState == 1) {
                rotationState = 4;
                } else { 
                rotationState -= 1; }
            RotateCounterClockwise();
            } else if
            (Input.GetKey(KeyCode.E)){
            if (rotationState == 4) {
                rotationState = 1;
                } else {
                rotationState += 1;
                }
            RotateClockWise();
            }
        }

    private void RotateCounterClockwise() { 
        // Rotates it but pushes it to unusual coordinate space. Make sure the transformation is occuring locally using block 2 as a reference. 
        GameObject[] children = blockParent.GetChildren();
        foreach (GameObject child in children) {
            if (rotationState == 1) {
                child.transform.position = new Vector3(-child.transform.position.y, child.transform.position.x, child.transform.position.z);
                }
            }
        }

    private void RotateClockWise() {

        }

    private void CheckForRotationalCollision() {
        childrenPositions.Clear();
        GetPositionsOfChildren(childrenPositions);
            // CheckIfCanRotate(childrenPositions);
            }

        }
