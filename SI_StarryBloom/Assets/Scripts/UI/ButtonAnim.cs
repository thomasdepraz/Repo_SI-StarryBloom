using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnim : MonoBehaviour
{
    public Animator anim;

    public void Select()
    {
        anim.SetBool("isSelected", true);
    }

    public void DeSelect()
    {
        anim.SetBool("isSelected", false);
    }
}
