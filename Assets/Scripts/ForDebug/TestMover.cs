using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMover : MonoBehaviour
{
    void Update()
    {
        // ¶‚ÉˆÚ“®
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Translate(-0.01f, 0.0f, 0.0f);
        }
        // ‰E‚ÉˆÚ“®
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Translate(0.01f, 0.0f, 0.0f);
        }
        // ‘O‚ÉˆÚ“®
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.Translate(0.0f, 0.0f, 0.01f);
        }
        // Œã‚ë‚ÉˆÚ“®
        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.Translate(0.0f, 0.0f, -0.01f);
        }
    }
}
