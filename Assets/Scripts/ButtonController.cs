// ﻿using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class ButtonController : MonoBehaviour {
//
// 	// Use this for initialization
// 	void Start () {
//
// 	}
//
// 	// Update is called once per frame
// 	void Update () {
//
// 	}
// }

using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    //Make sure to attach these Buttons in the Inspector
    public Button button;

    void Start()
    {
        Button btn1 = button.GetComponent<Button>();
        btn1.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Debug.Log("You have clicked the button!");
    }
}
