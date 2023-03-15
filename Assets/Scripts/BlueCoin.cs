using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueCoin : MonoBehaviour
{
    [SerializeField] GameObject _parent;
    public int GetCoin()
    {
        Destroy(_parent);
        return 5;
    }
}
