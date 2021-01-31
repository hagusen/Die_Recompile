///CREATED BY "Oscar Nordström"
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEditor;
using UnityEngine.UI;

public class WorldGeneration : MonoBehaviour {

    public GameObject fungus;

    public ImageReadingScript imageScript;
    [SerializeField]
    private int seed;

    [SerializeField]
    private bool DebugColors;

    [SerializeField]
    private bool randomSeed;
    private Texture2D room;

    [SerializeField]
    public int columnsOfRooms, rowsOfRooms;

    [SerializeField]
    public int tilesPerRow, tilesPerColumn;

    [SerializeField]
    private GameObject bunkerCommonWall, bunkerRareWall, bunkerCommonFloor, bunkerRareFloor, invWallBunker, bunkerBigObject;
    [SerializeField]
    private GameObject sewerCommonWall, sewerRareWall, sewerCommonFloor, sewerRareFloor, sewerBigObject, invWallSewer;
    [SerializeField]
    private GameObject facilityCommonWall, facilityRareWall, facilityCommonFloor, facilityRareFloor, facilityBigObject;

    public GameObject manager, world, elevatorExitPrefab;
    public int selected = 0;
    public enum Zone1 { Bunker, Sewer, Facility }
    public Zone1 zone1;
    public enum Zone2 { Bunker, Sewer, Facility }
    public Zone2 zone2;
    public enum Zone3 { Bunker, Sewer, Facility }
    public Zone3 zone3;
    public enum Zone4 { Bunker, Sewer, Facility }
    public Zone4 zone4;
    public enum Zone5 { Bunker, Sewer, Facility }
    public Zone5 zone5;
    public enum Zone6 { Bunker, Sewer, Facility }
    public Zone6 zone6;
    public enum Zone7 { Bunker, Sewer, Facility }
    public Zone7 zone7;
    public enum Zone8 { Bunker, Sewer, Facility }
    public Zone8 zone8;
    public enum Zone9 { Bunker, Sewer, Facility }
    public Zone9 zone9;




    public float wallHeight;

    [SerializeField]
    public int tileSize;

    private bool wallE, wallN, wallW, wallS;

    private bool solidWallE, solidWallN, solidWallW, solidWallS;

    [SerializeField]
    private int startToExitOffset;

    private List<int> ints;

    public int sceneIndex;

    public int floorXOffset;
    public int floors;

    public List<GameObject> FloorBakers;


    public int rareFloorPercentage;
    public int rareWallPercentage;
    [Range(0, 1)]
    public float dustPercentage;

    private Vector2 position;


    public bool safeForEditor;

    public List<Vector3>[] WPHs;

    public float pipeSpawnPercentage;
    public GameObject wallLight, floorLight;
    private Vector3 tempPos;
    private GameObject tempObject;
    private GameObject tempObjectCommon;
    private GameObject tempObjectRare;
    private GameObject tempBigObject;


    private GameLoader loader;


    private bool elevatorEntrance;
    void Start()
    {
        if(SceneManager.sceneCount == 1)
        {
            StartCoroutine(DoStart(null));
        }
    }

    public IEnumerator DoStart(GameLoader inData)
    {
        if (inData != null)
        {
            loader = inData;
            loader.SetRoomCount((rowsOfRooms + 2) * (columnsOfRooms + 2) * floors);
        }

        WPHs = new List<Vector3>[floors];
        for (int i = 0; i < WPHs.Length; i++)
        {
            WPHs[i] = new List<Vector3>();
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(sceneIndex));
        Debug.Log("Active Scene : " + SceneManager.GetActiveScene().name);

        if (randomSeed)
        {
            seed = System.DateTime.Now.Millisecond;
        }
        UnityEngine.Random.InitState(seed);

        if (Application.isEditor && safeForEditor)
        {
            floors = 1;
        }


        for (int i = 0; i < floors; i++)
        {
            try
            {
                FloorBakers[i].SetActive(true);

            }
            catch (ArgumentOutOfRangeException e)
            {
                Debug.LogWarning("We need some more Bakers");
            }

            yield return GenerateWorld(i);
        }
        if (WPHSpawner.ins)
        { // Don't put me in the folr loop pls >.<
            WPHSpawner.ins.Begin(WPHs);
        }


        Manager.Instance.gameObject.GetComponent<EnemySpawner>().DoStart(safeForEditor);
        Time.timeScale = 1;
        fungus.SetActive(true);

    }

