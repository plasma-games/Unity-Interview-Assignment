using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireRandomizer : MonoBehaviour
{
    //scalable randomizer, will switch positions of all children in case we want to add a 5th wire, or use 3 wires, etc
    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            int newPos = Random.Range(0, transform.childCount);
            Vector3 temp = transform.GetChild(i).position;
            transform.GetChild(i).position = transform.GetChild(newPos).position;
            transform.GetChild(newPos).position = temp;
        }
    }
}
