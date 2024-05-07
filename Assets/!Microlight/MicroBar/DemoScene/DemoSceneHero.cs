using UnityEngine;

namespace Microlight.MicroBar.DemoScene {
    public class DemoSceneHero : MonoBehaviour {
        [SerializeField] MicroBar _hpBar;

        [SerializeField] bool _randomDamageAndHeal;
        [SerializeField] float _damageHealAmount;
        [SerializeField] Vector2 _damageHealRandomRange;
        [SerializeField] bool _enableDebugMessages;
        readonly float _maxHP = 100;
        float _hp = 100;

        private void Start() {
            // HealthBar needs to be initalized at start
            if(_hpBar != null) _hpBar.Initialize(_maxHP);
        }
        public void Damage() {
            // Determine damage
            float damageAmount;
            if(_randomDamageAndHeal) damageAmount = Random.Range(_damageHealRandomRange.x, _damageHealRandomRange.y);
            else damageAmount = _damageHealAmount;

            // Deal damage
            _hp -= damageAmount;
            if(_hp < 0) _hp = 0;
            if(_enableDebugMessages) Debug.Log("Did " + damageAmount + " points of damage!");

            // Update HealthBar
            if(_hpBar != null) _hpBar.UpdateHealthBar(_hp);
        }
        public void Heal() {
            // Determine heal
            float damageAmount;
            if(_randomDamageAndHeal) damageAmount = Random.Range(_damageHealRandomRange.x, _damageHealRandomRange.y);
            else damageAmount = _damageHealAmount;

            // Heal
            _hp += damageAmount;
            if(_hp > _maxHP) _hp = _maxHP;
            if(_enableDebugMessages) Debug.Log("Healed " + damageAmount + " points of damage!");

            // Update HealthBar
            if(_hpBar != null) _hpBar.UpdateHealthBar(_hp);
        }
    }
}
