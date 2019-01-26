using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private float speed = 12.0f;
    private float minDistance = 1.5f;

    private bool followPlayer = false;
    private GameObject player;
    
    private Color[] possibleColors =
    {
        Color.green, // A
        Color.red, // B
        Color.blue, // X
        Color.yellow // Y
    };

    private Color color;

// Use this for initialization
    void Start()
    {
        color = possibleColors[UnityEngine.Random.Range(0, possibleColors.Length)];
        var monsterFeedBack = GetComponentInParent<MonsterFeedBack>();
        monsterFeedBack.setInputKey(whichInputButton());
        GetComponentInChildren<Renderer>().material.color = color;
    }

// Update is called once per frame
    void Update()
    {
        if (followPlayer && Vector3.Distance(transform.position, player.transform.position) > minDistance)
        {
            Debug.Log("FOLLOW");
            transform.position =
                Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !followPlayer)
        {
            var playerController = other.gameObject.GetComponent<PlayerController>();
            player = playerController.objectToFollow();
            if (playerController.addMonsterToList(this.gameObject))
                followPlayer = true;
        }
    }

    public string whichInputButton()
    {
        if (color == possibleColors[0]) // A
            return "joystick button 0";
        else if (color == possibleColors[1]) // B
            return "joystick button 1";
        else if (color == possibleColors[2]) // X
            return "joystick button 2";
        else // Y
            return "joystick button 3";
    }
}