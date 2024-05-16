using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResettable
{
    public void ResetModel();
}

// workaround for unity not serializing interfaces
public abstract class Resettable : MonoBehaviour, IResettable
{
    public abstract void ResetModel();
}