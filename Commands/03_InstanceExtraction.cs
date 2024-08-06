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
    public class _03_InstanceExtraction : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //Selection or extraction
            UIApplication uiapp = commandData.Application;
            Document doc = uiapp.ActiveUIDocument.Document;

      
            List<FamilyInstance> selectedColumnFamilyInstances = Extraction.allFamilyInstances(doc, BuiltInCategory.OST_StructuralColumns);
     

         
            Analysis.showFamilyInstanseData(selectedColumnFamilyInstances);
      

         
            return Result.Succeeded;
        }
    }
}
