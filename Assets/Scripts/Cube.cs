using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {
    PlayerMovement cubeParent;

    // Start is called before the first frame update
    void Start() {
        cubeParent = GetComponentInParent<PlayerMovement>();
        }

    // Update is called once per frame
    void Update() {
        if (!cubeParent.ControlsEnabled()) { // If player controls are disabled, prepare to check for line of cubes

            }
        }

    public void DestroyBlock() {
        Destroy(gameObject);
        }


    }




