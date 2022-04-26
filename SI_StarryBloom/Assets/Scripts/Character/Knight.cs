using UnityEngine;

[System.Serializable]
public class Knight
{
    public enum HealthState
    {
        ARMORED,
        NAKED
    }

    public enum PossessionState
    {
        NEUTRAL,
        POSSESSED
    }

    public HealthState healthState;
    public PossessionState possessionState;

    public KnightTower tower;

    public Rigidbody rigidbody;
    public ConfigurableJoint joint;
    public Transform transform;

    public Knight(GameObject knightObject)
    {
        rigidbody = knightObject.GetComponent<Rigidbody>();
        joint = knightObject.GetComponent<ConfigurableJoint>();
        transform = knightObject.transform;

        healthState = HealthState.ARMORED;
        possessionState = PossessionState.NEUTRAL;
    }

    public void SetTower(KnightTower tower)
    {
        possessionState = PossessionState.POSSESSED;
        this.tower = tower;
    }

    public void SetWeight(int weight)
    {
        rigidbody.mass = weight;
    }

    public void SetJoint(Knight otherKnight)
    {
        joint.connectedBody = otherKnight.rigidbody;
    }

    public void SetJoint(GameObject otherObject)
    {
        joint.connectedBody = otherObject.GetComponent<Rigidbody>();
    }

    public bool IsRoot()
    {
        return this == tower.knights[0].knight;
    }

    public bool IsTopKnight()
    {
        return this == tower.knights[tower.knights.Count - 1].knight;
    }
}
