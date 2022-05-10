using System;
using UnityEngine;
public abstract class AService : MonoBehaviour 
{
    private void Awake()
    {
        RegisterToServiceLocator();
    }

     void RegisterToServiceLocator()
    {
        ServiceLocator.Instance.RegisterService(this);
    }
}