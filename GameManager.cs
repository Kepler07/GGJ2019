using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BeatsPlayer))]
[RequireComponent(typeof(RythmInputManager))]
[RequireComponent(typeof(HealthManager))]
[RequireComponent(typeof(timerManager))]
public class GameManager : MonoBehaviour, IInputManagerCallback, IPlayerCallback, ITimerCallback
{
        public float beatRate = 1f;
        public float timeAfterToValidate = 0.5f;
        public float timeBeforeToValidate = 0.2f;
        public GameObject player;
        public GameObject gamePlayCanvas;
        public GameObject endGameCanvas;

        private BeatsPlayer _beatsPlayer;
        private RythmInputManager _rythmInputManager;
        private PlayerController _playerController;
        private HealthManager _healthManager;
        private timerManager _timerManager;
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
                _healthManager = GetComponent<HealthManager>();
                _timerManager = GetComponent<timerManager>();
                _beatsPlayer.InvokePlaySound(beatRate);
                _playerController.setCallback(this);
                _timerManager.setCallback(this);

                gamePlayCanvas.SetActive(true);
                endGameCanvas.SetActive(false);
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
                startPlayCoroutine();
        }

        private void startPlayCoroutine()
        {
                gamePlayCoroutine();
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
                _healthManager.inputSuccess();
        }

        public void popCurrentMonster(string input)
        {
                popMonster(input);
        }

        public void inputFailed(string input)
        {
                _healthManager.inputFail();
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
                var monsterToGrowUp = monsterList.Find(item =>
                {
                        var index = monsterList.IndexOf(item);
                        var inputKey = item.GetComponent<MonsterFeedBack>().GetInputKey();
                        return inputKey + "_" + index == key;
                });
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
                
                inputArray = monsterList.Select((item, index) => { return _playerController.getMonsterInput(item) + "_" + index; }).ToList();
                inputArray.Add("");
                StartGamePlay();
        }

        public void onGameOver()
        {
                _playerController.setIsFinish();
                gamePlayCanvas.SetActive(false);
                endGameCanvas.SetActive(true);
        }

        public int getCurrentHealth()
        {
                return _healthManager.getFinalhealth();
        }
}