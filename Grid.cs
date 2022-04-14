using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace odev
{
    class Grid
    {
        private Grid Prevoius_Grid;
        private List<Grid> PreviousGrids = new List<Grid>();
        private int satir; //row
        private int sutun; //column
        private int skor;
        private GridYon PCType;

        public Grid()
        {

        }

        public Grid(int satir, int sutun)
        {
            this.sutun = sutun;
            this.satir = satir;
        }

        public enum GridYon
        {
            Sol, Ust, Capraz  //Left, Up, Diagonal
        };

        public Grid(int satir, int sutun2, int skor)
        {
            this.sutun = sutun2;
            this.satir = satir;
            this.skor = skor;
        }

        public Grid(int satir, int sutun2, int skor, Grid x)
        {
            this.sutun = sutun2;
            this.satir = satir;
            this.skor = skor;
            this.Prevoius_Grid = x;
        }

        public Grid(int satir, int sutun, int skor, Grid x, GridYon y)
        {
            this.sutun = sutun;
            this.satir = satir;
            this.skor = skor;
            this.Prevoius_Grid = x;
            this.PCType = y;
        }

        public Grid GridPointer
        {
            set { this.Prevoius_Grid = value; }
            get { return this.Prevoius_Grid; }

        }

        public List<Grid> PrevCellPointer
        {
            set { this.PreviousGrids = value; }
            get { return this.PreviousGrids; }

        }

        public Grid this[int index]
        {
            set { this.PreviousGrids[index] = value; }
            get { return this.PreviousGrids[index]; }
        }

        public int GridScore
        {
            set { this.skor = value; }
            get { return this.skor; }

        }

        public int GridSatir
        {
            set { this.satir = value; }
            get { return this.satir; }

        }

        public int GridSutun
        {
            set { this.sutun = value; }
            get { return this.sutun; }

        }

        public GridYon Type
        {
            set { this.PCType = value; }
            get { return this.PCType; }

        }
    }
}
