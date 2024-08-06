using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB.Structure;

namespace RevitAPI_Course
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class _04_SymbolExtraction : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //Selection or extraction
            UIApplication uiapp = commandData.Application;
            Document doc = uiapp.ActiveUIDocument.Document;

      
            List<FamilySymbol> selectedColumnFamilySymbolsWithFamilyName = Extraction.allFamilySymbolWithFamilyName(doc, BuiltInCategory.OST_StructuralColumns,"Concrete-Rectangular-Column");
            List<Level> levelsInModel = Extraction.allLevels(doc);

        
            Analysis.showFamilySymbolData(selectedColumnFamilySymbolsWithFamilyName);
           

          
            return Result.Succeeded;
        }
    }
}
