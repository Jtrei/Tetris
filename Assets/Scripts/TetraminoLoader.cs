using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetraminoLoader : MonoBehaviour {

    [SerializeField] GameObject[] tetradPrefabs;

    Vector3 startingPos = new Vector3(4, 19, 0);

    public GameObject activeBlock;

    void Start() {
        StartGame();
        }

    void StartGame() {
        GameObject startingBlockPrefab = tetradPrefabs[Random.Range(0, tetradPrefabs.Length)];
        activeBlock = Instantiate(startingBlockPrefab, startingPos, Quaternion.identity);
        }

    public void SpawnBlock() {
        int tetradInt = Random.Range(0, tetradPrefabs.Length);
        GameObject newBlock = tetradPrefabs[tetradInt];
        activeBlock = Instantiate(newBlock, startingPos, Quaternion.identity);
        }
    }
