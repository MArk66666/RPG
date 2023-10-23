using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Potion", menuName = "ScriptableObjects/PotionItem")]
public class PotionItem : Item
{
    [SerializeField] private List<PotionEffect> effects;

    public enum EffectType
    {
        Health, Mana, Stamina
    }

    public override void Interact(Entity entity)
    {
        foreach (PotionEffect effect in effects)
        {
            switch (effect.EffectType)
            {
                case EffectType.Health:
                    Debug.Log(entity.name + " was healed by " + effect.Magnitude + " units!");
                    break;

                case EffectType.Mana:
                    Debug.Log(entity.name + "'s mana was increased by " + effect.Magnitude + " units!");
                    break;

                case EffectType.Stamina:
                    Debug.Log(entity.name + "'s stamina was increased by " + effect.Magnitude + " units!");
                    break;
            }
        }
    }

    [System.Serializable]
    private class PotionEffect
    {
        public EffectType EffectType;
        public int Magnitude;
    }
}