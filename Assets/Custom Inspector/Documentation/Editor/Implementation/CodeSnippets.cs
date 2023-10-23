using System;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;
using RangeAttribute = UnityEngine.RangeAttribute;

namespace CustomInspector.Documentation
{
    [System.Serializable]
    public class CodeSnippets
    {
#pragma warning disable IDE0090 // Use 'new(...)'
#pragma warning disable CS0414
#pragma warning disable IDE0052 // Remove unread private members
#pragma warning disable IDE0044 // Add readonly modifier
#pragma warning disable IDE0051 // Remove unused private members
#pragma warning disable IDE0059 // Unnecessary assignment of a value
        /*
            * 
            * Fields:
            * Naming convention: [SerializeField] ClassNameExamples classNameExamples = new();
            * name is same than classname, but first letter is lowercase
            * 
            * Classes:
            * set [System.Serializable]
            * it displays the attribute with the same name as the attribute plus the "Examples" word
            * 
            */


[SerializeField] AsButtonAttributeExamples asButtonAttributeExamples = new AsButtonAttributeExamples();

[System.Serializable]
class AsButtonAttributeExamples 
{
    bool GetSelectableBoolean() => selectableBoolean;
    [ShowMethod(nameof(GetSelectableBoolean))]

    [AsButton]
    public bool selectableBoolean;

    bool GetHoldDownBoolean() => holdDownBoolean;
    [ShowMethod(nameof(GetHoldDownBoolean))]

    [AsButton(staysPressed: false)]
    public bool holdDownBoolean;

    int GetInteger() => myInteger;
    [ShowMethod(nameof(GetInteger))]

    [AsButton]
    public int myInteger;

    InspectorButtonState GetButtonState() => buttonState;
    [ShowMethod(nameof(GetButtonState))]

    [AsButton]
    public InspectorButtonState buttonState;

    string GetString() => myString;
    [ShowMethod(nameof(GetString))]

    [AsButton(selectedColor = FixedColor.CherryRed,
              size = Size.small,
              label = "my string button",
              tooltip = "some tooltip")]
    public string myString;
}

[SerializeField] AsRangeAttributeExamples asRangeAttributeExamples = new AsRangeAttributeExamples();

[System.Serializable]
class AsRangeAttributeExamples 
{
    [Header("Ranges")]
    [AsRange(0, 10)]
    public Vector2 positiveRange
        = Vector2.up * 3.1415927f;

    [AsRange(10, 0)]
    public Vector2 negativeRange
        = new(1, 5);
}

[SerializeField] AssetsOnlyAttributeExamples assetsOnlyAttributeExamples = new AssetsOnlyAttributeExamples();

[System.Serializable]
class AssetsOnlyAttributeExamples
{
    [Header("Assets")]
    [AssetsOnly] public GameObject gob1;
    [AssetsOnly] public GameObject gob2;

    [AssetsOnly] public Transform chest;
    [AssetsOnly] public Transform leg;

    [AssetsOnly] public Camera cam;
}

[SerializeField] BackgroundColorAttributeExamples backgroundColorAttributeExamples = new BackgroundColorAttributeExamples();

[System.Serializable]
class BackgroundColorAttributeExamples
{
    [HorizontalLine("Really important")]
    [BackgroundColor]
    public int myNumber;

    [Header("Important")]
    [BackgroundColor(FixedColor.CherryRed)]
    public int yourNumber;

    [Header("3")]
    [BackgroundColor(FixedColor.DustyBlue)]
    public GameObject gob;
}

[SerializeField] ButtonAttributeExamples buttonAttributeExamples = new ButtonAttributeExamples();

[System.Serializable]
class ButtonAttributeExamples
{
    [HorizontalLine("default Method")]

    [Button(nameof(LogHelloWorld),
        tooltip = "This will log 'Hello, World!' in the console")]

    [Button(nameof(LogHelloWorld), label = "Hello, World!", size = Size.small)]

    [HideField]
    public bool @bool;

    void LogHelloWorld()
    {
        Debug.Log("Hello, World!");
    }

    [HorizontalLine("Method with parameter")]

    [Button(nameof(LogNumber), true)]
    public int _number;

    void LogNumber(int n)
    {
        Debug.Log(n.ToString());
    }
}

[SerializeField] CopyPasteAttributeExamples copyPasteAttributeExamples = new CopyPasteAttributeExamples();

[System.Serializable]
class CopyPasteAttributeExamples
{
    [CopyPaste] public Vector3 v1
        = Vector3.forward;
    [CopyPaste] public Vector3 v2
        = Vector3.one;

    [CopyPaste] public Color c1
        = Color.white;
    [CopyPaste] public Color c2
        = new(.5f, .4f, .2f, 1);

    [CopyPaste] public string @string = "Hello World";

    [ShowMethod(nameof(GetCurrentClipboard))]
    [SerializeField, HideField] bool b;

    string GetCurrentClipboard()
        => GUIUtility.systemCopyBuffer;
}


[SerializeField] DecimalsAttributeExamples decimalsAttributeExamples = new DecimalsAttributeExamples();

[System.Serializable]
class DecimalsAttributeExamples
{
    [Decimals(1)]
    public float oneDecimal = 0.1f;
    [Decimals(2)]
    public float twoDecimal = 0.02f;

    [HorizontalLine]

