namespace Matrix.Models
{
    public class MatrixViewModel
    {
        public int[,] Matrix { get; set; }
        public int Size { get; set; }
        public int[] Flattened { get; set; }
    }
}