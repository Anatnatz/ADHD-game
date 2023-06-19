using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public class EditorSceneSettings
{
    public static List<string> lastEdittedScene;

    static EditorSceneSettings()
    {
        if (lastEdittedScene == null) lastEdittedScene = new List<string>();
        EditorApplication.hierarchyChanged += OnHierarchyChanged;
    }

    static void OnHierarchyChanged()
    {
        // if (
        //     EditorApplication.isPlaying == false &&
        //     lastEdittedScene.Count > 0 &&
        //     lastEdittedScene[lastEdittedScene.Count - 1] !=
        //     EditorApplication.currentScene
        // )
        // {
        lastEdittedScene.Add(EditorApplication.currentScene);
        // }
    }

    [MenuItem("Edit/Play, But From Prelaunch Scene %SPACE")]
    public static void PlayFromPrelaunchScene()
    {
        if (EditorApplication.isPlaying == true)
        {
            EditorApplication.isPlaying = false;

            return;
        }

        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/Scenes/MainMenu.unity");
        EditorApplication.isPlaying = true;
    }

    [MenuItem("Edit/Load Last Editted Scene %&SPACE")]
    public static void StopPlayAtLastEdittedScene()
    {
        lastEdittedScene.RemoveAt(lastEdittedScene.Count - 1);
        EditorSceneManager
            .OpenScene(lastEdittedScene[lastEdittedScene.Count - 1]);
    }
}
