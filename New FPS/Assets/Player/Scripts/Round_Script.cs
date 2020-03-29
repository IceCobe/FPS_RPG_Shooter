using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Round_Script : MonoBehaviour
{
    public Text round;

    public void changetext(string current) {
        round.text = current;
    }
}
