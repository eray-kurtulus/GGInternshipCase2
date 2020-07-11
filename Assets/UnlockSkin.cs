using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class UnlockSkin : MonoBehaviour {

    Color green;
    Color gray;
    System.Random rand;
    GameObject[] skins;
    List<GameObject> lockedSkins;
    public Button unlockSkinButton;
    public Sprite[] skinSprites = new Sprite[9];


    // Start is called before the first frame update
    void Start() {

        green = new Color(0.162f, 0.528f, 0.177f, 1f);
        gray = new Color(0.502f, 0.502f, 0.502f, 1f);

        rand = new System.Random();

        skins = GameObject.FindGameObjectsWithTag("SkinButton");
        lockedSkins = new List<GameObject>();

        foreach (GameObject skin in skins) {
            if(skin.transform.GetChild(1).tag == "LockedSkin") {
                lockedSkins.Add(skin);
            }
        }

        unlockSkinButton.onClick.AddListener(onClick);
    }

    void onClick() {
        StartCoroutine(unlockSkin());
    }

    IEnumerator unlockSkin() {
        int chosen;
        for(int i = 0; i < 10; i++) {
            chosen = rand.Next(lockedSkins.Count);
            lockedSkins[chosen].GetComponent<Image>().color = green;
            // wait a little
            yield return new WaitForSeconds(0.1f * i);
            lockedSkins[chosen].GetComponent<Image>().color = gray;
        }
        chosen = rand.Next(lockedSkins.Count);
        lockedSkins[chosen].GetComponent<Image>().color = green;
        Debug.Log(lockedSkins[chosen].name[4] - '1');
        lockedSkins[chosen].transform.GetChild(1).GetComponent<Image>().sprite = skinSprites[(lockedSkins[chosen].name[4] - '1')];
    }

}
