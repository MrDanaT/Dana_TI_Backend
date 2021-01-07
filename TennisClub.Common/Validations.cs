namespace TennisClub.Common
{
    public static class Validations
    {

        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        public static bool IsValidId(this int id)
        {
            return id > 0;
        }
    }
}
