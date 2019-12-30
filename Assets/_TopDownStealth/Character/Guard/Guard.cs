namespace TopDownStealth.Characters
{
    public class Guard : Character
    {
        protected override void Awake()
        {
            base.Awake();

            Side = CharacterSide.Enemy;
        }

        protected override void Update()
        {
            base.Update();
        }

        protected override void Die()
        {
            Destroy(gameObject);
        }
    }
}