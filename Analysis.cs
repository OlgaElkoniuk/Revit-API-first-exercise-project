using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RevitAPI_Course
{
    internal class Analysis
    {
        public static void showElementsData(List <Element> allElements)
        {
            foreach (Element e in allElements) 
            {
                MessageBox.Show(e.Category.Name + "|" + e.Id.ToString());
            }
        }
        public static void showFamilyInstanseData(List<FamilyInstance> allFamilyInstance)
        {
            foreach (FamilyInstance e in allFamilyInstance)
            {
                MessageBox.Show(e.Category.Name + "|" + e.Id.ToString());
            }
        }
        public static void showFamilySymbolData(List<FamilySymbol> allFamilySymbols)
        {
            foreach (FamilySymbol e in allFamilySymbols)
            {
                MessageBox.Show(e.FamilyName + "|" + e.Name);
            }
        }
        public static void showElementTypesData(List<ElementType> allElementTypes)
        {
            foreach (ElementType e in allElementTypes)
            {
                MessageBox.Show(e.FamilyName + "|" + e.Name);
            }
        }
    }
}
