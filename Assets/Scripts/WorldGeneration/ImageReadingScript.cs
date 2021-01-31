///CREATED BY "Oscar Nordström"
///USED FOR "Generating a world"

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageReadingScript : MonoBehaviour {

    public Texture2D[] bunkerBlueprints;
    public Texture2D[] sewerBlueprints;
    public Texture2D[] facilityBlueprints;
    public Texture2D solidWall;
    public Texture2D startRoom;
    public Texture2D exitRoom;
    private Texture2D blueprint;
    private int randomNumber;
    private bool ExitRead = true;


    public void GetRandomBunker() {
        randomNumber = Random.Range(0, bunkerBlueprints.Length);
        blueprint = bunkerBlueprints[randomNumber];
        randomNumber = Random.Range(0, 3);
        for (int i = 0; i < randomNumber; i++) {
            blueprint = RotateTexture(blueprint, true);
        }
    }
    public void GetRandomSewer() {
        randomNumber = Random.Range(0, sewerBlueprints.Length);
        blueprint = sewerBlueprints[randomNumber];
        randomNumber = Random.Range(0, 3);
        for (int i = 0; i < randomNumber; i++) {
            blueprint = RotateTexture(blueprint, true);
        }
    }
    public void GetRandomFacility() {
        randomNumber = Random.Range(0, facilityBlueprints.Length);
        blueprint = facilityBlueprints[randomNumber];
        randomNumber = Random.Range(0, 3);
        for (int i = 0; i < randomNumber; i++) {
            blueprint = RotateTexture(blueprint, true);
        }
    }

    Texture2D RotateTexture(Texture2D originalTexture, bool clockwise) {
        Color32[] original = originalTexture.GetPixels32();
        Color32[] rotated = new Color32[original.Length];
        int w = originalTexture.width;
        int h = originalTexture.height;

        int iRotated, iOriginal;

        for (int j = 0; j < h; ++j) {
            for (int i = 0; i < w; ++i) {
                iRotated = (i + 1) * h - j - 1;
                iOriginal = clockwise ? original.Length - 1 - (j * w + i) : j * w + i;
                rotated[iRotated] = original[iOriginal];
            }
        }

        Texture2D rotatedTexture = new Texture2D(h, w);
        rotatedTexture.SetPixels32(rotated);
        rotatedTexture.Apply();
        return rotatedTexture;
    }

    public Texture2D GetEntrance() {
        return null;
    }
    public Texture2D ReadSolidImage() {
        return solidWall;
    }
    public Texture2D ReadStartRoom() {
        return startRoom;
    }

    public Texture2D ReadExitRoom() {
        exitRoom = RotateTexture(exitRoom, true);
        exitRoom = RotateTexture(exitRoom, true);
        return exitRoom;
    }
    public Texture2D GetExit() {
        if (ExitRead) {
            ExitRead = false;
            return ReadExitRoom();
        } else {
            return exitRoom;
        }
    }
    public Texture2D getImage() {
        return blueprint;
    }
}
