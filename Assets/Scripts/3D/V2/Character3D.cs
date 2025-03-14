using UnityEngine;
using Object = UnityEngine.Object;

namespace V2
{
    public class Character3D : MonoBehaviour, ICharacter3D, ISpellCharacter
    {
        [SerializeField, InterfaceType(typeof(ISkillManager))]
        private Object skillManager;

        private ISkillManager SkillManagerInstance => skillManager as ISkillManager;

        [SerializeField] private InputMapping playerInput;
        [SerializeField] private Character3DAnimation animation;
        [SerializeField] private GetTargetFromMouse getTarget;
        [SerializeField] private GameObject pointToSpawnSpells;
        [SerializeField] private GameObject target;
        [SerializeField] private AbilitiesConfiguration abilitiesConfiguration;
        [SerializeField] private GameObject principalBody;
        [SerializeField] private RPGControllerExtended rpgController;

        private ITarget targetSelected;
        private AbilitiesFactory _abilityFactory;

        private void Awake()
        {
            SkillManagerInstance.Configure(this);
        }

        private void Start()
        {
            _abilityFactory = new AbilitiesFactory(Instantiate(abilitiesConfiguration));

            SkillManagerInstance.EquipSkill(0, _abilityFactory.Create("placaje"));
            SkillManagerInstance.EquipSkill(1, _abilityFactory.Create("heal"));
            SkillManagerInstance.EquipSkill(2, _abilityFactory.Create("heal"));
            SkillManagerInstance.EquipSkill(3, _abilityFactory.Create("heal"));
        }

        private void OnEnable()
        {
            playerInput.OnInterruptAction += PlayerInputOnOnInterruptAction;
            playerInput.OnSetTargetAction += PlayerInputOnOnSetTargetAction;
            playerInput.OnAbility1 += PlayerInputOnOnAbility1;
            playerInput.OnAbility2 += PlayerInputOnOnAbility2;
            playerInput.OnAbility3 += PlayerInputOnOnAbility3;
            playerInput.OnAbility4 += PlayerInputOnOnAbility4;
        }


        private void PlayerInputOnOnAbility4()
        {
            SkillManagerInstance.ActivateSkill(4);
        }

        private void PlayerInputOnOnAbility3()
        {
            SkillManagerInstance.ActivateSkill(3);
        }

        private void PlayerInputOnOnAbility2()
        {
            SkillManagerInstance.ActivateSkill(2);
        }

        private void PlayerInputOnOnAbility1()
        {
            SkillManagerInstance.ActivateSkill(1);
        }

        private void OnDestroy()
        {
            playerInput.OnInterruptAction -= PlayerInputOnOnInterruptAction;
            playerInput.OnSetTargetAction -= PlayerInputOnOnSetTargetAction;
            playerInput.OnAbility1 -= PlayerInputOnOnAbility1;
            playerInput.OnAbility2 -= PlayerInputOnOnAbility2;
            playerInput.OnAbility3 -= PlayerInputOnOnAbility3;
            playerInput.OnAbility4 -= PlayerInputOnOnAbility4;
        }

        private void PlayerInputOnOnSetTargetAction()
        {
            target = getTarget.GetTarget();
        }

        private void PlayerInputOnOnInterruptAction()
        {
            SkillManagerInstance.InterruptAbility();
        }

        public Transform GetPointToSpawn()
        {
            return pointToSpawnSpells.transform;
        }

        public bool IsMoving()
        {
            return false;
        }

        public ITarget GetTarget()
        {
            return targetSelected;
        }

        public GameObject GetGameObject()
        {
            return principalBody;
        }

        public bool IsInFieldOfView(Transform transform)
        {
            //TODO: Implement
            return true;
        }

        public bool IsAlly(ITarget target)
        {
            //TODO: Implement
            return false;
        }

        public bool IsGrounded()
        {
            //TODO: Implement
            return true;
        }

        public float CurrentMana()
        {
            //TODO: Implement
            return 100;
        }

        public void GoTo(Vector2 direction2D)
        {
            Debug.Log($"direction {direction2D}");
            rpgController.SetExternalMovement(direction2D);
        }

        public void IsControlActivate(bool isEnable)
        {
            if (isEnable)
            {
                rpgController.ActivateControls();
            }
            else
            {
                rpgController.DeactivateControls();
            }
        }
    }
}