using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPool<T> : MonoBehaviour where T : Component
{

    public T[] prefab = new T[1];
    [SerializeField]
    private int[] amountToPreHeat;

    public static ObjectPool<T> ins { get; private set; }

    private Queue<T>[] objects;

    public T[] testarr;
    public int test = 0;

    void Awake() {
        if (!ins) {
            ins = this;
        }
        else {
            Debug.LogWarning("More than one singleton of this type " + Faces.GetFace(faceType.Mad, 2));
        }

        OnAwake();

    }

    public void OnAwake() {
        objects = new Queue<T>[prefab.Length];

        for (int i = 0; i < objects.Length; i++) {
            objects[i] = new Queue<T>();
        }

        for (int i = 0; i < amountToPreHeat.Length; i++) {
            AddObjects(amountToPreHeat[i], i);
        }
    }


    public T Get(int type) {

        if (objects[type].Count == 0) {
            var temp = Instantiate(prefab[type], gameObject.transform);
            temp.gameObject.SetActive(false);
            return temp;
        }

        return objects[type].Dequeue();
    }


    public void ReturnToPool(T objectToReturn, int type) {
        if (!objects[type].Contains(objectToReturn)) {
            objectToReturn.gameObject.SetActive(false);
            objects[type].Enqueue(objectToReturn);
        }
        else {
            //Debug.LogError(12);
        }
    }

    private void AddObjects(int count, int type) {
        for (int i = 0; i < count; i++) {
            var obj = Instantiate(prefab[type], gameObject.transform);
            obj.gameObject.SetActive(false);
            objects[type].Enqueue(obj);
            //
            test++;
        }

    }

    void Update() {

        //testarr = new T[objects[3].Count];
        //testarr = objects[3].ToArray();




    }





}
