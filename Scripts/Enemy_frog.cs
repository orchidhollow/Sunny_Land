using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_frog : Eemy//继承的父类
{
    private Rigidbody2D rb;
    //private Animator Anim;
    private Collider2D coll;
    public LayerMask ground;
    public Transform leftpoint,rightpoint;
    private bool faceleft=true;
    private float leftx,rightx;
    public float speed,jumpforce;
    protected override void Start()//继承虚函数时需要override
    {
        base.Start();//获得父级的Start
        rb=GetComponent<Rigidbody2D>();
        Anim=GetComponent<Animator>();
        coll=GetComponent<Collider2D>();
        transform.DetachChildren();//拆下子项目
        leftx=leftpoint.position.x;
        rightx=rightpoint.position.x;
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);
    }

    void Update()
    {
        SwichAnim();
    }
    void Movement()
    {
        if(coll.IsTouchingLayers(ground))
            {
                Anim.SetBool("jumping",true);
                rb.velocity=new Vector2(-speed,jumpforce);
            }
        if(faceleft)
        {
            
            if(transform.position.x<leftx)
            {
                rb.velocity=new Vector2(speed,jumpforce);
                transform.localScale=new Vector3(-1,1,1);
                faceleft=false;
            }
        }
        else
        {
            if(coll.IsTouchingLayers(ground))
            {
                Anim.SetBool("jumping",true);
                rb.velocity=new Vector2(speed,jumpforce);
            }
            if(transform.position.x > rightx)
            {
                rb.velocity=new Vector2(-speed,jumpforce);
                transform.localScale=new Vector3(1,1,1);
                faceleft=true;
            }
        }
        
    }

    void SwichAnim()
    {
        if(Anim.GetBool("jumping"))
        {
            if(rb.velocity.y<0.1)
            {
                Anim.SetBool("jumping",false);
                Anim.SetBool("falling",true);
            }
        }
        if(coll.IsTouchingLayers(ground)&&Anim.GetBool("falling"))
        {
            Anim.SetBool("falling",false);
        }
    }


}
