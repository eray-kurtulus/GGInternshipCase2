using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class UnlockSkin : MonoBehaviour {

    Color green;
    Color gray;
    System.Random rand;
    GameObject[] skins; // this will hold all skins
    List<GameObject> lockedSkins; // this will hold locked skins
    public Button unlockSkinButton;
    public Sprite[] skinSprites = new Sprite[9];


    // Start is called before the first frame update
    void Start() {

        green = new Color(0.162f, 0.528f, 0.177f, 1f);
        gray = new Color(0.502f, 0.502f, 0.502f, 1f);

        rand = new System.Random();

        // find all skins (skin buttons)
        skins = GameObject.FindGameObjectsWithTag("SkinButton");
        // initialize the lockedSkins list
        lockedSkins = new List<GameObject>();

        // for each skin, if its SkinImage child is tagged "LockedSkin",
        // add it to the lockedSkins list
        foreach (GameObject skin in skins) {
            if(skin.transform.GetChild(1).tag == "LockedSkin") {
                lockedSkins.Add(skin);
            }
        }

        // add on-click-listener to the unlock skin button
        unlockSkinButton.onClick.AddListener(onClick);
    }

    void onClick() {
        // if there are any locked skins left
        if(lockedSkins.Count > 0) {
            // start a coroutine so that waiting does not freeze the app
            StartCoroutine(unlockSkin());
            // TODO? think of a solution so that the button is not spammed
            // if the game requires a currency to unlock skins, this won't be an issue
        } else {
            // TODO? prompt a notification that says all skins are unlocked
            // or remove the unlock skin button?
            Debug.Log("All skins are unlocked!");
        }
    }

    IEnumerator unlockSkin() {

        int chosen; // randomly chosen locked skin

        // if there are more than 1 lockedSkins
        if(lockedSkins.Count > 1) {
            // fake-choose 8 times, paint green and then gray
            for(int i = 0; i < 8; i++) {
                chosen = rand.Next(lockedSkins.Count);
                lockedSkins[chosen].GetComponent<Image>().color = green;
                // wait a little (increasingly more)
                yield return new WaitForSeconds(0.1f * i);
                lockedSkins[chosen].GetComponent<Image>().color = gray;
            }
        }

        // choose for real, paint green
        chosen = rand.Next(lockedSkins.Count);
        lockedSkins[chosen].GetComponent<Image>().color = green;

        // get the SkinImage child, show real sprite, tag it as "Untagged"
        Transform skinImage = lockedSkins[chosen].transform.GetChild(1);
        skinImage.GetComponent<Image>().sprite = skinSprites[(lockedSkins[chosen].name[4] - '1')];
        skinImage.tag = "Untagged";

        // remove chosen skin from the lockedSkins list
        lockedSkins.RemoveAt(chosen);
    }

}
