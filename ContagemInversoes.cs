using System;
using System.Diagnostics;

namespace Trabalho_Final_Analise_Complexidade_Algoritmos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var tamanho = 50000;

            Console.WriteLine("Tamanho do vetor: {0}\n", tamanho);

            var vetorOriginal = gerarVetorAleatorio(tamanho);

            var vetorParaMergeSort = new int[tamanho];
            Array.Copy(vetorOriginal, vetorParaMergeSort, tamanho);

            // ---------------------------------------------------------
            // TESTE 1: FORÇA BRUTA O(n^2)
            // ---------------------------------------------------------
            Console.WriteLine("--- Teste Força Bruta O(n^2) ---");
            var stopwatchBruta = new Stopwatch();
            stopwatchBruta.Start();

            long inversoesBruta = ForcaBruta(vetorOriginal);

            stopwatchBruta.Stop();
            Console.WriteLine("Total de Inversões: {0}", inversoesBruta);
            Console.WriteLine("Tempo de Execução: {0}\n", stopwatchBruta.Elapsed);


            // ---------------------------------------------------------
            // TESTE 2: MERGE SORT O(n log n)
            // ---------------------------------------------------------
            Console.WriteLine("--- Teste MergeSort O(n log n) ---");
            var stopwatchMerge = new Stopwatch();
            stopwatchMerge.Start();

            long inversoesMerge = MergeSort(vetorParaMergeSort, 0, vetorParaMergeSort.Length - 1);

            stopwatchMerge.Stop();
            Console.WriteLine("Total de Inversões: {0}", inversoesMerge);
            Console.WriteLine("Tempo de Execução: {0}\n", stopwatchMerge.Elapsed);
        }

        static long ForcaBruta(int[] A)
        {
            long contador = 0;

            for (int i = 0; i < A.Length; i++)
            {
                for (int j = i + 1; j < A.Length; j++)
                {
                    if (A[i] > A[j])
                    {
                        contador++;
                    }
                }
            }
            return contador;
        }

        static int[] gerarVetorAleatorio(int tamanho)
        {
            var vetor = new int[tamanho];
            Random rnd = new Random();
            for (int i = 0; i < tamanho; i++)
            {
                vetor[i] = rnd.Next(-1000000000, 1000000000);
            }
            return vetor;
        }

        static long MergeSort(int[] A, int p, int r)
        {
            long contador = 0;
            if (p < r)
            {
                int q = (p + r) / 2;
                contador += MergeSort(A, p, q);
                contador += MergeSort(A, q + 1, r);
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

            for (int i = 0; i < n1; i++) L[i] = A[p + i];
            for (int j = 0; j < n2; j++) R[j] = A[q + 1 + j];

            int iL = 0, iR = 0, k = p;
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
                    inversoes += (n1 - iL); // Contabiliza as inversões na intercalação
                }
                k++;
            }

            while (iL < n1) { A[k] = L[iL]; iL++; k++; }
            while (iR < n2) { A[k] = R[iR]; iR++; k++; }

            return inversoes;
        }
    }
}
