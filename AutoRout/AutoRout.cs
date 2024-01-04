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
            double ExtensValue = 24;
            //Select Fasu
            Tuple<double, double, double> getFasu = CADAPIUtils.SelectBlock("Fasu");
            if (getFasu != null)
            {
                double xFasuCoord = getFasu.Item1;
                double yFasuCoord = getFasu.Item2;
                double rotationFasu = getFasu.Item3 * (180 / Math.PI);

                //Select Connector
                Tuple<double, double, double> getConnector = CADAPIUtils.SelectBlock("Connector");
                if (getConnector != null)
                {
                    double xConnectorCoord = getConnector.Item1;
                    double yConnectorCoord = getConnector.Item2;
                    double rotationConnector = getConnector.Item3 * (180 / Math.PI);

                    if (rotationFasu == 0 && rotationConnector == 0)
                    {
                        //Find Intersecting Point of Fasu and Connector C(xC, yC)
                        double xC = xConnectorCoord;
                        double yC = yFasuCoord;
                        CADAPIUtils.DrawRout3Point(xFasuCoord, yFasuCoord, xConnectorCoord, yConnectorCoord, xC, yC);
                    }
                    else if (rotationFasu == 0 && rotationConnector == 90.0)
                    {
                        //Defind extension point xFasuCoord + ExtDistance
                        double xExt = CADAPIUtils.PlusXDriect(xFasuCoord);
                        double yExt = yFasuCoord;
                        //Find Intersecting Point of Fasu and Connector C(xC, yC)
                        double xC = xExt;
                        double yC = yConnectorCoord;
                        CADAPIUtils.DrawRout4Point(xFasuCoord, yFasuCoord, xConnectorCoord, yConnectorCoord, xExt, yExt, xC, yC);
                    }
                    else if (rotationFasu == 0 && rotationConnector == 180.0)
                    {
                        //Defind extension point xFasuCoord + ExtDistance
                        double xExtFasu = CADAPIUtils.PlusXDriect(xFasuCoord);
                        double yExtFasu = yFasuCoord;
                        //Defind extension point yConnectorCoord + ExtDistance
                        double xExtConnector = xConnectorCoord;
                        double yExtConnector = yConnectorCoord -24;
                        //Find Intersecting Point of Fasu and Connector C(xC, yC)
                        double xC = xExtFasu;
                        double yC = yExtConnector;

                        CADAPIUtils.DrawRout5Point(xFasuCoord, yFasuCoord, xConnectorCoord, yConnectorCoord, xExtFasu, yExtFasu, xExtConnector, yExtConnector, xC, yC);
                    }
                    else if (rotationFasu == 90 && rotationConnector == 180)
                    {
                        //Defind extension point yFasuCoord + ExtDistance
                        double xExtFasu = xFasuCoord;
                        double yExtFasu = yFasuCoord + ExtensValue;
                        //Find Intersecting Point of Fasu and Connector C(xC, yC)
                        double xC = xConnectorCoord;
                        double yC = yExtFasu;

                        CADAPIUtils.DrawRout4Point(xFasuCoord, yFasuCoord, xConnectorCoord, yConnectorCoord, xExtFasu, yExtFasu, xC, yC);
                    }
                    else if (rotationFasu == 270 && rotationConnector == 270)
                    {
                        double xC = xFasuCoord;
                        double yC = yConnectorCoord;
                        CADAPIUtils.DrawRout3Point(xFasuCoord, yFasuCoord, xConnectorCoord, yConnectorCoord, xC, yC);
                    }
                    else if (rotationFasu == 270 && rotationConnector == 0)
                    {
                        double xExtFasu = xFasuCoord;
                        double yExtFasu = yFasuCoord - 24;
                        double xC = xConnectorCoord;
                        double yC = yExtFasu;

                        CADAPIUtils.DrawRout4Point(xFasuCoord, yFasuCoord, xConnectorCoord, yConnectorCoord, xExtFasu, yExtFasu, xC, yC);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }

                //CADAPIUtils.SelectBlock("Fasu");
                //Select Connector
                //CADAPIUtils.SelectBlock("Connector");

            }
        }
    }
}
