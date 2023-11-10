using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Level.UI
{
    public class EndLevel : MonoBehaviour
    {
        [SerializeField] private GameObject _menu;

        [SerializeField] private TMP_Text _titleText;

        [SerializeField] private string _mainMenu;

        public void Finish(EndType endType)
        {
            switch (endType)
            {
                case EndType.Win:
                    ShowMenu("You Win!");
                    break;

                case EndType.RanAway:
                    ShowMenu("You Ran Away!");
                    break;

                case EndType.Lost:
                    ShowMenu("You Lost!");
                    break;

            }
        }

        private void ShowMenu(string text)
        {
            _menu.SetActive(true);
            _titleText.text = text;

            Invoke(nameof(OpemMainMenu), 4);
        }

        private void OpemMainMenu() => SceneManager.LoadScene(_mainMenu);
    }
}