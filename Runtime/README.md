# Yans Views

A Unity package for managing UI views and their interactions.

## Overview

This package provides a flexible system for creating, showing, and hiding different UI views within a Unity project. It is designed to simplify the process of managing complex UI structures and interactions.

The core components of this package are:

- **`View`**: A base class for all UI elements. It provides common functionality such as controlling visibility (`SetVisibility`), and accessing the `RectTransform` and parent `Canvas`.
- **`EnumerableView<V>`**: A specialized view that manages a dynamic collection of other views of type `V`. It supports object pooling for efficient view reuse, dynamic instantiation of views, and uses an `IViewInstantiator` interface for custom view creation logic. It also allows accessing views by index and iterating through active views.
- **`ListAdapter<V>`**: An abstract base class for adapters that bind data to an `EnumerableView<V>`. It handles the creation, destruction, and binding of individual views within the list.
- **`UniversalAdapter<V>`**: A concrete implementation of `ListAdapter<V>` that allows for a flexible way to bind data to views using an `Action<V, int>` delegate.
- **`ButtonView`**: A specific implementation of `View` that represents a UI button. It includes properties for `Interactable` state and an `OnClick` event to handle user interactions.

## Features

- Base `View` class with common UI functionalities like visibility control.
- `EnumerableView<V>` for efficiently managing and displaying lists or grids of views, featuring:
    - Object pooling for view recycling.
    - Dynamic creation and release of views.
    - Customizable view instantiation.
- Adapter pattern (`ListAdapter<V>`, `UniversalAdapter<V>`) for populating views with data, promoting separation of concerns.
- `ButtonView` component for straightforward button implementation with click event handling and interactable state management.
- Designed for Unity projects, leveraging `UnityEngine.UI` components.

## Installation

To install this package, add the following line to your `manifest.json` file located in the `Packages` folder of your Unity project:

```json
{
  "dependencies": {
    "com.yans.views": "https://github.com/your-username/yans-views.git#upm"
  }
}
```

Alternatively, you can use the Unity Package Manager to install the package from a git URL.

## Usage

1. Create your custom views by inheriting from the `View` class.
2. Implement the necessary logic for showing, hiding, and updating your views.
3. Use adapters to populate your views with data.
4. Manage your views using the provided view management system.

For detailed examples and API documentation, please refer to the [documentation](#) (link to be added).

## Contributing

Contributions are welcome! If you have any ideas, suggestions, or bug reports, please open an issue or submit a pull request.

## License

This package is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
