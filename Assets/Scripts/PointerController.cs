using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PointerController : MonoBehaviour
{
    PointerAction pointer;
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out var hit, 10))//レイがあたったとき
        {
            if (hit.transform.gameObject.CompareTag("Button")) this.gameObject.GetComponent<LineRenderer>().enabled = true;
            var current = hit.transform.gameObject.GetComponentInParent<PointerAction>();
            if (pointer != null && pointer != current)
            {
                pointer.PointerOut();
            }
            if (current != null && pointer != current)
            {
                current.PointerIn();
            }
            pointer = current;

        }
        else this.gameObject.GetComponent<LineRenderer>().enabled = false;

        if (pointer != null)
        {
            if (SteamVR_Actions.default_GrabPinch.GetStateDown(SteamVR_Input_Sources.LeftHand))
            {
                pointer.PointerClick();
            }
        }
    }
}
