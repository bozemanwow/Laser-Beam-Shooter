using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemydeath : MonoBehaviour
{
    [SerializeField]
    [Range(0,30)]
    private float invokeTime;
    void Click()
    {
        gameObject.SetActive(false);
    }

    void OnDisable()
    {
        
        Invoke("BackOn", invokeTime);
    }
    void BackOn()
    {
        gameObject.SetActive(true);
    }

}
