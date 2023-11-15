using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing : MonoBehaviour
{

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
            UnityEditor.EditorApplication.isPlaying = false;
    }

}