    [Decimals(-1)]
    public float onlyTens = 20;
    [Decimals(-2)]
    public int onlyHundreds = 300;
}


[SerializeField] DisplayAutoPropertyAttributeExamples displayAutoPropertyAttributeExamples = new DisplayAutoPropertyAttributeExamples();

[System.Serializable]
class DisplayAutoPropertyAttributeExamples
{
    public int Foo
    { get; private set; } = 45;

    public GameObject Bar
    { private get; set; }

    [Header("auto-properties")]
    [DisplayAutoProperty(nameof(Foo))]
    [DisplayAutoProperty("Bar")]

    [HideField]
    public bool _;
}

[SerializeField] FixedValuesAttributeExamples fixedValuesAttributeExamples = new FixedValuesAttributeExamples();

[System.Serializable]
class FixedValuesAttributeExamples
{   
    [FixedValues(1, 7, 15)]
    public int integer;
    
    [FixedValues("Bob", "John", "Martin")]
    public string name;
}

[SerializeField] ForceFillAttributeExamples forceFillAttributeExamples = new ForceFillAttributeExamples();

[System.Serializable]
class ForceFillAttributeExamples
{
    [ForceFill] public GameObject gob1;
    [ForceFill(null)] public GameObject gob2;

    [ForceFill, SerializeField]
    string s2 = null;

    [ForceFill("<undefined>",
               "empty",
               "undefined")]
    public string s3 = "<undefined>";

    [HorizontalLine("others")]

    [ForceFill("(0, 0, 0)"), SerializeField]
    Vector3 c = new Vector3(0, 0, 0);

    [ForceFill("-1"), SerializeField]
    float f = -1;

    [HorizontalLine("only check if playing")] //test it by starting your game and then look back on this editorwindow
    //or only check if playing (because it would get filled)
    [ForceFill(onlyTestInPlayMode = true)] public GameObject gob = null;

    void Start()
    {
        //this.CheckForceFilled();
        gob2 = GameObject.FindObjectOfType<GameObject>();
    }
}

[SerializeField] GetSetAttributeExamples getSetAttributeExamples = new GetSetAttributeExamples();

[System.Serializable]
class GetSetAttributeExamples
{
    [MessageBox("Shown as vector2, but in fact is vector3", MessageBoxType.Info)]
    [GetSet(nameof(GetPosition), nameof(SetPosition))]

    [HideField] public Vector3 position;

    Vector2 GetPosition()
    {
        return (Vector2)position;
    }
    void SetPosition(Vector2 v)
    {
        position = new(v.x, v.y, 5);
    }

    [HorizontalLine]

    [MessageBox("You cant insert odd numbers", MessageBoxType.Info)]
    [GetSet(nameof(GetEvenNumber), nameof(SetEvenNumber),
            label = "only even numbers:", tooltip = "You cant insert odd numbers")]

    [HideField] public int evenNumber = 66;
    
    int GetEvenNumber()
        => evenNumber;
    void SetEvenNumber(int n)
        => evenNumber = n - n % 2;
}

[SerializeField] GUIColorAttributeExamples gUIColorAttributeExamples = new GUIColorAttributeExamples();

[System.Serializable]
class GUIColorAttributeExamples
{
    public int a, b;

    [GUIColor]
        public string s1 = "Hello World!";
    [GUIColor(FixedColor.Red)]
        public string s2 = "Hello World!";
    [GUIColor(FixedColor.Orange)]
        public string s3 = "Hello World!";
    [GUIColor(FixedColor.Yellow)]
        public string s4 = "Hello World!";
    [GUIColor(FixedColor.Green)]
        public string s5 = "Hello World!";
    [GUIColor(FixedColor.BabyBlue)]
        public string s6 = "Hello World!";
    [GUIColor(FixedColor.Magenta)]
        public string s7 = "Hello World!";

    public int c, d;
}

[SerializeField] HideFieldAttributeExamples hideFieldAttributeExamples = new HideFieldAttributeExamples();

[System.Serializable]
class HideFieldAttributeExamples
{
    [MessageBox("[Header remains] vs [all other attributes are hidden]", MessageBoxType.Info)]

    [BackgroundColor(FixedColor.Gray), ReadOnly(DisableStyle.OnlyText, LabelStyle.NoLabel)]
    public string info1 = "hiding a1 using HideFieldAttribute:";

    //Header is visible
    [Header("Header1")]
    [HideField]
    public bool a1;
    public bool a2;


    [Space(15), BackgroundColor(FixedColor.Gray), ReadOnly(DisableStyle.OnlyText, LabelStyle.NoLabel)]
    public string info2 = "hiding b1 using HideInInspectorAttribute:";

    //Header is hidden too
    [Header("Header2")]
    [HideInInspector]
    public bool b1;
    public bool b2;
}

[SerializeField] HookAttributeExamples hookAttributeExamples = new HookAttributeExamples();

[System.Serializable]
class HookAttributeExamples
{
    [Header("Logs previous and new value in console")]
    [MessageBox("Change value and look into console", MessageBoxType.Info)]
    [Hook(nameof(LogInput))]
    public float value = 0;

    void LogInput(float oldValue, float newValue)
    {
        Debug.Log($"Changed from {oldValue} to {newValue}");
    }
}

[SerializeField] HorizontalGroupAttributeExamples horizontalGroupAttributeExamples = new HorizontalGroupAttributeExamples();

[System.Serializable]
class HorizontalGroupAttributeExamples
{
    [SerializeField, HorizontalGroup(true)]
    SceneInfos offlineScene;
    [SerializeField, HorizontalGroup]
    SceneInfos onlineScene;


