Some tools for Unity, using ScriptableObject :

- Editor Variables
- Events
- Sets

Totally based on this awesome talk : https://www.youtube.com/watch?v=raQ3iHhE_Kk

## Editor Variables

Allows designers to create variables in the editor. Those variables are stored as `.asset` files and can be referenced in any components and scenes.

### Usage

To create a variable : `Create Menu > Variables`

Available types : `Bool`, `Float`, `Int`, `String`, `Color`, `GameObject`, `Vector3`.

**In code**

	public BoolVariable _aBool;
	// Or
	public BoolRef _anotherBool;
	
	// You can replace Bool with any other available type. Ex: FloatVariable, IntRef, etc.

With `[Type]Variable`, you reference the asset only. But with `[Type]Ref` you can either reference the asset file or directly set a value of the given type. In the inspector, you will be able to choose between `Variable Object` or `Direct`, in the `Reference Type` field.

## Events

TODO

## Sets

TODO