   IEnumerator GenerateWorld(int FloorIndex)
    {
        yield return GenerateWorldFloor(FloorIndex);
        elevatorEntrance = false;
        int roomPos = UnityEngine.Random.Range(1, (rowsOfRooms * 2 + columnsOfRooms * 2));
        int roomExitPos = UnityEngine.Random.Range(roomPos + startToExitOffset, (rowsOfRooms * 2 + columnsOfRooms * 2) - startToExitOffset);
        roomExitPos = roomExitPos % (rowsOfRooms * 2 + columnsOfRooms * 2);


        PositionConvertion();
        Debug.Log("Room Pos: " + roomPos + "   Room ExitPos: " + roomExitPos);

        //    roomPos = ints[roomPos - 1];
        //    roomExitPos = ints[roomExitPos - 1];

        
        roomPos = 0;
        roomExitPos = ints.Count - 2;


        Debug.Log("Room Pos: " + roomPos + "   Room ExitPos: " + roomExitPos);


        for (int i = 0; i < columnsOfRooms + 2; i++) {
            for (int j = 0; j < rowsOfRooms + 2; j++) {
                if (i == 0 || i == columnsOfRooms + 1 || j == 0 || j == rowsOfRooms + 1) {
                    if (!(i == 0 && (j == 0 || j == rowsOfRooms + 1) || i == columnsOfRooms + 1 && (j == 0 || j == rowsOfRooms + 1))) {
                        if (roomPos == 0) {
                            room = GetStart();
                            roomPos = -1;
                        } else {
                            if (roomExitPos == 0) {
                                room = GetExit();
                                roomExitPos = -1;
                                roomPos--;
                            } else {
                                room = GetSolid();
                                roomPos--;
                                roomExitPos--;
                            }
                        }
                    } else {
                        room = GetSolid();
                    }

                } else {
                    room = null;
                }
                yield return GenerateRoom(i, j, room, FloorIndex);

                if (loader != null)
                    loader.IncreaseCount();
            }
        }
        FloorBakers[FloorIndex].SetActive(false);
        Debug.LogWarning("Finished floor: " + FloorIndex);
        yield return true;
    }

