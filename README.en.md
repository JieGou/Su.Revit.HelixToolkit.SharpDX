# 🚀 Su.Revit.HelixToolkit.SharpDX User Guide

## 🌐 Project Repository

**GitHub**: https://github.com/ViewSuSu/Su.Revit.HelixToolkit.SharpDX  
**Gitee**: https://gitee.com/SususuChang/su.-revit.-helix-toolkit.-sharp-dx

---

## 📦 Installation

### Via NuGet (Recommended)

```bash
# Package Manager
Install-Package Su.Revit.HelixToolkit.SharpDX

# .NET CLI
dotnet add package Su.Revit.HelixToolkit.SharpDX
```

### Compatibility

- ✅ **Supported Versions**: Revit 2013 - Revit 2026
- ✅ **.NET Framework**: 4.8+
- ✅ **Dependencies**: HelixToolkit.Wpf.SharpDX, Revit API

---

## 📖 Introduction

Su.Revit.HelixToolkit.SharpDX is a high-performance 3D visualization toolkit specifically designed for Revit plugin development. Built on HelixToolkit.Wpf.SharpDX, it provides simple and easy-to-use APIs to create feature-rich 3D viewport windows in Revit plugins.

**Core Features**:
- 🚀 **High-Performance Rendering**: Index optimization for Solid triangular faces, capable of handling massive triangular face data in Solid models
- 🎯 **Complete Interaction**: Supports mouse hover highlighting, click selection, multi-selection, rotation, zoom, pan, and other complete interaction functions
- 📐 **Coordinate System Adaptation**: Automatic handling of coordinate system conversion between Revit and Helix for seamless integration
- 🎨 **Material System**: Supports multiple rendering methods including native Revit materials, custom colors, and texture materials
- ⚡ **Memory Optimization**: Efficient geometric data management and memory release mechanisms

---

## 🎯 Quick Start

### ⚡ Basic Usage

```csharp
// 1. 📦 Initialize the builder
var builder = HelixViewport3DBuilder.Init(
    revitDocument, 
    geometryObjects, 
    new Viewport3DXOptions()
);

// 2. 🖥️ Get the 3D viewport control
Viewport3DX viewport = builder.Viewport;

// 3. 📝 Add viewport to your WPF window
```

### 🔥 Complete Example

```csharp
// Prepare geometry objects to display
var geometryObjects = new List<GeometryObjectOptions>
{
    // Add your geometry objects...
};

// 🎨 Configure viewport options
var visualOptions = new Viewport3DXOptions
{
    BackgroundColor = System.Windows.Media.Colors.LightGray,
    FXAALevel = 4 // Anti-aliasing level
};

// 🏗️ Create builder
var builder = HelixViewport3DBuilder.Init(
    document, 
    geometryObjects, 
    visualOptions
);

// 📐 Set camera view
builder.SetCamera(revitView);

// ✨ Enable interaction features
builder.SetHoverHighlightEnabled(true)
       .SetClickHighlightEnabled(true);
```

---

## 🎮 Interaction Features

### 🖱️ Mouse Operations

| Operation | Function | Icon |
|-----------|----------|------|
| 🖱️ Middle Double-Click | Zoom to extent | 🔍 |
| 🖱️ Middle Drag | Pan view | 👐 |
| 🖱️ Shift + Right Click | Rotate view | 🔄 |
| 🖱️ Mouse Hover | Semi-transparent highlight | 👆 |
| 🖱️ Left Click | Select model | ✅ |
| 🖱️ Ctrl + Click | Multi-select models | 📋 |

### 🎨 Highlight Features

```csharp
// 🌈 Set highlight color
builder.SetHighlightColor(Colors.Red, 0.8f);  // Red highlight

// 💫 Enable blinking effect
builder.SetHighlightBlinking(true, 100);  // 100ms blink interval

// 🔧 Programmatically highlight specific objects
builder.HighlightGeometryObject(specificGeometry);
```

---

## 📊 View Control

### 🎥 Camera Settings

