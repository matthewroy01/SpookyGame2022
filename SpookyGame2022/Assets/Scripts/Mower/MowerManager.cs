using System;
using Environment.Interactables;
using MHR.StateMachine;
using Mower.States;
using UnityEngine;

namespace Mower
{
    [RequireComponent(typeof(MowerIdleState))]
    public class MowerManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private MowerHandle _mowerHandle;

        [Header("States")]
        [SerializeField] private MowerIdleState _idleState;
        private StateMachine _stateMachine;

        private void Awake()
        {
            _stateMachine = new StateMachine(_idleState);
            
            _idleState.SetManager(this);
        }

        private void OnEnable()
        {
            _mowerHandle.OnInteract += MowerHandle_OnInteract;
        }

        private void OnDisable()
        {
            _mowerHandle.OnInteract -= MowerHandle_OnInteract;
        }

        private void Update()
        {
            
        }

        private void FixedUpdate()
        {
            
        }

        private void MowerHandle_OnInteract()
        {
            
        }
    }
}