    private void PositionConvertion() {
        ints = new List<int>();
        for (int i = 1; i < columnsOfRooms + 1; i++) {
            ints.Add(i);
        }
        for (int i = 1; i < rowsOfRooms + 1; i++) {
            ints.Add(columnsOfRooms + 2 * i);
        }
        for (int i = 0; i < columnsOfRooms; i++) {
            ints.Add((columnsOfRooms * 2 + rowsOfRooms * 2) - i);
        }
        for (int i = 0; i < rowsOfRooms; i++) {
            ints.Add(ints[columnsOfRooms + rowsOfRooms - i - 1] - 1);
        }


    }
    // don't put new stuff in bigg loops!! they create garbage
    IEnumerator GenerateWorldFloor(int index)
    {
        int zone = 0;
        switch (index) {
            case 0:
            if ((int)zone1 == 0) {
                zone = 1;
            } else if ((int)zone1 == 1) {
                zone = 2;
            } else if ((int)zone1 == 2) {
                zone = 3;
            }
            break;

            case 1:
            if ((int)zone2 == 0) {
                zone = 1;
            } else if ((int)zone2 == 1) {
                zone = 2;
            } else if ((int)zone2 == 2) {
                zone = 3;
            }
            break;

            case 2:
            if ((int)zone3 == 0) {
                zone = 1;
            } else if ((int)zone3 == 1) {
                zone = 2;
            } else if ((int)zone3 == 2) {
                zone = 3;
            }
            break;

            case 3:
            if ((int)zone4 == 0) {
                zone = 1;
            } else if ((int)zone4 == 1) {
                zone = 2;
            } else if ((int)zone4 == 2) {
                zone = 3;
            }
            break;

            case 4:
            if ((int)zone5 == 0) {
                zone = 1;
            } else if ((int)zone5 == 1) {
                zone = 2;
            } else if ((int)zone5 == 2) {
                zone = 3;
            }
            break;

            case 5:
            if ((int)zone6 == 0) {
                zone = 1;
            } else if ((int)zone6 == 1) {
                zone = 2;
            } else if ((int)zone6 == 2) {
                zone = 3;
            }
            break;

            case 6:
            if ((int)zone7 == 0) {
                zone = 1;
            } else if ((int)zone7 == 1) {
                zone = 2;
            } else if ((int)zone7 == 2) {
                zone = 3;
            }
            break;

            case 7:
            if ((int)zone8 == 0) {
                zone = 1;
            } else if ((int)zone8 == 1) {
                zone = 2;
            } else if ((int)zone8 == 2) {
                zone = 3;
            }
            break;

            case 8:
            if ((int)zone9 == 0) {
                zone = 1;
            } else if ((int)zone9 == 1) {
                zone = 2;
            } else if ((int)zone9 == 2) {
                zone = 3;
            }
            break;

        }
        Debug.Log("Zone: " + zone);
        switch (zone) {
            case 0:
            Debug.LogError("Zone Not Selected");
            break;

            case 1:
            tempObjectCommon = bunkerCommonFloor;
            tempObjectRare = bunkerRareFloor;
            break;

            case 2:
            tempObjectCommon = sewerCommonFloor;
            tempObjectRare = sewerRareFloor;
            break;

            case 3:
            tempObjectCommon = facilityCommonFloor;
            tempObjectRare = facilityRareFloor;
            break;
        }

        Vector3 tilePos;
        Quaternion tileRot;
        Transform tileTransform;

        List<MeshFilter> commonFilters;

        for (int i = 0; i < ((tilesPerRow) * (rowsOfRooms + 2) / 2); i++) {
            for (int j = 0; j < ((tilesPerColumn) * (columnsOfRooms + 2) / 2); j++) {
                tilePos.x = (-tileSize + tileSize * i * 2) + 0.5f + index * floorXOffset;
                tilePos.y = 0f;
                tilePos.z = (-tileSize + tileSize * j * 2) + 0.5f;
                int rot = UnityEngine.Random.Range(0, 6);
                int rotx = 0;
                int rotz = 0;
                switch (rot) {
                    case 0:
                    rotx = 0;
                    rotz = 0;
                    break;

                    case 1:
                    rotx = 1;
                    rotz = 0;
                    break;

                    case 2:
                    rotx = 2;
                    rotz = 0;
                    break;

                    case 3:
                    rotx = 3;
                    rotz = 0;
                    break;

                    case 4:
                    rotx = 0;
                    rotz = 1;
                    break;

                    case 5:
                    rotx = 0;
                    rotz = 3;
                    break;
                }

                tileRot = Quaternion.Euler(rotx * 90, UnityEngine.Random.Range(0, 4) * 90, rotz * 90);

                if (UnityEngine.Random.Range(0, 100) > rareFloorPercentage) {

                   // commonFilters.Add(tempObject.GetComponent<MeshFilter>.)



                    tempObject = Instantiate(tempObjectCommon, tilePos, tileRot);
                    //tempObject.isStatic = true;
                    
                    

                } else {
                    tempObject = Instantiate(tempObjectRare, tilePos, tileRot);
                    //tempObject.isStatic = true;
                    WPHs[index].Add(tilePos);
                }
            }
        }

        //tempObject = Instantiate(tempObjectCommon, new Vector3(2 * tilePos.x + 0.5f + index * floorXOffset, tilePos.y, 2 * tilePos.z + 0.5f), Quaternion.Euler(rotx * 90, UnityEngine.Random.Range(0, 4) * 90, rotz * 90));

        yield return true;
    }

    Effect temp;
    void SpawnDust(Vector3 pos) {
        if (EffectPool.ins) {
            temp = EffectPool.ins.Get((int)EffectType.Dust);
            temp.SetValues(pos);
            temp.gameObject.SetActive(true);
        }
    }