```csharp
// Method 1: Use Revit view
builder.SetCamera(revitView);

// Method 2: Custom camera
builder.SetCamera(
    new XYZ(0, 0, 10),     // 📍 Camera position
    new XYZ(0, 0, -1),     // 👀 Look direction
    new XYZ(0, 1, 0)       // ⬆️ Up direction
);
```

### 🧭 Navigation Controls

- ✅ **View Cube**: Displayed at top-right, click for quick view switching
- ✅ **Auto Zoom**: Automatically adjusts to suitable view range on load
- ✅ **Anti-aliasing**: Configurable graphics quality settings

---

## 🛠️ Advanced Features

### 📡 Event Listening

```csharp
// 👂 Listen to model selection events
builder.OnModelSelected += (sender, args) => 
{
    var selectedModel = args.SelectedModel;
    var geometryObject = args.GeometryObject;
    var hitPoint = args.HitPoint;
    
    // 🎯 Handle selection logic
    Console.WriteLine($"Selected model: {geometryObject}");
};

// 👂 Listen to deselection events
builder.OnModelDeselected += (sender, args) => 
{
    // 🗑️ Clear selection state
};
```

### 🔍 Selection Management

```csharp
// 📋 Get currently selected models
var selectedModels = builder.GetSelectedModels();

// 📋 Get currently selected geometry objects
var selectedGeometry = builder.GetSelectedGeometryObjects();

// 🧹 Clear all selections
builder.ClearHighlight();
```

---

## ⚙️ Configuration Options

### 🎨 Visual Configuration

```csharp
var options = new Viewport3DXOptions
{
    BackgroundColor = Colors.Black,      // 🎨 Background color
    FXAALevel = 8,                       // 🔍 Anti-aliasing level (0-8)
    EnableRenderFrustum = true          // 🎯 Frustum culling
};
```

### 🔧 Feature Toggles

```csharp
// Enable/disable hover highlight
builder.SetHoverHighlightEnabled(true);

// Enable/disable click highlight  
builder.SetClickHighlightEnabled(true);
```

---

## 🎨 GeometryObjectOptions Usage Guide

### 📝 Basic Configuration

`GeometryObjectOptions` is used to configure how geometry objects are rendered:

#### Using Revit Material

```csharp
var options = new GeometryObjectOptions(
    geometryObject,    // 📐 Revit geometry object
    revitMaterial      // 🎨 Revit material (optional)
);
```

#### Using Custom Color

```csharp
var options = new GeometryObjectOptions(
    geometryObject,           // 📐 Revit geometry object
    Colors.Blue,              // 🔵 Custom color
    0.8f                      // 💧 Transparency (0-1)
);
```

#### Using Texture Material

```csharp
var options = new GeometryObjectOptions(
    geometryObject,           // 📐 Revit geometry object
    textureStream,            // 🖼️ Texture stream
    Colors.White,             // ⚪ Emissive color
    1.0f                      // 💧 Transparency
);
```

### ⚙️ Rendering Parameter Configuration

```csharp
var options = new GeometryObjectOptions(geometryObject, material)
{
    LevelOfDetail = 0.8,                              // 🎯 Detail level (0-1)
    MinAngleInTriangle = 0,                           // 📐 Minimum triangle angle
    MinExternalAngleBetweenTriangles = Math.PI / 4,   // 📏 Minimum external angle between triangles
    IsDrawSolidEdges = true,                          // 📏 Draw outline edges
    SolidEdgeThickness = 2f,                          // 🖊️ Edge thickness
    SolidEdgeSmoothness = 10f                         // ✨ Edge smoothness
};
```

### 🔧 Parameter Description

| Parameter | Description | Default | Impact |
|-----------|-------------|---------|--------|
| `LevelOfDetail` | Rendering detail level | 0.5 | Higher values create denser meshes, better precision but higher performance cost |
| `MinAngleInTriangle` | Minimum angle in triangle | 0 | Controls smoothness during mesh generation |
| `MinExternalAngleBetweenTriangles` | Minimum external angle between adjacent triangles | 2π | Determines smooth transition between surfaces |
| `IsDrawSolidEdges` | Whether to draw outline edges | true | Display boundary lines |
| `SolidEdgeThickness` | Edge line thickness | 2f | Line width in pixels |
| `SolidEdgeSmoothness` | Edge line smoothness | 10f | Higher values create smoother edges |

