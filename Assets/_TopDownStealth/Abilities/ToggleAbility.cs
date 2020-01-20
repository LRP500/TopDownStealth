namespace TopDownStealth
{
    public abstract class ToggleAbility : Ability
    {
        private void Awake()
        {
            Deactivate();
        }

        public virtual void Activate()
        {
            if (CanActivate())
            {
                Active = true;
                CooldownRatio.SetValue(0);
            }
        }

        public virtual void Deactivate()
        {
            if (Active)
            {
                Active = false;
                CooldownRatio.SetValue(1);
            }
        }

        protected override bool CanActivate()
        {
            return !Active && Holder.Power.Current >= PowerConsumption;
        }
    }
}