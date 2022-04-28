using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberOfKnightUI : MonoBehaviour
{
    public int numberOfKnight;
    public TextMeshProUGUI knights;

    private void Update()
    {
        knights.text = numberOfKnight.ToString();
    }
}
