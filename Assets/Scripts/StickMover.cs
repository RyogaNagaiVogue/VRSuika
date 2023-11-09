using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

//スティックで移動と回転できるようになる
//Ref: https://takoyakiroom.hatenablog.com/entry/2020/09/13/015413
public class StickMover : MonoBehaviour
{

    public SteamVR_Action_Vector2 walkAction;//Vector2のwalkActionに設定する
    public SteamVR_Action_Vector2 rotateAction;//Vector2のrotateActionに設定する
    public float rotateSpeed = 2.0f;
    public float walkSpeed = 2.0f;//歩くスピード
    public GameObject HMD;//Headを入れる　このHeadの方向に移動する算段

    void FixedUpdate()
    {
        Vector3 player_pos = transform.position;
        
        //=========================================
        //1.stickでの移動
        Vector3 direction = HMD.transform.TransformDirection(new Vector3(walkAction.axis.x, 0, walkAction.axis.y));
        player_pos.x += walkSpeed * Time.deltaTime * direction.x;
        player_pos.z += walkSpeed * Time.deltaTime * direction.z;

        transform.position = player_pos;

        //=========================================
        //2.stickでの回転
        float rotationAngle = 0;
        if(System.Math.Abs(rotateAction.axis.x) > 0.5f)rotationAngle = rotateAction.axis.x;
        transform.RotateAround(HMD.transform.position, transform.up, Time.deltaTime * rotateSpeed * rotationAngle);
    }
}