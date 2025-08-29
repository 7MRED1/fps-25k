
using UnityEngine;

namespace Weapons
{
    [System.Serializable]
    public class AmmoState
    {
        public int currentAmmo;
        public int maxAmmo;
        public int reserveAmmo;
        
        public AmmoState(int max, int reserve = 0)
        {
            maxAmmo = max;
            currentAmmo = max;
            reserveAmmo = reserve;
        }
        
        public bool CanFire()
        {
            return currentAmmo > 0;
        }
        
        public void Fire()
        {
            if (currentAmmo > 0)
                currentAmmo--;
        }
        
        public void Reload()
        {
            int ammoNeeded = maxAmmo - currentAmmo;
            int ammoToReload = Mathf.Min(ammoNeeded, reserveAmmo);
            
            currentAmmo += ammoToReload;
            reserveAmmo -= ammoToReload;
        }
        
        public void AddReserveAmmo(int amount)
        {
            reserveAmmo += amount;
        }
    }
}
