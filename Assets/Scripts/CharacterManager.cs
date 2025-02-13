using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;


public class CharacterManager : NetworkBehaviour
{
    public const int MAX_LIFE = 100;
    private NetworkVariable<int> health;
    public NetworkVariable<FixedString128Bytes> username;

    [SerializeField] Image m_HealthBarImage;
    [SerializeField] TMP_Text m_UsernameLabel;

    private void Awake()
    {
        health = new NetworkVariable<int>(MAX_LIFE);
        username = new NetworkVariable<FixedString128Bytes>("MultiplayerUseCasesUtilities.GetRandomUsername()"); 
    }
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        health.OnValueChanged += OnClientHealthChanged;
        username.OnValueChanged += OnClientUsernameChanged;
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
        health.OnValueChanged -= OnClientHealthChanged;
        username.OnValueChanged -= OnClientUsernameChanged;
    }

    private void OnClientUsernameChanged(FixedString128Bytes previousValue, FixedString128Bytes newValue)
    {
        m_UsernameLabel.text = newValue.ToString();
    }

    void ApplyDamage(int damage)
    {
        if(IsOwner && health.Value > 0)
        {
            ApplyDamageRpc(damage);   
        }        
    }

    [Rpc(SendTo.Server)]
    void ApplyDamageRpc(int damage)
    {
        if(!IsServer) return;
        
        if (health.Value > 0)
        {
            health.Value -= damage;
        }        
    }

    void OnClientHealthChanged(int previousHealth, int newHealth)
    {
        m_HealthBarImage.rectTransform.localScale = new Vector3((float)newHealth / 100.0f, 1);//(float)newHealth / 100.0f;
        const int k_MaxHealth = 100;
        float healthPercent = (float)newHealth / k_MaxHealth;
        Color healthBarColor = new Color(1 - healthPercent, healthPercent, 0);
        m_HealthBarImage.color = healthBarColor;
    }
}
