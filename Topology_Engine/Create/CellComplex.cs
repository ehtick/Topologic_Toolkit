﻿using System.Collections.Generic;
using System.Linq;
using BH.oM.Geometry;
using BH.Engine.Geometry;
using Topologic;

namespace BH.Engine.Topology
{
    public static partial class Create
    {
        /***************************************************/
        /**** Public Methods                            ****/
        /***************************************************/

        public static Topologic.CellComplex CellComplex(IEnumerable<Topologic.Face> faces)
        {
            return Topologic.CellComplex.ByFaces(faces);
        }

        /***************************************************/

        public static Topologic.CellComplex CellComplex(IEnumerable<Topologic.Cell> cells)
        {
            return Topologic.CellComplex.ByCells(cells);
        }

        /***************************************************/
    }
}
