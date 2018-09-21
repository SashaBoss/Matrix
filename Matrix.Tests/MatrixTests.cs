namespace Matrix.Tests
{
    using Matrix.Interfaces;
    using Matrix.Services;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;

    [TestClass]
    public class MatrixTests
    {
        private IMatrixService _matrixService;

        public MatrixTests()
        {
            this._matrixService = new MatrixService();
        }

        [TestMethod]
        public void TransposeSwap_SquareMatrixGiven_Transposed()
        {
            int[,] matrix =
            {
                {1, 2, 3 },
                {4, 5, 6 },
                {7, 8, 9}
            };

            int[,] expectedTransposed =
            {
                {1, 4, 7 },
                {2, 5, 8 },
                {3, 6, 9}
            };

            var transposed = this._matrixService.TransposeSwap(matrix);

            CollectionAssert.AreEqual(Flatten(expectedTransposed), Flatten(transposed.Matrix));
        }

        [TestMethod]
        public void TransposeInPlace_SquareMatrixGiven_Transposed()
        {
            int[,] matrix =
            {
                {1, 2, 3 },
                {4, 5, 6 },
                {7, 8, 9}
            };

            int[,] expectedTransposed =
            {
                {1, 4, 7 },
                {2, 5, 8 },
                {3, 6, 9}
            };

            var transposed = this._matrixService.TransposeInPlace(matrix, 3);

            CollectionAssert.AreEqual(Flatten(expectedTransposed), Flatten(transposed.Matrix));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TransposeInPlace_RoundMatrixGiven_ExceptionThrown()
        {
            int[,] matrix =
            {
                {1, 2, 3, 4 },
                {4, 5, 6, 6 },
                {7, 8, 9, 24}
            };

            var transposed = this._matrixService.TransposeInPlace(matrix, 3);
        }

        [TestMethod]
        public void GenerateRandom_SizeGiven_MatrixGenerated()
        {
            var size = 3;
            var matrix = this._matrixService.GenerateRandom(size);

            Assert.AreEqual(size, matrix.Size);
        }

        [TestMethod]
        public void FromFlattenedString_FlattenedStringArrayGivenSizeGiven_MatrixCreated()
        {
            string flattenedMatrix = "1,2,3,4,5,6,7,8,9";
            int size = 3;
            int[,] expectedMatrix =
            {
                {1, 2, 3 },
                {4, 5, 6 },
                {7, 8, 9}
            };

            var matrix = this._matrixService.FromFlattenedString(flattenedMatrix, size);

            Assert.IsInstanceOfType(matrix, typeof(int[,]));
            CollectionAssert.AreEqual(Flatten(expectedMatrix), Flatten(matrix));
        }

        private int[] Flatten(int[,] matrix)
        {
            return matrix.Cast<int>().ToArray();
        }
    }
}