    [HorizontalLine(2.5f, FixedColor.Gray, 30)]


    [MessageBox("combine with other attributes", MessageBoxType.Info)]

    [HorizontalGroup(true, size = 4),
    LabelSettings(LabelStyle.NoSpacing)]
    public string test = "combine with a button";

    [HorizontalGroup(size = 1),
    Button(nameof(Func), size = Size.small),
    HideField] public int b;
    void Func() { Debug.Log("Button pressed!"); }



    [HorizontalLine(2.5f, FixedColor.Gray, 30)]



    [HorizontalGroup(true),
    LabelSettings(LabelStyle.NoLabel)]
    public string hisName = "James";

    [HorizontalGroup,
    LabelSettings(LabelStyle.NoLabel)]
    public string hisName2 = "Robert";

    [HorizontalGroup,
    LabelSettings(LabelStyle.NoLabel)]
    public string hisName3 = "Smith";

    [HorizontalGroup(true),
    LabelSettings(LabelStyle.NoLabel)]
    public string herName = "Jennifer";

    [HorizontalGroup,
    LabelSettings(LabelStyle.NoLabel)]
    public string herName2 = "Susan";

    [HorizontalGroup,
    LabelSettings(LabelStyle.NoLabel)]
    public string herName3 = "Miller";


    [System.Serializable]
    class SceneInfos
    {
        [ForceFill] public string name = "start Scene";
        [Header("Some info")]
        [Min(0)] public int loadingTime = 5;
        public GameObject prefab = null;

        [HorizontalGroup(true), LabelSettings(LabelStyle.NoLabel)]
        public string foo = "Hello";
        [HorizontalGroup, LabelSettings(LabelStyle.NoLabel)]
        public string bar = "World";
    }
}

[SerializeField] HorizontalLineAttributeExamples horizontalLineAttributeExamples = new HorizontalLineAttributeExamples();

[System.Serializable]
class HorizontalLineAttributeExamples
{
    [HorizontalLine("Booleans", 2)]
    public bool myBool1 = true;
    public bool myBool2 = true;
    public bool myBool3 = true;
    public bool myBool4 = true;

    [HorizontalLine("Numbers")]

    public int myInt = -1;
    public float myFloat = -1;

    [HorizontalLine]

    public string myString = "<empty>";
    public string myString2 = "<empty>";

    [Space(20)]
    [HorizontalLine(1, FixedColor.Yellow, 1)]
    [HorizontalLine(1, FixedColor.Green, 1)]

    public string myString3 = "two lines";

    [HorizontalLine("My important property",
                        2, FixedColor.Red)]

    public GameObject myGameObject = null;
}


[SerializeField] IndentAttributeExamples indentAttributeExamples = new IndentAttributeExamples();

[System.Serializable]
class IndentAttributeExamples
{
    public int i1;
    [Indent(1)] public int i2;
    [Indent(2)] public int i3;
    public int i4;

    [HorizontalLine]

    public MyClass @class;

    public int i7;

    [System.Serializable]
    public class MyClass
    {
        public int i5;
        [Indent(-1)] public int i6;
    }
}

[SerializeField] InspectorIconAttributeExamples inspectorIconAttributeExamples = new InspectorIconAttributeExamples();

[System.Serializable]
class InspectorIconAttributeExamples
{
    [InspectorIcon(InspectorIcon.Camera)] 
    public string camName = "Bobs cam";

    [InspectorIcon(InspectorIcon.Favorite, true)]
    public string favorite = "look right! (the star) ->";

    [InspectorIcon(InspectorIcon.Light), InspectorIcon(InspectorIcon.Eye)]
    public string lightName = "LED";
}

[SerializeField] LabelSettingsAttributeExamples labelSettingsAttributeExamples = new LabelSettingsAttributeExamples();

[System.Serializable]
class LabelSettingsAttributeExamples
{
    [Header("short names?")]
    [LabelSettings(LabelStyle.NoSpacing)]
    public string @short = "tired of too big label space??";

    [Header("you hate your label?")]
    public string message = "Hello John!";

    [LabelSettings(LabelStyle.EmptyLabel)]
    public string message2, message3 = "hello";

    [Header("a very long one")]
    [LabelSettings(LabelStyle.NoLabel)]
    public string longString = "My very long string";
}

[SerializeField] LayerAttributeExamples layerAttributeExamples = new LayerAttributeExamples();

[System.Serializable]
class LayerAttributeExamples
{
    [Header("any Layer:")]

    [Layer] public int layer;

    [Header("specific Layers:")]

    [Layer("Default")]
    public int layer1;

    [Layer("TransparentFX")]
    public int layer2;
}

[SerializeField] MaskAttributeExamples maskAttributeExamples = new MaskAttributeExamples();

[System.Serializable]
class MaskAttributeExamples
{
    [Mask(5)] public int myInt = 5;
    [Space(10)]
    [Mask] public RigidbodyConstraints rc;
    [Space(10)]
    [Mask] public Ability ability
           = Ability.Look | Ability.Hear;
    public enum Ability
    {
        Look = 1 << 0,
        Hear = 1 << 1,
        Walk = 1 << 2,
        HearAndWalk = Hear | Walk,
    }

