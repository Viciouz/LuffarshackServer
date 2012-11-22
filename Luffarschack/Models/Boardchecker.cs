namespace Luffarschack.Models
{
    public enum Player {None, Cross, Circle}

    public class Boardchecker
    {
        public Player DetermineWinner(Player[,] playerArray)
        {
            for (int i = 0; i < playerArray.GetLength(0); i++)
            {
                for (int j = 0; j < playerArray.GetLength(1); j++)
                {
                    if (CheckCell(i, j, playerArray, 3))
                    {
                        return playerArray[i, j];
                    }
                }
            }

            return Player.None;
        }

        private bool CheckCell(int y, int x, Player[,] pA, int winSize)
        {
            if (pA[y, x] == Player.None) return false;

            int cellSize = (winSize - 1) / 2;

            int xLine = 1;
            int yLine = 1;
            int z1Line = 1;
            int z2Line = 1;

            for (int i = 1; i <= cellSize; i++)
            {
                if (EnoughHorizontalSpace(x, pA, cellSize) && pA[y, x] == pA[y, x - 1] && pA[y, x] == pA[y, x + 1])
                    xLine += 2;

                if (EnoughVerticalSpace(y, pA, cellSize) && pA[y, x] == pA[y - 1, x] && pA[y, x] == pA[y + 1, x])
                    yLine += 2;

                if (EnoughDiagonalSpace(x, y, pA, cellSize) && pA[y, x] == pA[y - i, x - i] && pA[y, x] == pA[y + i, x + i])
                    z1Line += 2;

                if (EnoughDiagonalSpace(x, y, pA, cellSize) && pA[y, x] == pA[y + i, x - i] && pA[y, x] == pA[y - i, x + i])
                    z2Line += 2;

                if (winSize == xLine || winSize == yLine || winSize == z1Line || winSize == z2Line)
                    return true;
            }

            return false;

        }

        private static bool EnoughHorizontalSpace(int x, Player[,] pA, int cellSize)
        {
            return x - cellSize >= 0 && x + cellSize < pA.GetLength(0);
        }

        private static bool EnoughVerticalSpace(int y, Player[,] pA, int cellSize)
        {
            return y - cellSize >= 0 && y + cellSize < pA.GetLength(1);
        }

        private static bool EnoughDiagonalSpace(int x, int y, Player[,] pA, int cellSize)
        {
            return EnoughHorizontalSpace(x, pA, cellSize) && EnoughVerticalSpace(y, pA, cellSize);
        }

        public bool FullBoard(Player[,] board)
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (board[i, j] == Player.None)
                        return false;

            return true;
        } 
    }
}