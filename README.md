# Shape Editor

This repository provide a simplistic `Custom Window` to draw shapes.

![Custom Shape Editor](https://i.stack.imgur.com/fzRS6.gif)

## Installation 

The Unity package manager is currently **not supported**. (see [#1](https://github.com/Seabirds-Games/shape-editor/issues/1))

To start using shape editor, pleases clone the project using:

`git clone https://github.com/Seabirds-Games/shape-editor.git`

export the project as a package the old way and import it in your games thanks to the [documentation](https://docs.unity3d.com/2018.1/Documentation/Manual/AssetPackages.html)...

## Usage

First, we'll have to create a `ShapeObject` through the Asset menu.
Go to `Assets/Create/Recgonizer/ShapeObject` to create the asset that will save the state of your shape.

Then, open Shape Editor by **double clicking** on the asset you've just create. It should open the actual shape editor.

You can start drawing! Holding the **Left click** enables you to draw a line from the clicking point to the position you release the button.
Every new line will *start at the end* of the first you just draw. (more behaviour can be added)

## Contributing

Pull requests are always welcome.

If you're unsure or have a question, please open first an [issue](https://github.com/Seabirds-Games/shape-editor/issues/new) so that we talk about the change before implementing it.

## License 

This project is under **GNU General Public License v3.0 or later**.
 
You can find a copy of the license in the repository [here](LICENSE).