    void Start()
    {
        bool thirdBool
            = (myInt & (1 << 3)) != 0;
    }
}

[SerializeField] MaxAttributeExamples maxAttributeExamples = new MaxAttributeExamples();

[System.Serializable]
class MaxAttributeExamples
{
    [Max(10)]
    public int @int;

    [Max(0)]
    public Vector3 vector3;

    //or combine it with a min
    [HorizontalLine("values: 0 - 10")]
    [Min(0), Max(10)]
    public float @float;

    //range looks different
    [HorizontalLine("[Range]")]
    [Range(0, 10)]
    public float rangeComparison;
}

[SerializeField] MessageBoxAttributeExamples messageBoxAttributeExamples = new MessageBoxAttributeExamples();

[System.Serializable]
class MessageBoxAttributeExamples
{
    [Header("Here are some message-boxes:")]
    [MessageBox("Booleans",
            MessageBoxType.Info)]
    public bool myBool1 = true;

    [MessageBox("These values are obsolete",
            MessageBoxType.Warning)]
    public int amount1 = 55;

    [MessageBox("Some error", MessageBoxType.Error)]

    [SerializeField, HideField]
    bool abc;
}

[SerializeField] MultipleOfAttributeExamples multipleOfAttributeExamples = new MultipleOfAttributeExamples();

[System.Serializable]
class MultipleOfAttributeExamples
{
    [MultipleOf(3)]
    public int @int = 6;

    [MultipleOf(0.3f)]
    public float @float = 1.2f;

    [HorizontalLine]

    public double step = .5f;
    [MultipleOf("step")]
    public float multipleOfStep;
}

[SerializeField] PreviewAttributeExamples previewAttributeExamples = new PreviewAttributeExamples();

[System.Serializable]
class PreviewAttributeExamples
{
    [MessageBox("Please drag/select values to see previews", MessageBoxType.Info)]

    [SerializeField, Preview(Size.small)] GameObject gob;
    [SerializeField, Preview] Sprite sprite;

    [ForceFill(errorMessage = "Select an icon/Sprite from the drop-down")]
    [SerializeField, Preview(Size.big)] Sprite icon;
}

[SerializeField] ProgressBarAttributeExamples progressBarAttributeExamples = new ProgressBarAttributeExamples();

[System.Serializable]
class ProgressBarAttributeExamples
{
    [Header("progress bars:")]
    [Space(20)]

    [SerializeField, ProgressBar(1)]
    float value1 = 0.6f;

    [HorizontalLine]

    [SerializeField, ProgressBar(100, size = Size.big)]
    int value2 = 20;

    [HorizontalLine]
    [SerializeField, ProgressBar(nameof(min), nameof(max), size = Size.big)]
    int value3 = 1;
    [SerializeField] float min = 0;
    [SerializeField] float max = 2;
}

[SerializeField] ReadOnlyAttributeExamples readOnlyAttributeExamples = new ReadOnlyAttributeExamples();

[System.Serializable]
class ReadOnlyAttributeExamples
{
    [HorizontalLine("Disabled")]

    [SerializeField, ReadOnly] int n;
    [SerializeField, ReadOnly] GameObject gob;
    [SerializeField, ReadOnly] Sprite spr;

    [HorizontalLine("Only text")]

    [SerializeField, ReadOnly(DisableStyle.OnlyText)]
    string info = "some info";

    [HorizontalLine]

    public bool show = true;

    [SerializeField, ShowIf(nameof(show)),
    ReadOnly(DisableStyle.OnlyText, LabelStyle.NoLabel)]
    string
    i1 = "This is a very deep explanation..",
    i2 = "Oho, what do i see there",
    i3 = "Hello, World!";
}

[SerializeField] RequireTypeAttributeExamples requireTypeAttributeExamples = new RequireTypeAttributeExamples();

[System.Serializable]
class RequireTypeAttributeExamples
{
    [Header("e.g. allow only specific interfaces")]

    [RequireType(typeof(IAge))]
        public MonoBehaviour agingScript;

    interface IAge
    {
        public abstract int GetAge();
    }

    [RequireType(typeof(IHuman))]
    public MonoBehaviour myHuman;

    interface IHuman : IAge
    {
        public abstract int GetHeight();
        public abstract int GetHairColor();
    }
}

[SerializeField] SelfFillPreview selfFillAttributeExamples = new SelfFillPreview();

[System.Serializable] //i hide this class on purpose, because SelfFill doesnt work on scriptable objects
class SelfFillAttributeExamples
{
    //In this example all these components are attached

    [SelfFill] public Camera cam;

    [SelfFill(true)] public AudioSource audio;
    [SelfFill(true)] public Light light;
    
    /*
     * this.CheckSelfFilled() == true;
     *
     * cam == GetComponent<Camera>()
     * audio == GetComponent<AudioSource>()
     * light == GetComponent<Light>()
     */
    
}
[System.Serializable]
class SelfFillPreview
{
    [HorizontalGroup]
    [ReadOnly(DisableStyle.OnlyText, LabelStyle.NoLabel)] public string label = "cam (auto-filled)";
    [HorizontalGroup(false, size = 2f)]
    [ReadOnly(DisableStyle.GreyedOut, LabelStyle.NoLabel)] public Camera cam;

