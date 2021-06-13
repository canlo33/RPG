using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Equipment", menuName = "Items/Equipment")]
public class Equipment : Item
{    public override void Use()
    {
        if (!GameMaster.instance.playerController.enabled)
            return;
        //Equip the item.
        EquipmentManager.instance.Equip(this);
        //Remove it from the inventory.
        RemoveFromInventory();
        //Add items stats to the player.
        base.Use();
        GameMaster.instance.playerController.PlaySoundEffect(GameMaster.instance.playerController.itemEquippedSound);
    }

}
