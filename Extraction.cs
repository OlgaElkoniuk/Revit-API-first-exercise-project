using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPI_Course
{
    internal class Extraction
    {
        public static List <Element> multipleElementSelection(UIApplication uiapp)
        {
            Selection sel = uiapp.ActiveUIDocument.Selection;
            Reference pickref = null;
            Document doc = uiapp.ActiveUIDocument.Document;
            Boolean flag = true;

            //Analysis

            //Creation
            List <Element> allSelection = new List<Element>();
            Transaction trans = new Transaction(doc);
         

            trans.Start("Starting process");
            while (flag)
            {
                try
                {
                    pickref = sel.PickObject(ObjectType.Element, "Select");
                    Element selected = doc.GetElement(pickref);
                    allSelection.Add(selected);
                }
                catch
                {
                    flag = false;
                }
            }
            //Creation process
            trans.Commit();
            return allSelection;
        }
        public class ISelectionSTructuralColumnsFilter: ISelectionFilter
        {
            public bool AllowElement(Element element)
            {
                if (element.Category.Name == "Structural Columns")
                {
                    return true;
                }
                return false;
            }
            public bool AllowReference(Reference reference, XYZ position) 
            {
                return false;
            }
        }
        public static List<Element> multipleStructuraColumnSelection(UIApplication uiapp)
        {
            Document doc = uiapp.ActiveUIDocument.Document;
            Selection sel = uiapp.ActiveUIDocument.Selection;
            Reference pickref = null;
            ISelectionSTructuralColumnsFilter filter = new ISelectionSTructuralColumnsFilter();
            List<Element> allSelection = new List<Element>();
            Boolean flag = true;

            Transaction trans = new Transaction(doc);
            trans.Start("Selection");
            while (flag)
            {
                try
                {
                    pickref = sel.PickObject(ObjectType.Element, filter, "Select");
                    Element selected = doc.GetElement(pickref);
                    allSelection.Add(selected);
                }
                catch
                {
                    flag = false;
                }
            }
            trans.Commit();
            return allSelection;
        }
        public static List<FamilyInstance> allFamilyInstances (Document doc, BuiltInCategory category)
        {
            List<FamilyInstance > familyInstances = new List<FamilyInstance>();
            FilteredElementCollector collector = new FilteredElementCollector(doc).OfCategory(category).WhereElementIsNotElementType();
            FilteredElementIterator famIT = collector.GetElementIterator();
            famIT.Reset();
            while (famIT.MoveNext())
            {
                Element efam = famIT.Current as Element;
                FamilyInstance famin = famIT.Current as FamilyInstance;
                familyInstances.Add(famin);
            }
            return familyInstances;
        }
        public static List<FamilySymbol> allFamilySymbol(Document doc, BuiltInCategory category)
        {
            List<FamilySymbol> familySymbols = new List<FamilySymbol>();
            FilteredElementCollector collector = new FilteredElementCollector(doc).OfCategory(category).OfClass(typeof(FamilySymbol));
            FilteredElementIdIterator famIT = collector.GetElementIdIterator();
            famIT.Reset();
            while (famIT.MoveNext())
            {
                ElementId eid = famIT.Current;
                FamilySymbol famsymb = doc.GetElement(eid) as FamilySymbol;
                familySymbols.Add(famsymb);
            }
            return familySymbols;
        }
        public static List<ElementType> allElemntTypesOfCategory(Document doc, BuiltInCategory category)
        {
            List<ElementType> elementTypes = new List<ElementType>();
            FilteredElementCollector collector = new FilteredElementCollector(doc).OfCategory(category).OfClass(typeof(FamilySymbol));
            FilteredElementIdIterator famIT = collector.GetElementIdIterator();
            famIT.Reset();
            while (famIT.MoveNext())
            {
                ElementId eid = famIT.Current;
                ElementType elemType = doc.GetElement(eid) as ElementType;
                elementTypes.Add(elemType);
            }
            return elementTypes;
        }
        public static List<FamilySymbol> allFamilySymbolWithFamilyName(Document doc, BuiltInCategory category, string famName)
        {
            List<FamilySymbol> familySymbols = new List<FamilySymbol>();
            FilteredElementCollector collector = new FilteredElementCollector(doc).OfCategory(category).OfClass(typeof(FamilySymbol));
            FilteredElementIdIterator famIT = collector.GetElementIdIterator();
            famIT.Reset();
            while (famIT.MoveNext())
            {
                ElementId eid = famIT.Current;
                FamilySymbol famsymb = doc.GetElement(eid) as FamilySymbol;
                if (famsymb.FamilyName == famName)
                {
                    familySymbols.Add(famsymb);
                }
            }
            return familySymbols;
        }
        public static List<Level> allLevels(Document doc)
        {
            List<Level> levelsInModel = new List<Level>();
            FilteredElementCollector collector = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Levels).WhereElementIsNotElementType();
            FilteredElementIdIterator famIT = collector.GetElementIdIterator();
            famIT.Reset();
            while (famIT.MoveNext())
            {
                ElementId eid = famIT.Current;
                Level level = doc.GetElement(eid) as Level;
                levelsInModel.Add(level);
            }
            return levelsInModel;
        }
        public static Element singleElementSelection(UIApplication uiapp)
        {
            Selection sel = uiapp.ActiveUIDocument.Selection;
            Reference pickref = null;
            Document doc = uiapp.ActiveUIDocument.Document;

          
            Transaction trans = new Transaction(doc);
            trans.Start("Starting process");

            pickref = sel.PickObject(ObjectType.Element, "Select");
            Element selected = doc.GetElement(pickref);

            trans.Commit();
            return selected;
        }
    }
}
