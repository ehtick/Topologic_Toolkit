﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BH.oM.Geometry;
using Topologic.Utilities;

namespace BH.Topologic.Core.Topology
{

    public static partial class Convert
    {
        //public static List<IGeometry> Geometry(this global::Topologic.Topology topology)
        public static IGeometry Geometry(this global::Topologic.Topology topology)
        {
            //return null;
            return BasicGeometry(topology); // will this do?
            //List<IGeometry> output = new List<IGeometry>();
            //topology.RecursiveGeometry(ref output);
            //return output;
        }

        //internal static void RecursiveGeometry(this global::Topologic.Topology topology, ref List<IGeometry> output)
        //{
        //    List<IGeometry> objects = new List<IGeometry>();
        //    objects.Add(BasicGeometry(topology));

        //    List<global::Topologic.Topology> subContents = topology.SubContents;
        //    List<IGeometry> subContentGeometries = new List<IGeometry>();
        //    foreach(global::Topologic.Topology subContent in subContents)
        //    {
        //        List<Object> dynamoThisGeometries = new List<Object>();
        //        RecursiveGeometry(subContent, ref subContentGeometries);
        //    }

        //    if (subContentGeometries.Count > 0)
        //    {
        //        objects.Add(subContentGeometries);
        //    }

        //    output.Add(objects);
        //}

        internal static IGeometry BasicGeometry(this global::Topologic.Topology topology)
        {
            global::Topologic.Vertex vertex = topology as global::Topologic.Vertex;
            if (vertex != null)
            {
                return Vertex.Convert.BasicGeometry(vertex);
            }

            global::Topologic.Edge edge = topology as global::Topologic.Edge;
            if (edge != null)
            {
                return Edge.Convert.BasicGeometry(edge);
            }

            global::Topologic.Wire wire = topology as global::Topologic.Wire;
            if (wire != null)
            {
                return Wire.Convert.BasicGeometry(wire);
            }

            global::Topologic.Face face = topology as global::Topologic.Face;
            if (face != null)
            {
                return Face.Convert.BasicGeometry(face);
            }

            global::Topologic.Shell shell = topology as global::Topologic.Shell;
            if (shell != null)
            {
                return Shell.Convert.BasicGeometry(shell);
            }

            global::Topologic.Cell cell = topology as global::Topologic.Cell;
            if (cell != null)
            {
                return Cell.Convert.BasicGeometry(cell);
            }

            global::Topologic.CellComplex cellComplex = topology as global::Topologic.CellComplex;
            if (cellComplex != null)
            {
                return CellComplex.Convert.BasicGeometry(cellComplex);
            }

            global::Topologic.Cluster cluster = topology as global::Topologic.Cluster;
            if (cluster != null)
            {
                return Cluster.Convert.BasicGeometry(cluster);
            }

            //global::Topologic.Aperture aperture = topology as global::Topologic.Aperture;
            //if (aperture != null)
            //{
            //    return Aperture.Convert.BasicGeometry(aperture);
            //}

            throw new NotImplementedException("Geometry for this shape is not supported yet");
        }
    }

    public static partial class Create
    {
        public static global::Topologic.Topology ByGeometry(BH.oM.Geometry.IGeometry geometry, double tolerance = 0.0001)
        {
            BH.oM.Geometry.Point bhomPoint = geometry as BH.oM.Geometry.Point;
            if (bhomPoint != null)
            {
                return BH.Topologic.Core.Vertex.Create.ByPoint(bhomPoint);
            }

            // Handle polyline and polycurve first
            BH.oM.Geometry.Polyline bhomPolyline = geometry as BH.oM.Geometry.Polyline;
            if (bhomPolyline != null)
            {
                return BH.Topologic.Core.Wire.Create.ByPolyLine(bhomPolyline);
            }

            BH.oM.Geometry.PolyCurve bhomPolyCurve = geometry as BH.oM.Geometry.PolyCurve;
            if (bhomPolyCurve != null)
            {
                return BH.Topologic.Core.Wire.Create.ByPolyCurve(bhomPolyCurve);
            }

            // Then curve
            BH.oM.Geometry.ICurve bhomCurve = geometry as BH.oM.Geometry.ICurve;
            if (bhomCurve != null)
            {
                return BH.Topologic.Core.Edge.Create.ByCurve(bhomCurve);
            }

            // Do polysurface first.
            BH.oM.Geometry.PolySurface bhomPolySurface = geometry as BH.oM.Geometry.PolySurface;
            if (bhomPolySurface != null)
            {
                return BH.Topologic.Core.Shell.Create.ByPolySurface(bhomPolySurface, tolerance);
            }

            // Then surface
            BH.oM.Geometry.ISurface bhomSurface = geometry as BH.oM.Geometry.ISurface;
            if (bhomSurface != null)
            {
                return BH.Topologic.Core.Face.Create.BySurface(bhomSurface);
            }

            BH.oM.Geometry.ISolid bhomSolid = geometry as BH.oM.Geometry.ISolid;
            if (bhomSolid != null)
            {
                return BH.Topologic.Core.Cell.Create.BySolid(bhomSolid, tolerance);
            }

            BH.oM.Geometry.CompositeGeometry bhomCompositeGeometry = geometry as BH.oM.Geometry.CompositeGeometry;
            if (bhomCompositeGeometry != null)
            {
                return BH.Topologic.Core.Cluster.Create.ByCompositeGeometry(bhomCompositeGeometry, tolerance);
            }

            throw new NotImplementedException("This BHoM geometry is not yet supported.");
        }

