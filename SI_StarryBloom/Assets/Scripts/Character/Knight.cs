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

    [HideInInspector] public KnightTower tower;

    public Rigidbody rigidbody;
    public ConfigurableJoint joint;
    public Transform transform;

    public GameObject dummyPrefab;
    public Rigidbody currentDummy;

    public Knight(GameObject knightObject)
    {
        rigidbody = knightObject.GetComponent<Rigidbody>();
        joint = knightObject.GetComponent<ConfigurableJoint>();
        transform = knightObject.transform;

        healthState = HealthState.ARMORED;
        possessionState = PossessionState.NEUTRAL;
    }

    private void SetTower(Player player)
    {
        if(player!=null)
        {
            possessionState = PossessionState.POSSESSED;
            tower = player.tower;
        }
        else
        {
            possessionState = PossessionState.NEUTRAL;
            tower = null;
        }
    }

    public void SetWeight(int weight)
    {
        rigidbody.mass = weight;
    }
    public void SetJoint(Knight otherKnight)
    {
        if (currentDummy != null)
            GameObject.Destroy(currentDummy);

        joint.connectedBody = otherKnight.rigidbody;
    }
    public void SetJoint(GameObject otherObject)
    {
        if (currentDummy != null)
            GameObject.Destroy(currentDummy);

        joint.connectedBody = otherObject.GetComponent<Rigidbody>();
    }
    public void DeleteJoint()
    {
        currentDummy = GameObject.Instantiate(dummyPrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        joint.connectedBody = currentDummy;
    }

    public bool IsRoot()
    {
        if (tower.knights.Count == 0)//Temp fix
            return true;

        return this == tower.knights[0].knight;
    }

    public bool IsTopKnight()
    {
        return this == tower.knights[tower.knights.Count - 1].knight;
    }

    public void SetPlayer(Player player)
    {
        SetTower(player);

        //Change costume 
        var renderer = transform.gameObject.GetComponent<KnightObject>().rend;
        renderer.material = PlayersManager.Instance.knightSkinsScheme.GetSkin(player != null ? player.ID : "", healthState == HealthState.NAKED);
    }
}
