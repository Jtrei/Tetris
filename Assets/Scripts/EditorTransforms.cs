using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EditorTransforms : MonoBehaviour
{

    [SerializeField] bool gridSnapEnabled;
    [SerializeField] [Range(1, 20)] int gridSize = 1;

    TextMesh textMesh;


    // Update is called once per frame
    void Update()
    {
        if (gridSnapEnabled)
        {
            Vector3 snapPos;
            snapPos.x = Mathf.RoundToInt(transform.position.x / gridSize) * gridSize;
            snapPos.y = Mathf.RoundToInt(transform.position.y / gridSize) * gridSize;
            snapPos.z = Mathf.RoundToInt(transform.position.z / gridSize) * gridSize;

            transform.position = new Vector3(snapPos.x, snapPos.y, snapPos.z);
        }

        textMesh = GetComponentInChildren<TextMesh>();

        if (textMesh != null)
        {
            UpdateLabel();
        }
    }

    private void UpdateLabel()
    {
        try
        {
            string labelText = $"({transform.position.x},{transform.position.y})";
            textMesh.text = labelText;
            gameObject.name = $"{labelText}";
        }
        catch
        {

        }
    }
}
