using System.Collections.Generic;

namespace Solution_RepositoryPattern.API.Utils
{
    public static class RestrictionFile
    {
        public static List<string> Extensions = new List<string> { ".jpg", ".png" };

        public static int MaximumSize = 1048576;
    }
}
