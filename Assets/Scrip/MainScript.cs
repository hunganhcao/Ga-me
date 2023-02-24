using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainScript : MonoBehaviour
{
    private float Vantoc = 10;
    private float DoCao = 15f;        
    private float TrongLuc = 5;
    private float NhayThap = 5;
    private float MaxSpeed = 15f;
    private float Speed ;
    private bool OnGround= true;
    private bool QuayPhai = true;
    private float KTraGiuPhim = 0.2f;
    private float TGGiuPhim = 0;
    private static float life ;
    private static float score = 0;

    public AudioManager AmThanh;
    public float Level = 0;
    public bool Transf = false;

    private Text txtLife;
    private Text txtScore;

    private Rigidbody2D r2d; 
    private Animator animator;

    public GameObject VictoryPnUI;
    public  GameObject DiedPnUI;
	
	void Start()
    {
        AudioManager.instance.Stop("Menu");
        r2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        

        txtLife = GameObject.Find("LifeTxt").GetComponent<Text>();
        life = 11;
        txtLife.text = life.ToString();
        
		txtScore = GameObject.Find("ScoreTxt").GetComponent<Text>();
        txtScore.text = score.ToString();
        
		
		
	}

    // Update is called once per frame
    void Update()
    {
        life= Int32.Parse(txtLife.text);
		score = Int32.Parse(txtScore.text);
		animator.SetFloat("Speed",Speed);
        animator.SetBool("OnGround",OnGround);
        Jump();
        ShootAndRun();
        //
        if(Transf== true)
        {
            switch (Level)
            {
                case 0:
                    {
                        StartCoroutine(MarioNormal());
                        Transf = false;
                        break;
                    }

                case 1:
                    {
                        StartCoroutine(MarioSuper());
                        Transf = false;
                        break;
                    }
                default: { Transf = false; break; }
            }
        }
        //
        if (this.gameObject.transform.position.y < -20f)
        {
            life = 0;
            txtLife.text = life.ToString();
            DiedPnUI.SetActive(true);
            
            Destroy(this.gameObject);
        }
        

        
    }
    private void FixedUpdate()
    {
        Dichuyen();
       
    }
    void Dichuyen()
    {
        float PhimNhanPhaiTrai = Input.GetAxis("Horizontal");
        r2d.velocity = new Vector2(Vantoc * PhimNhanPhaiTrai, r2d.velocity.y);
        Speed = Mathf.Abs(Vantoc * PhimNhanPhaiTrai);
        if (PhimNhanPhaiTrai > 0 && !QuayPhai) HuongMatMario();
        if (PhimNhanPhaiTrai < 0 && QuayPhai) HuongMatMario();
    }
    void HuongMatMario()
    {
        QuayPhai = !QuayPhai;
        Vector2 HuongQuay = transform.localScale;
        HuongQuay.x *= -1;
        transform.localScale = HuongQuay;

    }
    void Jump()
    {
		
		if (Input.GetKeyDown(KeyCode.Space) && OnGround == true)
        {
			OnGround = false;
            AudioManager.instance.Play("Jump");
			r2d.AddForce((Vector2.up) * DoCao,ForceMode2D.Impulse);
            
			
		}
		if (Input.GetKey(KeyCode.Space) )
		{
			OnGround = false;
		}

        
            if (r2d.velocity.y < 0)
            {
                r2d.velocity += Vector2.up * Physics2D.gravity.y * (TrongLuc - 1) * Time.deltaTime;
            }
            else if (r2d.velocity.y > 0 && !(Input.GetKey(KeyCode.Space)))
            {
                
                r2d.velocity += Vector2.up * Physics2D.gravity.y * (NhayThap - 1) * Time.deltaTime;

            }
        
        
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Ground" || col.gameObject.tag=="Enemies")
        {
            OnGround = true;
            
        }
        if (col.tag == "Coin")
        {
			//int score = Int32.Parse(txtScore.text);
			score += 1;
			txtScore.text = "" + score.ToString();
            AudioManager.instance.Play("Coin");
			Destroy(col.gameObject);
		}

        
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Ground" ||col.gameObject.tag == "Enemies")
        {
            OnGround = true;
            
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "EndLevel1")
        {
            SceneManager.LoadScene("Lv2");
         
        }
        if (col.gameObject.tag == "EndGame")
        {
            VictoryPnUI.SetActive(true);
        }
    }
    
    void ShootAndRun()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            TGGiuPhim += Time.deltaTime;
            if (TGGiuPhim < KTraGiuPhim)
            {

            }
            else { 
                Vantoc *= 1.5f;
                if(Vantoc>MaxSpeed) Vantoc= MaxSpeed;
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            Vantoc = 7f;
            TGGiuPhim= 0;
        }
    }
    IEnumerator MarioSuper()
    {
        float DoTre = 0.2f;
        animator.SetLayerWeight(animator.GetLayerIndex("Super"), 1);
        animator.SetLayerWeight(animator.GetLayerIndex("Mario"), 0);
        animator.SetLayerWeight(animator.GetLayerIndex("Super"), 1);
        yield return new WaitForSeconds(DoTre);
        animator.SetLayerWeight(animator.GetLayerIndex("Mario"), 1);
        animator.SetLayerWeight(animator.GetLayerIndex("Super"), 0);
        animator.SetLayerWeight(animator.GetLayerIndex("Super"), 1);
        yield return new WaitForSeconds(DoTre);
        animator.SetLayerWeight(animator.GetLayerIndex("Super"), 1);
        animator.SetLayerWeight(animator.GetLayerIndex("Mario"), 0);
        animator.SetLayerWeight(animator.GetLayerIndex("Super"), 1);
        yield return new WaitForSeconds(DoTre);

    }
    IEnumerator MarioNormal()
    {
        float DoTre = 0.2f;
        animator.SetLayerWeight(animator.GetLayerIndex("Super"), 0);
        animator.SetLayerWeight(animator.GetLayerIndex("Mario"), 1);
        animator.SetLayerWeight(animator.GetLayerIndex("Super"), 0);
        yield return new WaitForSeconds(DoTre);
        animator.SetLayerWeight(animator.GetLayerIndex("Mario"), 0);
        animator.SetLayerWeight(animator.GetLayerIndex("Super"), 1);
        animator.SetLayerWeight(animator.GetLayerIndex("Super"), 0);
        yield return new WaitForSeconds(DoTre);
        animator.SetLayerWeight(animator.GetLayerIndex("Super"), 0);
        animator.SetLayerWeight(animator.GetLayerIndex("Mario"), 1);
        animator.SetLayerWeight(animator.GetLayerIndex("Super"), 0);
        yield return new WaitForSeconds(DoTre);

    }
}
