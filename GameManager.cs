using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[RequireComponent(typeof(BeatsPlayer))]
[RequireComponent(typeof(RythmInputManager))]
public class GameManager : MonoBehaviour, IInputManagerCallback, IBeatsPlayerCallback
{
        public List<GameObject> monsterList;
        public float beatRate = 1f;
        public float timeAfterToValidate = 0.5f;
        public float timeBeforeToValidate = 0.2f;

        private BeatsPlayer _beatsPlayer;
        private RythmInputManager _rythmInputManager;
        private bool isStarted = false;
        private string[] inputArray = {"a","z","e", ""};

        private void Start()
        {
                _beatsPlayer = GetComponent<BeatsPlayer>();
                _rythmInputManager = GetComponent<RythmInputManager>();
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
                StartCoroutine("startPlayCoroutine");
                
                _rythmInputManager.setInputList(inputArray);
        }

        private IEnumerator startPlayCoroutine()
        {
                Debug.Log("The game gonna start in 3 seconds");
                yield return new WaitForSecondsRealtime(1);
                Debug.Log("The game gonna start in 2 seconds");
                yield return new WaitForSecondsRealtime(1);
                Debug.Log("The game gonna start in 1 seconds");
                yield return new WaitForSecondsRealtime(1);
                Debug.Log("GOGOGOGO");
                StartCoroutine("gamePlayCoroutine");
        }

        private void StopGamePlay()
        {
                _rythmInputManager.StopRepeatingFunctions();
                _beatsPlayer.StopRepeating();
                Debug.Log("STOP IT");
        }

        IEnumerator gamePlayCoroutine()
        {
                _rythmInputManager.InvokeEnableInput(beatRate, this);
                yield return new WaitForSecondsRealtime(timeBeforeToValidate);
                _beatsPlayer.InvokePlaySound(beatRate, this);
                yield return new WaitForSecondsRealtime(timeAfterToValidate);
                _rythmInputManager.InvokeDisableInput(beatRate);
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

        public void beatsPlayed()
        {
                //Debug.Log("Beats Played");
        }

        public void growUPCurrentMonster(string keyToGrowUp)
        {
                var script = getCurrentMonsterFeedBackScript(keyToGrowUp);
                if (script != null)
                {
                        script.sizeUp();
                }
        }
        
        public void growDownMonster(string keyToGrowDown)
        {
                var script = getCurrentMonsterFeedBackScript(keyToGrowDown);
                if (script != null)
                {
                        script.sizeDown();
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
                var monsterToGrowUp = monsterList.Find(item => item.GetComponent<MonsterFeedBack>().keyString == key);
                return monsterToGrowUp == null ? null : monsterToGrowUp.GetComponent<MonsterFeedBack>();
        }
}