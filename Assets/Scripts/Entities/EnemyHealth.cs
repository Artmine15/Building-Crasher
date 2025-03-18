using UnityEngine;

namespace Artmine15.HappyBirthday.v3.Gisha
{
    public class EnemyHealth : MonoBehaviour
    {
        private EntitiesContainer _thisContainer;
        private Enemy _thisEnemy;

        [SerializeField] private int _healthAmount;
        private int _currentHealthAmount;

        public void Initialize(EntitiesContainer entitiesContainer, Enemy enemy)
        {
            _thisContainer = entitiesContainer;
            _thisEnemy = enemy;
            RevertHealth();
        }

        public void RevertHealth()
        {
            _currentHealthAmount = _healthAmount;
        }

        public void ApplyDamage()
        {
            if (_currentHealthAmount > 0)
            {
                _currentHealthAmount--;
                if (_currentHealthAmount <= 0)
                    _thisContainer.DespawnEnemy(_thisEnemy);
            }
            else
            {
                _thisContainer.DespawnEnemy(_thisEnemy);
            }
        }
    }
}
