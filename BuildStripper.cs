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
        "Textures",
    };

    public int callbackOrder { get { return 0; } }   

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
            // to do - optimise
            string s1 = _value;
            string s2 = s1.Substring(s1.Length - 1);
            if (s2 == "~") { s1 = s1.Substring(0, s1.Length - 1); }

            AssetDatabase.MoveAsset("Assets/" + _value + "~", "Assets/" + s1);
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
