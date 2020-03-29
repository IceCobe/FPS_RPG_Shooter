using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ammo_Count : MonoBehaviour
{
    public Text ammo;
    
    public void changetext(string current) {
        ammo.text = current;
    }
}
