using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace odev
{
    class NWAlgoritması
    {
        //1. Adım : Initialization Adımı
        public static Grid[,] Initialization_Adimi(string seq1, string seq2, int match, int mismatch, int gap)
        {
            //iki dizilim olacak-- > m(1.dizi), n(2.dizi)
            int m = seq1.Length;
            int n = seq2.Length;

            Grid[,] matris = new Grid[n, m];

            for (int i = 0; i < matris.GetLength(1); i++)
            {
                //i*gap ile ilk satırı oluştur
                matris[0, i] = new Grid(0, i, i * gap);
            }

            for (int i = 0; i < matris.GetLength(0); i++)
            {
                //i*gap ile ilk sütunu oluştur
                matris[i, 0] = new Grid(i, 0, i * gap);
            }

            for (int j = 1; j < matris.GetLength(0); j++)
            {
                for (int i = 1; i < matris.GetLength(1); i++)
                {
                    matris[j, i] = maks(i, j, seq1, seq2, matris, match, mismatch, gap);
                }
            }
            return matris;
        }

        //2. Adım :  Matris Filling --> matris doldurma,  maksimum değer bulma
        public static Grid maks(int i, int j, string seq1, string seq2, Grid[,] matris, int match2, int mismatch2, int gap2)
        {
            Grid gecici = new Grid();
            int match;
            int gap = gap2;

            if (seq1[i] == seq2[j])
            {
                match = match2;
            }
            else
            {
                match = mismatch2;
            }

            int m1, m2, m3;
            m1 = matris[j - 1, i - 1].GridScore + match;
            m2 = matris[j, i - 1].GridScore + gap;
            m3 = matris[j - 1, i].GridScore + gap;

            int max = m1 >= m2 ? m1 : m2;
            int Mmax = m3 >= max ? m3 : max;
            if (Mmax == m1)
            {
                gecici = new Grid(j, i, m1, matris[j - 1, i - 1], Grid.GridYon.Capraz);
            }
            else
            {
                if (Mmax == m2)
                {
                    gecici = new Grid(j, i, m2, matris[j, i - 1], Grid.GridYon.Sol);
                }
                else
                {
                    if (Mmax == m3)
                    {
                        gecici = new Grid(j, i, m3, matris[j - 1, i], Grid.GridYon.Ust);
                    }
                }
            }

            return gecici;
        }

        //3. Adım : Treaceback Adımı (geri izleme)
        public static void Treaceback_Adimi(Grid[,] matris, string sq1, string sq2, List<char> seq1, List<char> seq2)
        {
            Grid gecerli_hucre = matris[sq2.Length - 1, sq1.Length - 1];

            while (gecerli_hucre.GridPointer != null)
            {
                if (gecerli_hucre.Type == Grid.GridYon.Capraz)
                {
                    seq1.Add(sq1[gecerli_hucre.GridSutun]);
                    seq2.Add(sq2[gecerli_hucre.GridSatir]);
                }

                if (gecerli_hucre.Type == Grid.GridYon.Sol)
                {
                    seq1.Add(sq1[gecerli_hucre.GridSutun]);
                    seq2.Add('-');
                }

                if (gecerli_hucre.Type == Grid.GridYon.Ust)
                {
                    seq1.Add('-');
                    seq2.Add(sq2[gecerli_hucre.GridSatir]);
                }

                gecerli_hucre = gecerli_hucre.GridPointer;
            }
        }
    }
}
