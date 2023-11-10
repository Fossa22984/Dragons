using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Level
{
    public class LevelUI : MonoBehaviour
    {
        [SerializeField] private LevelController _levelController;

        [SerializeField] private List<DragonItem> _dragonItemViews;
        [SerializeField] private DragonItem _alliedDragonItemPrefab;
        [SerializeField] private Transform _parentForAlliedDragonItems;

        public DragonItem GetDragonItem(int index)
        {
            return _dragonItemViews[index];
        }

        public void InitListInUi(List<GameObject> alliedDragons)
        {
            foreach (var dragon in alliedDragons)
                CreateCollectionDragoItem(dragon.GetComponent<DragonInfo>());
        }

        private void CreateCollectionDragoItem(DragonInfo dragon)
        {
            var dragonItemView = Instantiate(_alliedDragonItemPrefab, _parentForAlliedDragonItems);
            dragonItemView.SetDragonData(dragon.name, dragon.HealthPoint, dragon.Icon, dragon.Color);

            dragonItemView.SetSelectDragonClickCallback(() => OnSelectDragonHandler(_levelController.AlliedDragons, dragon.Name));
            _dragonItemViews.Add(dragonItemView);
        }

        private void OnSelectDragonHandler(List<GameObject> alliedDragons, string name)
        {
            for (int i = 0; i < alliedDragons.Count; i++)
            {
                if (alliedDragons[i].GetComponent<DragonInfo>().Name.Equals(name))
                {
                    _levelController.ChangeDragon(i);
                    return;
                }
            }
        }
    }
}