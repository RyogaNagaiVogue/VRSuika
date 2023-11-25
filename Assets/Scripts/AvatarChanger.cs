using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarChanger : MonoBehaviour
{
    public enum AvatarKind
    {
        Woman,
        Man,
        Girl,
        Boy

    }
    public AvatarKind avatarKind;
    public GameObject[] useAvatars;
    public GameObject playerScale;
    [SerializeField, Range(120, 190)] private int userHeight = 160;

    // Start is called before the first frame update
    void Start()
    {
        switch (avatarKind)
        {
            case AvatarKind.Woman:
                playerScale.transform.localScale = playerScale.transform.localScale * 170f / userHeight;
                foreach (GameObject useAvatar in useAvatars)
                {
                    if (useAvatar.name != "Woman") useAvatar.SetActive(false);
                }
                break;
            case AvatarKind.Man:
                playerScale.transform.localScale = playerScale.transform.localScale * 175f / userHeight;
                foreach (GameObject useAvatar in useAvatars)
                {
                    if (useAvatar.name != "Man") useAvatar.SetActive(false);
                }
                break;
            case AvatarKind.Girl:
                playerScale.transform.localScale = playerScale.transform.localScale * 133f / userHeight;
                foreach (GameObject useAvatar in useAvatars)
                {
                    if (useAvatar.name != "Girl") useAvatar.SetActive(false);
                }
                break;
            case AvatarKind.Boy:
                playerScale.transform.localScale = playerScale.transform.localScale * 156f / userHeight;
                foreach (GameObject useAvatar in useAvatars)
                {
                    if (useAvatar.name != "Boy") useAvatar.SetActive(false);
                }
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
