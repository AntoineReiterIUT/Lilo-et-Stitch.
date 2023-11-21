namespace Lilo_et_Stitch.Model.Transform
{
    /// <summary>
    /// This class represents a Coordinate
    /// </summary>
    /// <author>Maxime LAFFAYE</author>
    public class Coordinate
    {
        #region attributes 
        private int x;
        private int y;
        #endregion


        #region properties
        /// <summary>
        /// Property : read or edit the X value
        /// </summary>
        /// <author>Maxime LAFFAYE</author>
        public int X { get { return x; } set {  x = value; } }


        /// <summary>
        /// Property : read or edit the Y value
        /// </summary>
        /// <author>Maxime LAFFAYE</author>
        public int Y { get { return y; } set { y = value; } }
        #endregion


        #region operations
        /// <summary>
        /// Constructor of the coordinate
        /// </summary>
        /// <param name="x">X of the coordinate</param>
        /// <param name="y">Y of the coordinate</param>
        /// <author>Maxime LAFFAYE</author>
        public Coordinate(int x, int y) 
        { 
            this.x = x;
            this.y = y;
        }
        #endregion
    }
}
