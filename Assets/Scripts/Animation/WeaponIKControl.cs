using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This should probably work with an interface that provides a GetHandIK(AvatarIKGoal)
//Then something that listens for weapon changes from the WeaponHolder can find a provider and set it
public interface IIKProvider
{
    void ConfigureIK(Animator animator, AvatarIKGoal goal);
}

[RequireComponent(typeof(Animator))]
public class WeaponIKControl : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private bool _ikActive = false;
    [SerializeField]
    private AvatarIKGoal _ikGoal = AvatarIKGoal.RightHand;

    public IIKProvider IKProvider { get; set; }

    void OnAnimatorIK()
    {
        if (_ikActive && IKProvider != null)
        {
            IKProvider.ConfigureIK(_animator, _ikGoal);
            
        }
        else
        {
            _animator.SetIKPositionWeight(_ikGoal, 0);
            _animator.SetIKRotationWeight(_ikGoal, 0);
        }
    }
}
