using Matrix.Models;

namespace Matrix.Interfaces
{
    public interface IMatrixService
    {
        /// <summary>
        /// Reads from file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>Matrix.</returns>
        MatrixViewModel ReadFromFile(string fileName);

        /// <summary>
        /// Generates random square matrix.
        /// </summary>
        /// <param name="size">Size of dimension.</param>
        /// <returns>Matrix.</returns>
        MatrixViewModel GenerateRandom(int size);

        /// <summary>
        /// Transposes the matrix.
        /// </summary>
        /// <param name="matrix">Matrix to transpose.</param>
        /// <returns>Matrix.</returns>
        MatrixViewModel TransposeSwap(int[,] matrix);

        /// <summary>
        /// Transposes the matrix using O(1) memory using InPlace algorithm.
        /// </summary>
        /// <param name="matrix">Matrix to transpose.</param>
        /// <param name="size">Size of matrix.</param>
        /// <returns>Transposed matrix.</returns>
        MatrixViewModel TransposeInPlace(int[,] matrix, int size);

        /// <summary>
        /// Creates matrix from flattened one dimensional array.
        /// </summary>
        /// <param name="flattened">One dimnesional arry splitted by comma.</param>
        /// <param name="size">Size of dimension.</param>
        /// <returns>Matrix.</returns>
        int[,] FromFlattenedString(string flattened, int size);
    }
}
