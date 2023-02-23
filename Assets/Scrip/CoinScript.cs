using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class CoinScript : MonoBehaviour
{
    public Text txtScore;
    
    // Start is called before the first frame update
    void Start()
    {
        txtScore= GameObject.Find("ScoreTxt").GetComponent<Text>();
        
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    

}
