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
            if (playerController.addMonsterToList(gameObject))
                followPlayer = true;
        }
    }

    public string whichInputButton()
    {
        if (color == possibleColors[0]) // A
            return KeyCode.JoystickButton0.ToString();
        if (color == possibleColors[1]) // B
            return KeyCode.JoystickButton1.ToString();
        if (color == possibleColors[2]) // X
            return KeyCode.JoystickButton2.ToString();
        // Y
        return KeyCode.JoystickButton3.ToString();
    }
}