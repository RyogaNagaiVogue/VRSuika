using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class judgeGameOver : MonoBehaviour
{
    public FruitAppear fruitAppear;//�Q�[�����s�\���̐���
    public TextMeshProUGUI textMeshProUGUI;//�f�o�b�O�p
    [Header("SE")]
    public GameObject audioObject;
    AudioSource audioSource;
    [SerializeField] private AudioClip gameoverSE;//ゲームオーバーのSE
    [Header("ScoreOutput")]
    public CSVReader csvReader;//ランキングをCSVにアウトプットするやつです
    public Score score;//スコア計算　最後に出力する値を参照します
    void Start()
    {
        audioObject = GameObject.FindGameObjectWithTag("Audio");
        audioSource = audioObject.GetComponent<AudioSource>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fruit"))                           // Fruit�^�O�ɐڐG�����Ƃ�
        {
            if (other.gameObject.GetComponent<CombineFruits>().isAlreadyDropped)
            { //���Ƀh���b�v���Ă�����̂�������
                GameObject[] droppedFruits = GameObject.FindGameObjectsWithTag("Fruit");//���Ƀh���b�v���Ă���t���[�c�^�O��S�Ċi�[
                foreach (GameObject droppedFruit in droppedFruits)
                {
                    droppedFruit.GetComponent<Rigidbody>().isKinematic = true;//�S�t���[�c�̋@�\���~������
                }
                fruitAppear.isGameOver = true;
                fruitAppear.remainTime = 0;
                Debug.Log("GameOver");
                textMeshProUGUI.text = "ゲームオーバー";
                audioSource.PlayOneShot(gameoverSE);//SEをならす
                csvReader.RankingOutput(score.score);//スコアをアウトプットする
            }

        }
    }
}
