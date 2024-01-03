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

namespace AutoRout.APIUtils
{
    public class CADAPIUtils
    {
        public static void SelectBlock()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Editor editor = doc.Editor;

            PromptEntityOptions promptOptions = new PromptEntityOptions("\nSelect a block reference: ");
            promptOptions.SetRejectMessage("\nInvalid selection. Please select a block reference.");
            promptOptions.AddAllowedClass(typeof(BlockReference), true);
            
            PromptEntityResult promptResult = editor.GetEntity(promptOptions);

            if (promptResult.Status == PromptStatus.OK)
            {
                using (Transaction transaction = doc.TransactionManager.StartTransaction())
                {
                    BlockReference blockReference = transaction.GetObject(promptResult.ObjectId, OpenMode.ForRead) as BlockReference;

                    if (blockReference != null)
                    {
                        // Perform operations with the selected block reference
                        editor.WriteMessage($"\nSelected block reference: {blockReference.Name}");
                    }

                    transaction.Commit();
                }
            }

        }
    }
}
