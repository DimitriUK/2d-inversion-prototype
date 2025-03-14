using _Game.Core.Scripts.Interfaces;
using UnityEngine;

namespace _Game.Core.Scripts.Gameplay
{
    [RequireComponent(typeof(Collider2D))]
    public class InversionTrigger : MonoBehaviour
    {
        [Header("VFX")]
        [SerializeField] private ParticleSystem _activationEffect; //TODO: I need to add a VFX and maye some SFX too
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent<IInvertable>(out var invertable))
            {
                float test = transform.position.y - other.transform.position.y;

                switch (test)
                {
                    case > 0.1f when !invertable.IsInverted:
                    case < -0.1f when invertable.IsInverted:
                        invertable.Invert();
                        break;
                }
                
                if (_activationEffect != null)
                    Instantiate(_activationEffect, transform.position, transform.rotation);
            }
        }
    }
}