using UnityEngine;

namespace AttributeSystem
{
    [CreateAssetMenu(fileName = "TimedBuff", menuName = "AttributeSources/TimedBuff")]
    public class TimedBuff : Buff
    {
        [SerializeReference]
        private ITickable tickHandler;

        public TimedBuff(ITickable _tickHandler) {
            tickHandler = _tickHandler;
        }

        public void CustomUpdate(float deltaTime) {
            tickHandler?.Tick(deltaTime);
        }
    }
}