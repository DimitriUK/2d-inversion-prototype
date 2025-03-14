using UnityEngine;

namespace _Game.Core.Scripts.Objects
{
    [CreateAssetMenu]
    public class InputConfig : ScriptableObject
    {
        public int MouseShootButton;
        public int MouseAimButton;
        
        public KeyCode JumpKey = KeyCode.W;
        public KeyCode ReloadKey = KeyCode.R;
    }
}