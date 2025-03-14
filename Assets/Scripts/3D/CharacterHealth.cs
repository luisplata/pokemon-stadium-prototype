using UnityEngine;

public class CharacterHealth : MonoBehaviour, IHealable
{
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void ReceiveHeal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        Debug.Log($"Character healed! Current HP: {currentHealth}/{maxHealth}");
    }
}