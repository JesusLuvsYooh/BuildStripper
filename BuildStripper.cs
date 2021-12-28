// JesusLuvsYooh StephenAllenGames.co.uk
// Version 1.0

using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

class BuildStripper : IPreprocessBuildWithReport
{
    // - CHANGE -
    // Set to false if not doing headless server build
    private bool buildStrippedServer = true;

    // - CHANGE -
    // Example folders that are not needed on headless server build
    private string[] folderPaths = new string[] {
        "AddressablesFolder",
        "Animations",
        "Models",
        "Plugins",
        "Resources",
        "Scenes/Map3-Volcano",  // light map folder, not the scene file itself 
        "Shaders",
        "Sounds",
        "StreamingAssets",
        "Textures"
    };

    public int callbackOrder { get { return 0; } }
    private string stringValue;

    public void OnPreprocessBuild(BuildReport report)
    {
        if (buildStrippedServer)
        {
            EditorApplication.update += BuildCheck;
            StripBuild();
            Debug.Log("OnPreprocessBuild for target " + report.summary.platform + " at path " + report.summary.outputPath); 
        }
    }

    public void OnPostprocessBuild(BuildReport report)
    {
        if (buildStrippedServer)
        {
            RevertStripBuild();
            Debug.Log("OnPostprocessBuild for target " + report.summary.platform + " at path " + report.summary.outputPath);
        }
    }

    private void StripBuild()
    {
        foreach (string _value in folderPaths)
        {
            AssetDatabase.MoveAsset("Assets/" + _value, "Assets/" + _value + "~");
        }
    }

    private void RevertStripBuild()
    {
        foreach (string _value in folderPaths)
        {
            stringValue = _value;
            //end character is used instead of "Replace", just incase people have "~" symbol throughout folder/file names.
            if (stringValue.Substring(stringValue.Length - 1) == "~") { stringValue = stringValue.Substring(0, stringValue.Length - 1); }

            AssetDatabase.MoveAsset("Assets/" + _value + "~", "Assets/" + _value);
        }
    }

    private void BuildCheck()
    {
        //Debug.Log("BuildCheck " + BuildPipeline.isBuildingPlayer);
        if (!BuildPipeline.isBuildingPlayer)
        {
            EditorApplication.update -= BuildCheck;
            RevertStripBuild();
        }
    }


    /*
    [MenuItem("File/Build Stripped Server", priority = 1)]
    public static void ButtonStrippedServer()
    {
        Debug.Log("Button Stripped Server called.");
        StripBuild();
    }
    [MenuItem("File/Revert Stripped Server", priority = 1)]
    public static void ButtonStrippedServerRevert()
    {
        Debug.Log("Button Revert Stripped Server called.");
        RevertStripBuild();
    }
    */
}
