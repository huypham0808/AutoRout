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
using System.Windows.Forms;
using Application = Autodesk.AutoCAD.ApplicationServices.Application;

namespace AutoRout.APIUtils
{
    public class CADAPIUtils
    {
        public static Tuple<double, double, double> SelectBlock(string blockName)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Editor editor = doc.Editor;
      
            PromptEntityOptions promptOptions = new PromptEntityOptions($"\nSelect a {blockName}: ");
            promptOptions.SetRejectMessage("\nInvalid selection. Please select a block reference.");
            promptOptions.AddAllowedClass(typeof(BlockReference), true);
      
            PromptEntityResult promptResult = editor.GetEntity(promptOptions);

            if (promptResult.Status == PromptStatus.OK)
            {
                
                ObjectId selectedObjectID = promptResult.ObjectId;
                using (Transaction transaction = doc.TransactionManager.StartTransaction())
                {
                    BlockReference blockReference = transaction.GetObject(promptResult.ObjectId, OpenMode.ForRead) as BlockReference;

                    if (blockReference != null)
                    {
                        //editor.SetImpliedSelection(new ObjectId[] { });
                        editor.SetImpliedSelection(new ObjectId[] { selectedObjectID });
                        editor.WriteMessage($"\nSelected block reference: {blockReference.Name}");
                        double xPosition = blockReference.Position.X;
                        double yPosition = blockReference.Position.Y;
                        double rotation = blockReference.Rotation;
                        return new Tuple<double, double, double>(xPosition, yPosition, rotation);
                    }                   
                    transaction.Commit();
                }
            }
            return null;
        }
        public static void DrawRout3Point (double pointXFaSu, double pointYFaSu, 
            double pointXConnector, double pointYConnector, double pointXIntersect, double pointYIntersect)
        {
            // Get the current document and database
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;


            // Start a transaction
            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                // Open the Block table for read
                BlockTable acBlkTbl;
                acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId,
                                                OpenMode.ForRead) as BlockTable;

                // Open the Block table record Model space for write
                BlockTableRecord acBlkTblRec;
                acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace],
                                                OpenMode.ForWrite) as BlockTableRecord;

                // Create a polyline with two segments (3 points)
                using (Polyline acPoly = new Polyline())
                {
                    acPoly.AddVertexAt(0, new Point2d(pointXFaSu, pointYFaSu), 0, 0, 0);
                    acPoly.AddVertexAt(1, new Point2d(pointXIntersect, pointYIntersect), 0, 0, 0);
                    acPoly.AddVertexAt(2, new Point2d(pointXConnector, pointYConnector), 0, 0, 0);

                    // Add the new object to the block table record and the transaction
                    acBlkTblRec.AppendEntity(acPoly);
                    acTrans.AddNewlyCreatedDBObject(acPoly, true);
                }

                // Save the new object to the database
                acTrans.Commit();
            }
        }
        public static void DrawRout4Point (double pointXFaSu, double pointYFaSu,
            double pointXConnector, double pointYConnector, double xExtend, double yExtend, double pointXIntersect, double pointYIntersect)
        {
            // Get the current document and database
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;


            // Start a transaction
            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                // Open the Block table for read
                BlockTable acBlkTbl;
                acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId,
                                                OpenMode.ForRead) as BlockTable;

                // Open the Block table record Model space for write
                BlockTableRecord acBlkTblRec;
                acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace],
                                                OpenMode.ForWrite) as BlockTableRecord;

                // Create a polyline with two segments (3 points)
                using (Polyline acPoly = new Polyline())
                {
                    acPoly.AddVertexAt(0, new Point2d(pointXFaSu, pointYFaSu), 0, 0, 0);
                    acPoly.AddVertexAt(1, new Point2d(xExtend, yExtend), 0, 0, 0);
                    acPoly.AddVertexAt(2, new Point2d(pointXIntersect, pointYIntersect), 0, 0, 0);
                    acPoly.AddVertexAt(3, new Point2d(pointXConnector, pointYConnector), 0, 0, 0);

                    // Add the new object to the block table record and the transaction
                    acBlkTblRec.AppendEntity(acPoly);
                    acTrans.AddNewlyCreatedDBObject(acPoly, true);
                }

                // Save the new object to the database
                acTrans.Commit();
            }
        }
        public static void DrawRout5Point (double pointXFaSu, double pointYFaSu,
            double pointXConnector, double pointYConnector, double xFaSuExtend, double yFaSuExtend, double xConnectorExt, double yConnectorExt, double pointXIntersect, double pointYIntersect)
        {
            // Get the current document and database
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;


            // Start a transaction
            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                // Open the Block table for read
                BlockTable acBlkTbl;
                acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId,
                                                OpenMode.ForRead) as BlockTable;

                // Open the Block table record Model space for write
                BlockTableRecord acBlkTblRec;
                acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace],
                                                OpenMode.ForWrite) as BlockTableRecord;

                // Create a polyline with two segments (3 points)
                using (Polyline acPoly = new Polyline())
                {
                    acPoly.AddVertexAt(0, new Point2d(pointXFaSu, pointYFaSu), 0, 0, 0);
                    acPoly.AddVertexAt(1, new Point2d(xFaSuExtend, yFaSuExtend), 0, 0, 0);
                    acPoly.AddVertexAt(2, new Point2d(pointXIntersect, pointYIntersect), 0, 0, 0);
                    acPoly.AddVertexAt(3, new Point2d(xConnectorExt, yConnectorExt), 0, 0, 0);
                    acPoly.AddVertexAt(4, new Point2d(pointXConnector, pointYConnector), 0, 0, 0);

                    // Add the new object to the block table record and the transaction
                    acBlkTblRec.AppendEntity(acPoly);
                    acTrans.AddNewlyCreatedDBObject(acPoly, true);
                }

                // Save the new object to the database
                acTrans.Commit();
            }
        }
        public static double PlusXDriect (double xCoordinate)
        {
            return xCoordinate + 24;
        }
        public static double PlusYDirect(double yCoordinate)
        {
            if(yCoordinate > 0)
            {
                return yCoordinate + 24;
            }
            else if(yCoordinate < 0)
            {
                return yCoordinate - 24;
            }
            return yCoordinate;
        }
    }
}
