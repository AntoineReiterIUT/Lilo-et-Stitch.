using Lilo_et_Stitch.Model.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLilo_et_Stitch
{
    public class TestCase
    {
        [Fact]
        public void TestContructor()
        {
            // Arrange (Mise en place des données)
            Coordinate coord1 = new Coordinate(0, 0);
            Coordinate coord2 = new Coordinate(1, 1);

            // Act (Action à tester)
            Case myCase = new Case(coord1, coord2);

            // Assert (Vérification du résultat)
            Assert.Equal(coord1, myCase.Coord1);
            Assert.Equal(coord2, myCase.Coord2);
        }
    }
}
