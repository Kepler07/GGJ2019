using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class MonsterController : MonoBehaviour
{

    public MonsterColor color;
    
    private float speed = 12.0f;
    private float minDistance = 1.5f;

    private bool followPlayer = false;
    private GameObject player;


// Use this for initialization
    void Start()
    {
        var monsterFeedBack = GetComponentInParent<MonsterFeedBack>();
        monsterFeedBack.setInputKey(whichInputButton());
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
        switch (color)
        {
            case MonsterColor.GREEN:
                return KeyCode.JoystickButton0.ToString();
            case MonsterColor.RED:
                return KeyCode.JoystickButton1.ToString();
            case MonsterColor.BLUE:
                return KeyCode.JoystickButton2.ToString();
            case MonsterColor.YELLOW:
                return KeyCode.JoystickButton3.ToString();
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}

public enum MonsterColor
{
    GREEN,
    RED,
    BLUE,
    YELLOW
}