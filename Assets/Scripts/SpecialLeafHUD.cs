using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialLeafHUD : MonoBehaviour {
    public Texture2D LeafImage;

    private int numLeafs;

    private void Start() {
        numLeafs = FindObjectsOfType<SpecialLeaf>().Length;
    }

    private void OnGUI() {
        for (int i = 0; i < numLeafs; i++) {
            GUI.color = Color.yellow;
            GUI.DrawTexture(new Rect(15 + i * 30, 15, 30, 40), LeafImage);

            if (i < SpecialLeaf.numGot) {
                GUI.color = Color.white;
            } else {
                GUI.color = Color.black;
            }

            GUI.DrawTexture(new Rect(20 + i * 30, 20, 20, 30), LeafImage);
        }
    }
}
