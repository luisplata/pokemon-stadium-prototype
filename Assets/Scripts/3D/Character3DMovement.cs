using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character3DMovement : MonoBehaviour, ICharacter3D, ISpellCharacter
{
    [SerializeField] private GetTargetFromMouse getTarget;
    [SerializeField] private GameObject target;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Character3DAnimation animation;
    [SerializeField] private AbilityManager abilityManager;
    [SerializeField] private AbilityDatabase initialAbilities;
    [SerializeField] private GameObject pointToSpawnSpells;
    private AbilityDatabase initialAbilitiesInstance;

    public event Action OnMovement;
    public ICharacter3DAnimation Animation => animation;

    public GameObject GetTarget() => target;

    public Transform GetTransform() => transform;

    private InputAction movement, strafe, jump, leftClick;

    private void Start()
    {
        ConfigureAbilities();

        leftClick = GetInputAction("left_click");
        leftClick.performed += context => { target = getTarget.GetTarget(); };

        movement = GetInputAction("Movement");
        movement.started += context => { OnMovement?.Invoke(); };

        strafe = GetInputAction("Strafe");
        strafe.started += context => { OnMovement?.Invoke(); };

        jump = GetInputAction("Jump");
        jump.started += context => { OnMovement?.Invoke(); };

        //configure initial abilities
        initialAbilitiesInstance = Instantiate(initialAbilities);

        //add abilities
        var selectedAbilities = initialAbilitiesInstance.abilities.ToList();

        for (int i = 0; i < selectedAbilities.Count; i++)
        {
            abilityManager.EquipAbility(i, selectedAbilities[i]);
        }
    }

    private void Awake()
    {
        abilityManager.Configure(this);
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
        abilityAction.performed += context => { abilityManager.UseAbility(slot); };
    }

    private InputAction GetInputAction(string actionName)
    {
        return playerInput.actions.FindAction(actionName);
    }

    public ISpellCharacter GetSpellCharacter()
    {
        return this;
    }

    public void TakeDamage(float damage)
    {
        Debug.Log($"Damage {damage}");
    }

    public Transform GetPointToSpawn()
    {
        return pointToSpawnSpells.transform;
    }
}