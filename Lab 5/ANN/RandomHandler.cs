using System;

namespace ANN
{
    public static class RandomHandler
    {
        private static Random instance;
        public static Random Random
        {
            get
            {
                if(instance == null)
                {
                    instance = new Random();
                }
                return instance;
            }
        }
    }
}
