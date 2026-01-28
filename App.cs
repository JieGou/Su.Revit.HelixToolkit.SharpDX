#if RVT_26||RVT_25||RVT_24||RVT_23||RVT_22||RVT_21||RVT_20||RVT_19||RVT_18||RVT_17||RVT_16||RVT_15||RVT_14||RVT_13||RVT_12||RVT_11

#else
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ricaun.Revit.UI;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using RibbonPanel = Autodesk.Revit.UI.RibbonPanel;

namespace Su.Revit.HelixToolkit.SharpDX
{
    internal class App : IExternalApplication
    {
        private static RibbonPanel ribbonPanel;

        public Result OnStartup(UIControlledApplication application)
        {
            ribbonPanel = application.CreateRibbonPanel("RevitHelixToolkitSharpDX");
            ribbonPanel.RowStackedItems(
                ribbonPanel.CreatePushButton<HelixToolkitSharpDXTestCommand>("ShowDocumentWalls")
            );
            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            ribbonPanel?.Remove();
            return Result.Succeeded;
        }

        [Transaction(TransactionMode.Manual)]
        private class HelixToolkitSharpDXTestCommand : IExternalCommand
        {
            public Result Execute(
                ExternalCommandData commandData,
                ref string message,
                ElementSet elementSet
            )
            {
                UIDocument uiDoc = commandData.Application.ActiveUIDocument;
                Document doc = uiDoc.Document;

                var walls = new FilteredElementCollector(doc)
                    .OfClass(typeof(Wall))
                    .WhereElementIsNotElementType()
                    .Cast<Wall>();

                if (!walls.Any())
                {
                    MessageBox.Show("No walls found in the document.");
                    return Result.Failed;
                }

                List<GeometryObject> geometryObjects = new();

                Options options = new Options { };

                foreach (var wall in walls)
                {
                    GeometryElement geomElem = wall.get_Geometry(options);
                    if (geomElem == null)
                        continue;

                    foreach (GeometryObject geomObj in geomElem)
                    {
                        if (geomObj is Solid solid && solid.Volume > 0)
                        {
                            geometryObjects.Add(solid);
                        }
                        else if (geomObj is GeometryInstance instance)
                        {
                            GeometryElement instGeom = instance.GetInstanceGeometry();
                            foreach (var instObj in instGeom)
                            {
                                if (instObj is Solid instSolid && instSolid.Volume > 0)
                                {
                                    geometryObjects.Add(instSolid);
                                }
                            }
                        }
                    }
                }

                var builder = HelixViewport3DBuilder.Init(
                    doc,
                    geometryObjects.Select(x => new GeometryObjectOptions(x)),
                    new Viewport3DXOptions()
                );

                Viewport3DX viewport = builder.Viewport;

                Window window = new Window
                {
                    Title = "Wall Solid Viewer",
                    Width = 600,
                    Height = 600,
                    Content = viewport,
                    WindowStartupLocation = WindowStartupLocation.CenterScreen,
                };

                window.Show();

                return Result.Succeeded;
            }
        }
    }
}
#endif
