using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockParent : MonoBehaviour {


    GameObject[] children;
    Dictionary<GameObject, Vector2Int> childrenPosition = new Dictionary<GameObject, Vector2Int>();

    // Start is called before the first frame update
    void Start() {
        children = GetChildren();
        }


    public GameObject[] GetChildren() {
        children = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++) {
            children[i] = transform.GetChild(i).gameObject;
            }

        if (children.Length == 0) {
            Destroy(gameObject);
            }

        return children;
        }

    public Dictionary<GameObject, Vector2Int> GetChildrenAndPositions() {
        children = GetChildren();

        return childrenPosition;
        }
    }
