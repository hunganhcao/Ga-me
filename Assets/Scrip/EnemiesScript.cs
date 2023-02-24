using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;


public class EnemiesScript : MonoBehaviour
{
    
    private float VanTocVat=4;
    private bool DiChuyenTrai=true ;
    //public bool died=false;

    private Animator animator;
    private BoxCollider2D b2d;
    private Text txtScore;
    private Text txtLife;
	public GameObject DiedPnUI;
    private bool clickable=false;

	private void Start()
    {
        animator = GetComponent<Animator>();
        b2d= GetComponent<BoxCollider2D>();
        txtScore = GameObject.Find("ScoreTxt").GetComponent<Text>();
        txtLife = GameObject.Find("LifeTxt").GetComponent<Text>();
        


    }
    void Update()
    {
        //animator.SetBool("Died",died);
        if (gameObject.transform.position.y < -20f)
        {
            Destroy(this.gameObject);
        }
        if (gameObject.transform.position.x>=Camera.main.transform.position.x) {
            if(Input.GetMouseButtonDown(0))
            {
                clickable= true;
            }
        }
        
    }
    private void FixedUpdate()
    {
        System.Random rnd = new System.Random();
        for (int i = 0; i < 10; i++)
        {
            var temp = rnd.Next(4, 15);
            VanTocVat = temp;
        }
        Vector2 Dichuyen =transform.position;
        if (DiChuyenTrai) { Dichuyen.x -= VanTocVat * Time.deltaTime; }
        else Dichuyen.x += VanTocVat * Time.deltaTime;
        transform.position = Dichuyen;
        
    }
    private void OnMouseDown()
    {
        if (clickable)
        {
            int score = Int32.Parse(txtScore.text);
            score += 2;

            txtScore.text = "" + score.ToString();
            Destroy(this.gameObject);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)

    {
        if (collision.gameObject.tag == "Player")// && died==false)
        {
            int life = Int32.Parse(txtLife.text);
            if (life == 1)
            {
                txtLife.text = "0";  
                DiedPnUI.SetActive(true);
                Destroy(collision.gameObject);
            }
            else
            {
                life -=1;
                txtLife.text=life.ToString();

            }
            
          
        }
        if (collision.contacts[0].normal.x > 0)
        {
            DiChuyenTrai = true;
            QuayMat();
        }
        else
        {
            DiChuyenTrai = false;
            //QuayMat();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
         if (other.gameObject.tag == "Player")
        {
                //this.b2d.isTrigger= true;
                int score = Int32.Parse(txtScore.text);
                score += 2;
                txtScore.text = "" + score.ToString();
                Destroy(this.gameObject);

        }
    }
    
    
    private void QuayMat()
    {
        DiChuyenTrai =! DiChuyenTrai;
        Vector2 HuongQuay = transform.localScale;
        HuongQuay.x *= (-1);
        transform.localScale = HuongQuay;
    }

}

