namespace Trabalho_Final_Analise_Complexidade_Algoritmos
{
    internal class Program
    {
        static (int min, int max) FindMaxMin(int[] arr, int left, int right)
        {
            // Caso base 1
            if (left == right)
                return (arr[left], arr[right]);

            // Caso base 2
            if (right == left + 1)
            {
                if (arr[left] < arr[right])
                    return (arr[left], arr[right]);
                else
                    return (arr[right], arr[left]);
            }

            // Divisão
            int mid = (left + right) / 2;

            // Conquista
            var (minLeft, maxLeft) = FindMaxMin(arr, left, mid);
            var (minRight, maxRight) = FindMaxMin(arr, mid + 1, right);

            // Combinação
            int minGlobal = minLeft < minRight ? minLeft : minRight;
            int maxGlobal = maxLeft > maxRight ? maxLeft : maxRight;

            return (minGlobal, maxGlobal);
        }
        static void Main(string[] args)
        {
            int[] A = { 4, 7, 9, 2, 5, -1, 0, 6 };

            var (min, max) = FindMaxMin(A, 0, A.Length - 1);

            Console.WriteLine($"Array: [{string.Join(",", A)}]");
            Console.WriteLine($"Mínimo: {min}");
            Console.WriteLine($"Máximo: {max}");
        }
    }
}
