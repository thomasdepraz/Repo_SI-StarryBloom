using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KnightTower
{
    public List<Knight> knights = new List<Knight>();
    public Knight root;

    public KnightTower(List<Knight> knights)
    {
        this.knights = knights;
        InitializeKnights();
    }

    private void InitializeKnights()
    {
        for (int i = 0; i < knights.Count; i++)
        {
            knights[i].SetTower(this);
            if(i<knights.Count-1)
            {
                knights[i].SetJoint(knights[i + 1]);
            }
        }

        //set base knight weight
        SetRoot(knights[0]);

        GameObject.Destroy(knights[knights.Count-1].joint);
    }

    private void SetRoot(Knight newRoot)
    {
        if (root != null)
        {
            root.SetWeight(1);
            root.rigidbody.constraints = UnityEngine.RigidbodyConstraints.None;
        }

        root = newRoot;
        root.SetWeight(1000);
        root.rigidbody.constraints = UnityEngine.RigidbodyConstraints.FreezeRotation;
    }
}
