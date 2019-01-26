using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BeatsPlayer))]
[RequireComponent(typeof(RythmInputManager))]
public class GameManager : MonoBehaviour, IInputManagerCallback, IPlayerCallback
{
        public float beatRate = 1f;
        public float timeAfterToValidate = 0.5f;
        public float timeBeforeToValidate = 0.2f;
        public GameObject player;

        private BeatsPlayer _beatsPlayer;
        private RythmInputManager _rythmInputManager;
        private PlayerController _playerController;
        private bool isStarted = false;
        private List<string> inputArray;
        private List<GameObject> monsterList;

        public GameManager()
        {
                inputArray = new List<string> {""};
        }

        private void Start()
        {
                _beatsPlayer = GetComponent<BeatsPlayer>();
                _rythmInputManager = GetComponent<RythmInputManager>();
                _playerController = player.GetComponent<PlayerController>();
                _beatsPlayer.InvokePlaySound(beatRate);
                _playerController.setCallback(this);
        }

        private void Update()
        {
                if (Input.GetKeyUp("space"))
                {
                        toggleStartPauseRythm();
                }
        }

        private void toggleStartPauseRythm()
        {
                if (isStarted)
                {
                        isStarted = !isStarted;
                        StopGamePlay();
                }
                else
                {
                        isStarted = !isStarted;
                        StartGamePlay();        
                }
        }

        private void StartGamePlay()
        {
                _rythmInputManager.setInputList(inputArray.ToArray());
                StartCoroutine("startPlayCoroutine");
        }

        private void startPlayCoroutine()
        {
                StartCoroutine("gamePlayCoroutine");
        }

        private void StopGamePlay()
        {
                _beatsPlayer.RemoveCallback();
        }

        void gamePlayCoroutine()
        {
                _rythmInputManager.SetCallback(this);
                _beatsPlayer.SetCallback(_rythmInputManager, timeBeforeToValidate, timeAfterToValidate);
        }

        public void inputSuccess(string input)
        {
                Debug.Log("You won 5 points");
                popMonster(input);

        }

        public void inputFailed(string input)
        {
                Debug.Log("FAILED");
        }

        public void growUPCurrentMonster(string keyToGrowUp, float timeToWait)
        {
                
                var script = getCurrentMonsterFeedBackScript(keyToGrowUp);
                if (script != null)
                {
                        script.sizeUp(timeToWait);
                }
        }

        public void popMonster(string keyToPop)
        {
                var script = getCurrentMonsterFeedBackScript(keyToPop);
                if (script != null)
                {
                        script.pop();
                }
        }

        private MonsterFeedBack getCurrentMonsterFeedBackScript(string key)
        {
                if (monsterList == null) return null;
                var monsterToGrowUp = monsterList.Find(item => item.GetComponentInParent<MonsterFeedBack>().GetInputKey() == key);
                return monsterToGrowUp == null ? null : monsterToGrowUp.GetComponentInParent<MonsterFeedBack>();
        }

        public void onMonsterAdded(List<GameObject> newMonsterList)
        {
                if (monsterList == null)
                {
                        startPlayCoroutine();
                }
                else
                {
                        StopGamePlay();
                }
                monsterList = newMonsterList;
                inputArray = monsterList.Select(item => { return _playerController.getMonsterInput(item); }).ToList();
                inputArray.Add("");
                StartGamePlay();
        }
}