using System.Collections;
using ThunderRoad;
using UnityEngine;
// ReSharper disable UnusedType.Global

namespace WearableClothes
{
    public class WearableStripItemModule : ItemModule
    {
        // ReSharper disable once ParameterHidesMember
        public override void OnItemLoaded(Item item)
        {
            base.OnItemLoaded(item); 
            item.gameObject.AddComponent<WearableStripItem>();
        }
    }


    public class WearableStripItem : MonoBehaviour
    {
        private Item _item;

        private IEnumerator UnEquipWardrobes(Creature creature, Item item)
        {
            item.holder.UnSnap(item, true);
            item.Despawn();

            creature.equipment.UnequipAllWardrobes(true);

            yield return null;
        }

        private void Start()
        {
            _item = GetComponent<Item>();
            _item.OnSnapEvent += holder => { StartCoroutine(UnEquipWardrobes(holder.creature, _item)); };
        }
    }
}