        public static List<global::Topologic.Topology> ByVerticesIndices(IEnumerable<global::Topologic.Vertex> vertices, IEnumerable<List<int>> vertexIndices)
        {
            return global::Topologic.Topology.ByVerticesIndices(vertices, vertexIndices);
        }

        public static global::Topologic.Topology ByImportedBRep(String path)
        {
            return global::Topologic.Topology.ByImportedBRep(path);
        }

    }


    public static partial class Query
    {
        public static int Dimensionality(this global::Topologic.Topology topology)
        {
            return topology.Dimensionality;
        }

        public static List<global::Topologic.Topology> Contents(this global::Topologic.Topology topology)
        {
            return topology.Contents;
        }

        public static List<global::Topologic.Context> Contexts(this global::Topologic.Topology topology)
        {
            return topology.Contexts;
        }

        public static string Analyze(this global::Topologic.Topology topology)
        {
            return topology.Analyze();
        }

        public static List<global::Topologic.Shell> Shells(this global::Topologic.Topology topology)
        {
            return topology.Shells;
        }

        public static List<global::Topologic.Face> Faces(this global::Topologic.Topology topology)
        {
            return topology.Faces;
        }

        public static List<global::Topologic.Wire> Wires(this global::Topologic.Topology topology)
        {
            return topology.Wires;
        }

        public static List<global::Topologic.Edge> Edges(this global::Topologic.Topology topology)
        {
            return topology.Edges;
        }

        public static List<global::Topologic.Vertex> Vertices(this global::Topologic.Topology topology)
        {
            return topology.Vertices;
        }

        public static List<global::Topologic.Cell> Cells(this global::Topologic.Topology topology)
        {
            return topology.Cells;
        }

        public static List<global::Topologic.CellComplex> CellComplexes(this global::Topologic.Topology topology)
        {
            return topology.CellComplexes;
        }

        public static bool IsSame(this global::Topologic.Topology topology, global::Topologic.Topology otherTopology)
        {
            return topology.IsSame(otherTopology);
        }

        public static global::Topologic.Topology ClosestSimplestSubshape(this global::Topologic.Topology topology, global::Topologic.Topology selector)
        {
            return topology.ClosestSimplestSubshape(selector);
        }

        public static String TypeAsString(this global::Topologic.Topology topology)
        {
            return topology.TypeAsString();
        }

        public static int Type(this global::Topologic.Topology topology)
        {
            return topology.Type();
        }

        public static Dictionary<String, Object> Dictionary(this global::Topologic.Topology topology)
        {
            return topology.Dictionary();
        }

    }

    public static partial class Modify
    {
        public static global::Topologic.Topology AddContent(this global::Topologic.Topology topology, global::Topologic.Topology content)
        {
            return topology.AddContent(content);
        }

        public static global::Topologic.Topology AddContent(this global::Topologic.Topology topology, global::Topologic.Topology content, int typeFilter = 255)
        {
            return topology.AddContent(content, typeFilter);
        }

        public static global::Topologic.Topology AddApertures(this global::Topologic.Topology topology, IEnumerable<global::Topologic.Topology> apertureTopologies)
        {
            return topology.AddApertures(apertureTopologies);
        }

        public static global::Topologic.Topology AddContext(this global::Topologic.Topology topology, global::Topologic.Context context)
        {
            return topology.AddContext(context);
        }

        public static global::Topologic.Topology RemoveContext(this global::Topologic.Topology topology, global::Topologic.Context context)
        {
            return topology.RemoveContext(context);
        }

        public static global::Topologic.Topology SetDictionary(this global::Topologic.Topology topology, Dictionary<String, Object> dictionary)
        {
            return topology.SetDictionary(dictionary);
        }

    }

    public static partial class Compute
    {
        public static global::Topologic.Topology Difference(this global::Topologic.Topology topology, global::Topologic.Topology otherTopology)
        {
            return topology.Difference(otherTopology);
        }

        public static global::Topologic.Topology Impose(this global::Topologic.Topology topology, global::Topologic.Topology otherTopology)
        {
            return topology.Impose(otherTopology);
        }

        public static global::Topologic.Topology Imprint(this global::Topologic.Topology topology, global::Topologic.Topology otherTopology)
        {
            return topology.Imprint(otherTopology);
        }

        public static global::Topologic.Topology Intersect(this global::Topologic.Topology topology, global::Topologic.Topology otherTopology)
        {
            return topology.Intersect(otherTopology);
        }

        public static global::Topologic.Topology Merge(this global::Topologic.Topology topology, global::Topologic.Topology otherTopology)
        {
            return topology.Merge(otherTopology);
        }

        public static global::Topologic.Topology SelfMerge(this global::Topologic.Topology topology)
        {
            return topology.SelfMerge();
        }

        public static global::Topologic.Topology Slice(this global::Topologic.Topology topology, global::Topologic.Topology otherTopology)
        {
            return topology.Slice(otherTopology);
        }

        public static global::Topologic.Topology Divide(this global::Topologic.Topology topology, global::Topologic.Topology otherTopology)
        {
            return topology.Divide(otherTopology);
        }

        public static global::Topologic.Topology Union(this global::Topologic.Topology topology, global::Topologic.Topology otherTopology)
        {
            return topology.Union(otherTopology);
        }

        public static global::Topologic.Topology XOR(this global::Topologic.Topology topology, global::Topologic.Topology otherTopology)
        {
            return topology.XOR(otherTopology);
        }

        public static bool ExportToBRep(this global::Topologic.Topology topology, String path)
        {
            return topology.ExportToBRep(path);
        }

        public static global::Topologic.Topology ShallowCopy(this global::Topologic.Topology topology)
        {
            return topology.ShallowCopy();
        }

    }
}