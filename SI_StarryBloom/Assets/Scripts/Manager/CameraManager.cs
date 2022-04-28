using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [Header("References")]
    public CinemachineVirtualCamera virtualCamera;
    public CinemachineTargetGroup targetGroup;

    [Header("Data")]
    public float targetRadius;

    public void AddTransformToGroup(Transform transform)
    {
        targetGroup.AddMember(transform, 1, targetRadius);
    }
    public void RemoveTransformFromGroup(Transform transform)
    {
        targetGroup.RemoveMember(transform);
    }
}
