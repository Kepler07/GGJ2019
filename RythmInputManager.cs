﻿using System;
using UnityEditor;
using UnityEngine;

public class RythmInputManager : MonoBehaviour, IBeatsCallback
{
    private IInputManagerCallback _callback;
    private string _inputPressed;
    private string[] _inputArray;
    private int _currentIndexInInputArray;
    private bool _hasToCheckInput = false;
    private int _inputArraySize;
    private bool _isInitialize = false;
    private bool _hasFindInput = false;
    

    public void setInputList(string[] inputArray)
    {
        _inputArray = inputArray; 
        _inputArraySize = inputArray.Length;
        _currentIndexInInputArray = 0;
        _isInitialize = true;
    }

    private void FixedUpdate()
    {
        if (_hasToCheckInput)
        {
            getInputPressed();
            checkInput();    
        }
    }

    private void getInputPressed()
    {
        if (Input.GetKeyUp("a"))
        {
            _inputPressed = "a";
        }

        if (Input.GetKeyUp("z"))
        {
            _inputPressed = "z";
        }

        if (Input.GetKeyUp("e"))
        {
            _inputPressed = "e";
        }
    }

    private void EnableInput()
    {
        if(!_isInitialize) throw new Exception("You have to initialize the RythmInputManager before starting it tard");
        if (_callback != null)
        {
            _callback.growUPCurrentMonster(_inputArray[_currentIndexInInputArray]);
        }
        _hasToCheckInput = true;
    }

    private void DisableInput()
    {
        //We check if the input has been find during the elapsed time !
        if (_hasFindInput && _callback != null)
        {
            _callback.inputSuccess(_inputArray[_currentIndexInInputArray]);
        }
        else if(!_hasFindInput && _callback != null)
        {
            _callback.inputFailed(_inputArray[_currentIndexInInputArray]);
        }
        
        if (_callback != null)
        {
            _callback.growDownMonster(_inputArray[_currentIndexInInputArray]);
        }

        _hasFindInput = false;
        _hasToCheckInput = false;
        _inputPressed = null;
        _currentIndexInInputArray++;
        if (_currentIndexInInputArray > _inputArraySize - 1)
        {
            _currentIndexInInputArray = 0;
        }
    }
    
    public void SetCallback(IInputManagerCallback callback)
    {
        _callback = callback;
    }

    public void StopRepeatingFunctions()
    {
        DisableInput();
        _callback = null;
        _currentIndexInInputArray = 0;
    }

    private void checkInput()
    {
        var currentInputTested = _inputArray[_currentIndexInInputArray];

        if (currentInputTested == "")
        {
            if (_inputPressed == null)
            {
                _hasFindInput = true;
            }
            else
            {
                _hasFindInput = false;
                _hasToCheckInput = false;
                _inputPressed = null;
            }
        }
        else
        {
            if (_inputPressed == null)
            {
                    _hasFindInput = false;
            }
            else
            {
                if (_inputPressed == currentInputTested)
                {
                    _hasFindInput = true;
                    _hasToCheckInput = false;
                    _inputPressed = null;
                }
                else
                {
                    _hasFindInput = false;
                    _hasToCheckInput = false;
                    _inputPressed = null;
                }
            }
        }
    }

    public void beforeSound()
    {
        EnableInput();
    }

    public void afterSound()
    {
        DisableInput();
    }
}