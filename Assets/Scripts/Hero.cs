using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    int _coin = 0;
    private void OnCollisionEnter(Collision collision)
    { 
        if(collision.gameObject.GetComponent<BlueCoin>() != null)
        {
            _coin += collision.gameObject.GetComponent<BlueCoin>().GetCoin();
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<BlueCoin>() != null)
        {
            _coin += other.gameObject.GetComponent<BlueCoin>().GetCoin();
        }
    }

}
