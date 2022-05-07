using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.GetComponentInChildren<Character>())
        {
            transform.parent.GetComponent<TransitionController>().InitiateTransition(collision.transform);
        }
    }
}
