using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playercontroller : MonoBehaviour
{
    [SerializeField]private Rigidbody2D rb;//私有但可以显示
    private Animator anim;//私有不显示
    public float speed=10f;//速度
    public float jumpforce;//弹跳力
    public Transform CellingCheck,groundcheck;//顶点检测

    //public AudioSource JumpAudio;//弹跳音效
    //public AudioSource GemAudio;//钻石音效
    //public AudioSource CherryAudio;//樱桃音效
    //public AudioSource HurtAudio;//受伤音效
    public AudioSource BGM;

    public LayerMask ground;//场景
    public Collider2D coll;
    public Collider2D crouchcoll;

    public int cherry;//樱桃
    public int gem;//钻石
    public Text cherryNum;//樱桃UI
    public Text gemNum;//钻石UI

    private bool isHurt;//默认为false
    private bool isGround=false;
    private int JumpNum=0;

    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        anim=GetComponent<Animator>();
        cherry = PlayerPrefs.GetInt("Cherry");
        gem=PlayerPrefs.GetInt("Gem");
        cherryNum.text = cherry.ToString();
        gemNum.text=gem.ToString();
        //JumpAudio=GetComponent<AudioSource>();//赋值音源
    }

    // Update is called once per frame
    void Update()//自适应
    {
        isGround=coll.IsTouchingLayers(ground);
        if(!isHurt)
        {
            move();
        }
        changeAnim();
    }

    void move()
    {
        float HorizontalMove=0;
        float FaceDirection=0;
        //float VerticalMove=0;
        HorizontalMove = Input.GetAxis("Horizontal");//-1~1浮点型
        FaceDirection=Input.GetAxisRaw("Horizontal");//-1,0,1
        //VerticalMove=Input.GetAxisRaw("Vertical");
        if (HorizontalMove!=0)//角色移动
        {
            
            rb.velocity = new Vector2(HorizontalMove*speed ,rb.velocity.y);//（x,y）
            //velocity 表示沿一定路线运动的速度
            anim.SetFloat("running",Mathf.Abs(FaceDirection));//通过facedirection改变running的值来改变动画
        }
        if(FaceDirection!=0)//角色翻转
        {
            transform.localScale=new Vector3(FaceDirection,1,1);//(x,y,z)
        }
        /*if(Input.GetButton("Jump")&&coll.IsTouchingLayers(ground))//跳（有输入且接触ground）
        {
            rb.velocity=new Vector2(rb.velocity.x,jumpforce);
            anim.SetBool("jumping",true);//将jumping的值改为true
            JumpAudio.Play();//播放音乐
        }*/
        if(isGround&&JumpNum==0)//二段跳，不理解
        {
            JumpNum=1;
        }
        if(Input.GetButtonDown("Jump")&&JumpNum>0)
        {
            rb.velocity=Vector2.up*jumpforce;//等价于 new Vector2(0,1)
            JumpNum--;
            anim.SetBool("jumping",true);
            //JumpAudio.Play();
            SoundManager.instance.Jump_Audio();
        }
        if(!Physics2D.OverlapCircle(CellingCheck.position,0.1f,ground))//圆形检测
        {
            if(Input.GetButton("Crouch")&&coll.IsTouchingLayers(ground))//蹲
            {
                anim.SetBool("crouching",true);
                rb.velocity = new Vector2(HorizontalMove*speed/2 ,rb.velocity.y);
                crouchcoll.enabled=false;
            }
            else
            {
                anim.SetBool("crouching",false);
                crouchcoll.enabled=true;
            }
        }
        
    }


    void changeAnim()//动画转换
    {
        //anim.SetBool("idle",false);
        
        if (anim.GetBool("jumping"))
        {
            if(rb.velocity.y<0)//下降动画
            {
                anim.SetBool("jumping",false);
                anim.SetBool("falling",true);
            }
        }
        else if (isHurt)
        {
            anim.SetBool("hurt",true);
            anim.SetFloat("running",0);
            if (Mathf.Abs(rb.velocity.x)<0.1f)//受伤时速度小于1则停止受伤状态
            {
                anim.SetBool("hurt",false);
                //anim.SetBool("idle",true);
                isHurt=false;
            }
        }
        else if (coll.IsTouchingLayers(ground))//钢体接触到ground层
        {
            anim.SetBool("falling",false);
            //anim.SetBool("idle",true);
        }
    }
    
    //收集物品#碰撞触发器
     private void OnTriggerEnter2D(Collider2D other) 
     {
         
         if (other.tag=="cherry")
         {
            //CherryAudio.Play();
            SoundManager.instance.Cherry_Audio();
             Destroy(other.gameObject);
             cherry++;
             PlayerPrefs.SetInt("Cherry",cherry);
             cherryNum.text = cherry.ToString();//转文本
         }
         if (other.tag=="gem")
         {
            //GemAudio.Play();
            SoundManager.instance.Gem_Audio();
             Destroy(other.gameObject);
             gem++;
             PlayerPrefs.SetInt("Gem",gem);
             gemNum.text=gem.ToString();
         }
         if(other.tag=="DeadLine")
         {
            BGM.pitch=0.1f;
            Invoke("Restar",2f);
         }
     }

     //消灭敌人
     private void OnCollisionEnter2D(Collision2D other) 
     {
        if (other.gameObject.tag=="enemy")
        {
            //Enemy_frog frog = other.gameObject.GetComponent<Enemy_frog>();//引入frog的所有函数和组件
            Eemy enemy=other.gameObject.GetComponent<Eemy>();
            if(anim.GetBool("falling"))//当前状态为下降时可以消灭敌人
            {
                enemy.JumpOn();
                //eagle.JumpOn();
                rb.velocity=new Vector2(rb.velocity.x,jumpforce);
                anim.SetBool("jumping",true);//将jumping的值改为true
            }
            else if(transform.position.x<other.gameObject.transform.position.x)//从左边接触
            {
                //HurtAudio.Play();
                SoundManager.instance.Hurt_Audio();
                rb.velocity=new Vector2(-5,rb.velocity.y);//向左移动
                isHurt=true;//进入受伤动画
            }
            else if(transform.position.x>other.gameObject.transform.position.x)
            {
                //HurtAudio.Play();
                SoundManager.instance.Hurt_Audio();
                rb.velocity=new Vector2(5,rb.velocity.y);
                isHurt=true;
                
            }
        }
     }

     void Restar()
     {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);//重置当前场景         
     }
}