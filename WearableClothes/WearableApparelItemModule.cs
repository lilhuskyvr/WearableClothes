using System.Collections;
using ThunderRoad;
using UnityEngine;
using UnityEngine.SceneManagement;

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

            var itemData = Catalog.GetData<ItemData>(item.data.id.Substring(0, item.data.id.Length - 1));
            item.holder.UnSnap(item, true);
            item.Despawn();

            var content = new ContainerData.Content(itemData);

            if (creature.isPlayer)
            {
                var wardrobe = itemData.GetModule<ItemModuleWardrobe>().GetWardrobe(creature);
                for (int index = 0; index < wardrobe.manikinWardrobeData.channels.Length; ++index)
                {
                    ContainerData.Content wornContent = creature.equipment.GetWornContent(
                        wardrobe.manikinWardrobeData.channels[index], wardrobe.manikinWardrobeData.layers[index]);
                    if (wornContent != null)
                        GameManager.playerData.inventory.Remove(wornContent);
                }

                GameManager.playerData.inventory.Add(content);
            }

            creature.equipment.EquipWardrobe(
                content
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