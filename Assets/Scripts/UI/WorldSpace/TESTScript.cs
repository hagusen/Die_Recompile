using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTScript : MonoBehaviour {

    public int rows;
    public int columns;

    public int startToExitOffset;


    private List<int> ints;

    // Start is called before the first frame update
    void Start() {
        Debug.Log("Rows: " + rows + "  Columns: " + columns);

        int roomPos = Random.Range(0, (columns * 2 + rows * 2) - 1);
        int roomExitPos = Random.Range(roomPos, (columns * 2 + rows * 2) - startToExitOffset);
        roomExitPos = (roomPos + roomExitPos) % (columns * 2 + rows * 2);

        Lists();
        Debug.Log("Room Position: " + roomPos + "  Exit Room Position: " + roomExitPos);
        roomPos = ints[roomPos - 1];
        roomExitPos = ints[roomExitPos - 1];
        Debug.Log("Room Position: " + roomPos + "  Exit Room Position: " + roomExitPos);
    }



    private void Lists() {
        ints = new List<int>();
        for(int i = 1; i < rows + 1; i++) {
            ints.Add(i);
            
        }
        for(int i = 1; i < columns + 1; i++) {
            ints.Add(rows + 2 * i);
        }
        for (int i = 0; i < rows; i++) {
            ints.Add((rows * 2 + columns * 2) - i);
        }
        for (int i = 0; i < columns; i++) {
            ints.Add(ints[rows + columns - i - 1] - 1);
        }
        foreach(int i in ints) {
            Debug.Log(i);
        }

    }

    // Update is called once per frame
    void Update() {

    }
}
