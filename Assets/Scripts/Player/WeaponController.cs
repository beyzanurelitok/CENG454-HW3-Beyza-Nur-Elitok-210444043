using UnityEngine;
using CoreBreach.Interfaces;

namespace CoreBreach.Player
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private BaseGun baseGun;
        [SerializeField] private bool useDoubleShot = false;
        [SerializeField] private bool useRapidFire = false;

        private IWeapon _currentWeapon;
        private float _fireTimer = 0f;

        private void Start()
        {
            BuildWeaponChain();
        }

        private void BuildWeaponChain()
        {
            _currentWeapon = baseGun;

            if (useDoubleShot)
            {
                DoubleShotDecorator ds = gameObject.AddComponent<DoubleShotDecorator>();
                ds.SetWeapon(_currentWeapon);
                _currentWeapon = ds;
            }

            if (useRapidFire)
            {
                RapidFireDecorator rf = gameObject.AddComponent<RapidFireDecorator>();
                rf.SetWeapon(_currentWeapon);
                _currentWeapon = rf;
            }
        }

        private void Update()
        {
            _fireTimer += Time.deltaTime;

            if (Input.GetMouseButton(0) && _fireTimer >= _currentWeapon.FireRate)
            {
                _fireTimer = 0f;
                _currentWeapon.Fire(transform.position, transform.forward);
            }
        }
    }
}