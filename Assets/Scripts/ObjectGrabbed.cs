using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(Rigidbody))]

public class ObjectGrabbed : MonoBehaviour
{
    ///<summary>///
    ///掴むものにつけて掴めるようにする関数
    ///</summary>///

    private Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags & (~Hand.AttachmentFlags.SnapOnAttach) & (~Hand.AttachmentFlags.DetachOthers) & (~Hand.AttachmentFlags.VelocityMovement);
    private Interactable interactable;
    public FruitAppear fruitAppear;


    void Start()
    {
        if (SceneManager.GetActiveScene().name != "TitleScene")
            fruitAppear = GameObject.FindGameObjectWithTag("FruitAppear").GetComponent<FruitAppear>();
        //GetComponent<Rigidbody>().isKinematic = true;//静止状態にする（殴ったときに浮遊していくのを防止します）
    }
    void Awake()
    {
        if (SceneManager.GetActiveScene().name != "TitleScene")
            interactable = this.GetComponent<Interactable>();
    }

    private void HandHoverUpdate(Hand hand)//物体間の衝突毎に呼び出す関数
    {
        GrabTypes startingGrabType = hand.GetGrabStarting();
        bool isGrabEnding = hand.IsGrabEnding(this.gameObject);

        //何も掴んでいないときに何かを掴んだときの処理
        if (interactable.attachedToHand == null && startingGrabType != GrabTypes.None)
        {
            hand.HoverLock(interactable);
            hand.AttachObject(gameObject, startingGrabType, attachmentFlags);//アタッチする
            //GetComponent<Rigidbody>().isKinematic = false;//静止状態を解除する（殴ったときに浮遊していくのを防止します）
        }

        //物体を離したときの処理
        else if (isGrabEnding)
        {
            hand.DetachObject(gameObject);//アタッチを外す
            hand.HoverUnlock(interactable);
            GetComponent<Rigidbody>().useGravity = true;//重力を適応させる
            fruitAppear.FruitDropped();//ドロップ関数の発動


        }
    }
}