---

## 💡 Usage Tips

### 🚀 Performance Optimization

- ✅ Use `EnableSwapChainRendering` to improve rendering performance
- ✅ Set appropriate `FXAALevel` to balance quality and performance
- ✅ Call `Clear()` promptly to release resources
- ✅ Adjust `LevelOfDetail` based on requirements to avoid unnecessary details
- ✅ Utilize Solid triangular face index optimization to handle massive data

### 🎯 Best Practices

1. **📱 Responsive Design**: Viewport automatically adapts to container size
2. **🔄 Real-time Updates**: Support dynamic add/remove of geometry objects
3. **🎮 User Friendly**: Provide intuitive mouse interaction feedback
4. **🎨 Visual Consistency**: Maintain visual style similar to Revit
5. **⚡ Performance Balance**: Adjust rendering parameters based on scene complexity
6. **💾 Memory Management**: Timely cleanup of unused geometry objects

### 🔄 Scene Management

```csharp
// 🧹 Clear scene
builder.Clear();

// 📦 Re-add objects
builder.Add(newGeometryObjects);

// 🎯 Reset camera
builder.SetCamera(newView);
```

---

## ❓ Frequently Asked Questions

### ❓ How to change highlight color?
```csharp
builder.SetHighlightColor(Colors.Blue, 0.7f);  // 🔵 Blue highlight
```

### ❓ How to disable all interactions?
```csharp
builder.SetHoverHighlightEnabled(false)
       .SetClickHighlightEnabled(false);
```

### ❓ How to get world coordinates of click position?
```csharp
builder.OnModelSelected += (sender, args) => 
{
    var worldPosition = args.HitPoint;  // 🌍 World coordinates
};
```

### ❓ How to optimize performance for complex models?
```csharp
var options = new GeometryObjectOptions(geometryObject, material)
{
    LevelOfDetail = 0.3,      // 🎯 Reduce detail level
    IsDrawSolidEdges = false  // 📏 Disable edge drawing
};
```

### ❓ How to handle material transparency?
```csharp
// Method 1: Use color transparency
var options = new GeometryObjectOptions(geometryObject, Colors.Red, 0.5f);

// Method 2: Use Revit material transparency
var material = document.GetElement(materialId) as Autodesk.Revit.DB.Material;
var options = new GeometryObjectOptions(geometryObject, material);
```

### ❓ How to handle Solid models with massive triangular faces?
```csharp
// The library has built-in triangular face index optimization, automatically handling massive data
// Just create GeometryObjectOptions normally
var options = new GeometryObjectOptions(largeSolidModel, material);
```

---

## 📞 Technical Support

If you encounter issues during use, please check:

- ✅ Revit document object is correctly passed
- ✅ Geometry object collection contains valid data
- ✅ Viewport control is properly added to WPF visual tree
- ✅ Event handlers are correctly registered and unregistered
- ✅ Rendering parameters are within reasonable ranges
- ✅ Memory usage is normal, call Clear() promptly to release resources

### 🔍 Debugging Tips

```csharp
// Check selected models
var selected = builder.GetSelectedModels();
Console.WriteLine($"Selected {selected.Count()} models");

// Check geometry object mapping
var geometryObjects = builder.GetSelectedGeometryObjects();
foreach (var geoObj in geometryObjects)
{
    Console.WriteLine($"Geometry object type: {geoObj.GetType()}");
}
```

### 📚 Additional Resources

- 📖 **Full Source Code**: Visit the GitHub or Gitee repository above
- 🐛 **Issue Reporting**: Welcome to submit issues in the repository
- 💡 **Feature Suggestions**: Welcome to submit Pull Requests or feature suggestions
- 📋 **Release Notes**: Check the repository's Release page for latest version information

---

**🎉 Start using Su.Revit.HelixToolkit.SharpDX to create outstanding 3D visualization experiences!**