    void Start()
    {
        cam = Camera.main;
    }
}

[SerializeField] ShowAssetReferenceAttributeExamples showAssetReferenceAttributeExamples = new ShowAssetReferenceAttributeExamples();

[System.Serializable]
class ShowAssetReferenceAttributeExamples
{
    [ShowAssetReference("CodeSnippets")] // in this case you could also use 'nameof(CodeSnippets)'
    public A a;

    [HorizontalLine]

    [ShowAssetReference]
    public TestClass testClass;


    [System.Serializable]
    public class A
    {
        public string name = "Some Custom Class";
        [Min(0)] public int amount = 10;
    }
}

[SerializeField] ShowIfAttributeExamples showIfAttributeExamples = new ShowIfAttributeExamples();

[System.Serializable]
class ShowIfAttributeExamples
{
    [HorizontalLine("with Booleans")]

    [MessageBox("Toggle this bool value to expose the custom colors", MessageBoxType.Info)]
    public bool customColors = false;
    [ShowIf(nameof(customColors))]
    public Color headColor = Color.white;
    [ShowIf(nameof(customColors))]
    public Color bodyColor = Color.black;

    [HorizontalLine]

    [MessageBox("Tick both conditions", MessageBoxType.Info)]
    public bool condition1 = true;
    public bool condition2 = false;

    [ShowIf(nameof(condition1), style = DisabledStyle.GreyedOut)]
    public GameObject cond1True = null;

    [ShowIf(BoolOperator.And,
            nameof(condition1),
            nameof(condition2),
            style = DisabledStyle.Invisible),
    Indent(-1)]
    public string someText = "both conditions are true";

    [HorizontalLine("with Comparisons")]

    [MessageBox("Please fill material to expose the tiling", MessageBoxType.Info)]
    public Material material;
    [ShowIf(ComparisonOp.NotNull, nameof(material))]
    public Vector2 tiling;

    [HorizontalLine]

    [MessageBox("Make a and b same value to expose an info", MessageBoxType.Info)]
    public int a;
    public int b = 1;
    [ShowIf(ComparisonOp.Equals, nameof(a), nameof(b))]
    [ReadOnly(DisableStyle.OnlyText)] public string info = "both are the same";

    [HorizontalLine("custom functions")]

    [MessageBox("expose by clicking the toggle", MessageBoxType.Info)]
    
    public bool toggle1;

    [ShowIf(nameof(MyMethod))]
    public float float1, float2;
    public bool MyMethod()
        => toggle1 == true;

        
    [HorizontalLine("with " + nameof(StaticConditions))]
    [MessageBox("hit play-mode for testing", MessageBoxType.Info)]

    [ShowIf(StaticConditions.IsNotPlaying, style = DisabledStyle.GreyedOut)]
    [Indent(-1)]
    public string playername = "Bob";

    [ShowIf(StaticConditions.IsPlaying)]
    [Button(nameof(Jump))] [SerializeField, HideField] bool _;
    void Jump() { }
}

[SerializeField] ShowIfIsAttributeExamples showIfIsAttributeExamples = new ShowIfIsAttributeExamples();

[System.Serializable]
class ShowIfIsAttributeExamples
{
    public enum Labeling { NoLabel, CustomLabel }

    public Labeling labeling;

    [ShowIfIs(nameof(labeling), Labeling.CustomLabel)]
    public string labelText = "my label";
}

[SerializeField] ShowIfNotAttributeExamples showIfNotAttributeExamples = new ShowIfNotAttributeExamples();

[System.Serializable]
class ShowIfNotAttributeExamples
{
    public Material customMaterial = null;
    [ShowIfNot(ComparisonOp.Null, nameof(customMaterial))]
    public Vector2 tiling = Vector2.one;


    [MessageBox("tick all conditions", MessageBoxType.Info)]

    public bool condition1 = true;
    public bool condition2 = true;
    public bool condition3 = false;

    [ShowIfNot(BoolOperator.And,
                nameof(condition1),
                nameof(condition2),
                nameof(condition3))]
    public string notAllTrue = "not all conditions are true";


    [HorizontalLine("Functions")]

    [MessageBox("A field is visible if even number is set to an even value", MessageBoxType.Info)]
    public int evenNumber = 1;
    [ShowIfNot(nameof(IsEven))]
    public float value = -1;

    public bool IsEven()
    {
        return evenNumber % 2 == 0;
    }
}

[SerializeField] ShowMethodAttributeExamples showMethodAttributeExamples = new ShowMethodAttributeExamples();

[System.Serializable]
class ShowMethodAttributeExamples
{
    [ShowMethod(nameof(GetTime))]

    [ShowMethod(nameof(GetTime),
        label = "current time",
        tooltip = "updated on each gui-update")]

    [SerializeField, HideField]
    bool someBool = false;

    string GetTime()
    {
        return DateTime.Now.ToString();
    }
}

[SerializeField] TabAttributeExamples tabAttributeExamples = new TabAttributeExamples();

[System.Serializable]
class TabAttributeExamples
{
    [Tab("Inventory")] public GameObject item;
    [Tab("Inventory"), Min(0)] public float weight;
    
    [Tab("Stats"), Unwrap]
    public Stats stats;

    [Tab(InspectorIcon.Settings)]
    public float size;

