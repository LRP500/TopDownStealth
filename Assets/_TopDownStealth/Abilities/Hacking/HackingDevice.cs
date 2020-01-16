using UnityEngine;

namespace TopDownStealth
{
    public class HackingDevice : ToggleAbility
    {
        public override void Activate()
        {
            base.Activate();

            if (Active)
            {

            }
        }

        public override void Deactivate()
        {
            base.Deactivate();
        }

        protected override bool CanActivate()
        {
            return true;
        }
    }
}
