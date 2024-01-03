using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;
using AutoRout.APIUtils;
namespace AutoRout
{
    public class AutoRout
    {
        [CommandMethod("ArentAutoRout")]
        public static void Autorout()
        {
            CADAPIUtils.SelectBlock();
        }
    }
}
