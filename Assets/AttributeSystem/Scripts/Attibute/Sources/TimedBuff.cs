using UnityEngine;

namespace AttributeSystem
{
    [CreateAssetMenu(fileName = "TimedBuff", menuName = "AttributeSources/TimedBuff")]
    public class TimedBuff : Buff
    {
        private ITickable tickHandler;
        
        public TimedBuff(ITickable _tickHandler) {
            tickHandler = _tickHandler;
        }

        public void Update(float deltaTime) {
            tickHandler.Tick(deltaTime);
        }
    }
}