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
    public class _00_MainAddinStructure : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //Selection or extraction
            UIApplication uiapp = commandData.Application;
            Document doc = uiapp.ActiveUIDocument.Document;

            //List <Element> selectedElements = Extraction.multipleElementSelection(uiapp);
            //List<Element> selectedColumnElements = Extraction.multipleStructuraColumnSelection(uiapp);
            //List<FamilyInstance> selectedColumnFamilyInstances = Extraction.allFamilyInstances(doc, BuiltInCategory.OST_StructuralColumns);
            //List<FamilySymbol> selectedColumnFamilySymbols = Extraction.allFamilySymbol(doc, BuiltInCategory.OST_StructuralColumns);
            //List<ElementType> selectedElementTypes = Extraction.allElemntTypesOfCategory(doc, BuiltInCategory.OST_StructuralColumns);
            List<FamilySymbol> selectedColumnFamilySymbolsWithFamilyName = Extraction.allFamilySymbolWithFamilyName(doc, BuiltInCategory.OST_StructuralColumns,"Concrete-Rectangular-Column");
            List<Level> levelsInModel = Extraction.allLevels(doc);

            //Analysis.showElementsData(selectedColumnElements);
            //Analysis.showFamilyInstanseData(selectedColumnFamilyInstances);
            //Analysis.showFamilySymbolData(selectedColumnFamilySymbolsWithFamilyName);
            //Analysis.showElementTypesData(selectedElementTypes);

            //Creation
            Transaction trans = new Transaction(doc);
            trans.Start("StartingProcess");
            if (!selectedColumnFamilySymbolsWithFamilyName[0].IsActive)
            {
                selectedColumnFamilySymbolsWithFamilyName[0].Activate();
                doc.Regenerate();
            }

            FamilyInstance fam = doc.Create.NewFamilyInstance(new XYZ(0, 0, 0), selectedColumnFamilySymbolsWithFamilyName[0],levelsInModel[0], StructuralType.Column);

            trans.Commit();
            return Result.Succeeded;
        }
    }
}
