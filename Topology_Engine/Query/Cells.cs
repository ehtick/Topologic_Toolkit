﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BH.oM.Geometry;
using Topologic;

namespace BH.Engine.Topology
{
    public static partial class Query
    {
        /***************************************************/
        /**** Public Methods                            ****/
        /***************************************************/

        public static List<Cell> Cells(this CellComplex cellComplex)
        {
            return cellComplex.Cells();
        }

        /***************************************************/

        public static List<Cell> Cells(this Cluster cluster)
        {
            return cluster.Cells();
        }

        /***************************************************/

        public static List<Cell> Cells(this Topologic.Face face)
        {
            return face.Cells();
        }

        /***************************************************/

        public static List<Cell> Cells(this Shell shell)
        {
            return shell.Cells();
        }

        /***************************************************/
    }
}
