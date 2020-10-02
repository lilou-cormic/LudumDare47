using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

namespace PurpleCable
{
    public class SaveStateManager : MonoBehaviour
    {
        private static XElement SaveState { get; set; } = null;

        private static string GetSaveSlotName(int slot)
        {
            return $"SaveState{slot}";
        }

        public static void NewSaveState()
        {
            SaveState = new XElement("SaveState");
        }

        public static void SaveSlot(int slot)
        {
            if (SaveState == null)
                NewSaveState();

            PlayerPrefs.SetString(GetSaveSlotName(slot), SaveState.ToString());
        }

        public static string LoadSlot(int slot)
        {
            string saveStateString = PlayerPrefs.GetString(GetSaveSlotName(slot));

            if (string.IsNullOrEmpty(saveStateString))
                NewSaveState();
            else
                SaveState = XElement.Parse(saveStateString);

            Inventory.FromXElement(SaveState.Element("Inventory"));

            return (string)SaveState.Attribute("CurrentScene");
        }

        public static void DeleteSlot(int slot)
        {
            PlayerPrefs.DeleteKey(GetSaveSlotName(slot));
        }

        public static void SaveScene(string sceneName, IEnumerable<XElement> elements, IEnumerable<XElement> stateElements)
        {
            SaveState.Attribute("CurrentScene")?.Remove();
            SaveState.Add(new XAttribute("CurrentScene", sceneName));

            SaveState.Element(sceneName)?.Remove();
            SaveState.Add(new XElement(sceneName, elements));

            var xStates = SaveState.Element("States");
            {
                if (xStates == null)
                {
                    xStates = new XElement("States");
                    SaveState.Add(xStates);
                }

                foreach (var stateElement in stateElements)
                {
                    xStates.Element(stateElement.Name)?.Remove();
                    xStates.Add(stateElement);
                }
            }

            SaveState.Element("Inventory")?.Remove();
            SaveState.Add(Inventory.ToXElement("Inventory"));
        }

        public static XElement LoadScene(string sceneName)
        {
            return SaveState.Element(sceneName);
        }

        public static bool GetStateValue(string stateName)
        {
            var xStates = SaveState.Element("States");

            if (xStates == null)
                return false;

            var xStateElement = xStates.Element(stateName);

            if (xStateElement == null)
                return false;

            return (bool)xStateElement;
        }
    }
}
