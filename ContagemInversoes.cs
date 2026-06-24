using System;
using System.Diagnostics;

namespace Trabalho_Final_Analise_Complexidade_Algoritmos
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var tamanho = 10;

            Console.WriteLine("Tamanho do vetor: {0}\n", tamanho);

            for (int i = 0; i < 3; i++)
            {
                var vetor = gerarVetor(tamanho, i);

                var stopwatch = new Stopwatch();
                stopwatch.Start();

                long totalInversoes = MergeSort(vetor, 0, vetor.Length - 1);

                stopwatch.Stop();

                string tipo = string.Empty;
                switch (i)
                {
                    case 0:
                        tipo = "Melhor Caso (Crescente)"; break;
                    case 1:
                        tipo = "Pior Caso (Decrescente)"; break;
                    case 2:
                        tipo = "Caso Médio (Aleatório)"; break;
                    default:
                        tipo = "Desconhecido"; break;
                }

                Console.WriteLine("Caso: {0}", tipo);
                Console.WriteLine("Tempo passado: {0}", stopwatch.Elapsed);
                Console.WriteLine("Total de Inversões: {0}\n", totalInversoes);
            }
        }

        static int[] gerarVetor(int tamanho, int tipo)
        {
            var vetor = new int[tamanho];
            Random rnd = new Random();
            for (int i = 0; i < tamanho; i++)
            {
                if (tipo == 0)
                    vetor[i] = i + 1;
                else if (tipo == 1)
                    vetor[i] = tamanho - i;
                else if (tipo == 2)
                    vetor[i] = rnd.Next(-1000000, 1000000); //randomização dos números 
            }

            return vetor;
        }

        static long MergeSort(int[] A, int p, int r)
        {
            long contador = 0;
            if (p < r)
            {
                int q = (p + r) / 2;

                // Soma as inversões da metade esquerda
                contador += MergeSort(A, p, q);

                // Soma as inversões da metade direita
                contador += MergeSort(A, q + 1, r);

                // Soma as inversões encontradas durante a intercalação
                contador += Intercala(A, p, q, r);
            }
            return contador;
        }

        static long Intercala(int[] A, int p, int q, int r)
        {
            int n1 = q - p + 1;
            int n2 = r - q;

            int[] L = new int[n1];
            int[] R = new int[n2];

            for (int i = 0; i < n1; i++)
                L[i] = A[p + i];

            for (int j = 0; j < n2; j++)
                R[j] = A[q + 1 + j];

            int iL = 0, iR = 0;
            int k = p;

            long inversoes = 0; 

            while (iL < n1 && iR < n2)
            {
                if (L[iL] <= R[iR])
                {
                    A[k] = L[iL];
                    iL++;
                }
                else
                {
                    A[k] = R[iR];
                    iR++;

                    inversoes += (n1 - iL);
                }
                k++;
            }

            while (iL < n1)
            {
                A[k] = L[iL];
                iL++;
                k++;
            }

            while (iR < n2)
            {
                A[k] = R[iR];
                iR++;
                k++;
            }

            return inversoes;
        }
    }
}