using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other) {
        ManagerScript.Instance.Score += 50;
        Destroy(other.gameObject);
        GameMechanics.coinDestroyed = true;
        GameMechanics.timeFloat = 5f;
    }

}
