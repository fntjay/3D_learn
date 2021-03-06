﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUserAction
{
    void shoot();//射击动作
}

public class UserGUI : MonoBehaviour
{
    private IUserAction action;
    private float width, height;
    private string countDownTitle;

    void Start()
    {
        countDownTitle = "Start";
        action = SSDirector.getInstance().currentScenceController as IUserAction;
    }

    float castw(float scale)
    {
        return (Screen.width - width) / scale;
    }

    float casth(float scale)
    {
        return (Screen.height - height) / scale;
    }

    void OnGUI()
    {
        width = Screen.width / 12;
        height = Screen.height / 12;

        //倒计时
        GUI.Label(new Rect(castw(2f) + 20, casth(6f) - 20, 50, 50), ((RoundController)SSDirector.getInstance().currentScenceController).leaveSeconds.ToString());

        //分数
        GUI.Button(new Rect(580, 10, 80, 30), ((RoundController)SSDirector.getInstance().currentScenceController).scoreRecorder.getScore().ToString());

        if (SSDirector.getInstance().currentScenceController.state != State.WIN && SSDirector.getInstance().currentScenceController.state != State.LOSE
            && GUI.Button(new Rect(10, 10, 80, 30), countDownTitle))
        {

            if (countDownTitle == "Start")
            {
                //恢复场景
                countDownTitle = "Pause";
                SSDirector.getInstance().currentScenceController.Resume();
            }
            else
            {
                //暂停场景
                countDownTitle = "Start";
                SSDirector.getInstance().currentScenceController.Pause();
            }
        }

        if (SSDirector.getInstance().currentScenceController.state == State.WIN)//胜利
        {
            if (GUI.Button(new Rect(castw(2f), casth(6f), width, height), "Win!"))
            {
                //选择重来
                SSDirector.getInstance().currentScenceController.Restart();
            }
        }
        else if (SSDirector.getInstance().currentScenceController.state == State.LOSE)//失败
        {
            if (GUI.Button(new Rect(castw(2f), casth(6f), width, height), "Lose!"))
            {
                SSDirector.getInstance().currentScenceController.Restart();
            }
        }
    }

    void Update()
    {
        //监测用户射击
        action.shoot();
    }

}