    private Texture2D GetStart() {
        return imageScript.ReadStartRoom();
    }

    private Texture2D GetSolid() {
        return imageScript.ReadSolidImage();
    }

    private Texture2D GetRandomBunkerRoom() {
        imageScript.GetRandomBunker();
        return imageScript.getImage();
    }
    private Texture2D GetRandomSewerRoom() {
        imageScript.GetRandomSewer();
        return imageScript.getImage();
    }
    private Texture2D GetRandomFacilityRoom() {
        imageScript.GetRandomFacility();
        return imageScript.getImage();
    }
    private Texture2D GetExit() {
        return imageScript.GetExit();
    }

    IEnumerator GenerateRoom(int i, int j, Texture2D room, int floorIndex) {
        solidWallE = false;
        solidWallN = false;
        solidWallW = false;
        solidWallS = false;
        if (i == 0 || i == columnsOfRooms + 1 || j == 0 || j == rowsOfRooms + 1)


            if (i == 0) {
                solidWallS = true;
            }
        if (i == columnsOfRooms + 1) {
            solidWallN = true;
        }
        if (j == 0) {
            solidWallW = true;
        }
        if (j == rowsOfRooms + 1) {
            solidWallE = true;
        }

        int zone = 0;
        switch (floorIndex) {
            case 0:
            if ((int)zone1 == 0) {
                zone = 1;
            } else if ((int)zone1 == 1) {
                zone = 2;
            } else if ((int)zone1 == 2) {
                zone = 3;
            }
            break;

            case 1:
            if ((int)zone2 == 0) {
                zone = 1;
            } else if ((int)zone2 == 1) {
                zone = 2;
            } else if ((int)zone2 == 2) {
                zone = 3;
            }
            break;

            case 2:
            if ((int)zone3 == 0) {
                zone = 1;
            } else if ((int)zone3 == 1) {
                zone = 2;
            } else if ((int)zone3 == 2) {
                zone = 3;
            }
            break;

            case 3:
            if ((int)zone4 == 0) {
                zone = 1;
            } else if ((int)zone4 == 1) {
                zone = 2;
            } else if ((int)zone4 == 2) {
                zone = 3;
            }
            break;

            case 4:
            if ((int)zone5 == 0) {
                zone = 1;
            } else if ((int)zone5 == 1) {
                zone = 2;
            } else if ((int)zone5 == 2) {
                zone = 3;
            }
            break;

            case 5:
            if ((int)zone6 == 0) {
                zone = 1;
            } else if ((int)zone6 == 1) {
                zone = 2;
            } else if ((int)zone6 == 2) {
                zone = 3;
            }
            break;

            case 6:
            if ((int)zone7 == 0) {
                zone = 1;
            } else if ((int)zone7 == 1) {
                zone = 2;
            } else if ((int)zone7 == 2) {
                zone = 3;
            }
            break;

            case 7:
            if ((int)zone8 == 0) {
                zone = 1;
            } else if ((int)zone8 == 1) {
                zone = 2;
            } else if ((int)zone8 == 2) {
                zone = 3;
            }
            break;

            case 8:
            if ((int)zone9 == 0) {
                zone = 1;
            } else if ((int)zone9 == 1) {
                zone = 2;
            } else if ((int)zone9 == 2) {
                zone = 3;
            }
            break;

        }

        switch (zone) {
            case 0:
            Debug.LogError("Zone Not Selected Floor index: " + floorIndex);
            break;

            case 1:
            tempObjectCommon = bunkerCommonWall;
            tempObjectRare = bunkerRareWall;
            tempBigObject = bunkerBigObject;
            if (room == null) {
                room = GetRandomBunkerRoom();
            }
            break;

            case 2:
            tempObjectCommon = sewerCommonWall;
            tempObjectRare = sewerRareWall;
            tempBigObject = sewerBigObject;
            if (room == null) {
                room = GetRandomSewerRoom();
            }
            break;

            case 3:
            tempObjectCommon = facilityCommonWall;
            tempObjectRare = facilityRareWall;
            tempBigObject = facilityBigObject;
            if (room == null) {
                room = GetRandomFacilityRoom();
            }
            break;
        }

        Color[] pixels = room.GetPixels();
        int p = 0;
        if (DebugColors) {
            DoLog(pixels);
        }

        for (int k = 0; k < tilesPerRow; k++) {

            for (int l = 0; l < tilesPerColumn; l++, p++) {
                tempPos.x = l * tileSize + j * tilesPerRow * tileSize + floorIndex * floorXOffset;
                tempPos.y = wallHeight / 2 + 1;
                tempPos.z = k * tileSize + i * tilesPerColumn * tileSize;
                if (pixels[p].Equals(Color.black)) {
                    if (UnityEngine.Random.Range(0, 100) > rareFloorPercentage) {
                        tempObject = Instantiate(tempObjectCommon, tempPos, Quaternion.Euler(0, UnityEngine.Random.Range(0, 4) * 90, 0));
                        tempObject.transform.SetParent(FloorBakers[floorIndex].transform, true);
                        tempObject.isStatic = true;
                    } else {
                        tempObject = Instantiate(tempObjectRare, tempPos, Quaternion.Euler(0, UnityEngine.Random.Range(0, 4) * 90, 0));
                        tempObject.transform.SetParent(FloorBakers[floorIndex].transform, true);
                        tempObject.isStatic = true;
                    }

                } else {
                    if (pixels[p].Equals(Color.blue)) {
                        Debug.Log("Exit Spawned (Blue Pixel)");
                        tempObject = Instantiate(elevatorExitPrefab, new Vector3(tempPos.x, tempPos.y - 0.5f, tempPos.z), Quaternion.Euler(0, 180, 0));
                        tempObject.GetComponent<ElevatorScript>().floorIndex = floorIndex;
                        tempObject.GetComponent<ElevatorScript>().floorDestination = floorIndex + 1;
                        if (elevatorEntrance) {
                            tempObject.GetComponent<ElevatorScript>().isEntrance = true;
                            elevatorEntrance = false;
                        } else {
                            tempObject.GetComponent<ElevatorScript>().isEntrance = false;
                            elevatorEntrance = true;
                        }
                        tempObject.GetComponent<ElevatorScript>().world = this.GetComponent<WorldGeneration>();
                        tempObject.GetComponent<ElevatorScript>().spawner = manager.GetComponent<EnemySpawner>();
                    } else {
                        if (pixels[p].Equals(Color.green)) {
                            tempObject = Instantiate(tempBigObject, tempPos, Quaternion.identity);
                        } else {
                            if (pixels[p].Equals(Color.white)) {
                                if (UnityEngine.Random.Range(0f, 100f) <= pipeSpawnPercentage) {
                                    this.gameObject.GetComponent<PipeGenerator>().PipeStart(tempPos.x, tempPos.z, zone);
                                }
                            } else {
                                if (pixels[p].Equals(Color.red)) {
                                    tempObject = Instantiate(tempObjectCommon, tempPos, Quaternion.Euler(0, UnityEngine.Random.Range(0, 4) * 90, 0));
                                    tempObject.transform.SetParent(FloorBakers[floorIndex].transform, true);
                                    tempObject.isStatic = true;
                                    tempObject = Instantiate(wallLight, new Vector3(tempPos.x, 10, tempPos.z), Quaternion.identity);
                                } else {
                                    if (pixels[p].Equals(Color.cyan)) {
                                        tempObject = Instantiate(floorLight, new Vector3(tempPos.x, 10, tempPos.z), Quaternion.identity);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }


        for (int k = 0; k < tilesPerColumn; k++) {

            tempPos = new Vector3(-tileSize + j * tilesPerRow * tileSize + floorIndex * floorXOffset, wallHeight / 2 + 1, k * tileSize + i * tilesPerColumn * tileSize);
            if (solidWallW) {
                tempObject = Instantiate(invWallBunker, tempPos, Quaternion.identity);
                tempObject.transform.SetParent(FloorBakers[floorIndex].transform, true);
                tempObject.isStatic = true;
            }



        }

        for (int k = 0; k < tilesPerColumn; k++) {

            tempPos = new Vector3((tilesPerRow) * tileSize + j * tilesPerRow * tileSize + floorIndex * floorXOffset, wallHeight / 2 + 1, k * tileSize + i * tilesPerColumn * tileSize);
            if (solidWallE) {
                tempObject = Instantiate(invWallBunker, tempPos, Quaternion.identity);
                tempObject.transform.SetParent(FloorBakers[floorIndex].transform, true);
                tempObject.isStatic = true;
            }


        }



        for (int k = 0; k < tilesPerRow + 2; k++) {

            tempPos = new Vector3(-tileSize + j * tilesPerRow * tileSize + k * tileSize + floorIndex * floorXOffset, wallHeight / 2 + 1, -tileSize + i * tilesPerColumn * tileSize);
            if (solidWallS) {
                tempObject = Instantiate(invWallBunker, tempPos, Quaternion.identity);
                tempObject.isStatic = true;
            } else {
                if ((solidWallW && k == 0) || solidWallE && k == (tilesPerRow + 1)) {
                    tempObject = Instantiate(invWallBunker, tempPos, Quaternion.identity);
                    tempObject.isStatic = true;
                }
            }



        }
        for (int k = 0; k < tilesPerRow + 2; k++) {

            tempPos = new Vector3(-tileSize + j * (tilesPerRow) * tileSize + k * tileSize + floorIndex * floorXOffset, wallHeight / 2 + 1, tilesPerColumn * tileSize + i * tilesPerColumn * tileSize);
            if (solidWallN) {
                tempObject = Instantiate(invWallBunker, tempPos, Quaternion.identity);
                tempObject.isStatic = true;
            } else {
                if ((solidWallW && k == 0) || solidWallE && k == (tilesPerRow + 1)) {
                    tempObject = Instantiate(invWallBunker, tempPos, Quaternion.identity);
                    tempObject.isStatic = true;
                }

            }

        }

        yield return true;
    }

    public string GetFloorType(int inData)
    {
        switch (inData)
        {
            case 0:
                return zone1.ToString();

            case 1:
                return zone2.ToString();

            case 2:
                return zone3.ToString();

            case 3:
                return zone4.ToString();

            case 4:
                return zone5.ToString();

            case 5:
                return zone6.ToString();

            case 6:
                return zone7.ToString();

            case 7:
                return zone8.ToString();

            case 8:
                return zone9.ToString();

        }
        return null;
    }

    public void DoLog(Color[] pixels) {
        int black = 0;
        int blue = 0;
        int clear = 0;
        int cyan = 0;
        int gray = 0;
        int green = 0;
        int grey = 0;
        int magenta = 0;
        int red = 0;
        int white = 0;
        int yellow = 0;

        foreach (Color c in pixels) {
            if (c.Equals(Color.black)) {
                black++;
            } else if (c.Equals(Color.blue)) {
                blue++;
            } else if (c.Equals(Color.clear)) {
                clear++;
            } else if (c.Equals(Color.cyan)) {
                cyan++;
            } else if (c.Equals(Color.gray)) {
                gray++;
            } else if (c.Equals(Color.green)) {
                green++;
            } else if (c.Equals(Color.grey)) {
                grey++;
            } else if (c.Equals(Color.magenta)) {
                magenta++;
            } else if (c.Equals(Color.red)) {
                red++;
            } else if (c.Equals(Color.white)) {
                white++;
            } else if (c.Equals(Color.yellow)) {
                yellow++;
            }
        }
        Debug.Log("Black: " + black);
        Debug.Log("Blue: " + blue);
        Debug.Log("Cyan: " + cyan);
        Debug.Log("Gray: " + gray);
        Debug.Log("Green: " + green);
        Debug.Log("Grey: " + grey);
        Debug.Log("Magenta: " + magenta);
        Debug.Log("Red: " + red);
        Debug.Log("White: " + white);
        Debug.Log("Yellow: " + yellow);
        if (clear > 0) {
            Debug.LogError("Clear: " + clear);
        }
        Debug.Log(pixels.Length);
    }
}