    [System.Serializable]
    public class Stats
    {
        [ProgressBar(100)]
        public int health = 79;
        [ProgressBar(1)]
        public float stamina = 1;
        [Min(.01f)]
        public float speed = 1.3f;
    }
}

[SerializeField] TagAttributeExamples tagAttributeExamples = new TagAttributeExamples();

[System.Serializable]
class TagAttributeExamples
{
    [Tag] public string tag1 = "Player";
    [Tag] public string tag2;
}

[SerializeField] TitleAttributeExamples titleAttributeExamples = new TitleAttributeExamples();

[System.Serializable]
class TitleAttributeExamples
{
    [MessageBox("the [Title]-attribute allows other attributes to draw before", MessageBoxType.Info)]
    [Button(nameof(MyFunc))]
    [Title("My boolean")]
    [Button(nameof(MyFunc))]
    public bool b1;

    [HorizontalLine("")]

    [MessageBox("the [Header]-attribute draws first", MessageBoxType.Info)]
    [SerializeField, HideField] bool _;

    [Button(nameof(MyFunc))]
    [Header("My boolean")]
    [Button(nameof(MyFunc))]
    public bool b2;

    void MyFunc()
    { }
}

[SerializeField] ToolbarAttributeExamples toolbarAttributeExamples = new ToolbarAttributeExamples();

[System.Serializable]
class ToolbarAttributeExamples
{
    [HorizontalLine("Booleans")]

    [Toolbar] public bool edit;
    [Toolbar(20, 0)] public bool create;

    [HorizontalLine("Enums", 1, 2)]

    [Toolbar]
    public Animal animal;
    public enum Animal { Dog, Cat, Bird }

    [Toolbar]
    public EditType type;
    public enum EditType { create, edit, delete, update }
}

[SerializeField] UnfoldAttributeExamples unfoldAttributeExamples = new UnfoldAttributeExamples();

[System.Serializable]
class UnfoldAttributeExamples
{
    [Unfold] public MyClass unfolded;

    [System.Serializable]
    public class MyClass
    {
        public int number1;
        public int number2;
    }
}

[SerializeField] UnitAttributeExamples unitAttributeExamples = new UnitAttributeExamples();

[System.Serializable]
class UnitAttributeExamples
{
    [Unit("per secound")] public int amount = 5;
    [Unit("cm")] public int jumpHeight = 80;
    [Unit("feet")] public int distance = 100;
}

[SerializeField] UnwrapAttributeExamples unwrapAttributeExamples = new UnwrapAttributeExamples();

[System.Serializable]
class UnwrapAttributeExamples
{
    [HorizontalLine("unwrapped")]
    [Unwrap] public MyClass unwrapped;
    
    [HorizontalLine]
    [Unwrap(applyName = true)] public MyClass a;

    [HorizontalLine("default display")]
    public MyClass wrapped;


    [System.Serializable]
    public class MyClass
    {
        public int number1;
        public int number2;
    }
}

[SerializeField] URLAttributeExamples uRLAttributeExamples = new URLAttributeExamples();

[System.Serializable]
class URLAttributeExamples
{
    public int a;    
    public int b;

    [URL("http://mbservices.de/")]
    // you can also add a label and tooltip
    [URL("www.google.com/", label = "google:",
                            tooltip = "This is a tooltip")]
    
    public int c;
    public int d;
}

[SerializeField] ValidateAttributeExamples validateAttributeExamples = new ValidateAttributeExamples();

[System.Serializable]
class ValidateAttributeExamples
{
    [HorizontalLine("Change values:", 0)]

    [Validate(nameof(IsEven))]
        public int evenNumber = 2;

    [HorizontalLine]

    [Validate(nameof(IsOdd), "Value has to be odd!")]
        public int oddNumber = 1;

    bool IsEven(int i)
        => i % 2 == 0;
    bool IsOdd(int i)
        => Math.Abs(i % 2) == 1;
}

//----------------------------------------------------Types------------------------------------------------
[HorizontalLine("Types", 3)]


[SerializeField] Array2DExamples array2DExamples = new Array2DExamples();

[System.Serializable]
class Array2DExamples
{
    [SerializeField, Array2D] Array2D<Sprite> images
         = new Array2D<Sprite>(2, 2);

    [HorizontalLine]

    [Array2D] public Array2D<int> numbers;
}

[SerializeField] DynamicSliderExamples dynamicSliderExamples = new DynamicSliderExamples();

[System.Serializable]
class DynamicSliderExamples
{   
    [Header("A changable range")]
    public DynamicSlider sliderValue
        = new DynamicSlider(5, 1, 10);

    [Header("only one custom side")]
    public DynamicSlider value2
        = new DynamicSlider(5, 1, 10, FixedSide.FixedMin);

    public DynamicSlider value3
        = new DynamicSlider(5, 1, 10, FixedSide.FixedMax);

    [HorizontalLine]

    public bool useSlider = false;
    [DynamicSlider, ShowIf(nameof(useSlider), style = DisabledStyle.GreyedOut)]
    public DynamicSlider slider
        = new DynamicSlider(1, 0, 2);


    void Increment()
    {
        //implicit conversion to float 
        float a = sliderValue;
        a++;
        sliderValue.value = a;
    }
}

[SerializeField] FilePathExamples filePathExamples = new FilePathExamples();

[System.Serializable]
class FilePathExamples
{
    [HorizontalLine("some files:")]
    public FilePath filePath
                = new FilePath("Assets");

    public FilePath meshPath
                = new FilePath(typeof(Mesh));

