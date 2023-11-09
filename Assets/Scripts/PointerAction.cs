using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

//ポインタのアクションができるやつ
public class PointerAction : MonoBehaviour
{
    private Selectable selectable;
    private enum LoadScene
    {
        TitleScene,//タイトル
        NormalSuika,//通常仕様のスイカ
        HalloweenSuika,//ハロウィン仕様のスイカ
        Restart,
    }
    [SerializeField] private LoadScene loadScene;
    public FruitAppear fruitAppear;

    void Start()
    {
        selectable = GetComponent<Selectable>();
    }

    public void PointerIn()
    {
        selectable.Select();
    }

    public void PointerOut()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void PointerClick()//ポインタクリックしたときに何をするか決めるやつ
    {
        switch (loadScene)
        {
            case LoadScene.TitleScene:
                SceneManager.LoadScene("TitleScene");
                break;
            case LoadScene.NormalSuika:
                SceneManager.LoadScene("NormalSuika");
                break;
            case LoadScene.HalloweenSuika:
                SceneManager.LoadScene("HalloweenSuika");
                break;
            case LoadScene.Restart:
                fruitAppear.Restart();
                break;
        }
    }


    public void OnClick()
    {
        Debug.Log("Click");
    }
}
