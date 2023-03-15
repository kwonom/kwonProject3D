using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collision Gameobject" + other.gameObject.name);
        //Debug.Log("collider " + other.name);

        if(other.GetComponent<Monster>() != null)
        {
            other.GetComponent<Monster>().hitted();
        }
    }
}
