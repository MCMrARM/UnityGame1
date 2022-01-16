namespace Mahou.Config
{
    struct Tags
    {
        public const string PLAYER = "Player";
        public const string PROJECTILE = "Projectile";

        public static bool IsValidTarget(string tag)
        {
            return tag != PROJECTILE;
        }
    }
}
