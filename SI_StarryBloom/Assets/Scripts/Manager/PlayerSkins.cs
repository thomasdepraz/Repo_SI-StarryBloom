using UnityEngine;

[CreateAssetMenu(fileName = "New Knight Skins", menuName = "Data/Knight Skins")]
public class PlayerSkins : ScriptableObject
{
    [Header("Armored Skins")]
    public Material sunSkin;
    public Material moonSkin;
    public Material crossSkin;
    public Material cloverSkin;
    public Material neutralSkin;

    [Header("Naked Skins")]
    public Material sunSkinNaked;
    public Material moonSkinNaked;
    public Material crossSkinNaked;
    public Material cloverSkinNaked;
    public Material neutralSkinNaked;

    public Material GetSkin(string playerID, bool isNaked)
    {
        Material skin = null;
        switch (playerID)
        {
            case "Player_1":
                skin  = isNaked ? sunSkinNaked : sunSkin;
                break;
            case "Player_2":
                skin = isNaked ? moonSkinNaked : moonSkin;
                break;
            case "Player_3":
                skin = isNaked ? crossSkinNaked : crossSkin;
                break;
            case "Player_4":
                skin = isNaked ? cloverSkinNaked : cloverSkin;
                break;
            case "":
                skin = isNaked ? neutralSkinNaked : neutralSkin;
                break;
            default:
                break;
        }
        return skin;
    }
}

