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

        /// <summary>
        /// Will test if the coordinate is created properly.
        /// </summary>
        [Fact]
        public void NewCoord()
        {
            Coordinate coord1 = new Coordinate(15,16);

            Assert.Equal(15,coord1.X);
            Assert.Equal(16,coord1.Y);

        }
    }
}
