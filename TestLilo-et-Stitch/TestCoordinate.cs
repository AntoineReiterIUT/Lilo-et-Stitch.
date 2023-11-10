using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lilo_et_Stitch.Model.Transform;


namespace TestLilo_et_Stitch
{
    /// <summary>
    /// Multiple tests will be ran to check if the class named coordinate works correctly.
    /// </summary>
    public class TestCoordinate
    {
        #region operations
        /// <summary>
        /// Will test if the coordinate is created properly.
        /// </summary>
        /// <author>Maxime LAFFAYE</author>
        [Fact]
        public void NewCoord()
        {
            //Creation of the coordinate
            Coordinate coord1 = new Coordinate(15,16);

            //Checking if it's been properly created
            Assert.Equal(15,coord1.X);
            Assert.Equal(16,coord1.Y);

        }
        #endregion
    }
}
