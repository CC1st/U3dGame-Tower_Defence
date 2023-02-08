using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    public static Transform[] position;

    void Awake()
    {
        position = new Transform[transform.childCount];
        for(int i = 0; i < position.Length; i++)
        {
            position[i] = transform.GetChild(i);
        }
    }
}
