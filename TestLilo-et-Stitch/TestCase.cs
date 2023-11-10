using Lilo_et_Stitch.Model.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLilo_et_Stitch
{
    /// <summary>
    /// This class will run tests on the class "Case"
    /// </summary>
    public class TestCase
    {
        #region operations
        /// <summary>
        /// Test creation case
        /// </summary>
        /// <authors>Mattis GALOPPIN</authors>
        [Fact]
        public void TestContructor()
        {
            //Creation of the fake coordinates
            Coordinate coord1 = new Coordinate(0, 0);
            Coordinate coord2 = new Coordinate(1, 1);

            //Creation of the fake case with the coordinates above
            Case myCase = new Case(coord1, coord2);

            //Checking if it's been properly created
            Assert.Equal(coord1, myCase.Coord1);
            Assert.Equal(coord2, myCase.Coord2);
        }
        #endregion
    }
}
