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
    public class _07_FamilyWithData : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //Selection or extraction
            UIApplication uiapp = commandData.Application;
            Document doc = uiapp.ActiveUIDocument.Document;

            Element selected = Extraction.singleElementSelection(uiapp);
            FamilySymbol famS = doc.GetElement(selected.GetTypeId()) as FamilySymbol;
            Level lvl = doc.GetElement(selected.LevelId) as Level;

            Location location = selected.Location;
            LocationPoint LP = location as LocationPoint;
            XYZ centerPoint = LP.Point;
            Parameter par = selected.get_Parameter(BuiltInParameter.INSTANCE_LENGTH_PARAM);
            double lenght = par.AsDouble();
            string val = selected.LookupParameter("Reference").AsString();

            List<XYZ> arraOfPoints = new List<XYZ>();

            for(int i = 1; i<3; i++)
            {
                XYZ point = centerPoint.Add(new XYZ(i*lenght, 0, 0));
                arraOfPoints.Add(point);
            }

            
            List<FamilySymbol> selectedColumnFamilySymbolsWithFamilyName = Extraction.allFamilySymbolWithFamilyName(doc, BuiltInCategory.OST_StructuralColumns,"Concrete-Rectangular-Column");
            List<Level> levelsInModel = Extraction.allLevels(doc);


            //Creation
            Transaction trans = new Transaction(doc);
            trans.Start("StartingProcess");
            if (!selectedColumnFamilySymbolsWithFamilyName[0].IsActive)
            {
                selectedColumnFamilySymbolsWithFamilyName[0].Activate();
                doc.Regenerate();
            }

            foreach(XYZ p in arraOfPoints)
            {
                FamilyInstance fam = doc.Create.NewFamilyInstance(p, famS, lvl, StructuralType.Column);
                fam.get_Parameter(BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS).Set("Checked value");
                fam.LookupParameter("Reference").Set(val);
            }
          

            trans.Commit();
            return Result.Succeeded;
        }
    }
}
