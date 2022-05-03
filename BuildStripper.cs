// JesusLuvsYooh StephenAllenGames.co.uk
// https://github.com/JesusLuvsYooh/BuildStripper
// Version 1.1

using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

class BuildStripper : IPreprocessBuildWithReport
{
    // - CHANGE -
    // If true, automatically sets buildStrippedServer = true  If "Server Build" is ticked in Unitys build window.
    private bool autoDetectServerBuild = true;
    // Do not change, use the above boolean. Only Overwrite if you want to strip from a non "Server Build".
    private bool buildStrippedServer = false;  

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
        if (autoDetectServerBuild && EditorUserBuildSettings.standaloneBuildSubtarget == StandaloneBuildSubtarget.Server) { buildStrippedServer = true; }
        
        if (buildStrippedServer)
        {
            EditorApplication.update += BuildCheck;
            StripBuild();
            UnityEngine.Debug.Log("OnPreprocessBuild for target " + report.summary.platform + " at path " + report.summary.outputPath); 
        }
    }

    public void OnPostprocessBuild(BuildReport report)
    {
        if (buildStrippedServer)
        {
            RevertStripBuild();
            UnityEngine.Debug.Log("OnPostprocessBuild for target " + report.summary.platform + " at path " + report.summary.outputPath);
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

    // We run this check to detect build failures or cancellations, to then apply the Revert function.
    private void BuildCheck()
    {
        // UnityEngine.Debug.Log("BuildCheck " + BuildPipeline.isBuildingPlayer);
        if (!BuildPipeline.isBuildingPlayer)
        {
            EditorApplication.update -= BuildCheck;
            RevertStripBuild();
        }
    }


    /*
    [MenuItem("File/Setup Stripped Folders", priority = 1)]
    public static void ButtonStrippedServer()
    {
        UnityEngine.Debug.Log("Button Stripped Server called.");
        StripBuild();
    }
    [MenuItem("File/Revert Stripped Folders", priority = 1)]
    public static void ButtonStrippedServerRevert()
    {
        UnityEngine.Debug.Log("Button Revert Stripped Server called.");
        RevertStripBuild();
    }
    */
}
