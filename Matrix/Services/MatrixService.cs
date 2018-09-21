namespace Matrix.Services
{
    using Matrix.Interfaces;
    using Matrix.Models;
    using System;
    using System.IO;
    using System.Linq;

    public class MatrixService : IMatrixService
    {
        public int[,] FromFlattenedString(string flattened, int size)
        {
            var splitted = flattened.Split(',').ToArray();
            int[] oneDArray = splitted.Select(int.Parse).ToArray();
            var twoDArray = this.ToRectangular<int>(oneDArray, size);

            return twoDArray;
        }

        public MatrixViewModel GenerateRandom(int n)
        {
            Random random = new Random();

            int[,] array = new int[n, n];

            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    array[i, j] = random.Next();
                }
            }

            return new MatrixViewModel
            {
                Matrix = array,
                Size = array.GetLength(0),
                Flattened = Flatten(array)
            };
        }

        public MatrixViewModel ReadFromFile(string fileName)
        {
            var data = File.ReadLines(fileName).Select(x => x.Split(',')).ToArray();
            int[,] array = new int[data.Length, data[0].Length];

            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < data[i].Length; j++)
                {
                    array[i, j] = Convert.ToInt32(data[i][j]);
                }
            }

            return new MatrixViewModel
            {
                Matrix = array,
                Size = array.GetLength(0),
                Flattened = Flatten(array)
            };
        }

        public MatrixViewModel TransposeSwap(int[,] matrix)
        {
            int w = matrix.GetLength(0);
            int h = matrix.GetLength(1);

            int[,] result = new int[h, w];

            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    result[j, i] = matrix[i, j];
                }
            }

            return new MatrixViewModel
            {
                Matrix = result,
                Size = result.GetLength(0),
                Flattened = Flatten(result)
            };
        }

        private T[,] ToRectangular<T>(T[] flatArray, int width)
        {
            int height = (int)Math.Ceiling(flatArray.Length / (double)width);
            T[,] result = new T[height, width];
            int rowIndex, colIndex;

            for (int index = 0; index < flatArray.Length; index++)
            {
                rowIndex = index / width;
                colIndex = index % width;
                result[rowIndex, colIndex] = flatArray[index];
            }
            return result;
        }

        private int[] Flatten(int[,] matrix)
        {
            return matrix.Cast<int>().ToArray();
        }
        
        public MatrixViewModel TransposeInPlace(int[,] matrix, int size)
        {
            CheckIfSquare(matrix);
            int[] m = Flatten(matrix);
            int rowsCount = size;
            int colsCount = size;
            for (int start = 0; start <= colsCount * rowsCount - 1; ++start)
            {
                int next = start;
                int i = 0;
                do
                {
                    ++i;
                    next = (next % rowsCount) * colsCount + next / rowsCount;
                } while (next > start);

                if (next >= start && i != 1)
                {
                    int tmp = m[start];
                    next = start;
                    do
                    {
                        i = (next % rowsCount) * colsCount + next / rowsCount;
                        m[next] = (i == start) ? tmp : m[i];
                        next = i;
                    } while (next > start);
                }
            }

            return new MatrixViewModel
            {
                Matrix = ToRectangular(m, rowsCount),
                Size = matrix.GetLength(0),
                Flattened = m
            };
        }

        private void CheckIfSquare(int[,] matrix)
        {
            if (matrix.GetLength(0) != matrix.GetLength(1))
                throw new Exception("Only square matrix allowed");
        }
    }
}