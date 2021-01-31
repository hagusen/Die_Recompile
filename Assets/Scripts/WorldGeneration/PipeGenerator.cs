using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeGenerator : MonoBehaviour {

    private Vector2 position;
    public int pipeMinLength, pipeMaxLength;


    public int turnPercentage;
    private int pipeLength;

    private Vector2 dir;

    private bool inWall;
    private RaycastHit hit;
    public LayerMask layer;

    public float height;


    public GameObject bunkerPipeStraight, bunkerPipeTurn, bunkerPipeEnd, bunkerPipeSegment;
    public GameObject sewerPipeStraight, sewerPipeTurn, sewerPipeEnd, sewerPipeSegment;
    public GameObject facilityPipeStraight, facilityPipeTurn, facilityPipeEnd, facilityPipeSegment;


    private GameObject tmpPipeStraight, tmpPipeTurn, tmpPipeEnd, tmpPipeSegment;
    public float segmentPercentage;
    private bool doTurn;
    private Vector2 tmpDir;
    void Awake() {
        hit = new RaycastHit();
        layer = LayerMask.GetMask("WallTiles");
    }
    public void PipeStart(float x, float z, int zone) {


        switch (zone) {
            case 1:
            tmpPipeStraight = bunkerPipeStraight;
            tmpPipeTurn = bunkerPipeTurn;
            tmpPipeEnd = bunkerPipeEnd;
            tmpPipeSegment = bunkerPipeSegment;
            break;

            case 2:
            tmpPipeStraight = sewerPipeStraight;
            tmpPipeTurn = sewerPipeTurn;
            tmpPipeEnd = sewerPipeEnd;
            tmpPipeSegment = sewerPipeSegment;
            break;

            case 3:
            tmpPipeStraight = facilityPipeStraight;
            tmpPipeTurn = facilityPipeTurn;
            tmpPipeEnd = facilityPipeEnd;
            tmpPipeSegment = facilityPipeSegment;
            break;
        }

        position = new Vector2(x, z);
        pipeLength = Random.Range(pipeMinLength, pipeMaxLength);

        switch (Random.Range(0, 4)) {
            case 0:
            dir = Vector2.right;
            break;

            case 1:
            dir = Vector2.up;
            break;

            case 2:
            dir = Vector2.left;
            break;

            case 3:
            dir = Vector2.down;
            break;
        }



        CheckWall(position);
        //Pipe start outside loop compensate for exclusive random.
        if (!inWall) {
            if(dir.x != 0) {
                Instantiate(tmpPipeEnd, new Vector3(position.x, Random.Range(0.0500f, 0.0510f) + height, position.y), Quaternion.Euler(new Vector3(0, dir.x * 90 + 180, 0)));
            } else {
                Instantiate(tmpPipeEnd, new Vector3(position.x, Random.Range(0.0500f, 0.0510f) + height, position.y), Quaternion.Euler(new Vector3(0, dir.y * 90 + 90, 0)));
            }

        }

       // int segmentDif = 0;
        for (int i = 0; i < pipeLength; i++) {
            position += dir;
           // segmentDif++;
            CheckWall(position);
            if(pipeLength -1 != i) {

                if (Random.Range(0, 100) <= turnPercentage) {

                    tmpDir = dir;
                    if (Random.Range(0, 2) == 0) {
                        if (dir == Vector2.up || dir == Vector2.down) {
                            dir = Vector2.left;
                        }
                        else {
                            dir = Vector2.up;
                        }
                    }
                    else {
                        if (dir == Vector2.up || dir == Vector2.down) {
                            dir = Vector2.right;
                        }
                        else {
                            dir = Vector2.down;
                        }
                    }

                    if (!inWall) {
                        if ((dir.x < 0 || dir.y < 0)) {
                            if (tmpDir.x < 0 || tmpDir.y < 0) {
                                Instantiate(tmpPipeTurn, new Vector3(position.x, Random.Range(0.0500f, 0.0510f) + height, position.y), Quaternion.Euler(new Vector3(0, -tmpDir.x * 90 + dir.x * 90, 0)));
                                if(Random.Range(0, 100) < segmentPercentage) {
                                    Instantiate(tmpPipeSegment, new Vector3(position.x, Random.Range(0.0500f, 0.0510f) + height, position.y), Quaternion.Euler(new Vector3(0, -tmpDir.x * 90 + dir.x * 90, 0)));
                                }
                            } else {
                                Instantiate(tmpPipeTurn, new Vector3(position.x, Random.Range(0.0500f, 0.0510f) + height, position.y), Quaternion.Euler(new Vector3(0, 180, 0)));
                                if (Random.Range(0, 100) < segmentPercentage) {
                                    Instantiate(tmpPipeSegment, new Vector3(position.x, Random.Range(0.0500f, 0.0510f) + height, position.y), Quaternion.Euler(new Vector3(0, 180, 0)));
                                }
                            }
                        } else {
                            if (tmpDir.x < 0 || tmpDir.y < 0) {
                                Instantiate(tmpPipeTurn, new Vector3(position.x, Random.Range(0.0500f, 0.0510f) + height, position.y), Quaternion.identity);
                                if (Random.Range(0, 100) < segmentPercentage) {
                                    Instantiate(tmpPipeSegment, new Vector3(position.x, Random.Range(0.0500f, 0.0510f) + height, position.y), Quaternion.identity);
                                }

                            } else {
                                Instantiate(tmpPipeTurn, new Vector3(position.x, Random.Range(0.0500f, 0.0510f) + height, position.y), Quaternion.Euler(new Vector3(0, tmpDir.y * 90 + -dir.y * 90, 0)));
                                if (Random.Range(0, 100) < segmentPercentage) {
                                    Instantiate(tmpPipeSegment, new Vector3(position.x, Random.Range(0.0500f, 0.0510f) + height, position.y), Quaternion.Euler(new Vector3(0, tmpDir.y * 90 + -dir.y * 90, 0)));
                                }
                            }
                        }
                    }
                }
                else {
                    if (!inWall) {
                        if (dir.y != 0) {
                            Instantiate(tmpPipeStraight, new Vector3(position.x, Random.Range(0.0500f, 0.0510f) + height, position.y), Quaternion.identity);
                            if (Random.Range(0, 100) < segmentPercentage) {
                                Instantiate(tmpPipeSegment, new Vector3(position.x, Random.Range(0.0500f, 0.0510f) + height, position.y), Quaternion.identity);
                            }
                        }
                        else {
                            Instantiate(tmpPipeStraight, new Vector3(position.x, Random.Range(0.0500f, 0.0510f) + height, position.y), Quaternion.Euler(new Vector3(0, 90, 0)));
                            if (Random.Range(0, 100) < segmentPercentage) {
                                Instantiate(tmpPipeSegment, new Vector3(position.x, Random.Range(0.0500f, 0.0510f) + height, position.y), Quaternion.Euler(new Vector3(0, 90, 0)));
                            }
                        }
                    }
                }
            }else {
                if (!inWall) {
                    if(pipeLength -1 == i) {
                        if (dir.x != 0) {
                            Instantiate(tmpPipeEnd, new Vector3(position.x, Random.Range(0.0500f, 0.0510f) + height, position.y), Quaternion.Euler(new Vector3(0, dir.x * 90, 0)));
                        }
                        else {
                            Instantiate(tmpPipeEnd, new Vector3(position.x, Random.Range(0.0500f, 0.0510f) + height, position.y), Quaternion.Euler(new Vector3(0, dir.y * 90 + 90 + 180, 0)));
                        }
                    }
                }
            }

        }




    }
    private void CheckWall(Vector2 position) {
        if (Physics.Raycast(new Vector3(position.x, 11, position.y), Vector3.down, out hit, 10.0f, layer, QueryTriggerInteraction.Ignore)) {
            inWall = true;
        }
        else {
            inWall = false;
        }
    }
}
