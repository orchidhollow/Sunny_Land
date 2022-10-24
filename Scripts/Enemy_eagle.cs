using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_eagle : Eemy
{
    private Rigidbody2D rb;
    public Transform uppoint,downpoint;
    private float upy,downy;
    public float speed;
    private bool To_up=true;
    // Start is called before the first frame update
    protected override void Start()//继承虚函数时需要override
    {
        base.Start();//获得父级的Start
        rb= GetComponent<Rigidbody2D>();
        transform.DetachChildren();
        upy=uppoint.position.y;
        downy=downpoint.position.y;
        Destroy(uppoint.gameObject);
        Destroy(downpoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }


    void Movement()
    {
        if (To_up)
        {
            rb.velocity=new Vector2(rb.velocity.x,speed);
            if(transform.position.y>upy)
            {
                To_up=false;
            }
        }
        else
        {
            rb.velocity=new Vector2(rb.velocity.x,-speed);
            if(transform.position.y<downy)
            {
                To_up=true;
            }
        }
    }



    

}
