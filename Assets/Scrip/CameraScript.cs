using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Transform player;
    private float minX = 5,maxX=86;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;

    }
    private void Update()
    {
        if(player != null)
        {
            Vector3 vitri = transform.position;
            vitri.x = player.position.x;
            if (vitri.x < minX) vitri.x = minX;
            if (vitri.x >maxX) vitri.x = maxX;
            transform.position = vitri;
        }
    }

}
