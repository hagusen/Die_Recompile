using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setCursor : MonoBehaviour{

    public Texture2D cursor;
    public int pixelsOfCursor;
    // Start is called before the first frame update
    void Start()
    {
        cursorSet(cursor);

    }

    void cursorSet(Texture2D cursor) {
        Cursor.SetCursor(cursor, new Vector2(pixelsOfCursor / 2, pixelsOfCursor / 2), CursorMode.ForceSoftware);
    }
}
