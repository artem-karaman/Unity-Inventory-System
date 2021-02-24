using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace InventorySystem.Samples.Scripts.Components
{
   public class LoadSceneBehaviour : MonoBehaviour
   {
      public void LoadScene(string sceneName)
      {
         SceneManager.LoadSceneAsync(sceneName);
      }

      [Serializable]
      public enum SceneNames
      {
         HorizontalInventory,
         MainInventory
      }
   }
}
