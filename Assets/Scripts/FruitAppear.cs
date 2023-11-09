using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using TMPro;

public class FruitAppear : MonoBehaviour
{
    [SerializeField] private GameObject[] fruit;//フルーツ
    [SerializeField] private GameObject respawnFruit;//リスポーン地点
    [SerializeField] private GameObject uiFruit;//NextのUIとして表示するフルーツ
    [SerializeField] private GameObject nextFruits;//次に落とすフルーツ
    [SerializeField] private GameObject currentFruits;//今落とすフルーツ
    public bool isGameOver = false;//ゲームオーバー判定

    public GameObject audioObject;//音関係
    AudioSource audioSource;//音関係
    [SerializeField] private AudioClip drop, reset;//SE
    public int totalFruitsNumber = 0;//今のフルーツナンバー
    public Score score;
    public CSVReader csvReader;
    public TextMeshProUGUI gameoverText;
    void Start()
    {
        int n = Random.Range(0, fruit.Length);//n個のフルーツの中からランダムに
        currentFruits = Instantiate(fruit[n], respawnFruit.transform.position, respawnFruit.transform.rotation);//��̈ʒu�Ɏ��̃t���[�c���o��������
        currentFruits.GetComponent<CombineFruits>().isAlreadyDropped = false;//�r���Ƀh���b�v�������̔��� �r�������яo�����I�u�W�F�N�g���𔻒肷�鏉���������_�ł�false
        currentFruits.GetComponent<CombineFruits>().fruitID = totalFruitsNumber;
        totalFruitsNumber++;

        n = Random.Range(0, fruit.Length);//�����_���Ńt���[�c����
        nextFruits = Instantiate(fruit[n], uiFruit.transform.position, uiFruit.transform.rotation);//ui�̈ʒu�Ɏ��̃t���[�c���o��������
        nextFruits.transform.parent = uiFruit.transform;//�ꏏ�ɐU�������܂�
        nextFruits.GetComponent<CombineFruits>().isAlreadyDropped = false;//�h���b�v���ĂȂ��݂��
        nextFruits.GetComponent<CombineFruits>().fruitID = totalFruitsNumber;
        totalFruitsNumber++;
    }

    // Update is called once per frame
    public void FruitDropped()
    {
        if (!isGameOver)
        {
            //currentFruits.transform.parent = null;//�e�����@VR�������炢��Ȃ����
            audioSource = audioObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(drop);//�Ђ�[��

            StartCoroutine(DelayCoroutine(1.0f, () =>
            {
                RespawnFruit();
            }));
        }
        /*else
        {
            if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);//���Ȃ���
        }*/

    }

    void Update()
    {
        if (isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);//���Ȃ���
        }
        /*else
        {
         
        }*/

    }


    private void RespawnFruit()
    {
        if (currentFruits) currentFruits.GetComponent<CombineFruits>().isAlreadyDropped = true;//�������ĂȂ���Ί��Ƀh���b�v���ꂽ���̂ɂ���
        //nextFruits.transform.parent = null;//�e����
        nextFruits.transform.position = respawnFruit.transform.position;//��̈ʒu�Ɏ��̃t���[�c���ړ�������
        currentFruits = nextFruits;//��Ɉړ������t���[�c�������Ƃ��t���[�c�ɂ���
        //currentFruits.transform.parent = respawnFruit.transform;//�ꏏ�Ɉړ������܂��@VR���������炢��Ȃ��X�N���v�g

        int n = Random.Range(0, fruit.Length);//�����_���Ńt���[�c����

        nextFruits = Instantiate(fruit[n], uiFruit.transform.position, uiFruit.transform.rotation);//ui�̈ʒu�Ɏ��̃t���[�c���o��������
        //nextFruits.transform.parent = uiFruit.transform;//�ꏏ�ɐU�������܂�
        nextFruits.GetComponent<CombineFruits>().isAlreadyDropped = false;//�h���b�v���ĂȂ��݂��
        nextFruits.GetComponent<CombineFruits>().fruitID = totalFruitsNumber;//n番目のフルーツだよ！
        totalFruitsNumber++;
    }

    private IEnumerator DelayCoroutine(float seconds, UnityAction callback)//�R���[�`����n�b�㏈�����ł���
    {
        yield return new WaitForSeconds(seconds);
        callback?.Invoke();
    }

    public void Restart()
    {
        audioSource = audioObject.GetComponent<AudioSource>();
        audioSource.PlayOneShot(reset);//�Ђ�[��

        /*1.ゲームオーバーの解除*/
        isGameOver = false;
        gameoverText.text = "";
        totalFruitsNumber = 0;

        /*2.全オブジェクトの削除*/
        GameObject[] droppedFruits = GameObject.FindGameObjectsWithTag("Fruit");//���Ƀh���b�v���Ă���t���[�c�^�O��S�Ċi�[
        foreach (GameObject droppedFruit in droppedFruits)
        {
            Destroy(droppedFruit);
        }

        GameObject[] droppedCandies = GameObject.FindGameObjectsWithTag("Candy");//���Ƀh���b�v���Ă���t���[�c�^�O��S�Ċi�[
        foreach (GameObject droppedCandy in droppedCandies)
        {
            Destroy(droppedCandy);
        }

        /*3.スコアリセット*/
        score.score = 0;
        csvReader.RankingUpdate();
        score.scoreToString();

        /*4.初期状態の構築*/
        int n = Random.Range(0, fruit.Length);//n個のフルーツの中からランダムに
        currentFruits = Instantiate(fruit[n], respawnFruit.transform.position, respawnFruit.transform.rotation);//��̈ʒu�Ɏ��̃t���[�c���o��������
        currentFruits.GetComponent<CombineFruits>().isAlreadyDropped = false;//�r���Ƀh���b�v�������̔��� �r�������яo�����I�u�W�F�N�g���𔻒肷�鏉���������_�ł�false
        currentFruits.GetComponent<CombineFruits>().fruitID = totalFruitsNumber;
        totalFruitsNumber++;

        n = Random.Range(0, fruit.Length);//�����_���Ńt���[�c����
        nextFruits = Instantiate(fruit[n], uiFruit.transform.position, uiFruit.transform.rotation);//ui�̈ʒu�Ɏ��̃t���[�c���o��������
        nextFruits.transform.parent = uiFruit.transform;//�ꏏ�ɐU�������܂�
        nextFruits.GetComponent<CombineFruits>().isAlreadyDropped = false;//�h���b�v���ĂȂ��݂��
        nextFruits.GetComponent<CombineFruits>().fruitID = totalFruitsNumber;
        totalFruitsNumber++;

    }
}
