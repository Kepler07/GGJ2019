using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject followDuCul;
    public float speed = 8f;
    public float brake = 10f;
    public int MAX_MONSTERS = 4;

    private Rigidbody rb;

    private float curSpeed;

    private bool inputApplied;

    private float moveHorizontal;
    private float moveVertical;
    private IPlayerCallback _callback;
    
    
    private List<GameObject> monsterList = new List<GameObject>();
    private List<string> inputList = new List<string>();

    public PlayerController()
    {
    }

    public void setCallback(IPlayerCallback callback)
    {
        _callback = callback;
    }

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");


        if (moveHorizontal == 0.0f && moveVertical == 0.0f)
            inputApplied = false;
        else
            inputApplied = true;

    }

    void FixedUpdate()
    {
        if (inputApplied)
        {
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            rb.velocity = (movement * speed);
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
        else
        {
            curSpeed = rb.velocity.magnitude;
            float newSpeed = curSpeed - brake * Time.deltaTime;
            if (newSpeed < 0)
            {
                newSpeed = 0;
            }

            rb.velocity = rb.velocity.normalized * newSpeed;
        }
    }

    public Rigidbody Rb
    {
        get { return rb; }
        set { rb = value; }
    }

    public bool addMonsterToList(GameObject monster)
    {
        if (!monsterList.Contains(monster) && monsterList.Count <= MAX_MONSTERS - 1)
        {
            monsterList.Add(monster);
            inputList.Add(getMonsterInput(monster));
            if (_callback != null)
            {
                _callback.onMonsterAdded(monsterList);    
            }
            return true;
        }

        return false;
    }

    public string getMonsterInput(GameObject monster)
    {
        return monster.GetComponent<MonsterController>().whichInputButton();
    }

    public GameObject objectToFollow()
    {
        if (monsterList.Count == 0)
        {
            return followDuCul;
        }

        else
        {
            return monsterList.Last();
        }
    }

    public string[] getInputArray()
    {
        return inputList.ToArray();
    }

    public List<GameObject> MonsterList
    {
        get { return monsterList; }
        set { monsterList = value; }
    }
}