    [HorizontalLine]

    [ReadOnly, AssetPath]
    public FilePath path = new FilePath();

    void SomeFunc()
    {
        if(filePath.HasPath())
        {
            string path = filePath.GetPath();
        }
    }
}

[SerializeField] FolderPathExamples folderPathExamples = new FolderPathExamples();

[System.Serializable]
class FolderPathExamples
{
    [HorizontalLine("some folder paths")]
    public FolderPath folderPath
                = new FolderPath("Assets");

    public FolderPath path2 
                = new FolderPath();

    public FolderPath path3
                = new FolderPath("Assets/Trash");

    [HorizontalLine]

    [ReadOnly, AssetPath]
    public FolderPath path = new("Assets/");

    void SomeFunc()
    {
        try
        {
            Mesh mesh = path2.LoadAsset<Mesh>("MyMesh.mesh");
            path3.CreateAsset(null, "Abc.mesh");
        }
        catch (NullReferenceException e)
        {
            Debug.LogException(e);
        }
    }
}

[SerializeField] MessageDrawerExamples messageDrawerExamples = new MessageDrawerExamples();

[System.Serializable]
class MessageDrawerExamples
{
    [Button(nameof(LogTime), label = "Add current Time")]  
    [Button(nameof(LogWarning))]
    [Button(nameof(LogError))]

    [MessageDrawer] public MessageDrawer md = new MessageDrawer();

    void LogTime()
    {
        md.DrawMessage(DateTime.Now.ToString());
    }
    void LogWarning()
    {
        md.DrawWarning("You added a warning");
    }
    void LogError()
    {
        md.DrawError("You added an error message");
    }
}

[SerializeField] ReorderableDictionaryExamples reorderableDictionaryExamples = new ReorderableDictionaryExamples();

[System.Serializable]
class ReorderableDictionaryExamples
{
    [SerializeField]
    [Dictionary]
    ReorderableDictionary<int, string> dict1 = new();

    [SerializeField, Dictionary(keySize: .2f)]
    ReorderableDictionary<string, MyClass> dict2 = new();

    [System.Serializable]
    class MyClass
    {
        public int a;
        public int b;
        public int c;
    }
}

[SerializeField] SerializableDateTimeExamples serializableDateTimeExamples = new SerializableDateTimeExamples();

[System.Serializable]
class SerializableDateTimeExamples
{
    //leave out the attribute to display the class normally
    public SerializableDateTime time1
        = DateTime.Today;

    [HorizontalLine]

    [SerializableDateTime(SerializableDateTime.InspectorFormat.DateEnums)]
    public SerializableDateTime time2;

    [HorizontalLine]

    [SerializableDateTime(SerializableDateTime.InspectorFormat.AddTextInput)]
    public SerializableDateTime time3;
}

[SerializeField] SerializableDictionaryExamples serializableDictionaryExamples = new SerializableDictionaryExamples();

[System.Serializable]
class SerializableDictionaryExamples
{
    [Dictionary] //dont forget the attribute!
    public SerializableDictionary<int, string> dict1
        = new SerializableDictionary<int, string>();

    [HorizontalLine]

    [Button(nameof(AddRandomValue))]
    [SerializeField, HideField] bool _;

    void AddRandomValue()
    {
        dict1.TryAdd(UnityEngine.Random.Range(-2000, 2000), "some random value");
        //Access: myDictionary[key] = value
    }

    [HorizontalLine("More examples")]

    [Dictionary]
    public SerializableDictionary<int, MyClass> dict2 = new SerializableDictionary<int, MyClass>();
    [Dictionary]
    public SerializableDictionary<int, GameObject> dict3 = new SerializableDictionary<int, GameObject>();

    [System.Serializable]
    public class MyClass
    {
        public string name = "empty";
        public int id = -1;

        public MyClass(string name, int id)
        {
            this.name = name;
            this.id = id;
        }

        public override string ToString()
        {
            return $"{name} ({id})";
        }

        public override bool Equals(object obj)
        {
            if(obj is MyClass c)
            {
                return name == c.name && id == c.id;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return id;
        }
    }
}

[SerializeField] SerializableSortedDictionaryExamples serializableSortedDictionaryExamples = new SerializableSortedDictionaryExamples();

[System.Serializable]
class SerializableSortedDictionaryExamples
{
    [Dictionary] //dont forget the attribute!
    public SerializableSortedDictionary<int, string> dict1
        = new SerializableSortedDictionary<int, string>();

    [HorizontalLine]

    [Button(nameof(AddRandomValue))]
    [SerializeField, HideField] bool a;

    void AddRandomValue()
    {
        dict1.TryAdd(UnityEngine.Random.Range(-2000, 2000), "some random value");
        //Access: myDictionary[key] = value
    }
}

[SerializeField] SerializableSetExamples serializableSetExamples = new SerializableSetExamples();

[System.Serializable]
class SerializableSetExamples
{
    [Set] //dont forget the attribute!
    public SerializableSet<int> set1
        = new SerializableSet<int>();

    [HorizontalLine]

    [Button(nameof(AddRandomValue))]
    [SerializeField, HideField] bool a;

    void AddRandomValue()
    {
        set1.TryAdd(UnityEngine.Random.Range(-2000, 2000));
    }

    [HorizontalLine("custom class example")]

    [Set]
    public SerializableSet<MyClass> set2
        = new SerializableSet<MyClass>();

