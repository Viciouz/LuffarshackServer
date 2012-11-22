namespace Luffarschack.Models
{
    public class BoardConverter
    {
        public Player[,] ConvertToPlayerArray (int[] arr, int rowCount)
        {
            var playerBoard = new Player[rowCount,rowCount];

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < rowCount; j++)
                {
                    if (arr[i + j] == 1)                
                      playerBoard[i,j] = Player.Circle;
                    else if (arr[i + j] == 2)
                        playerBoard[i,j] = Player.Cross;
                    else 
                        playerBoard[i,j] = Player.None;
                }
            }

            return playerBoard;
        }

        public int[] ConvertToArray (Player[,] pArr, int rowCount)
        {
            var arr = new int[rowCount*rowCount];

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < rowCount; j++)
                {
                    if(pArr[i,j] == Player.Circle)
                        arr[i + j] = 1;
                    else if(pArr[i,j] == Player.Cross)
                        arr[i + j] = 2;
                    else
                        arr[i + j] = 0;
                }
            }

            return arr;
        }
    }
}
