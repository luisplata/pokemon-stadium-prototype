using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace V2
{
    public class InputMapping : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput;

        private InputAction movement, strafe, jump, leftClick;
        private ICharacter3D character;

        public event Action OnInterruptAction;
        public event Action OnSetTargetAction;
        public event Action OnAbility1, OnAbility2, OnAbility3, OnAbility4;
        private Dictionary<int, Action> slots = new();

        private InputAction GetInputAction(string actionName)
        {
            return playerInput.actions.FindAction(actionName);
        }

        public void Configure(ICharacter3D character3D)
        {
            character = character3D;
        }

        private void Start()
        {
            leftClick = GetInputAction("left_click");
            leftClick.performed += context => { OnSetTargetAction?.Invoke(); };

            movement = GetInputAction("Movement");
            movement.started += context => { OnInterruptAction?.Invoke(); };

            strafe = GetInputAction("Strafe");
            strafe.started += context => { OnInterruptAction?.Invoke(); };

            jump = GetInputAction("Jump");
            jump.started += context => { OnInterruptAction?.Invoke(); };

            slots.Add(0, OnAbility1);
            slots.Add(1, OnAbility2);
            slots.Add(2, OnAbility3);
            slots.Add(3, OnAbility4);
            
            ConfigureAbilities();
        }


        private void ConfigureAbilities()
        {
            BindAbility("ability_1", 0);
            BindAbility("ability_2", 1);
            BindAbility("ability_3", 2);
            BindAbility("ability_4", 3);
        }

        private void BindAbility(string actionName, int slot)
        {
            InputAction abilityAction = GetInputAction(actionName);
            abilityAction.performed += context => { slots[slot]?.Invoke(); };
        }
    }
}