    [System.Serializable]
    public class MyClass
    {
        public string name = "empty";
        public int id = -1;

        public MyClass(string name, int id)
        {
            this.name = name;
            this.id = id;
        }

        public override string ToString()
        {
            return $"{name} ({id})";
        }

        public override bool Equals(object obj)
        {
            if(obj is MyClass c)
            {
                return name == c.name && id == c.id;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return id;
        }
    }
}

[SerializeField] SerializableSortedSetExamples serializableSortedSetExamples = new SerializableSortedSetExamples();

[System.Serializable]
class SerializableSortedSetExamples
{
    [Set] //dont forget the attribute!
    public SerializableSortedSet<int> set1
        = new SerializableSortedSet<int>() { 1, 3, -75, 2 };

    [HorizontalLine]

    [Button(nameof(AddRandomValue))]
    [SerializeField, HideField] bool a;

    void AddRandomValue()
    {
        set1.TryAdd(UnityEngine.Random.Range(-2000, 2000));
    }
}


[SerializeField] StaticsDrawerExamples staticsDrawerExamples = new StaticsDrawerExamples();

[System.Serializable]
class StaticsDrawerExamples
{
    [Header("My values")]
    public string hello = "Hello!";


    static int a = 6;
    static float b = 9.5f;
    static GameObject c = null;
    static Color d = Color.white;
    public static Vector2 e = new Vector2(0.5f, 8);

    [SerializeField] StaticsDrawer sDrawer;
}

[SerializeField] SerializableTupleExamples serializableTupleExamples = new SerializableTupleExamples();

[System.Serializable]
class SerializableTupleExamples
{
    [Tuple] public SerializableTuple<int, string> tuple1 = new(1, "a");
    [Tuple] public SerializableTuple<int, float, GameObject> tuple2 = new(1, 1.5f, null);
}

//----------------------------------------------------Unitys------------------------------------------------
[HorizontalLine("Unitys", 3)]

[SerializeField] DelayedAttributeExamples delayedAttributeExamples = new DelayedAttributeExamples();

[System.Serializable]
class DelayedAttributeExamples
{
    [Delayed]
    public string delayed = "Edit here";

    public string instant = "Edit here";


    [ShowMethod(nameof(GetDelayedOne))]
    [ShowMethod(nameof(GetInstantOne))]

    [HideField]
    public bool b2;

    string GetDelayedOne()
        => delayed;
    string GetInstantOne()
        => instant;
}


[SerializeField] HeaderAttributeExamples headerAttributeExamples = new HeaderAttributeExamples();

[System.Serializable]
class HeaderAttributeExamples
{
    [Header("First")]
    public string a;
    public string b;
    public string c;

    [Header("Second")]
    public string a2;
    public string b2;
    public string c2;
}

[SerializeField] MinAttributeExamples minAttributeExamples = new MinAttributeExamples();

[System.Serializable]
class MinAttributeExamples
{
    [Min(0)]
    public int i = 5;

    [Min(10)]
    public float f = 5;

    [Min(0)]
    public Vector3 v = Vector3.up;
}

[SerializeField] MultilineAttributeExamples multilineAttributeExamples = new MultilineAttributeExamples();

[System.Serializable]
class MultilineAttributeExamples
{
    [Multiline(lines: 4)]
    public string info = "Hello, World!";
}

[SerializeField] NonReorderableAttributeExamples nonReorderableAttributeExamples = new NonReorderableAttributeExamples();

[System.Serializable]
class NonReorderableAttributeExamples
{
    [Header("non reorderable list")]
    [NonReorderable]
    public string[] list1 = new string[] { "Abc", "Def", "Ghi", "Jkl"};
}

[SerializeField] RangeAttributeExamples rangeAttributeExamples = new RangeAttributeExamples();

[System.Serializable]
class RangeAttributeExamples
{
    [Range(0, 10)] public int @int;
    [Range(0, 10)] public float @float;
}

[SerializeField] SpaceAttributeExamples spaceAttributeExamples = new SpaceAttributeExamples();

[System.Serializable]
class SpaceAttributeExamples 
{
    public int int1;
    public int int2;
    [Space(20)]
    public float float1;
    public float float2;
}

[SerializeField] TooltipAttributeExamples tooltipAttributeExamples = new TooltipAttributeExamples();

[System.Serializable]
class TooltipAttributeExamples 
{
    [MessageBox("Hover over these fields:", MessageBoxType.Info)]

    [Tooltip("some tooltip")]
    public int @int;

    [Tooltip("some other tooltip")]
    [SerializeField] Abc someClass;

    [System.Serializable] class Abc
    {
        [Tooltip("a third tooltip")]
        public int i;
    }
}

[SerializeField] TextAreaAttributeExamples textAreaAttributeExamples = new TextAreaAttributeExamples();

[System.Serializable]
class TextAreaAttributeExamples 
{
    [TextArea(1, 20)]
    public string someString;

    [TextArea(minLines: 1, maxLines: 20)]
    public string otherString = "a\nb\nc\nd\ne\nf\ng";
}


#pragma warning restore CS0414
#pragma warning restore IDE0052 // Remove unread private members
#pragma warning restore IDE0051 // Remove unused private members
#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore IDE0059 // Unnecessary assignment of a value
#pragma warning restore IDE0090 // Use 'new(...)'
}
}