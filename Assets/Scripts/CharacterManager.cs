using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

struct SyncableCustomData : INetworkSerializable
{
    public int Health;
    public FixedString128Bytes Username; //value-type version of string with fixed allocation. Strings should be avoided in general when dealing with netcode. Fixed strings are a "less bad" option.

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref Health);
        serializer.SerializeValue(ref Username);
    }
}

public class CharacterManager : NetworkBehaviour
{
    const int MAX_LIFE = 100;

    NetworkVariable<SyncableCustomData> m_SyncedCustomData = new NetworkVariable<SyncableCustomData>(writePerm: NetworkVariableWritePermission.Owner); //you can adjust who can write to it with parameters

    [SerializeField] Image m_HealthBarImage;
    [SerializeField] TMP_Text m_UsernameLabel;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (IsClient)
        {
            m_SyncedCustomData.Value = new SyncableCustomData
            {
                Health = MAX_LIFE,
                Username = MultiplayerUseCasesUtilities.GetRandomUsername()
            };

            OnClientCustomDataChanged(m_SyncedCustomData.Value, m_SyncedCustomData.Value);
            m_SyncedCustomData.OnValueChanged += OnClientCustomDataChanged; //this will be called on the client whenever the value is changed by the server
        }
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
        if (IsClient)
        {
            m_SyncedCustomData.OnValueChanged -= OnClientCustomDataChanged;
        }
    }

    void ApplyDamage(int damage)
    {
        if(!IsOwner) return;
        
        if (m_SyncedCustomData.Value.Health > 0)
        {
            int old = m_SyncedCustomData.Value.Health;
            int salud = old - damage;
            m_SyncedCustomData.Value = new SyncableCustomData
            {
                Health = salud,
                Username = MultiplayerUseCasesUtilities.GetRandomUsername()
            };
            
        }        
    }

    void OnClientHealthChanged(int previousHealth, int newHealth)
    {
        m_HealthBarImage.rectTransform.localScale = new Vector3((float)newHealth / 100.0f, 1);//(float)newHealth / 100.0f;
        OnClientUpdateHealthBarColor(newHealth);
        //note: you could use the previousHealth to play a healing/damage animation
    }

    void OnClientUpdateHealthBarColor(int newHealth)
    {
        const int k_MaxHealth = 100;
        float healthPercent = (float)newHealth / k_MaxHealth;
        Color healthBarColor = new Color(1 - healthPercent, healthPercent, 0);
        m_HealthBarImage.color = healthBarColor;
    }

    void OnClientUsernameChanged(string newUsername)
    {
        m_UsernameLabel.text = newUsername;
    }

    void OnClientCustomDataChanged(SyncableCustomData previousValue, SyncableCustomData newValue)
    {
        OnClientHealthChanged(previousValue.Health, newValue.Health);
        OnClientUsernameChanged(newValue.Username.ToString());
    }
}
