using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationalKey : MonoBehaviour
{

    GameObject tetrad;
    Dictionary<int, Vector2Int[]> RotationalStates = new Dictionary<int, Vector2Int[]>();

    private void Start()
    {
        AquirePrefab();
        AquireRotationalStateDictionary();
    }

    void AquirePrefab()
    {

    }

    void AquireRotationalStateDictionary()
    {
        switch (tetrad.name)
        {
            case "Red":
            {
                
                break;
            } 
        }
    }
}
