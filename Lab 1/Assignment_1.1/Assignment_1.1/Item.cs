using System;

namespace Assignment_1._1
{
    public class Item : IComparable
    {
        public Item(int Id, int Benefit, int Weight)
        {
            this.Id = Id;
            this.Benefit = Benefit;
            this.Weight = Weight;
        }

        public int Id { get; }

        public int Benefit { get; }

        public int Weight { get; }

        public int CompareTo(object obj)
        {
            int otherID = (obj as Item).Id;

            if (Id < otherID)
                return -1;
            if (Id == otherID)
                return 0;
            else
                return 1;
        }

        public override bool Equals(object obj)
        {
            return obj is Item item && Id == item.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public static bool operator ==(Item left, Item right) => left.Equals(right);
        public static bool operator !=(Item left, Item right) => !(left == right);
    }
}
