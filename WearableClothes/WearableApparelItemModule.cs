using System.Collections;
using ThunderRoad;
using UnityEngine;

namespace WearableClothes
{
    public class WearableApparelItemModule : ItemModule
    {
        public override void OnItemLoaded(Item item)
        {
            base.OnItemLoaded(item);
            item.gameObject.AddComponent<WearableApparelItem>();
        }
    }


    public class WearableApparelItem : MonoBehaviour
    {
        private Item _item;

        private IEnumerator EquipWardrobe(Creature creature, Item item)
        {
            yield return new WaitForSeconds(1);

            var itemData = Catalog.GetData<ItemData>(item.data.id);
            item.holder.UnSnap(item);
            item.Despawn(1);
            creature.equipment.EquipWardrobe(
                new ContainerData.Content(itemData)
            );
            yield return null;
        }

        private void Start()
        {
            _item = GetComponent<Item>();
            _item.OnSnapEvent += holder => { StartCoroutine(EquipWardrobe(holder.creature, _item)); };
        }
    }
}