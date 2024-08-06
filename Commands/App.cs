using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using System.Reflection;
using System.Windows.Media.Imaging;
using System.IO;

namespace RevitAPI_Course.Commands
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
        class App : IExternalApplication

    {
        void AddRiboonPanel(UIControlledApplication application)
        {
            String tabName = "Test tools";
            application.CreateRibbonTab(tabName);
            RibbonPanel ribbonPanel = application.CreateRibbonPanel(tabName,"Tools");
            string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;

           
            createPushButton(thisAssemblyPath, ribbonPanel, "cmdMesageText", "Show a" + "\r\n" + "message", "_01_MessageTest", "config.png");
            createPushButton(thisAssemblyPath, ribbonPanel, "cmdSelection", "Show all" + "\r\n" + "selections", "_02_SelectionOfObjects", "config.png");
            createPushButton(thisAssemblyPath, ribbonPanel, "cmdInstances", "Show all" + "\r\n" + "instances", "_03_InstanceExtraction", "config.png");
            createPushButton(thisAssemblyPath, ribbonPanel, "cmdSymbols", "Show all" + "\r\n" + "symballs", "_04_SymbolExtraction", "config.png");
            createPushButton(thisAssemblyPath, ribbonPanel, "cmdElements", "Show all" + "\r\n" + "types", "_05_ElementTypesExtraction", "config.png");
            createPushButton(thisAssemblyPath, ribbonPanel, "cmdInstanceCreation", "Create an" + "\r\n" + "instance", "_06_FamilyInstanceCreation", "config.png");
            createPushButton(thisAssemblyPath, ribbonPanel, "cmdFamilyData", "Create an" + "\r\n" + "instance with data", "_07_FamilyWithData", "config.png");

        }
        public void createPushButton(string AssemblyPath, RibbonPanel ribbonPanel, string cmd, string text, string assemblyt, string filename)
        {
            PushButtonData A1 = new PushButtonData(cmd, text, AssemblyPath, "RevitAPI_Course." + assemblyt);
            PushButton pb1 = ribbonPanel.AddItem(A1) as PushButton;
            pb1.ToolTip = text;
            pb1.LargeImage = pngImageSource("RevitAPI_Course.resources." + filename);
        }
        Result IExternalApplication.OnShutdown(Autodesk.Revit.UI.UIControlledApplication application)
        {
            return Result.Succeeded;
        }
        Result IExternalApplication.OnStartup(Autodesk.Revit.UI.UIControlledApplication application)
        {
            AddRiboonPanel(application);
            return Result.Succeeded;
        }
        public System.Windows.Media.ImageSource pngImageSource(string embeddedPath)
        {
            Stream stream = this.GetType().Assembly.GetManifestResourceStream(embeddedPath);
            var decoder = new System.Windows.Media.Imaging.PngBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            return decoder.Frames[0];
        }
    }
}
