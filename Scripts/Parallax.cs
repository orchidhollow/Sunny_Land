using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform Cam;
    public float moveRate;
    private float startPointx,startPointy;
    public bool lockY=false;
    // Start is called before the first frame update
    void Start()
    {
        startPointx=transform.position.x;
        startPointy=transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(lockY)
        {
            transform.position=new Vector2(startPointx+Cam.position.x*moveRate,transform.position.y);
        }
        else
        {
            transform.position=new Vector2(startPointx+Cam.position.x*moveRate,startPointy+Cam.position.y*moveRate);
        }
        
    }
}
