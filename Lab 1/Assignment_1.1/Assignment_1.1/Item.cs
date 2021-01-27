
namespace Assignment_1._1
{
    class Item
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
    }
}
