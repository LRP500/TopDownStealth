namespace TopDownStealth
{
    public class Enemy : Character
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
