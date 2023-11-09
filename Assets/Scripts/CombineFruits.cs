using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombineFruits : MonoBehaviour
//�t���[�c�ɃA�^�b�`���āC�t���[�c�̓����蔻��Ə�񑀍�Ɏg��
{
    [Header("Fruit")]
    [SerializeField] private GameObject combinedFruit;//�ł��������̃t���[�c�̃v���n�u���L�^
    private GameObject generatedFruit;//���ۂɐ��ݏo���ꂽ�t���[�c�̃I�u�W�F�N�g���i�[
    public bool isAlreadyDropped = false;//�r���Ƀh���b�v�������̔��� �r�������яo�����I�u�W�F�N�g���𔻒肷�鏉���������_�ł�false
    public int fruitID;//ドロップフルーツのID
    [Header("SE")]
    public GameObject audioObject;//�|���̌��ʉ��Ȃ������
    AudioSource audioSource;//�|���̌��ʉ��Ȃ������
    [SerializeField] private AudioClip pon;//�|���̌��ʉ�
    [Header("Score")]
    public Score score;//�X�R�A�̂��
    [Header("Halloween")]
    [SerializeField] private bool isPumpkin = false;//ハロウィン仕様：かぼちゃかどうか

    private void Start()
    {
        if (this.gameObject.name == "None(Clone)") Destroy(this.gameObject);//�X�C�J������Ȃ玩������
        audioObject = GameObject.FindGameObjectWithTag("Audio");
        audioSource = audioObject.GetComponent<AudioSource>();
        if (SceneManager.GetActiveScene().name != "TitleScene")
            score = GameObject.FindGameObjectWithTag("Score").GetComponent<Score>();
    }
    //�Փ˂����Ƃ��Ƀt���[�c�̍������s��
    void OnCollisionEnter(Collision collision)
    {
        if (gameObject.CompareTag("Fruit"))//これがフルーツなら(自分がキャンディの場合は何も起こさない　ぶつかったフルーツの関数をトリガーさせる)
        {
            if (collision.gameObject.CompareTag("Fruit"))                         // Fruit同士でぶつかったら
            {
                if (collision.gameObject.name == this.gameObject.name)
                {//同一名のフルーツなら
                    if (collision.gameObject.GetComponent<CombineFruits>().fruitID < fruitID)
                    {//2つのうちフルーツIDが小さい方について(先に落とされたものについて)
                        generatedFruit = Instantiate(combinedFruit, this.transform.position, this.transform.rotation);//先に落とされたフルーツの位置に進化先フルーツを入れる
                        generatedFruit.GetComponent<Rigidbody>().useGravity = true;//重力落下させます;
                        generatedFruit.GetComponent<CombineFruits>().isAlreadyDropped = true;//既に落とされた物体である情報
                        generatedFruit.GetComponent<CombineFruits>().fruitID = fruitID;//順番の引継ぎ
                        audioSource.PlayOneShot(pon);//SEをならす
                        score.UpdateScore(this.name);//フルーツの種類に応じてスコアを出す
                    }
                    Destroy(collision.gameObject);
                    Destroy(this.gameObject);
                }
            }
            else if (collision.gameObject.CompareTag("Candy"))//ハロウィン仕様のキャンディの実装キャンディとぶつかったなら
            {
                if (!isPumpkin)//カボチャとキャンディがぶつかったら両者消滅するのでここは省略
                {
                    generatedFruit = Instantiate(combinedFruit, this.transform.position, this.transform.rotation);//自分の位置に進化先フルーツを出す
                    generatedFruit.GetComponent<Rigidbody>().useGravity = true;//重力落下させます;
                    generatedFruit.GetComponent<CombineFruits>().isAlreadyDropped = true;//既に落とされた物体である情報
                    generatedFruit.GetComponent<CombineFruits>().fruitID = fruitID;//順番の引継ぎ
                }
                audioSource.PlayOneShot(pon);//SEをならす
                if (this.gameObject.name != "Watermelon(Clone)") score.UpdateScore(this.name);//フルーツの種類に応じてスコアを出す ただしスイカをキャンディにあてたときにはなにも起こらない
                Destroy(collision.gameObject);
                Destroy(this.gameObject);
            }
        }
    }
}
